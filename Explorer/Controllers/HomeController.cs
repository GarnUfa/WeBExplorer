using Explorer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kendo.Mvc;



namespace Explorer.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private ExplorerContext context;
        private IWebHostEnvironment hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ExplorerContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
            _logger = logger;
            
        }


        [HttpPost]
        public async Task<IActionResult> Index(string folderName)
        {
            
            FoldersModel folder = new FoldersModel() { Name = folderName };
            await context.Folders.AddAsync(folder);
            await context.SaveChangesAsync();
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFileFromDB([FromForm]FilesViewModel Contents)
        {
            FilesModel file = new FilesModel();
            byte[] Data = null;
            if (Contents.Content != null)
            {
                using (var binaryReader = new BinaryReader(Contents.Content.OpenReadStream()))
                {
                    Data = binaryReader.ReadBytes((int)Contents.Content.Length);
                }
                file.Description = Contents.Description;
                file.Name = Contents.Name;
                file.Content = Data;
            }
            await context.Files.AddAsync(file);
            await context.SaveChangesAsync();
            return View("Index");
        }

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
        public IActionResult testing()
        {
            ViewExplorerModel viewExplorerModel = new ViewExplorerModel(ref context);
            return View();
        }
        public JsonResult testing2()
        {
            return new JsonResult(context);
        }
    }
}
