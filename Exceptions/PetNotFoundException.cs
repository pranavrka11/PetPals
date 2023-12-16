using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Exceptions
{
    internal class PetNotFoundException:ApplicationException
    {
        public PetNotFoundException() { }

        public PetNotFoundException(string message) : base(message) { }
    }
}
