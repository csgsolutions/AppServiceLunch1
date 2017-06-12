using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using AppServiceDemo1.Models;

namespace AppServiceDemo1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            string connectionString = "Server=tcp:csg-sql-bargeops.database.windows.net,1433;Initial Catalog=Lunch1;Persist Security Info=False;User ID=lunch1;Password=RCMqFuNltvAC7EHF8ysp57;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var db = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                db.Open();

                var customers = db.Query<CustomerModel>(@"
                    SELECT * 
                    FROM SalesLT.Customer 
                    ORDER BY LastName, FirstName"
                );

                return View(customers);
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
