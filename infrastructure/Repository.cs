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
    
    public IEnumerable<News> GetAllNews()
    {
        var sql = $@"select * from news.articles;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Query<News>(sql);
        }
    }


    

}