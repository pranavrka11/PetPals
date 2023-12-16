using PetPals.Entities;
using PetPals.Exceptions;
using PetPals.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Dao
{
    internal class PetPalsRepo : IPetPalsRepo
    {
        //DB Connectivity
        public string connectionString;
        SqlCommand cmd = null;

        public PetPalsRepo()
        {
            connectionString = DbConnUtil.getConnectionString();
            cmd = new SqlCommand();
        }


        //Implementation for the user management methods
        public void registerUser(int userID, string userName)
        {
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Users(userid, username) values(@userid ,@name)";
                cmd.Parameters.AddWithValue("@name", userName);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Connection= conn;
                conn.Open();
                int addUserStatus = cmd.ExecuteNonQuery();
                if(addUserStatus>1)
                {
                    Console.WriteLine("Adding user");
                }
                else
                {
                    throw new UserNotAddedException("That userid already exists");
                }
            }
        }

        public List<User> displayUsers()
        {
            List<User> allUsers = new List<User>();
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                cmd.CommandText = "select * from Users";
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    User u = new User();
                    u.userID = (int)reader["userid"];
                    u.name = (string)reader["username"];
                    allUsers.Add(u);
                }
            }

            return allUsers;
        }

        //Method implementations for Donation management
        public void recordCashDonation(int donationID, string donorName, double donationAmount)
        {
            using(SqlConnection conn=new SqlConnection(connectionString)) 
            {
                if (donationAmount > 1000)
                {
                    cmd.CommandText = "insert into Donations(donationid, donorname, donationtype, donationamount, donationdate) values(@id, @name, @amount, @date, @type)";
                    cmd.Parameters.AddWithValue("id", donationID);
                    cmd.Parameters.AddWithValue("@name", donorName);
                    cmd.Parameters.AddWithValue("@amount", donationAmount);
                    DateTime Date = DateTime.Now;
                    cmd.Parameters.AddWithValue("@date", Date);
                    string dtype = "Cash";
                    cmd.Parameters.AddWithValue("@type", dtype);
                    cmd.Connection = conn;
                    conn.Open();
                    int addDonationStatus = cmd.ExecuteNonQuery();
                    if (addDonationStatus > 0)
                    {
                        Console.WriteLine("Donation recorded successfully");
                    }
                    else
                        Console.WriteLine("Something went wrong");
                }
                else
                    throw new InsufficientFundsException("Sorry, you have to enter the amount greater than Rs. 1000");
            }
        }

        public void recordItemDonation(int donationID, string donorName, string donationItem)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Donations(donationid, donorname, donationtype, donationitem, donationdate) values(@donationid, @name, @amount, @date, @type)";
                cmd.Parameters.AddWithValue("@donationid", donationID);
                cmd.Parameters.AddWithValue("@name", donorName);
                cmd.Parameters.AddWithValue("@amount", donationItem);
                DateTime Date = DateTime.Now;
                cmd.Parameters.AddWithValue("@date", Date);
                string dtype = "Item";
                cmd.Parameters.AddWithValue("@type", dtype);
                cmd.Connection = conn;
                conn.Open();
                int addDonationStatus = cmd.ExecuteNonQuery();
                if (addDonationStatus > 0)
                {
                    Console.WriteLine("Donation recorded successfully");
                }
                else
                    Console.WriteLine("Something went wrong");
            }
        }


        //Implementation for pets management methods
        public void addPet(int petID, string petName, int petAge, string petBreed, string petType)
        {
            if (petAge > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "insert into Pets(PetID, PetName, age, Breed, PetType, AvailableForAdoption) values(@pid, @name, @age, @breed, @type, @avbl)";
                    cmd.Parameters.AddWithValue("@pid", petID);
                    cmd.Parameters.AddWithValue("@name", petName);
                    cmd.Parameters.AddWithValue("@age", petAge);
                    cmd.Parameters.AddWithValue("@breed", petBreed);
                    cmd.Parameters.AddWithValue("@type", petType);
                    string avbla = "available";
                    cmd.Parameters.AddWithValue("@avbl", avbla);

                    cmd.Connection = conn;
                    conn.Open();
                    int addPetStatus = cmd.ExecuteNonQuery();

                    if (addPetStatus > 0)
                    {
                        Console.WriteLine("Pet data added successfully");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                    }
                }
            }
            else
                throw new InvalidPetAgeException("Pet age has to be a postivive number");
        }

        public void removePet(int petID)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.CommandText = "delete from Pets where PetID=@petid";
                cmd.Parameters.AddWithValue("@petid", petID);

                int removePetStatus= cmd.ExecuteNonQuery();
                if (removePetStatus > 0)
                    Console.WriteLine("Removed pet data successfully");
                else
                    throw new PetNotFoundException("It seems like the petID you entered is invalid");
            }
        }

        public List<Pet> showAvailablePets()
        {
            List<Pet> availablePets = new List<Pet>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.CommandText = "select * from Pets where AvailableForAdoption='available'";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader=cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pet pet = new Pet();
                    pet.name = (string)reader["PetName"];
                    pet.age = (int)reader["age"];
                    pet.type = (string)reader["PetType"];
                    pet.breed = (string)reader["Breed"];
                    availablePets.Add(pet);
                }
            }

            return availablePets;
        }

        //Implementation for Adoption event methods

        public void registerUserForEvent(int userID, int eventid, string participantName, string participantType)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Participants values(@userid, @eventid, @pname, @ptype)";
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Parameters.AddWithValue("@eventid", eventid);
                cmd.Parameters.AddWithValue("@pname", participantName);
                cmd.Parameters.AddWithValue("@ptype", participantType);

                cmd.Connection = conn;
                conn.Open();

                int addParticipantStatus=cmd.ExecuteNonQuery();
                if (addParticipantStatus > 0)
                {
                    Console.WriteLine("Participant registered successfully!");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
            }
        }

        public void adoptPet(int petid, int userid)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.CommandText = "update Pets set AvailableForAdoption='notAvailable' where PetID=@petid";
                cmd.Parameters.AddWithValue("@petid", petid);
                cmd.Connection = conn;
                conn.Open();
                int petUpdate = cmd.ExecuteNonQuery();
                cmd.CommandText = "update Users set adoptedpetid=@petid where userid=@userid";
                cmd.Parameters.AddWithValue("@userid", userid);
                int userUpdate = cmd.ExecuteNonQuery();
                if (userUpdate > 0 && petUpdate > 0)
                    Console.WriteLine("Congratulations! You have successfully adopted a pet!");
                else
                    Console.WriteLine("Something went wrong");
            }
        }

    }
}
