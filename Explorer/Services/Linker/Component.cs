using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI.Fluent;

namespace Explorer.Services.Linker
{
    public abstract class Component
    {
        public string name;
        public int ID;
        public int? parentID;
        public abstract TreeViewItemFactory View(TreeViewItemFactory item);

        public Component(string name, int ID, int? parentID)
        {
            this.name = name;
            this.ID = ID;
            this.parentID = parentID;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
    }
}
