using LibraryAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models;

public class Book
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Title { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Author { get; set; }
    [Required]
    public Genre Genre { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime? UpdatedAt { get; set; } = null;
}
