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

    public Book CreateBook(string title, string publisher, string coverImgUrl)
    {
        try
        {
            return _repository.CreateBook(title, publisher, coverImgUrl);
        }
        catch (Exception)
        {
            throw new Exception("Could not create books, u noob");
        }
    }
    
    
    public Book updateBook(int id, string title, string publisher, string coverImgUrl)
    {
        try
        {
            return _repository.UpdateBookById(id, title, publisher, coverImgUrl);
        }
        catch (Exception)
        {
            throw new Exception("Could not update books, u noob");
        }
    }
    
    public bool deleteBook(int id)
    {
        try
        {
            return _repository.DeleteBook(id);
        }
        catch (Exception)
        {
            throw new Exception("Could not delete books, u noob");
        }
    }
    
    

}