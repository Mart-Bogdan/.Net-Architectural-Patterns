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
    public class BlogUserRepository : IBlogUserRepository
    {
        SqlConnection _connection;

        public BlogUserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Entity.BlogPost> GetUsersPost(int userId)
        {
            List<Entity.BlogPost> result = new List<BlogPost>();
            string queryString = "Select bp.Id, bp.UserId, bp.Content, bp.Created from BlogPost bp where bp.UserId = @userId";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@userId", userId);
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

        public void Insert(Entity.BlogUser entity)
        {
            string queryString = "insert into BlogUser (UserPassword,Name,Nick) values (@UserPassword,@Name,@Nick)";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Nick", entity.Nick);
            command.BeginExecuteNonQuery();
        }

        public void Update(Entity.BlogUser entity)
        {
            //
            string queryString = "UPDATE BlogUser set UserPassword = @UserPassword,   Name = @Name,  Nick = @Nick where Id = @Id ";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Nick", entity.Nick);
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.BeginExecuteNonQuery();
        }

        public int GetCount()
        {
            string queryString = "select count(*) from BlogUser";
            SqlCommand command = new SqlCommand(queryString, _connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count;
        }

        public Entity.BlogUser GetById(int id)
        {
            string queryString = "select bu.Id,bu.Name,bu.Nick,bu.UserPassword from  BlogUser bu where bu.Id = @userId";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.AddWithValue("@userId", id);
            SqlDataReader reader = command.ExecuteReader();
            BlogUser user = null;
            while (reader.Read())
            {
                user = new BlogUser();
                user.Id = (int)reader["Id"];
                user.Name = (string)reader["Name"];
                user.Nick = (string)reader["Nick"];
                break;
            }
            return user;  
        }

        public void Delete(int id)
        {
            string queryString = "delete from BlogUser where Id = @id";
            SqlCommand command = new SqlCommand(queryString, _connection);
            command.Parameters.Add("@id",id);
            command.ExecuteNonQuery();
        }
    }
}
