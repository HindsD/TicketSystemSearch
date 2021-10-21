using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{
    public class EnhancementsFile
    {
        public string filePath2 { get; set; }
        public List<Ticket> Enhancements { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public EnhancementsFile(string enhanceFilePath)
        {
            filePath2 = enhanceFilePath;
            Enhancements = new List<Ticket>();
            try
            {
                StreamReader sr = new StreamReader(filePath2);
                //sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var enhance = new Ticket.Enhancement();
                    string line = sr.ReadLine();
                        string[] enhanceDetails = line.Split('|');
                        enhance.ticketId = int.Parse(enhanceDetails[0]);
                        enhance.summary = enhanceDetails[1];
                        enhance.status = enhanceDetails[2];
                        enhance.priority = enhanceDetails[3];
                        enhance.submitter = enhanceDetails[4];
                        enhance.assigned = enhanceDetails[5];
                        enhance.watching = enhanceDetails[6];
                        enhance.software = enhanceDetails[7];
                        enhance.cost = enhanceDetails[8];
                        enhance.estimate = enhanceDetails[9];
                        
                        Enhancements.Add(enhance);
                }  
                sr.Close();
                logger.Info("Enhancements in file {Count}", Enhancements.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTicket(Ticket.Enhancement enhancement)
        {
            try
            {
                try{
                    enhancement.ticketId = Enhancements.Max(m => m.ticketId) + 1;
                }
                catch{
                    enhancement.ticketId = 1;
                }

                StreamWriter sw = new StreamWriter(filePath2, true);
                sw.WriteLine($"{enhancement.ticketId}|{enhancement.summary}|{enhancement.status}|{enhancement.priority}|{enhancement.submitter}|{enhancement.assigned}|{enhancement.watching}|{enhancement.software}|{enhancement.cost}|{enhancement.estimate}");
                sw.Close();
                Enhancements.Add(enhancement);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}