using hoot_api_people.Models;

namespace hoot_api_people.Data;

public class PeopleInitialiser
{
    
    public static void Initialise(PeopleContext context)
    {
        context.Database.EnsureCreated();

        if (context.People == null || context.People.Any())
        {
            return;
        }

        var people = new Person[]
        {
        new Person{FirstName="Guillermo",LastName="Duncan"},
        new Person{FirstName="Damion",LastName="Roy"},
        new Person{FirstName="Kaylen",LastName="Paul"},
        new Person{FirstName="Tristen",LastName="Dean"},
        new Person{FirstName="Kamren",LastName="Escobar"},
        new Person{FirstName="Kasen",LastName="Tyler"},
        new Person{FirstName="Jennifer",LastName="Thomas"},
        new Person{FirstName="Case",LastName="Compton"},
        new Person{FirstName="Deon",LastName="Coleman"},
        new Person{FirstName="Raina",LastName="Brooks"},
        new Person{FirstName="Alexis",LastName="Galloway"},
        new Person{FirstName="Melvin",LastName="Simpson"},
        new Person{FirstName="Steve",LastName="McKnight"},
        new Person{FirstName="Meredith",LastName="Shaffer"},
        new Person{FirstName="Franco",LastName="Barker"},
        new Person{FirstName="Kallie",LastName="McClure"},
        new Person{FirstName="Clarissa",LastName="Robles"},
        new Person{FirstName="Hailey",LastName="Wheeler"},
        new Person{FirstName="Oswaldo",LastName="Giles"},
        new Person{FirstName="Wade",LastName="Dean"}
        };
   
        foreach (Person p in people)
        {
            context.People.Add(p);
        }

        context.SaveChanges();

    }

}