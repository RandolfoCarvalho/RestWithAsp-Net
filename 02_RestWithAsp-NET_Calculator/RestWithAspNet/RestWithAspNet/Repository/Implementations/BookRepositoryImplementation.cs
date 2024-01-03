using RestWithAspNet.Data;
using RestWithAspNet.Model;

namespace RestWithAspNet.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private MysqlContext _context;

        public BookRepositoryImplementation(MysqlContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            //por questões de legibilidade ficará "persons"
            return _context.Books.ToList();
        }
        public Book FindById(long id)
        {

            //logica para puxar do banco de dados
            return _context.Books.SingleOrDefault(p => p.Id == id);

        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
            return book;
        }
        public Book Update(Book book)
        {
            bool exists = _context.Persons.Any(p => p.Id == book.Id);
            if (!exists) return null;
            //quando o id do banco de dados for igual o id do obj vamos buscar na base e armazenar em var
            var result = _context.Books.SingleOrDefault(p => p.Id == book.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new Exception("Error" + e.Message);
                }
            }
            return book;
        }
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    //remove por Id mas poderiamos simplesmente remover passando person
                    _context.Persons.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new Exception("Error" + e.Message);
                }
            }
        }
        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

    }
}
