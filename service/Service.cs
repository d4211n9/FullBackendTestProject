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
    
    public IEnumerable<NewsFeedItem> GetAllNews()
    {
        try
        {
            var articles =  _repository.GetAllNews();
            
            var newsFeedItems = articles.Select(article => new NewsFeedItem()
            {
                ArticleId = article.ArticleId,
                Headline = article.Headline,
                Body = article.Body.Length > 51 ? article.Body.Substring(0, 50) : article.Body,
                ArticleImgUrl = article.ArticleImgUrl
            });

            return newsFeedItems;
        }
        catch (Exception)
        {
            throw new Exception("Could not get news feed");
        }
    }

    public News CreateNewsArticle(News news)
    {
        return _repository.CreateNewsArticle(news);
    }
    
    public bool DeleteNewsArticle(int id)
    { 
        return _repository.DeleteNewsArticle(id);
    }

    public News GetFullArticle(int id)
    {
        return _repository.GetFullArticle(id);
    }

    public News UpdateNews(News news)
    {
        return _repository.UpdateNews(news);
    }
    
    public IEnumerable<News> SearchNews(SearchCrit searchCrit)
    {
        return _repository.SearchArticles(searchCrit);
    }
}