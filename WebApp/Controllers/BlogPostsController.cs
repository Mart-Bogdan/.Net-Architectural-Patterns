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
        IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateInstance();
        //
        // GET: /BlogPosts/

        public ActionResult Index()
        {
            IList<BlogPostWithAuthor> blogPostWithAuthors = unitOfWork.BlogPostRepository.GetAllWithUserNick();
            return View(blogPostWithAuthors);
        }


        //
        // GET: /BlogPosts/{id}
        public string Get(int id)
        {
            var blogPost = unitOfWork.BlogPostRepository.GetById(id);
            return blogPost.Title+blogPost.Content;
            //return View("BlogView");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(disposing)
                unitOfWork.Dispose();
        }
    }
}
