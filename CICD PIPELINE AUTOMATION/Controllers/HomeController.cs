/*
SOLID :

S -> Single Responsibility Principle :

A class should have only one responsibility, hence only a single purpose.

Benefits of SRP:
1. Easier to understand
2. Easier to maintain
3. Changed less frequently
4. Easily and thoroughly testable

XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

O -> Open/Closed Principle :

A class should be open for extension but closed for modification.

Benefits of OCP:
1. Not allowing modifications provides the advantage of not introducing bugs
2. All dependent classes will not need to adapt

In an essence if you are using interface, you are using Open/Closed principle.

XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX




*/




using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CICD_PIPELINE_AUTOMATION.Models;
using Snowflake.Data.Client;
using System.Net;
using System.IO;
using CICD_PIPELINE_AUTOMATION.Services;
using CICD_PIPELINE_AUTOMATION.Contracts;

namespace CICD_PIPELINE_AUTOMATION.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserContract userService;
        private readonly IItemContract itemService;
        private readonly IDatabaseContract databaseService;
        private readonly ILogger<HomeController> _logger;
        private string conStr;
        RoleInformation roleInformation;
        private IConInfo conInfo;

        public HomeController(ILogger<HomeController> logger, IConInfo conInfo, IUserContract userService, IItemContract itemService, IDatabaseContract databaseService)
        {
            _logger = logger;
            this.conInfo = conInfo;
            roleInformation = new RoleInformation();
            this.userService = userService;
            this.itemService = itemService;
            this.databaseService = databaseService;
        }

        public IActionResult Index(string conStr)
        {
            roleInformation = databaseService.GetDatabase();
            ViewBag.databases = roleInformation.databases;
            return View();
        }

        public IActionResult PostData(Objects data)
        {
            itemService.SendItems(data);
            ViewData["Message"] = "Inserted Successfully";
            TempData["msg"] = "<script>alert('Inserted Successfully');</script>";
            return RedirectToAction("Index");
            //insert into "DEV_DB"."RAW"."SCHEMA_OBJECT_LIST" values(id, ojject_name, object_type, schema, sourcedb, dest_db);
        }

        public IActionResult Items(Objects objects)
        {
            Database database = itemService.GetItems(objects);
            ViewBag.schemas = database.Schemas;
            ViewBag.sourceDB = objects.SourceDb;
            ViewBag.targetDB = objects.TargetDb;
            return View();
        }

        public IActionResult Login(SUser suser)
        {
            string conStr = userService.LoginUser(suser);
            if (conStr!=null)
            {
                return RedirectToAction("Index", new { conStr });
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
