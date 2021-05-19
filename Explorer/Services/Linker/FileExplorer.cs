using Explorer.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public class FileExplorer : Component
    {
        FileExtensionsModel extensionsModel = new FileExtensionsModel();
        int FileExtensionID;
        public FileExplorer(string name, int ID, int? parentID, List<FileExtensionsModel> FileExtensList, int FileExtensionID) : base(name, ID, parentID, FileExtensList)
        {
            this.FileExtensionID = FileExtensionID;
            this.HtmlAttributes = new Dictionary<string, string> { ["id"] = "file" };
        }


        public override void Rename(string newName)
        {
            this.Text = newName;
        }
        public override void SetComponentExtension()
        {
            extensionsModel = this.FileExtensList.Where(ext => ext.ID== FileExtensionID).ToList()[0];
            this.ImageUrl = "data:image/vnd.microsoft.icon;base64," + Convert.ToBase64String(extensionsModel.Icon);
        }

    }
}

