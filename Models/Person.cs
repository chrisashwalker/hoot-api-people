using System.ComponentModel.DataAnnotations;

namespace hoot_api_people.Models;

public class PersonData
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = "";

    [MaxLength(50)]
    public string LastName { get; set; } = "";
}

public class Person : PersonData
{
    public int Id { get; set; }
}

public class PersonBuilder : PersonData 
{
    public Person Build(int id)
    {
        return new Person
        {
            Id = id,
            FirstName = this.FirstName,
            LastName = this.LastName
        };
    }
}