using System.Collections.Generic;
using RestWithAspNet.Data.Converter.Contract;
using RestWithAspNet.Data.Converter.Implementations;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Generic;

namespace RestWithAspNet.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id) => _repository.Exists(id);

    }
}