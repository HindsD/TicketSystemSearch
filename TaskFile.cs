using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{
    public class TaskFile
    {
        public string filePath { get; set; }
        public List<Ticket> Tasks { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public TaskFile(string taskFilePath)
        {
            filePath = taskFilePath;
            Tasks = new List<Ticket>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                //sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var task = new Ticket.Task();
                    string line = sr.ReadLine();
                        string[] taskDetails = line.Split('|');
                        task.ticketId = int.Parse(taskDetails[0]);
                        task.summary = taskDetails[1];
                        task.status = taskDetails[2];
                        task.priority = taskDetails[3];
                        task.submitter = taskDetails[4];
                        task.assigned = taskDetails[5];
                        task.watching = taskDetails[6];
                        task.projectName = taskDetails[7];
                        task.dueDate = taskDetails[8];
                        Tasks.Add(task);
                }  
                sr.Close();
                logger.Info("Tasks in file {Count}", Tasks.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTicket(Ticket.Task task)
        {
            try
            {
                try{
                    task.ticketId = Tasks.Max(m => m.ticketId) + 1;
                }
                catch{
                    task.ticketId = 1;
                }

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{task.ticketId}|{task.summary}|{task.status}|{task.priority}|{task.submitter}|{task.assigned}|{task.watching}|{task.projectName}|{task.dueDate}");
                sw.Close();
                Tasks.Add(task);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}