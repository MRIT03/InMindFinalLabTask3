using System.ComponentModel.DataAnnotations;

namespace InMindLab1.Models;

public class Books
{
    [Required]
    public int id {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public int author_id {get; set;}
    [Required]
    public long isbn {get; set;}
    [Required]
    public DateTime published_year {get; set;}

    public Books()
    {
        
    }
    public Books(int id, string name, int author_id, long isbn, DateTime published_year)
    {
        this.id = id;
        this.Name = name;
        this.author_id = author_id;
        this.isbn = isbn;
        this.published_year = published_year;
    }

}