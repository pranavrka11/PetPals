using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entities
{
    internal abstract class Donation
    {
        public string donorName { get; set; }
        public double donationDate {  get; set; }

        public Donation() { }
    }
}
