using WebApplication1.Entities;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Professions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;



/// <summary>
/// API controller for managing professions
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProfessionController : ControllerBase
{
    private readonly IProfessionService _professionService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the ProfessionController
    /// </summary>
    /// <param name="professionService">The profession service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProfessionController(
        IProfessionService professionService,
        IMapper mapper)
    {
        _professionService = professionService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all professions
    /// </summary>
    /// <returns>A collection of all professions</returns>
    /// <response code="200">Returns the list of professions</response>
    [HttpGet] [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var professions = _professionService.GetAllProfessions();
        return Ok(professions);
    }

    /// <summary>
    /// Gets a specific profession by ID
    /// </summary>
    /// <param name="id">The profession ID</param>
    /// <returns>The requested profession</returns>
    /// <response code="200">Returns the requested profession</response>
    /// <response code="404">If the profession is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        try
        {
            var profession = _professionService.GetProfessionById(id);
            return Ok(profession);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Profession not found");
        }
    }
    
    /// <summary>
    ///  Gets a specific profession by code
    /// </summary>
    /// <param name="code">The profession code</param>
    /// <returns>The requested profession</returns>
    /// <response code="200">Returns the requested profession</response>
    /// <response code="404">If the profession is not found</response>
    [HttpGet("code/{code}")]
    public IActionResult GetByCode(string code)
    {
        try
        {
            var profession = _professionService.GetProfessionByCode(code);
            return Ok(profession);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Profession not found");
        }
    }

    /// <summary>
    /// Creates a new profession
    /// </summary>
    /// <param name="profession">The profession to create</param>
    /// <returns>The created profession</returns>
    /// <response code="201">Returns the newly created profession</response>
    /// <response code="400">If the profession is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(CreateProfessionRequest profession)
    {
        if (profession == null)
            return BadRequest("Profession cannot be null");

        var createdProfession = _professionService.CreateProfession(profession);
        return CreatedAtAction(nameof(GetById), new { id = createdProfession.Id }, createdProfession);
    }

    /// <summary>
    /// Updates an existing profession
    /// </summary>
    /// <param name="id">The ID of the profession to update</param>
    /// <param name="profession">The updated profession information</param>
    /// <returns>The updated profession</returns>
    /// <response code="200">Returns the updated profession</response>
    /// <response code="404">If the profession is not found</response>
    /// <response code="400">If the profession is null</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, UpdateProfessionRequest profession)
    {
        if (profession == null)
            return BadRequest("Profession cannot be null");

        try
        {
            var updatedProfession = _professionService.UpdateProfession(id, profession);
            return Ok(updatedProfession);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Profession not found");
        }
    }

    /// <summary>
    /// Deletes a profession
    /// </summary>
    /// <param name="id">The ID of the profession to delete</param>
    /// <returns>A confirmation message</returns>
    /// <response code="200">If the profession was successfully deleted</response>
    /// <response code="404">If the profession is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        try
        {
            _professionService.DeleteProfession(id);
            return Ok(new { message = "Profession deleted" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Profession not found");
        }
    }
}