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
        private IUnitOfWork uow = UnitOfWorkFactory.CreateInstance();

        [HttpGet]
        public Result<IList<BlogPost>> GetPostsOfCurrentUser(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPost>>.Unauthorized;
            
            return new Result<IList<BlogPost>>(uow.BlogPostRepository.GetByUserId(user.Id));
        }

        [HttpGet]
        public Result<IList<BlogPostWithAuthor>> GetAllWithUserNick(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPostWithAuthor>>.Unauthorized;

            return new Result<IList<BlogPostWithAuthor>>(uow.BlogPostRepository.GetAllWithUserNick());
        }

        //TODO this should be disabled in production, to big overhead on DB and network
        [HttpGet]
        public Result<IList<BlogPost>> GetAll(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPost>>.Unauthorized;

            return new Result<IList<BlogPost>>(uow.BlogPostRepository.GetAll());
        }

        [HttpPost]
        public Result<int> Save([FromUri]string token, [FromBody] BlogPost post)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null) 
                return Result<int>.Unauthorized;

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

            

            return postId;
            
        }


        [HttpGet]
        public Result<int> Count([FromUri] string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<int>.Unauthorized;

            return uow.BlogPostRepository.GetCount();

        }


        [HttpGet]
        public Result<int> CountByUserId([FromUri] string token, [FromUri] int userId)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<int>.Unauthorized;

            return uow.BlogPostRepository.GetCountByUserId(userId);

        }
        [HttpGet]
        public Result<BlogPost> Get([FromUri] string token, [FromUri] int id)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<BlogPost>.Unauthorized;

            return uow.BlogPostRepository.GetById(id);
        }
        [HttpGet]
        public Result<bool> Delete([FromUri] string token, [FromUri] int id)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<bool>.Unauthorized;

            return uow.BlogPostRepository.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                uow.Dispose();

            base.Dispose(disposing);
        }
    }
}
