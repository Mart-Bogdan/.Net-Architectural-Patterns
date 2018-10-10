using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Abstract.Security;
using WebApp.Standard.Api.Models.Responces;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standard.Entity.Views;
using BlogPost = WorkWithDB.Standard.Entity.Entities.BlogPost;

namespace WebAppCore.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IAccessTokenValidator _tokenValidator;
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(IAccessTokenValidator tokenValidator, IBlogPostRepository blogPostRepository)
        {
            _tokenValidator = tokenValidator;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public Result<IList<BlogPost>> GetPostsOfCurrentUser(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPost>>.Unauthorized;

            return new Result<IList<BlogPost>>(_blogPostRepository.GetByUserId(user.Id));
        }

        [HttpGet]
        public Result<IList<BlogPostWithAuthor>> GetAllWithUserNick(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPostWithAuthor>>.Unauthorized;

            return new Result<IList<BlogPostWithAuthor>>(_blogPostRepository.GetAllWithUserNick());
        }

        //TODO this should be disabled in production, to big overhead on DB and network
        [HttpGet]
        public Result<IList<BlogPost>> GetAll(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<IList<BlogPost>>.Unauthorized;

            return new Result<IList<BlogPost>>(_blogPostRepository.GetAll());
        }

        [HttpPost]
        public Result<int> Save(string token, [FromBody] BlogPost post)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<int>.Unauthorized;

            if (post.Id != 0)
            {
                var existingPost = _blogPostRepository.GetById(post.Id);
                if (existingPost == null)
                    return new Result<int> { ErrorMessage = "404 Incorrect ID", HasError = true };

                if (existingPost.UserId != user.Id)
                    return Result<int>.Forbidden;
            }
            else
            {
                post.Created = DateTimeOffset.Now;
            }

            post.UserId = user.Id;

            int postId = _blogPostRepository.Upsert(post);



            return postId;

        }


        [HttpGet]
        public Result<int> Count(string token)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<int>.Unauthorized;

            return _blogPostRepository.GetCount();

        }


        [HttpGet]
        public Result<int> CountByUserId(string token, int userId)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<int>.Unauthorized;

            return _blogPostRepository.GetCountByUserId(userId);

        }
        [HttpGet]
        public Result<BlogPost> Get(string token, int id)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<BlogPost>.Unauthorized;

            return _blogPostRepository.GetById(id);
        }
        [HttpGet]
        public Result<bool> Delete(string token, int id)
        {
            var user =
                _tokenValidator.ValidateToken(token);

            if (user == null)
                return Result<bool>.Unauthorized;

            return _blogPostRepository.Delete(id);
        }
    }
}
