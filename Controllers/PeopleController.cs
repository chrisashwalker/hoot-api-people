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

    [HttpGet("{id}")]
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
    
}
