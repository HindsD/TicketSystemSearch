using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{
    public class BugFile
    {
        public string filePath { get; set; }
        public List<Ticket> Bugs { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public BugFile(string bugFilePath)
        {
            filePath = bugFilePath;
            Bugs = new List<Ticket>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                //sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var bug = new Ticket.Bug();
                    string line = sr.ReadLine();
                        string[] bugDetails = line.Split('|');
                        bug.ticketId = int.Parse(bugDetails[0]);
                        bug.summary = bugDetails[1];
                        bug.status = bugDetails[2];
                        bug.priority = bugDetails[3];
                        bug.submitter = bugDetails[4];
                        bug.assigned = bugDetails[5];
                        bug.watching = bugDetails[6];
                        bug.severity = bugDetails[7];
                        Bugs.Add(bug);
                }  
                sr.Close();
                logger.Info("Bugs in file {Count}", Bugs.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTicket(Ticket.Bug bug)
        {
            try
            {
                try{
                    bug.ticketId = Bugs.Max(m => m.ticketId) + 1;
                }
                catch{
                    bug.ticketId = 1;
                }

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{bug.ticketId}|{bug.summary}|{bug.status}|{bug.priority}|{bug.submitter}|{bug.assigned}|{bug.watching}|{bug.severity}");
                sw.Close();
                Bugs.Add(bug);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}