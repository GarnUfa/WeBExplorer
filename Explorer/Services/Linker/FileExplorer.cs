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
        public FileExplorer(string name, int ID, int? parentID, FileExtensionsModel extensionsModel) : base(name, ID, parentID, extensionsModel)
        {
            this.SpriteCssClass = "file";
        }


        public override void Rename(string newName)
        {
            this.Text = newName;
        }

    }
}

