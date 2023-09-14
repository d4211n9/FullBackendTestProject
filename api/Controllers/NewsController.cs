using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace FullBackendTestProject.Controllers;

[ApiController]
public class NewsController : ControllerBase
{
    private readonly Service _service;

    public NewsController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/feed")]
    public IEnumerable<NewsFeedItem> GetNews()
    {
        return _service.GetAllNews();
    }
    
    [HttpPost]
    [Route("/api/articles")]
    public News CreateArticle([FromBody] News newsArticle)
    {
        return _service.CreateNewsArticle(newsArticle);
    }
    
    [HttpDelete]
    [Route("/api/articles/{id}")]
    public bool DeleteArticle([FromHeader] int id)
    {
         return _service.DeleteNewsArticle(id);
    }


    [HttpGet]
    [Route("/api/articles/{id}")]
    public News GetFullArticle([FromHeader] int id)
    {
        
        return _service.GetFullArticle(id);
    }
    
    [HttpPut]
    [Route("/api/articles/{articleId}")]
    public News UpdateArticle([FromRoute] int articleId, [FromBody] News article)
    {
        article.ArticleId = articleId;
        return _service.UpdateNews(article);
    }

    [HttpGet]
    [Route("/api/articles/")]
    public IEnumerable<News> SearchNews([FromQuery] SearchCrit searchCrit)
    {
        return _service.SearchNews(searchCrit);
    }

}
