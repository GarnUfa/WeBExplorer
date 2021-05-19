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
        FileExtensionsModel extensionsModel;
        public Component(string name, int ID, int? parentID, FileExtensionsModel extensionsModel, bool HasChildren = false)
        {
            this.parentID = parentID;
            this.Id = ID.ToString();
            this.Text = name;
            this.HasChildren = HasChildren;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
        public virtual void SetIsHaveChild(bool isHave) { }
    }
}
