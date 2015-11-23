using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Abstract;
using WorkWithDB.Entity;

namespace WorkWithDB.Repository
{
    internal class BlogPostRepository :BaseRepository<int,BlogPost>, IBlogPostRepository 
    {

        public BlogPostRepository(SqlConnection connection):base(connection)
        {}

        public override int Insert(Entity.BlogPost entity)
        {
            return
                base.ExecuteScalar<int>(
                        "insert into BlogPost (Content,Created,UserId) values (@Content,@Created,@UserId)",
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

        protected override BlogPost DefaultRowMapper(SqlDataReader reader)
        {
            return new BlogPost
            {
                Id = (int) reader["Id"],
                UserId = (int) reader["UserId"], 
                Content = (string) reader["Content"], 
                Created = (DateTimeOffset) reader["Created"],
            };
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

        public IList<Entity.BlogPost> FetchAll()
        {
            return base.ExecuteSelect("Select bp.Id, bp.UserId, bp.Content, bp.Created from BlogPost bp ");
        }
    }
}
