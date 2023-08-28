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
    public IEnumerable<Book> GetBooks()
    {
        return _service.GetAllBooks();
    }


}
