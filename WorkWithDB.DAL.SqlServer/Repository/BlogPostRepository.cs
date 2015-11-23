using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Infrastructure;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.SqlServer.Repository
{
    internal class BlogPostRepository : BaseRepository<int,BlogPost>, IBlogPostRepository 
    {

        public BlogPostRepository(SqlConnection connection, SqlTransaction transaction):base(connection,transaction)
        {}

        public override int Insert(Entity.BlogPost entity)
        {
            return (int)
                base.ExecuteScalar<decimal>(
                        "insert into BlogPost (Content,Created,UserId) values (@Content,@Created,@UserId) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Content", entity.Content},
                            {"Created", entity.Created},
                            {"UserId", entity.UserId  },
                        }
                    );

        }

        public override bool Update(Entity.BlogPost entity)
        {
            var res = base.ExecuteNonQuery(
                    "update BlogPost set Content = @Content ,UserId = @UserId  where Id = @Id ",
                    new SqlParameters
                    {
                        {"Content", entity.Content},
                        {"Id", entity.Id},
                        {"UserId", entity.UserId  },
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

        public Entity.BlogPost GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                    "select bp.Id,bp.UserId,bp.Content,bp.Created from BlogPost bp where bp.Id = @Id",
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
                new SqlParameters(){{"id",id}});

            if(res>1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public IList<BlogPost> GetAll()
        {
            return base.ExecuteSelect("Select bp.Id, bp.UserId, bp.Content, bp.Created from BlogPost bp ");
        }
        
        public IList<Entity.BlogPost> GetByUserId(int userId)
        {
            return base.ExecuteSelect(
                "Select bp.Id, bp.UserId, bp.Content, bp.Created from BlogPost bp ",
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
            };
        }
    }
}
