using LibraryAPI.Models;

namespace LibraryAPI.DTO;

public class GetAllBooksResponse
{
    public List<BookResponse> books { get; set; } = new List<BookResponse>();
    public int quantity { get; set; }
}
