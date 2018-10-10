using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standard.Entity.Entities;
using WorkWithDB.Standard.Entity.Views;
using WorkWithDB.Standartd.DAL.SqlServer.Infrastructure;

namespace WorkWithDB.Standartd.DAL.SqlServer.Repository
{
    public class BlogPostRepository : BaseRepository<int, BlogPost>, IBlogPostRepository
    {

        public BlogPostRepository(SqlConnection connection, SqlTransactionManager transactionManager)
            : base(connection, transactionManager)
        { }

        public override int Insert(BlogPost entity)
        {
            return (int)
                base.ExecuteScalar<decimal>(
                        "insert into BlogPost (Content,Created,UserId,Title) values (@Content,@Created,@UserId,@Title) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Content", entity.Content},
                            {"Created", entity.Created},
                            {"UserId", entity.UserId  },
                            {"Title", entity.Title  },
                        }
                    );

        }

        public override bool Update(BlogPost entity)
        {
            var res = base.ExecuteNonQuery(
                    "update BlogPost set Content = @Content ,UserId = @UserId, Title =@Title  where Id = @Id ",
                    new SqlParameters
                    {
                        {"Content", entity.Content},
                        {"Id", entity.Id},
                        {"UserId", entity.UserId  },
                        {"Title", entity.Title  },
                    }
                );

            return res > 0;
        }

        public int GetCount()
        {
            return base.ExecuteScalar<int>("select count(*) from BlogPost");
        }
        public int GetCountByUserId(int userId)
        {
            return base.ExecuteScalar<int>(
                "select count(*) from BlogPost where UserId = @userId",
                new SqlParameters()
                    {
                        {"userId", userId}
                    }
                );
        }

        public BlogPost GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                    "select bp.Id,bp.UserId,bp.Content,bp.Created, bp.Title from BlogPost bp where bp.Id = @Id",
                    new SqlParameters()
                    {
                        {"Id",id}
                    }
                );
        }


        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from BlogPost where Id = @id",
                new SqlParameters() { { "id", id } });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public IList<BlogPost> GetAll()
        {
            return base.ExecuteSelect("Select bp.Id, bp.UserId, bp.Content, bp.Created, bp.Title from BlogPost bp");
        }

        public IList<BlogPostWithAuthor> GetAllWithUserNick()
        {
            return base.ExecuteSelect("Select bp.Id, bp.UserId, bp.Title, bp.Created, u.Nick " +
                                      "     from BlogPost bp " +
                                      "     JOIN BlogUser u on bp.UserId = u.Id" +
                                      "     order by bp.Created desc",
                                      reader => new BlogPostWithAuthor
                                      {
                                          Id = (int)reader["Id"],
                                          UserId = (int)reader["UserId"],
                                          Title = (string)reader["Title"],
                                          Created = (DateTimeOffset)reader["Created"],
                                          AuthorNick = (string)reader["Nick"],
                                      }
                                    );

        }

        public IList<BlogPost> GetByUserId(int userId)
        {
            return base.ExecuteSelect(
                "Select bp.Id, bp.UserId, bp.Content, bp.Created,bp.Title from BlogPost bp where bp.UserId = @userId",
                new SqlParameters()
                    {
                        {"userId", userId}
                    }
                );
        }


        protected override BlogPost DefaultRowMapping(SqlDataReader reader)
        {
            return new BlogPost
            {
                Id = (int)reader["Id"],
                UserId = (int)reader["UserId"],
                Content = (string)reader["Content"],
                Created = (DateTimeOffset)reader["Created"],
                Title = (string)reader["Title"],
            };
        }
    }
}
