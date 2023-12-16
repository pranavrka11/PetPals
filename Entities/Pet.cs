using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entities
{
    internal class Pet
    {
        public string name { get; set; }
        public int age { get; set; }
        public string breed { get; set; }
        public string type {  get; set; }

        public Pet() { }

        public Pet(string name, int age, string breed)
        {
            this.name = name;
            this.age = age;
            this.breed = breed;
        }

        public override string ToString()
        {
            return $"Name: {name}\t Age: {age}\t Type: {type}\t Breed: {breed}";
        }
    }
}
