using hoot_api_people.Models;

namespace hoot_api_people.Data;

public class PeopleDataStore
{
    
    public static PeopleDataStore Current { get; } = new PeopleDataStore();
    public ICollection<Person> People { get; set; }

    public PeopleDataStore()
    {
        People = Enumerable.Range(1, 5).Select(index => new Person
        {
            Id = index,
            FirstName = $"Person{index}"
        })
        .ToList();
    }

}
