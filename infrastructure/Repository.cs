using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        var sql = $@"select * from library.books;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Book>(sql);
        }
    }


    public Book CreateBook(string title, string publisher, string coverImgUrl)
    {
        var sql = $@"INSERT INTO library.books (title, publisher, coverimgurl) VALUES (@title, @publisher, @coverImgUrl)
 RETURNING *;";
        
        
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new {title, publisher, coverImgUrl});
        }
    }
    
    
    public Book UpdateBookById(int bookIdToUpdate, string newTitle, string newPublisher, string newCoverImgUrl)
    {
        var sql = @$"
UPDATE library.books SET title = @newTitle, publisher = @newPublisher, coverimgurl = @newCoverImgUrl WHERE bookid = @bookIdToUpdate
RETURNING *;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new { bookIdToUpdate, newTitle, newPublisher, newCoverImgUrl });
        }
    }
    
    public bool DeleteBook(int bookid)
    {
        var sql = $@"DELETE FROM library.books WHERE bookid = @bookId;";
        
        
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { bookid }) == 1;
        }
    }
    
    
    
}