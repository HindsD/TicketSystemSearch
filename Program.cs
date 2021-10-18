﻿using System;
using System.IO;
using NLog.Web;
using System.Linq;

namespace TicketSystemSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger(); // addslogger
            logger.Info("Program started"); //logs it started
            Ticket ticket = new Ticket();
            var bug = new Ticket.Bug();
            var enhancement = new Ticket.Enhancement();
            var task = new Ticket.Task();

            string choice;
            do
            {
                Console.WriteLine("1) List tickets."); //asks the user what they'd like to do
                Console.WriteLine("2) Create new ticket.");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();
                logger.Info("User choice: " + choice); //keeps track of the user's choice

                if (choice == "1"){
                    string typeChoice;
                    do
                    {
                        Console.WriteLine("Which type of ticket would you like to read?");//asks the user which type of ticket they want to access
                        Console.WriteLine("1) Bug/Defect Tickets");
                        Console.WriteLine("2) Enhancement Tickets");
                        Console.WriteLine("3) Task Tickets");
                        Console.WriteLine("Enter any other key to exit.");
                        typeChoice = Console.ReadLine();
                        if (typeChoice == "1") // 1 reads bugs
                        {
                            bug.ReadTicket();
                        }
                        else if (typeChoice == "2") // 2 reads enhancements
                        {
                            enhancement.ReadTicket();
                        }
                        else if (typeChoice == "3") // 3 reads tasks
                        {
                            task.ReadTicket();
                        }
                    }while (typeChoice == "1" || typeChoice == "2" || typeChoice == "3");//will loop as long as 1, 2, or 3 is entered
                }
                else if (choice == "2")
                {
                    string typeChoice;
                    do
                    {
                        Console.WriteLine("What type of ticket would you like to make?"); //asks the user what type of ticket to make
                        Console.WriteLine("1) Bug/Defect Tickets");
                        Console.WriteLine("2) Enhancement Tickets");
                        Console.WriteLine("3) Task Tickets");
                        Console.WriteLine("Enter any other key to exit.");
                        typeChoice = Console.ReadLine();
                        if (typeChoice == "1") // 1 creates a bug ticket
                        {
                            bug.Questions();
                        }
                        else if (typeChoice == "2") // 2 creates an enhancement ticket
                        {
                            enhancement.Questions();
                        }
                        else if (typeChoice == "3")// 3 creates a task ticket
                        {
                            task.Questions();
                        }
                    }while (typeChoice == "1" || typeChoice == "2" || typeChoice == "3"); //will loop as long as 1, 2, or 3 is entered
                }

            } while (choice == "1" || choice == "2"); // keeps looping as long as 1 or 2 is entered
            logger.Info("Program ended"); // logs that it ended
        }
    }
}
