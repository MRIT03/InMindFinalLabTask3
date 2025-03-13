using System.ComponentModel.DataAnnotations;

namespace InMindLab1.Models;

public class Author
{
    [Required]
    public int author_id {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public DateTime birth_date {get; set;}
    [Required]
    public string country {get; set;}
    
    public Author()
    {
    }
    public Author(int author_id, string name, DateTime birth_date, string country)
    {
        this.author_id = author_id;
        this.Name = name;
        this.birth_date = birth_date;
        this.country = country;
    }
}