using InMindLab1.Models;
using System.Linq;
namespace InMindLab1.Services;

public class LibraryService : ILibraryService
{
    List<Author> authors = new List<Author>
    {
        new Author(1, "J.K. Rowling", new DateTime(1965, 7, 31), "United Kingdom"),
        new Author(2, "George Orwell", new DateTime(1903, 6, 25), "United Kingdom"),
        new Author(3, "Jane Austen", new DateTime(1775, 12, 16), "United Kingdom"),
        new Author(4, "Mark Twain", new DateTime(1775, 11, 30), "United States"),
        new Author(5, "Haruki Murakami", new DateTime(1949, 1, 12), "Japan")
    };

    List<Books> books = new List<Books>
    {
        new Books(1, "Harry Potter and the Sorcerer's Stone", 1, 9780747532743, new DateTime(1997, 6, 26)),
        new Books(2, "1984", 2, 9780451524935, new DateTime(1949, 6, 8)),
        new Books(3, "Pride and Prejudice", 3, 9780679783268, new DateTime(1813, 1, 28)),
        new Books(4, "Adventures of Huckleberry Finn", 4, 9780486280615, new DateTime(1885, 12, 10)),
        new Books(5, "Norwegian Wood", 5, 9780375704024, new DateTime(1997, 9, 4)),
        new Books(6, "Harry Potter and the Chamber of Secrets", 1, 9780747538493, new DateTime(1998, 7, 2)),
        new Books(7, "Animal Farm", 2, 9780451526342, new DateTime(1945, 8, 17))
    };

    
    public LibraryService()
    {
        
    }
    public List<Books> booksInYear(int year, string order_by)
    {
        var booksInYear = books.Where(b => b.published_year.Year == year);
        return order_by == "desc" ? booksInYear.OrderByDescending(b => b.published_year).ToList(): booksInYear.ToList();
    }

    public List<Author> authorsInYear(int year)
    {
        var authorsInYear = authors.Where(a => a.birth_date.Year == year);
        return authorsInYear.ToList();
    }

    public List<Author> authorsInYearAndCountry(int year, string country)
    {
        var authorsInYearAndCountry = authors.Where(a => a.birth_date.Year == year && a.country == country);
        return authorsInYearAndCountry.ToList();
    }

    public int totalBooks()
    {
        return books.Count();
    }

    public List<Books> getBooks(int page, int page_size)
    {
        // The pages will be treated as indexed by 1
        var booksInPage = books.Skip((page-1) * page_size).Take(page_size);
        return booksInPage.ToList();
    }
}