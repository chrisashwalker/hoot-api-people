using Microsoft.AspNetCore.Mvc;
using hoot_api_people.Data;
using hoot_api_people.Models;

namespace hoot_api_people.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetPeople()
    {
        return Ok(PeopleDataStore.Current.People);
    }

    [HttpGet("{id}", Name = "GetPerson")]
    public ActionResult<Person> GetPerson(int id)
    {
        var person = PeopleDataStore.Current.People
            .FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPost]
    public ActionResult<Person> CreatePerson(PersonBuilder personToCreate)
    {
        var maxId = PeopleDataStore.Current.People
            .Count<Person>();

        var newPerson = personToCreate.Build(++maxId);

        PeopleDataStore.Current.People.Add(newPerson);

        return CreatedAtRoute("GetPerson", 
            new {
                newPerson.Id
            },
            newPerson
        );
    }
    
}
