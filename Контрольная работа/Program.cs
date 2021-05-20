using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ControlWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string fileName = "contolData.json";

            Person[] persons = GetPersonArray();
            string jsonContent = JsonConvert.SerializeObject(persons);

            Console.WriteLine($"Преобразованные данные:\n{jsonContent}\n");
            new FileOperator().WriteFile(fileName, jsonContent);

            string jsonFileContent = new FileOperator().GetFileContentByPath(fileName);
            Console.WriteLine($"Полученные данные:\n{jsonFileContent}\n");
            Person[] personsBack = JsonConvert.DeserializeObject<Person[]>(jsonContent);

            List<Person> correctPersons = new List<Person>();
            if (personsBack != null)
            {
                foreach (var person in personsBack)
                {
                    if (person.JustBag != null)
                    {
                        if (person.JustBag.IsGoodForTaking)
                        {
                            correctPersons.Add(person);
                        }
                    }
                }
            }

            string result = string.Empty;
            foreach (var person in correctPersons)
            {
                result += $"{person.Name} {person.JustBag.AvgThingWeight}\n";
            }

            Console.WriteLine($"Список подходящих по условию людей:\n{result}");
        }

        private static Person[] GetPersonArray()
        {
            Person[] persons = {
                new Person {
                    Name = "Вадим",
                    HandBag = new Bag{ Things =  new List<Thing>
                    {
                        new Thing {Weight = 3},
                        new Thing {Weight = 3},
                        new Thing {Weight = 3},
                        new Thing {Weight = 3},
                        new Thing {Weight = 3}
                    }},
                    JustBag = null
                },
                new Person {
                    Name = "Петров",
                    HandBag = new Bag{ Things =  new List<Thing>
                    {
                        new Thing {Weight = 1.4},
                    }},
                    JustBag = new Bag{ Things =   new List<Thing>
                    {
                        new Thing {Weight = 1.1232123},
                        new Thing {Weight = 1.23233232},
                        new Thing {Weight = 1.334343434}
                    }}
                },
                new Person {
                    Name = "Иванов",
                    HandBag = new Bag{ Things =   new List<Thing>
                    {
                        new Thing {Weight = 10.4},
                        new Thing {Weight = 20.3},
                        new Thing {Weight = 30.4}
                    }},
                    JustBag = new Bag{ Things =  new List<Thing>
                    {
                        new Thing {Weight = 3},
                        new Thing {Weight = 3.2}
                    }}
                }
            };
            return persons;
        }
    }
}
