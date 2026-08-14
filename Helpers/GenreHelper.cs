using LibraryAPI.Enums;

namespace LibraryAPI.Helpers;

public class GenreHelper
{
    public static string GetGenreName(Genre genre)
    {
        return genre switch
        {
            Genre.Fiction => "Ficção",
            Genre.Fantasy => "Fantasia",
            Genre.ScienceFiction => "Ficção Científica",
            Genre.Romance => "Romance",
            Genre.Mystery => "Mistério",
            Genre.Thriller => "Suspense",
            Genre.Horror => "Terror",
            Genre.Adventure => "Aventura",
            Genre.Biography => "Biografia",
            Genre.History => "História",
            Genre.Poetry => "Poesia",
            Genre.Drama => "Drama",
            Genre.Comedy => "Comédia",
            _ => "Desconhecido"
        };
    }

    public static Genre GetGenreFromName(string genre)
    {
        return genre switch
        {
            "Ficção" => Genre.Fiction,
            "Fantasia" => Genre.Fantasy,
            "Ficção Científica" => Genre.ScienceFiction,
            "Romance" => Genre.Romance,
            "Mistério" => Genre.Mystery,
            "Suspense" => Genre.Thriller,
            "Terror" => Genre.Horror,
            "Aventura" => Genre.Adventure,
            "Biografia" => Genre.Biography,
            "História" => Genre.History,
            "Poesia" => Genre.Poetry,
            "Drama" => Genre.Drama,
            "Comédia" => Genre.Comedy,
            _ => throw new Exception("Genre not found")
        };
    }
}
