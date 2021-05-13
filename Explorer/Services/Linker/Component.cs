using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI.Fluent;
using Kendo.Mvc.UI;

namespace Explorer.Services.Linker
{
    public abstract class Component : TreeViewItemModel
    {
        public string name;
        public int ID;
        public int? parentID;
        public bool isHaveChild;
        public TreeViewItemModel viewItem;
        public abstract TreeViewItemModel View();

        public Component(string name, int ID, int? parentID, bool isHaveChild = false)
        {
            this.name = name;
            this.ID = ID;
            this.parentID = parentID;
            this.isHaveChild = isHaveChild;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
        public virtual void SetIsHaveChild(bool isHave) { }
    }
}
