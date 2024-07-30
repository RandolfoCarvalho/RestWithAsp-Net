using RestWithAspNet.Model.Base;
using RestWithAspNet.Data;
using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RestWithAspNet.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MysqlContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MysqlContext Context)
        {
            _context = Context;
            //Setando o dbset diretamente pois é um tipo generico, que depende do tipo que esta sendo passado
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
          try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;

            } catch (Exception e)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            try
            {
                var result = dataset.FirstOrDefault(p => p.Id == id);
                if(result != null)
                {
                    try
                    {
                        _context.Remove(result);
                        _context.SaveChanges();

                    } catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                } catch(Exception e)
                {
                    throw;
                }
            } else
            {
                return null;
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        /*Executa uma consulta SQL bruta usando o Entity Framework Core e mapeia os resultados para uma lista de objetos do tipo */
        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    /*xecuta a consulta SQL e retorna a primeira coluna da primeira linha no conjunto de resultados retornado pela consulta*/
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}
