using System;
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
            
            string bugFilePath = Directory.GetCurrentDirectory() + "\\Tickets.csv";
            BugFile bugFile = new BugFile(bugFilePath);

            string enhancementFilePath = Directory.GetCurrentDirectory() + "\\Enhancements.csv";
            EnhancementsFile enhanceFile = new EnhancementsFile(enhancementFilePath);

            string taskFilePath = Directory.GetCurrentDirectory() + "\\Task.csv";
            TaskFile taskFile = new TaskFile(taskFilePath);

            string choice;
            do
            {
                Console.WriteLine("1) List tickets."); //asks the user what they'd like to do
                Console.WriteLine("2) Create new ticket.");
                Console.WriteLine("3) Find Ticket");
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
                            foreach(Ticket.Bug m in bugFile.Bugs)
                                {
                                    Console.WriteLine(m.Display());
                                }
                        }
                        else if (typeChoice == "2") // 2 reads enhancements
                        {
                            foreach(Ticket.Enhancement m in enhanceFile.Enhancements)
                                {
                                    Console.WriteLine(m.Display());
                                }
                        }
                        else if (typeChoice == "3") // 3 reads tasks
                        {
                            foreach(Ticket.Task m in taskFile.Tasks)
                                {
                                    Console.WriteLine(m.Display());
                                }
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
                            Console.WriteLine("Enter a Bug Ticket (Y/N)?");
                            string resp = Console.ReadLine().ToUpper();
                            if (resp != "Y") { break; }
                            var bug = new Ticket.Bug();
                            Console.WriteLine("Enter the summary of the ticket.");
                            bug.summary = Console.ReadLine();
                            Console.WriteLine("Enter the current status.");
                            bug.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority.");
                            bug.priority = Console.ReadLine();
                            Console.WriteLine("Who submitted the ticket?");
                            bug.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the ticket?");
                            bug.assigned = Console.ReadLine();
                            Console.WriteLine("Who is watching?");
                            bug.watching = Console.ReadLine();
                            Console.WriteLine("What is the severity of the bug?");
                            bug.severity = Console.ReadLine();
                        
                                try{
                                    bugFile.AddTicket(bug);
                                }catch (Exception ex)
                                {
                                    logger.Error("Unable to add ticket!"); // logs an exception
                                    logger.Error(ex.Message);
                                }
                        }            
                        else if (typeChoice == "2") // 2 creates an enhancement ticket
                        {
                            Console.WriteLine("Enter an Enhancement Ticket (Y/N)?");
                            string resp = Console.ReadLine().ToUpper();
                            if (resp != "Y") { break; }
                            var enhance = new Ticket.Enhancement();
                            Console.WriteLine("Enter the summary of the ticket.");
                            enhance.summary = Console.ReadLine();
                            Console.WriteLine("Enter the current status.");
                            enhance.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority.");
                            enhance.priority = Console.ReadLine();
                            Console.WriteLine("Who submitted the ticket?");
                            enhance.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the ticket?");
                            enhance.assigned = Console.ReadLine();
                            Console.WriteLine("Who is watching?");
                            enhance.watching = Console.ReadLine();
                            Console.WriteLine("What is the software?");
                            enhance.software = Console.ReadLine();
                            Console.WriteLine("What is the cost?");
                            enhance.cost = Console.ReadLine();
                            Console.WriteLine("What is the estimate?");
                            enhance.estimate = Console.ReadLine();
                        
                            try{
                                enhanceFile.AddTicket(enhance);
                            }catch (Exception ex)
                        {
                            logger.Error("Unable to add ticket!"); // logs an exception
                            logger.Error(ex.Message);
                        }
                    
                        }
                        else if (typeChoice == "3")// 3 creates a task ticket
                        {
                            Console.WriteLine("Enter a Task Ticket (Y/N)?");
                            string resp = Console.ReadLine().ToUpper();
                            if (resp != "Y") { break; }
                            var task = new Ticket.Task();
                            Console.WriteLine("Enter the summary of the ticket.");
                            task.summary = Console.ReadLine();
                            Console.WriteLine("Enter the current status.");
                            task.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority.");
                            task.priority = Console.ReadLine();
                            Console.WriteLine("Who submitted the ticket?");
                            task.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the ticket?");
                            task.assigned = Console.ReadLine();
                            Console.WriteLine("Who is watching?");
                            task.watching = Console.ReadLine();
                            Console.WriteLine("What is the project name?");
                            task.projectName = Console.ReadLine();
                            Console.WriteLine("When is the due date?");
                            task.dueDate = Console.ReadLine();
                        
                            try{
                                taskFile.AddTicket(task);
                            }catch (Exception ex)
                        {
                            logger.Error("Unable to add ticket!"); // logs an exception
                            logger.Error(ex.Message);
                        }
                    }
                        
                    }while (typeChoice == "1" || typeChoice == "2" || typeChoice == "3"); //will loop as long as 1, 2, or 3 is entered
                }
                else if  (choice == "3")
                {
                    string choiceFind;
                    do
                    {
                        Console.WriteLine("");
                        Console.WriteLine("1) Search based on Status"); //asks the user what they'd like to do
                        Console.WriteLine("2) Search based on Priority");
                        Console.WriteLine("3) Search based on Submitter");
                        Console.WriteLine("Enter any other key to exit.");
                        choiceFind = Console.ReadLine();

                        if(choiceFind == "1"){
                            Console.WriteLine("Enter the status in which you want to search");
                            string statusSearched = Console.ReadLine();
                            var Status = bugFile.Bugs.Where(m => m.status.Contains(statusSearched));
                            var Status1 = enhanceFile.Enhancements.Where(m => m.status.Contains(statusSearched));
                            var Status2 = taskFile.Tasks.Where(m => m.status.Contains(statusSearched));
                            Console.WriteLine("");

                            foreach(var m in Status)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Status1)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Status2)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                                Console.WriteLine($"\nThere are {Status.Count()+Status1.Count()+Status2.Count()} tickets with the status {statusSearched}\n");
                        }

                        else if (choiceFind == "2")
                        {
                            Console.WriteLine("Enter the priority in which you want to search");
                            string prioritySearched = Console.ReadLine();
                            var Priority = bugFile.Bugs.Where(m => m.priority.Contains(prioritySearched));
                            var Priority1 = enhanceFile.Enhancements.Where(m => m.priority.Contains(prioritySearched));
                            var Priority2 = taskFile.Tasks.Where(m => m.priority.Contains(prioritySearched));
                            Console.WriteLine("");

                            foreach(var m in Priority)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Priority1)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Priority2)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                                Console.WriteLine($"\nThere are {Priority.Count()+Priority1.Count()+Priority2.Count()} tickets with the status {prioritySearched}\n");
                        }

                        else if (choiceFind == "3")
                        {
                            Console.WriteLine("Enter the submitter in who you want to search");
                            string submitterSearched = Console.ReadLine();
                            var Submitter = bugFile.Bugs.Where(m => m.submitter.Contains(submitterSearched));
                            var Submitter1 = enhanceFile.Enhancements.Where(m => m.submitter.Contains(submitterSearched));
                            var Submitter2 = taskFile.Tasks.Where(m => m.submitter.Contains(submitterSearched));
                            Console.WriteLine("");

                            foreach(var m in Submitter)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Submitter1)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                            foreach(var m in Submitter2)
                            {
                                Console.WriteLine($"{m.Display()}");
                            }
                                Console.WriteLine($"\nThere are {Submitter.Count()+Submitter1.Count()+Submitter2.Count()} tickets with the status {submitterSearched}\n");
                        
                        }

                    }while(choiceFind == "1" || choiceFind == "2" || choiceFind == "3");
                }
            } while (choice == "1" || choice == "2" || choice == "3"); // keeps looping as long as 1 or 2 is entered
            logger.Info("Program ended"); // logs that it ended
        }
    }
}