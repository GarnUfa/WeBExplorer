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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Kendo.Mvc.UI.Fluent;
using System.Drawing;
using Microsoft.AspNetCore.WebUtilities;
using Explorer.Services.Linker;

namespace Explorer.Controllers
{
    public class HomeController : Controller
    {
        public static string selectedID;
        public static string selectedArgId;
        private readonly ILogger<HomeController> _logger;
        private ExplorerContext context;
        private IWebHostEnvironment hostEnvironment;
        private IViewExplorerModel viewExplorerModel;

        public HomeController(ILogger<HomeController> logger, ExplorerContext context, IWebHostEnvironment hostEnvironment, IViewExplorerModel viewExplorerModel)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
            _logger = logger;
            this.viewExplorerModel = viewExplorerModel;

        }


        [HttpPost]
        public async Task<IActionResult> AddFolder(string folderName)
        {
            
            FoldersModel folder = new FoldersModel() { Name = folderName, ParentID = int.Parse(selectedID) };
            await context.Folders.AddAsync(folder);
            await context.SaveChangesAsync();
            //вынести в отдельный метод во вьюексплорермодель
            viewExplorerModel.AddAllComponentsFromDB(context);
            viewExplorerModel.AddGroupedComponents();
            return View("WebExplorer");
        }
        [HttpPost]
        public async Task<IActionResult> AddFileFromDB([FromForm]FilesViewModel Contents)
        {
            string typeFile = Contents.Content.ContentType;
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
                file.FoldersModelID = int.Parse(selectedID);
                //Определяем ID иконки 
                var ExtensionFile = context.FileExtensions.Where(ext => ext.FileType == typeFile).ToList()[0];
                if(ExtensionFile is null)
                {
                    //По хорошему надо перебрать всю таблицу и найти FileType = undefined, если вдруг id его поменяется
                    file.FileExtensionsModelID = 4;
                }
                else
                {
                    file.FileExtensionsModelID = ExtensionFile.ID;
                }
            }
            await context.Files.AddAsync(file);
            await context.SaveChangesAsync();
            viewExplorerModel.AddAllComponentsFromDB(context);
            viewExplorerModel.AddGroupedComponents();
            return View("WebExplorer");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFile()
        {
            var AllComp = viewExplorerModel.AddAllComponentsFromDB(context);
            List<Component> nodes = new List<Component>();
            if(selectedArgId == "folder")
            {
                var x = context.Folders.Where(fold => fold.ID == int.Parse(selectedID)).ToList()[0];
                context.Folders.Remove(x);
                await context.SaveChangesAsync();
                Component comp = AllComp.Where(c => c.Id == selectedID && c is DirectoryExplorer).ToList()[0];
                nodes.Add(comp);
                if (comp.parentID!=0)
                {
                    var compNodes = AllComp.Where(c => c.Id == comp.parentID.ToString());
                    nodes.AddRange(compNodes);
                }
                //Удалить из БД всю ноду, удалить из Группы компонентов
            }
            if(selectedArgId =="file")
            {
                
            }
            return View();
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
        
        public IActionResult WebExplorer()
        {
            viewExplorerModel.AddAllComponentsFromDB(context);
            viewExplorerModel.AddGroupedComponents();
            return View();
        }
        [HttpPost]
        public IActionResult SelectedReturn(string i, string ar)
        {
            selectedID = i;
            selectedArgId = ar;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddIcon(IFormFile Icon, string ExtensionName, IFormFile Pattern = null)
        {
            string pattern = ExtensionName;
            byte[] DataIcon = null;
            FileExtensionsModel extension = new FileExtensionsModel();
            using (var binaryReader = new BinaryReader(Icon.OpenReadStream()))
            {
                DataIcon = binaryReader.ReadBytes((int)Icon.Length);
            }
            extension.FileType = pattern;
            extension.Icon = DataIcon;
            await context.FileExtensions.AddAsync(extension);
            await context.SaveChangesAsync();
            return View();
        }
        public IActionResult AddIcon()
        {
            return View();
        }
    }
}
