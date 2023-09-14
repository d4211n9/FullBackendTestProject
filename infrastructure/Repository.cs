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
    
    public IEnumerable<NewsFeedItem> GetAllNews()
    {
        var sql = $@"select articleid, headline, articleimgurl, body from news.articles;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Query<NewsFeedItem>(sql);
        }
    }
    
    public News GetFullArticle(int id)
    {
        var sql = $@"select * FROM news.articles
                    WHERE articleid = @ArticleId;";
        
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<News>(sql, id);
        }
    }

    public News CreateNewsArticle(News news)
    {
        var sql = $@" 
            insert into news.articles (headline, body, author, articleimgurl) VALUES (@headline, @body, @author, @articleimgurl)
             RETURNING articleid;";
        using (var conn = _dataSource.OpenConnection())
        {
            int article =conn.Execute(sql, news);
            news.ArticleId = article;
            
            return news;
        }
    }
    
    public bool DeleteNewsArticle(int id)
    {
        var sql = @"DELETE FROM news.articles
                    WHERE articleid = @ArticleId";
        using (var conn = _dataSource.OpenConnection())
        {
            var rowsAffected = conn.Execute(sql, new {id}) == 1;
            return rowsAffected;
        }
    }

    public News UpdateNews(News news)
    {
     var sql = @"UPDATE news.articles
                    SET headline = @Headline,
                        body = @Body,
                        author = @Author,
                        articleimgurl = @ArticleImgUrl
                    WHERE articleid = @ArticleId 
                    RETURNING *;";   
        
     using (var conn = _dataSource.OpenConnection())
     {
         return conn.QueryFirst<News>(sql, news);
     }
    }
    
    
    public IEnumerable<News> SearchArticles(SearchCrit searchCriteria)
    {
        using (var conn = _dataSource.OpenConnection())
        {
            var sql = @"SELECT * FROM news.articles
                    WHERE body LIKE '%' || @SearchTerm || '%'
                    LIMIT @PageSize";

            return conn.Query<News>(sql, searchCriteria);
        }
    }
    
    
}