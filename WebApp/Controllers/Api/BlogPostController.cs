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
        public Result<List<BlogPost>> GetPostsOfCurrentUser(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null) 
                return Result<List<BlogPost>>.Unauthorized;

            using (var uow = UnitOfWorkFactory.CreateInstance())
            {
                return new Result<List<BlogPost>>(uow.BlogPostRepository.GetByUserId(user.Id).ToList());
            }
        }
        [HttpGet]
        public Result<IList<BlogPostWithAuthor>> GetAllWithUserNick(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPostWithAuthor>>.Unauthorized;

            using (var uow = UnitOfWorkFactory.CreateInstance())
            {
                return new Result<IList<BlogPostWithAuthor>>(uow.BlogPostRepository.GetAllWithUserNick());
            }
        }

        [HttpPost]
        public Result<int> Save([FromUri]string token, [FromBody] BlogPost post)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null) 
                return Result<int>.Unauthorized;

            using (var uow = UnitOfWorkFactory.CreateInstance())
            {
                if (post.Id != 0)
                {
                    var existingPost = uow.BlogPostRepository.GetById(post.Id);
                    if (existingPost == null)
                        return new Result<int> {ErrorMessage = "404 Incorrect ID", HasError = true};

                    if (existingPost.UserId != user.Id)
                        return Result<int>.Forbidden;
                }
                else
                {
                    post.Created = DateTimeOffset.Now;
                }

                post.UserId = user.Id;

                int postId = uow.BlogPostRepository.Upsert(post);
                uow.Commit();

                return new Result<int>(postId);
            }
        }
    }
}
