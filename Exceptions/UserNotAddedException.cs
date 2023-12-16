using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Exceptions
{
    internal class UserNotAddedException:ApplicationException
    {
        public UserNotAddedException()
        {

        }

        public UserNotAddedException(string message) : base(message) { }
    }
}
