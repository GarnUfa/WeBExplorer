using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI.Fluent;
using Kendo.Mvc.UI;
using Explorer.Models;

namespace Explorer.Services.Linker
{
    public abstract class Component : TreeViewItemModel
    {
        public int? parentID;
        public List<FileExtensionsModel> FileExtensList;
        public Component(string name, int ID, int? parentID, List<FileExtensionsModel> FileExtensList, bool HasChildren = false)
        {
            this.parentID = parentID;
            this.Id = ID.ToString();
            this.Text = name;
            this.HasChildren = HasChildren;
            this.FileExtensList = FileExtensList;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
        public virtual void SetIsHaveChild(bool isHave) { }
        public virtual void SetComponentExtension() { }
    }
}
