using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPut]
    public ActionResult ReplacePerson(Person replacement)
    {
        var person = PeopleDataStore.Current.People
            .FirstOrDefault(p => p.Id == replacement.Id);

        if (person == null)
        {
            return NotFound();
        }

        person.FirstName = replacement.FirstName;
        person.LastName = replacement.LastName;

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult UpdatePerson(int id, [FromBody] JsonPatchDocument<PersonData> patchDocument)
    {
        var person = PeopleDataStore.Current.People
            .FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        patchDocument.ApplyTo(person, ModelState);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
    
}
