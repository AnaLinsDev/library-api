using LibraryAPI.DTO;
using LibraryAPI.Enums;
using LibraryAPI.Helpers;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

/*
 TODO: Adicionar status code 200, 201, 204, 400, 404, 409 e 500 para todos endpoints
 */

public class LibraryController : LibraryBaseController
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    private static List<Book> libraryBooks = new List<Book>()
        {
            new Book
            {
                Title = "Orgulho e Preconceito",
                Genre = Genre.Romance,
                Author = "Jane Austen",
                Price = 39.90M,
                Stock = 10
            },
            new Book
            {
                Title = "O Senhor dos Anéis",
                Genre = Genre.Fantasy,
                Author = "J. R. R. Tolkien",
                Price = 59.90M,
                Stock = 7
            },
            new Book
            {
                Title = "O Iluminado",
                Genre = Genre.Horror,
                Author = "Stephen King",
                Price = 49.90M,
                Stock = 5
            }
        };


    [HttpGet]
    [EndpointSummary("Get all book")]
    [ProducesResponseType(typeof(GetAllBooksResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetAll()
    {
        var response = new GetAllBooksResponse
        {
            quantity = libraryBooks.Count,
            books = libraryBooks.Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Genre = GenreHelper.GetGenreName(book.Genre),
                Author = book.Author,
                Price = book.Price,
                Stock = book.Stock
            }).ToList()
        };

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [EndpointSummary("Get book")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute] Guid id)
    {
        var book = libraryBooks.Find(book => book.Id == id);

        if (book == null)
        {
            return NotFound("Book not found.");
        }

        var response = new BookResponse
        {
            Id = id,
            Title = book.Title,
            Genre = GenreHelper.GetGenreName(book.Genre),
            Author = book.Author,
            Price = book.Price,
            Stock = book.Stock
        };

        return Ok(response);
    }

    [HttpPost]
    [EndpointSummary("Create new book")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateBookRequest request)
    {
        try
        {
            Book bookToInsert = new Book
            {
                Title = request.Title,
                Genre = GenreHelper.GetGenreFromName(request.Genre),
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock
            };


            _libraryService.CreateBook(
                libraryBooks,
                bookToInsert,
                request
            );

            var response = new BookResponse
            {
                Id = bookToInsert.Id,
                Title = bookToInsert.Title,
                Genre = request.Genre,
                Author = bookToInsert.Author,
                Price = bookToInsert.Price,
                Stock = bookToInsert.Stock
            };
            return Created(string.Empty, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    [EndpointSummary("Update book")]
    [Route("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateBookRequest request)
    {
        var bookToUpdate = libraryBooks.Find(book => book.Id == id);


        if (bookToUpdate == null)
        {
            return NotFound("Book not found.");
        }

        try
        {
            _libraryService.UpdateBook(
                libraryBooks,
                bookToUpdate,
                request
            );

            return Ok($"Updated book with id {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [EndpointSummary("Delete book")]
    [Route("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var bookToRemove = libraryBooks.Find(book => book.Id == id);

        if (bookToRemove == null)
        {
            return NotFound("Book not found.");
        }

        libraryBooks.Remove(bookToRemove);

        return Ok($"Deleted book with id {id}");
    }

}
