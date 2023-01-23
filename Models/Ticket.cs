using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public string ProjectName { get; set; }
        public string DepartmentName { get; set; }
        public string RequestedBy { get; set; }
        public string DescriptionOfProblem { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}