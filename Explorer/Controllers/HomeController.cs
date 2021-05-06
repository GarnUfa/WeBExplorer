using Explorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ExplorerContext context;

        public HomeController(ILogger<HomeController> logger, ExplorerContext context)
        {
            this.context = context;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Index(string folderName)
        {
            FoldersModel folder = new FoldersModel() { Name = folderName };
            context.Folders.Add(folder);
            await context.SaveChangesAsync();
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await context.Folders.ToListAsync());
        //}

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Explorer()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
