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
            context.Folders.Add(folder);
            await context.SaveChangesAsync();
            return View();
        }
        //------------------------//
        [HttpPost]
        public async Task<IActionResult> AddFileFromDB([FromForm]IFormFile file)
        {
            if(file != null)
            {
            }
            //foreach (IFormFile source in file)
            //{
            //    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

            //    filename = this.EnsureCorrectFilename(filename);

            //    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
            //        await source.CopyToAsync(output);
            //}

            return this.View();
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            return hostEnvironment.WebRootPath + "\\uploads\\" + filename;
        }
        //------------------------//
        //[HttpPost]
        //public async Task<IActionResult> Index(FilesModel file)
        //{
        //    //FoldersModel folder = new FoldersModel() { Name = folderName };
        //    //context.Folders.Add(folder);
        //    //await context.SaveChangesAsync();
        //    return View();
        //}
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
        [HttpPost]
        public IActionResult Add()
        {
            return Ok("fdsfsd");
        }
    }
}
