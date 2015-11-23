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
    public class BlogPostRepository : IBlogPostRepository 
    {
        SqlConnection _connection;

        public BlogPostRepository(SqlConnection connection)
        {
            _connection = connection;
        }
        public void Insert(Entity.BlogPost entity)
        {
            string queryString = "insert into BlogPost (Content,Created,UserId) values (@Content,\'@Created\',@UserId)";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@Content", entity.Content);
            command.Parameters.AddWithValue("@Created", entity.Created.ToString("{yyyy-mm-dd}"));
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.BeginExecuteNonQuery();
        }

        public void Update(Entity.BlogPost entity)
        {
            string queryString = "update BlogPost set Content = @Content ,UserId = @UserId  where Id = @Id ";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@Content", entity.Content);
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.BeginExecuteNonQuery();
        }

        public int GetCount()
        {
            string queryString = "select count(*) from BlogPost";
            SqlCommand command = new SqlCommand(queryString, _connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count;
        }

        public Entity.BlogPost GetById(int id)
        {
            string queryString = "select bp.Id,bp.UserId,bp.Content,bp.Created from BlogPost bp where bp.Id = @Id";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = command.ExecuteReader();
            BlogPost bp = null;
            while (reader.Read())
            {
                bp = new BlogPost();
                bp.Id = (int)reader["Id"];
                bp.UserId = (int)reader["UserId"];

                bp.Content = (string)reader["Content"];
                bp.Created = (DateTimeOffset)reader["Created"];
                break;
            }
            return bp;
        }

        public void Delete(int id)
        {
            string queryString = "delete from BlogPost where Id = @id";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.Add("@id", id);
            command.ExecuteNonQuery();
        }

        public List<Entity.BlogPost> Fetch()
        {

            List<Entity.BlogPost> result = new List<BlogPost>();
            string queryString = "Select bp.Id, bp.UserId, bp.Content, bp.Created from BlogPost bp ";
            SqlCommand command = new SqlCommand(queryString, _connection);
            SqlDataReader reader = command.ExecuteReader();
            BlogPost post = null;
            while (reader.Read())
            {
                post = new BlogPost();
                post.Id = (int)reader["Id"];
                post.UserId = (int)reader["UserId"];
                post.Content = (string)reader["Content"];
                post.Created = (DateTimeOffset)reader["Created"];
                result.Add(post);
            }

            return result;
        }
    }
}
