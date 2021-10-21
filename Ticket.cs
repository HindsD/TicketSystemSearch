using System;
using System.IO;
using NLog.Web;

namespace TicketSystemSearch
{
    public class Ticket
    {
        public int ticketId { get; set; }
        public string summary { get;  set; }
        public string status { get;  set; }
        public string priority { get;  set; }
        public string submitter { get;  set; }
        public string assigned { get;  set; }
        public string watching { get;  set; }
        public string[] arr { get;  set; }

        public virtual string Display(){
            
                return $"TicketID: "+ticketId+"\nSummary: "+summary+"\nStatus: "+status
            +"\nPriority: "+priority+"\nSubmitter: "+submitter+"\nAssigned: "+assigned+"\nWatching: "+watching+"\n";
            
        }

        

        public class Bug : Ticket
        {
            public string severity { get; set; }

        public override string Display()
        {
            return $"TicketID: "+ticketId+"\nSummary: "+summary+"\nStatus: "+status
            +"\nPriority: "+priority+"\nSubmitter: "+submitter+"\nAssigned: "+assigned+"\nWatching: "+watching+"\nSeverity: "+severity+"\n";
        }
        }

        public class Enhancement : Ticket
        {
            public string software { get; set; }
            public string cost { get; set; }
            public string estimate {get; set; }
        
            public override string Display()
            {
                return $"TicketID: "+ticketId+"\nSummary: "+summary+"\nStatus: "+status+"\nPriority: "+priority+"\nSubmitter: "+submitter+
                "\nAssigned: "+assigned+"\nWatching: "+watching+"\nSoftware: "+software+"\nCost: "+cost+"\nEstimate: "+estimate+"\n";
            }
        }

        public class Task : Ticket
        {
            public string projectName {get; set;}
            public string dueDate {get; set;}
            public override string Display()
            {
                return $"TicketID: "+ticketId+"\nSummary: "+summary+"\nStatus: "+status+"\nPriority: "+priority+"\nSubmitter: "+submitter+
                "\nAssigned: "+assigned+"\nWatching: "+watching+"\nProject Name: "+projectName+"\nDue Date: "+dueDate+"\n";
            }
        }

    }
}