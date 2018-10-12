using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.Models;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standard.Entity.Entities;
using WorkWithDB.Standard.Entity.Views;

namespace WebAppCore.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogUserRepository _userRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, IBlogUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
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

        public IActionResult AddBlogPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlogPost(BlogPost mc)
        {
            if (ModelState.IsValid)
            {
                var blogUsers = _userRepository.GetAll();

                var randomUser = blogUsers[new Random().Next(blogUsers.Count - 1)];

                mc.Created = DateTimeOffset.Now;
                mc.UserId = randomUser.Id;
                _blogPostRepository.Insert(mc);
                
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteBlogPost(int id)
        {
           bool result =  _blogPostRepository.Delete(id);

            return View(result);
        }

        [HttpPost]
        public IActionResult EditBlogPost(BlogPost mc)
        {
            if (ModelState.IsValid)
            {
                var oldPost = _blogPostRepository.GetById(mc.Id);

                oldPost.Title = mc.Title;
                oldPost.Content = mc.Content;
                oldPost.UserId = mc.UserId;
                
                _blogPostRepository.Update(oldPost );
            }

            var users = _userRepository.GetAll();
            ViewBag.Users = users;
            return View(mc);
        }

        public ActionResult EditBlogPost(int id)
        {
            BlogPost blogPost =  _blogPostRepository.GetById(id);
            var users = _userRepository.GetAll();
            ViewBag.Users = users;
            return View(blogPost);
        }
        
        [HttpPost]
        public IActionResult EditBlogPostRaw(BlogPost mc)
        {
            if (ModelState.IsValid)
            {
                _blogPostRepository.Update(mc);
            }

            return View();
        }

        public ActionResult EditBlogPostRaw(int id)
        {
            BlogPost blogPost =  _blogPostRepository.GetById(id);
            return View(blogPost);
        }

    }
}
