using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TicketSystem.Models;
using Microsoft.Ajax.Utilities;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult TicketEntry()
        {
            string constr = ConfigurationManager.ConnectionStrings["tsConnectionString"].ToString();
            SqlConnection con = new SqlConnection(constr);

            SqlDataAdapter da = new SqlDataAdapter("Select * From Department", constr);
            DataTable dt = new DataTable();
            da.Fill(dt);

            
            ViewBag.DepartmentList = ToSelectList(dt, "DId","DName");

            SqlDataAdapter da1 = new SqlDataAdapter("Select EId, EName From Employee", constr);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            ViewBag.EmployeeList = ToSelectList(dt1, "EId", "EName");

            return View();
        }

        [HttpPost]
        public ActionResult TicketEntry([Bind(Include = "ProjectName, DepartmentName, RequestedBy, DescriptionOfProject, CreatedAt")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("TicketList");
            }

            return View(ticket);
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }


    }
}