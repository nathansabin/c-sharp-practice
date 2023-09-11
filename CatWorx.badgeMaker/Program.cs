using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calls function to take user data from CLI 
            List<Employee> employees = GetEmployees();

            // takes user data and prints it to command line
            PrintEmployees(employees);
        }

        // Takes user data and returns to main function.
        static List<Employee> GetEmployees() 
        {  
            List<Employee> employees = new List<Employee>();
            int id = 1;

            // Will keep checking for user data to add to list until user enters blank string.
            while (true) {
                Console.WriteLine("Please enter a first name or leave blank to move on:");
                string firstName = Console.ReadLine() ?? "";
                if (firstName == "") {
                    break;
                }
                Console.WriteLine("Please enter last name:");
                string lastName = Console.ReadLine() ?? "";


                Employee currentEmployee = new Employee(firstName, lastName, id, "https://placeKitten.com/300/300");
                employees.Add(currentEmployee);
                id++;
            }

            // returns data to main
            return employees;
        }
        static void PrintEmployees(List<Employee> employees)
        {   
            // Loops through data prints each entry to the CLI.
            for (int i = 0; i < employees.Count; i++) {
                    string template = "{0,-10}\t{1,-20}\t{2}";
                    Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
        }
    }
}
