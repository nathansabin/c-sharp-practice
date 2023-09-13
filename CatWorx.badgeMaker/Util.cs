using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker 
{
    class Util 
    {
            // Loops through data prints each entry to the CLI.
        public static void PrintEmployees(List<Employee> employees)
        {   
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
        async public static Task MakeBadges(List<Employee> employees)
        {
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;  

            int PHOTO_LEFT_X = 184;
            int PHOTO_TOP_Y = 215;
            int PHOTO_RIGHT_X = 486;
            int PHOTO_BOTTOM_Y = 517;

            int COMPANY_NAME_Y = 150;
            int EMPLOYEE_NAME_Y = 600;
            int EMPLOYEE_ID_Y = 730;

            using (HttpClient client = new HttpClient()) 
            {
                for (int i = 0; i < employees.Count; i++) 
                {
                    // converts background and cat image to to SKI format
                    SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));
                    SKImage backGround = SKImage.FromEncodedData(File.OpenRead("badge.png"));

                    // Creates an object that will work as a blank image than converts to an object that can be altered
                    SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);
                    SKCanvas canvas = new SKCanvas(badge);

                    SKPaint paint = new SKPaint();
                    paint.TextSize = 42.0f;
                    paint.IsAntialias = true;
                    paint.Color = SKColors.White;
                    // paint.IsStroke = false;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.Typeface = SKTypeface.FromFamilyName("Arial");

                    // Company name
                    canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);

                    // employee name
                    paint.Color = SKColors.Black;
                    canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint);

                    paint.Typeface = SKTypeface.FromFamilyName("Courier New");

                    // Employee ID
                    paint.Typeface = SKTypeface.FromFamilyName("Courier New");
                    canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint);

                    // adds background badge image to cover entire background than overlays cat into place
                    canvas.DrawImage(backGround, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
                    canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));
                    
                    // adds picture to data file in png format
                    SKImage finalImage = SKImage.FromBitmap(badge);
                    SKData data = finalImage.Encode();
                    string template = "data/{0}_badge.png";
                    data.SaveTo(File.OpenWrite(string.Format(template, employees[i].GetId())));
                }
            }
        }      
    }
}
