using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.SqlServer.Infrastructure
{
    internal abstract class BaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        protected BaseRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        protected T ExecuteScalar<T>(string sql, IDictionary<string, object> parameters = null)
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                return (T)command.ExecuteScalar();
            }
        }

        protected int ExecuteNonQuery(string sql, IDictionary<string, object> parameters=null)
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                return command.ExecuteNonQuery();
            }
        }

        protected T ExecuteSingleRowSelect<T>(
            string sql,
            Func<SqlDataReader,T> rowMapping, 
            IDictionary<string, object> parameters = null
            )
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.NextResult())
                    {
                        return rowMapping(reader);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }


        protected IList<T> ExecuteSelect<T>(
            string sql,
            Func<SqlDataReader, T> rowMapping,
            IDictionary<string, object> parameters = null
            )
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    List<T> list = new List<T>(1);
                    while (reader.NextResult())
                    {
                        list.Add(rowMapping(reader));
                    }

                    return list;
                }
            }
        }


        protected TEntity ExecuteSingleRowSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSingleRowSelect(sql, DefaultRowMapping, sqlParameters);
        }

        protected IList<TEntity> ExecuteSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSelect(sql, DefaultRowMapping, sqlParameters);
        }

        private static void FillParameters(IDictionary<string, object> parameters, SqlCommand command)
        {
            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
        }


        public TKey Upsert(TEntity entity)
        {
            if (Object.Equals(entity.Id, default(TKey)))
                return Insert(entity);
            else
            {
                if (Update(entity))
                    return entity.Id;
                else
                    return default(TKey);
            }
        }

        public abstract TKey Insert(TEntity entity);
        public abstract bool Update(TEntity entity);
        protected abstract TEntity DefaultRowMapping(SqlDataReader reader);

    }
}