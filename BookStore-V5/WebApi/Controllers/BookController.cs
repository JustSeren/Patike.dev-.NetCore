using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookViewByIdModel result;

            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = id;
            GetBookByIdValidator validator = new GetBookByIdValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Hande();


            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookByIdCommand command = new DeleteBookByIdCommand(_context);

            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }
    }
}