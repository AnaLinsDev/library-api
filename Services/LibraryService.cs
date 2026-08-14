using LibraryAPI.DTO;
using LibraryAPI.Enums;
using LibraryAPI.Exceptions;
using LibraryAPI.Helpers;
using LibraryAPI.Models;
using System.Collections.Generic;

namespace LibraryAPI.Services;

/*
4 - Regra de Negocio: title e author não devem existir duplicados;
5 - Regra de Negocio: price não pode ser negativo;
6 - Regra de Negocio: stock não pode ser negativo;
7 - Regra de Negocio: genre deve estar numa lista de gêneros válidos.
8 - Regra de Negocio: Quando o livro é criado, preencher CreatedAt em alterações, atualizar UpdatedAt.

*/
public class LibraryService
{
    public void CreateBook(
        List<Book> libraryBooks,
        Book bookToInsert,
        BookRequest request)
    {
        ValidateBook(libraryBooks, null, request);

        MapBook(bookToInsert, request);

        bookToInsert.CreatedAt = DateTime.UtcNow;
        bookToInsert.UpdatedAt = null;

        libraryBooks.Add(bookToInsert);
    }

    public void UpdateBook(
        List<Book> libraryBooks,
        Book bookToUpdate,
        BookRequest request)
    {
        ValidateBook(libraryBooks, bookToUpdate.Id, request);

        MapBook(bookToUpdate, request);

        bookToUpdate.UpdatedAt = DateTime.UtcNow;
    }

    private void ValidateBook(
        List<Book> libraryBooks,
        Guid? bookId,
        BookRequest request)
    {
        if (request.Title.Count() < 2 || request.Title.Count() > 120)
            throw new BusinessException("Title must have between 2 and 120 char");

        if (request.Author.Count() < 2 || request.Author.Count() > 120)
            throw new BusinessException("Author must have between 2 and 120 char");

        if (request.Price < 0)
            throw new BusinessException("Price can't be negative.");

        if (request.Stock < 0)
            throw new BusinessException("Stock can't be negative.");

        if (string.IsNullOrWhiteSpace(request.Genre))
            throw new BusinessException("Genre is required.");

        if (BookTitleAndAuthorAlreadyExists(
            libraryBooks,
            bookId,
            request))
        {
            throw new ConflictException(
                "A book with this title and author already exists.");
        }

        // Também valida/converte o gênero
        GenreHelper.GetGenreFromName(request.Genre);
    }

    private static void MapBook(Book book, BookRequest request)
    {
        book.Title = request.Title;
        book.Author = request.Author;
        book.Price = request.Price;
        book.Stock = request.Stock;
        book.Genre = GenreHelper.GetGenreFromName(request.Genre);
    }

    private static bool BookTitleAndAuthorAlreadyExists(
        List<Book> libraryBooks,
        Guid? bookId,
        BookRequest request)
    {
        return libraryBooks.Any(book =>
            book.Id != bookId &&
            book.Title.Equals(
                request.Title,
                StringComparison.OrdinalIgnoreCase) &&
            book.Author.Equals(
                request.Author,
                StringComparison.OrdinalIgnoreCase));
    }
}
