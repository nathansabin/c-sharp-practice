using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;

namespace CatWorx.BadgeMaker 
{
    class Util 
    {
        public static void PrintEmployees(List<Employee> employees)
        {   
            // Loops through data prints each entry to the CLI.
            for (int i = 0; i < employees.Count; i++) {
                    string template = "{0,-10}\t{1,-20}\t{2}";
                    Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
        }

        // function for Creating CSV file and adding all user data 
        public static void MakeCSV(List<Employee> employees)
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                file.WriteLine("ID,Name,PhotoURL");
                for (int i = 0; i < employees.Count; i++) 
                {
                    string template = "{0},{1},{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
                }
            }     
        }

        // Formats badge for and Employees
        public static void MakeBadges(List<Employee> employees)
        {
            HttpClient client = new HttpClient();
            // SKImage newImage = SKImage.FromEncodedData(File.OpenRead("badge.png"));

            // SKData data = newImage.Encode();
            // data.SaveTo(File.OpenWrite("data/employeeBadge.png"));

        }      
    }
}