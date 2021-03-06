﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Infrastructure;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.SqlServer.Repository
{
    internal class BlogUserRepository : BaseRepository<int, BlogUser>, IBlogUserRepository
    {
        public BlogUserRepository(SqlConnection connection, SqlTransactionManager transactionManager) : base(connection, transactionManager) { }

        public override int Insert(Entity.BlogUser entity)
        {
            return (int)
                base.ExecuteScalar<decimal>(
                    "insert into BlogUser (UserPassword,Name,Nick) values (@UserPassword,@Name,@Nick) SELECT SCOPE_IDENTITY()",
                    new SqlParameters
                    {
                        {"UserPassword", entity.UserPassword},
                        {"Name", entity.Name                },
                        {"Nick", entity.Nick                },
                    }
                    );
        }

        public override bool Update(Entity.BlogUser entity)
        {
            var res = base.ExecuteNonQuery(
                    "UPDATE BlogUser set UserPassword = @UserPassword,   Name = @Name,  Nick = @Nick where Id = @Id ",
                    new SqlParameters
                    {
                        {"UserPassword", entity.UserPassword},
                        {"Name", entity.Name                },
                        {"Nick", entity.Nick                },
                        {"Id", entity.Id                    },
                    }
                );

            return res > 0;
        }



        public int GetCount()
        {
            return base.ExecuteScalar<int>("select count(*) from BlogUser");
        }

        public Entity.BlogUser GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                    "select bu.Id,bu.Name,bu.Nick,bu.UserPassword from  BlogUser bu where bu.Id = @userId",
                    new SqlParameters()
                    {
                        {"userId",id}
                    }
                );
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from BlogUser where Id = @id",
                new SqlParameters() { { "id", id } });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public IList<BlogUser> GetAll()
        {
            return base.ExecuteSelect("select bu.Id,bu.Name,bu.Nick,bu.UserPassword from  BlogUser bu ");
        }

        public BlogUser GetByLoginPassword(string login, string password)
        {
            return base.ExecuteSingleRowSelect(
                    "select bu.Id,bu.Name,bu.Nick,bu.UserPassword from  BlogUser bu where bu.Nick = @login AND bu.UserPassword = @password",
                    new SqlParameters()
                    {
                        {"login",login},
                        {"password",password},
                    }
                );
        }


        protected override BlogUser DefaultRowMapping(SqlDataReader reader)
        {
            var user = new BlogUser
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Nick = (string)reader["Nick"],
                //Can't show passwords
                //UserPassword = (string)reader["UserPassword"]
            };

            return user;
        }
    }
}
