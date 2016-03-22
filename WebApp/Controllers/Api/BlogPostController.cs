using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApp.Abstract;
using WebApp.Abstract.Security;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WebApp.Controllers.Api
{
    public class BlogPostController : ApiController
    {
        private IAccessTokenValidator _tokenValidator = BlFactory.AccessTokenValidator;

        [HttpGet]
        public Result<List<BlogPost>> GetPostsOfCurrentUser(string userToken)
        {
            var result =
                _tokenValidator.ValidateToken(userToken);

            if (result != null)
            {
                using (var uow = UnitOfWorkFactory.CreateInstance())
                {
                    return new Result<List<BlogPost>>(uow.BlogPostRepository.GetByUserId(result.Id).ToList());
                }
            }
            return new Result<List<BlogPost>>();
        }
        [HttpGet]
        public Result<List<BlogPostWithAuthor>> GetAllWithUserNick(string userToken)
        {
            var result =
                _tokenValidator.ValidateToken(userToken);

            if (result != null)
            {
                using (var uow = UnitOfWorkFactory.CreateInstance())
                {
                    return new Result<List<BlogPostWithAuthor>>(uow.BlogPostRepository.GetAllWithUserNick().ToList());
                }
            }
            return new Result<List<BlogPostWithAuthor>>();
        }


    }
}
