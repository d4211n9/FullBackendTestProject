using infrastructure;
using infrastructure.DataModels;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        try
        {
            return _repository.GetAllBooks();
        }
        catch (Exception)
        {
            throw new Exception("Could not get books");
        }
    }
    public IEnumerable<News> GetAllNews()
    {
        try
        {
            return _repository.GetAllNews();
        }
        catch (Exception)
        {
            throw new Exception("Could not get news feed");
        }
    }
    

}