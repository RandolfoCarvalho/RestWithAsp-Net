using RestWithAspNet.Model;

namespace RestWithAspNet.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book Update(Book person);
        Book FindById(long id);
        List<Book> FindAll();
        void Delete(long id);
        bool Exists(long id);

    }

}
