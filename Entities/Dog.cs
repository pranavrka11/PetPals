using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entities
{
    internal class Dog:Pet
    {
        public string dogBreed {  get; set; }

        public Dog() { }

        public Dog(string name, int age, string breed, string dogBreed):base(name, age, breed) 
        {
            this.dogBreed = breed;
        }

        //public override string ToString()
        //{
        //    return 
        //}
    }
}
