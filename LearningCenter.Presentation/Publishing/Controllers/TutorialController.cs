using _1_API.Request;
using _1_API.Response;
using _2_Domain;
using _3_Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorialController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITutorialData _tutorialData;
    private readonly ITutorialDomain _tutorialDomain;

    public TutorialController(ITutorialData tutorialData, ITutorialDomain tutorialDomain, IMapper mapper)
    {
        _tutorialData = tutorialData;
        _tutorialDomain = tutorialDomain;
        _mapper = mapper;
    }


    // GET: api/Tutorial
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var data = await _tutorialData.GetAllAsync();
        var result = _mapper.Map<List<Tutorial>, List<TutorialResponse>>(data);

        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/Search
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> GetSearchAsync(string? name, int? year)
    {
        var data = await _tutorialData.GetSearchAsync(name, year);
        var result = _mapper.Map<List<Tutorial>, List<TutorialResponse>>(data);

        if (result.Count == 0) return StatusCode(StatusCodes.Status404NotFound);

        return Ok(result);
    }

    // GET: api/Tutorial/5
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(int id)
    {
        var data = _tutorialData.GetById(id);
        var result = _mapper.Map<Tutorial, TutorialResponse>(data);

        if (result != null)
            return Ok(result);

        return StatusCode(StatusCodes.Status404NotFound);
    }

    // POST: api/Tutorial
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TutorialRequest input)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            var tutorial = _mapper.Map<TutorialRequest, Tutorial>(input);
            var result = await _tutorialDomain.SaveAsync(tutorial);

            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);

            return BadRequest();
        }
        catch (Exception e)
        {
            //throw e;
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    // PUT: api/Tutorial/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TutorialRequest input)
    {
        if (ModelState.IsValid)
        {
            var tutorial = _mapper.Map<TutorialRequest, Tutorial>(input);

            var result = _tutorialDomain.Update(tutorial, id);

            if (result)
                return Ok();
        }

        return StatusCode(StatusCodes.Status400BadRequest);
    }

    // DELETE: api/Tutorial/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _tutorialDomain.Delete(id);

        return Ok();
    }
}