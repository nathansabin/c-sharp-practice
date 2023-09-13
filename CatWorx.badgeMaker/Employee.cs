using System;

namespace CatWorx.BadgeMaker
{
    class Employee 
    {
        private string FirstName;
        private string LastName;
        private int Id;
        private string PhotoUrl;

        // adds data
        public Employee(string firstName, string lastname, int id, string photoUrl){
            FirstName = firstName;
            LastName = lastname;
            Id = id;
            PhotoUrl = photoUrl;
        }

        public string GetFullName() {
            return FirstName + " " + LastName;
        }
        public int GetId() {
            return Id;
        }
        public string GetPhotoUrl() {
            return PhotoUrl;
        }

        public string GetCompanyName() {
            return "Cat Worx";
        }
    }
}