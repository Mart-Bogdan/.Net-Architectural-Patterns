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

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        //
        // GET: /BlogPosts/

        public ActionResult GetAllBlogPosts()
        {
            IList<BlogPostWithAuthor> blogPostWithAuthors = _blogPostRepository.GetAllWithUserNick();
            return View(blogPostWithAuthors);
        }

        public ActionResult GetAllBlogs()
        {
            IList<BlogPost> blogPostWithAuthors = _blogPostRepository.GetAll();
            return View(blogPostWithAuthors);
        }
        //
        // GET: /BlogPosts/{id}
        public ActionResult GetblogPost(int id)
        {
            var blogPost = _blogPostRepository.GetById(id);
            PostRequestModel postRequestModelblogPost =
                new PostRequestModel();
            postRequestModelblogPost.Title = blogPost.Title;
            postRequestModelblogPost.Content =  blogPost.Content;

            return View(postRequestModelblogPost);
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
                mc.Created = DateTimeOffset.Now;
                mc.UserId = 1;
                _blogPostRepository.Insert(mc);
            }

            return View();
        }

        public ActionResult DeleteblogPost(int id)
        {
           bool result =  _blogPostRepository.Delete(id);

            return View(result);
        }
    }
}
