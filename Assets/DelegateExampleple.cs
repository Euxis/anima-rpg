using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DelegateExampleple : MonoBehaviour
{
    private Action<string> exampleAction;
    private Func<string, bool> exampleFunc;
    private List<Person> names = new()
    {
        new() { PersonName = "Kenjiro", Age = 1 },
        new() { PersonName = "Lucy", Age = 2 },
        new() { PersonName = "Caeser", Age = 1 },
        new() { PersonName = "Billy", Age = 2 },
    };

    public struct Person
    {
        public string PersonName;
        public int Age;
    }

    private void Start()
    {
        var anotherList = names
            .Select(x => x.Age)
            .ToList();

        var person = anotherList
            .Find(x => x == 1);

        Debug.Log(person);

        anotherList
            .ForEach(x =>
            {
                //Debug.Log(x);
            });
    }

    private void DoThing(string thing)
    {
        thing += "1";
    }
}
