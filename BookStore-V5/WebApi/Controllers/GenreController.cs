using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.CreateGenre;
using WebApi.Applications.GenreOperations.DeleteGenre;
using WebApi.Applications.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WepApi.Applications.GenreOperations.Queries.GetGenreDetails;
using WepApi.Applications.GenreOperations.Queries.GetGenres;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);


        }

        [HttpGet("id")]
        public ActionResult GetGenreDetails(int id)
        {
            GetGenreDeteilsQuery query = new GetGenreDeteilsQuery(_context, _mapper);
            query.GenreId = id;

            GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id) //geriye obje donmediği sadece ok dondüğü için Iactionresult
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
