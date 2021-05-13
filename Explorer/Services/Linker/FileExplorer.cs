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
        public FileExplorer(string name, int ID, int? parentID) : base(name, ID, parentID)
        {

        }


        public override void Rename(string newName)
        {
            this.name = newName;
        }

        public override TreeViewItemModel View()
        {
            return this.viewItem;
        }
    }
}

