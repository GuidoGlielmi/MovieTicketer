using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class ShowController : ControllerBase
{
  private readonly ILogger<ShowController> _logger;
  private readonly IUpdatableMovieTicketerService<Show> _showService;

  public ShowController(ILogger<ShowController> logger, IUpdatableMovieTicketerService<Show> showService)
  {
    _logger = logger;
    _showService = showService;
  }

  [HttpGet]
  public IEnumerable<Show> Get()
  {
    return _showService.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Show?> GetOne([FromRoute] Guid id)
  {
    return _showService.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Show show)
  {
    _showService.Create(show);
    return CreatedAtAction(nameof(GetOne), new { id = show.Id.ToString() }, show);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Show show)
  {
    var foundShow = _showService.Get(show.Id);
    if (foundShow is null)
      return NotFound();
    _showService.Update(show);
    return Ok();
  }

  [HttpDelete]
  public ActionResult<Guid> Delete([FromBody] Guid id)
  {
    var foundShow = _showService.Get(id);

    if (foundShow is null)
      return NotFound();

    _showService.Delete(foundShow);
    return Ok();
  }
}
