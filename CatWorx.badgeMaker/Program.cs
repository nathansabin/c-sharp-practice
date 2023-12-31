﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
    class Program
    {

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

         async static Task Main(string[] args)
        {
            // Calls function to take user data from CLI 
            List<Employee> employees = GetEmployees();

            // prints users, adds data to csv generates badges
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
        }
    }
    
}
