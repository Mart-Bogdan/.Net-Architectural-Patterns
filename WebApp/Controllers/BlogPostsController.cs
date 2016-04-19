using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity.Views;

namespace WebApp.Controllers
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

        public ActionResult Index()
        {
            IList<BlogPostWithAuthor> blogPostWithAuthors = _blogPostRepository.GetAllWithUserNick();
            return View(blogPostWithAuthors);
        }


        //
        // GET: /BlogPosts/{id}
        public string Get(int id)
        {
            var blogPost = _blogPostRepository.GetById(id);
            return blogPost.Title+blogPost.Content;
            //return View("BlogView");
        }
    }
}
