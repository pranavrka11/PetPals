using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entities
{
    internal class User
    {
        public int userID { get; set; }
        public string name { get; set; }

        public User(int userID, string name)
        {
            this.userID = userID;
            this.name = name;
        }

        public User()
        {
        }

        public override string ToString()
        {
            return $"ID of the Member: {userID}\t Name of the Member: {name}";
        }
    }
}
