using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using hoot_api_people.Data;
using hoot_api_people.Models;

namespace hoot_api_people.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{

    private readonly PeopleContext _context;

    public PeopleController(PeopleContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetPeople()
    {
        return Ok(_context.People.ToList());
    }

    [HttpGet("{id}", Name = "GetPerson")]
    public ActionResult<Person> GetPerson(int id)
    {
        var person = _context.People
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
        var maxId = _context.People.Max(p => p.Id);

        var newPerson = personToCreate.Build(++maxId);

        _context.People.Add(newPerson);
        _context.SaveChanges();

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
        var person = _context.People
            .FirstOrDefault(p => p.Id == replacement.Id);

        if (person == null)
        {
            return NotFound();
        }

        person.FirstName = replacement.FirstName;
        person.LastName = replacement.LastName;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult UpdatePerson(int id, [FromBody] JsonPatchDocument<PersonData> patchDocument)
    {
        var person = _context.People
            .FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        patchDocument.ApplyTo(person, ModelState);
        _context.SaveChanges();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult<Person> DeletePerson(int id)
    {
        var person = _context.People
            .FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        _context.People.Remove(person);
        _context.SaveChanges();
        MessageService.PostDeletionMessage("person", person.Id);

        return NoContent();
    }
}
