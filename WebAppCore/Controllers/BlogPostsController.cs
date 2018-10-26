using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.Entity.Entities;
using WebApp.Core.Entity.Views;
using WebAppCore.Models.BlogPostModels;

namespace WebAppCore.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogUserRepository _userRepository;
        private readonly UserManager<BlogUser> _userManager;

        public BlogPostsController(IBlogPostRepository blogPostRepository, IBlogUserRepository userRepository, UserManager<BlogUser> userManager)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        //
        // GET: /BlogPosts/

        public ActionResult Index()
        {
            IList<BlogPostWithAuthor> blogPostWithAuthors = _blogPostRepository.GetAllWithUserNick();
            return View(blogPostWithAuthors);
        }

        public ActionResult ShowCuts()
        {
            IList<BlogPost> blogPostWithAuthors = _blogPostRepository.GetAll();
            return View(blogPostWithAuthors);
        }
        //
        // GET: /BlogPosts/{id}

        public ActionResult GetBlogPost(int id)
        {
            var blogPost = _blogPostRepository.GetByIdWithAuthor(id);
     

            return View(blogPost);
        }

        [Authorize(Roles = "Administrator,Blogger")]
        public IActionResult AddBlogPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Blogger")]
        public IActionResult AddBlogPost(BlogPostCreateModel mc)
        {
            if (ModelState.IsValid)
            {
    
                //This method should not fail with null reference, as we are authorized here!
                // ReSharper disable once RedundantAssignment
                var userId = ((ClaimsIdentity) User.Identity)
                    .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

                userId = _userManager.GetUserId(User);
                
                var post = new BlogPost
                {
                    Created = DateTimeOffset.Now, 
                    UserId = userId,
                    Content = mc.Content,
                    Title = mc.Title,
                };



                _blogPostRepository.Insert(post);
                
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Blogger")]
        public IActionResult DeleteBlogPost(int id)
        {
            var isAdmin = User.Claims
                .Where(c=>c.Type==_userManager.Options.ClaimsIdentity.RoleClaimType).Any(c=>c.Value=="Administrator");
            if (isAdmin)
            {
                bool result = _blogPostRepository.Delete(id);
                return View(result);
            }
            else
            {
                var post = _blogPostRepository.GetById(id);
                if (post == null)
                {
                    return NotFound();
                }

                if (post.UserId != _userManager.GetUserId(User))
                {
                    return Forbid();
                }
                
                bool result = _blogPostRepository.Delete(id);
                return View(result);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Blogger")]
        public IActionResult EditBlogPost(BlogPost mc)
        {
            if (ModelState.IsValid)
            {
                var oldPost = _blogPostRepository.GetById(mc.Id);

                        
                var isAdmin = User.Claims
                    .Where(c=>c.Type==_userManager.Options.ClaimsIdentity.RoleClaimType).Any(c=>c.Value=="Administrator");
                if (isAdmin)
                {
                    oldPost.UserId = mc.UserId;
                }
                else if (oldPost.UserId != _userManager.GetUserId(User))
                {
                    return Forbid();
                }
                
                oldPost.Title = mc.Title;
                oldPost.Content = mc.Content;
                
                _blogPostRepository.Update(oldPost );
            }

            var users = _userRepository.GetAll();
            ViewBag.Users = users;
            return View(mc);
        }

        [Authorize(Roles = "Administrator,Blogger")]
        public ActionResult EditBlogPost(int id)
        {
            
            BlogPost blogPost =  _blogPostRepository.GetById(id);
            
            var isAdmin = User.Claims
                .Where(c=>c.Type==_userManager.Options.ClaimsIdentity.RoleClaimType).Any(c=>c.Value=="Administrator");
            if (isAdmin || blogPost.UserId == _userManager.GetUserId(User))
            {
                var users = _userRepository.GetAll();
                ViewBag.Users = users;
                return View(blogPost);
            }
            else
            {
                return Forbid();
            }

        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditBlogPostRaw(BlogPost mc)
        {
            if (ModelState.IsValid)
            {
                _blogPostRepository.Update(mc);
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditBlogPostRaw(int id)
        {
            BlogPost blogPost =  _blogPostRepository.GetById(id);
            return View(blogPost);
        }

    }
}
