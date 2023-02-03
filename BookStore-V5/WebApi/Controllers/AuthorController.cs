using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthors;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);

        }

        [HttpGet("id")]
        public ActionResult GetAuthorDetails(int id)
        {
            GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_context, _mapper);
            query.AuthorID = id;

            GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updateAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }
    }
}