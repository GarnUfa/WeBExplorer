using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI.Fluent;
using Kendo.Mvc.UI;

namespace Explorer.Services.Linker
{
    public abstract class Component 
    {
        public string name;
        public int ID;
        public int? parentID;
        public bool HasChildren;
        public TreeViewItemModel viewItem;
        public abstract TreeViewItemModel View();

        public Component(string name, int ID, int? parentID, bool HasChildren = false)
        {
            viewItem = new TreeViewItemModel()
            {
                Id = ID.ToString(),
                Text = name,
                HasChildren = HasChildren,
            };
            this.name = name;
            this.ID = ID;
            this.parentID = parentID;
            this.HasChildren = HasChildren;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
        public virtual void SetIsHaveChild(bool isHave) { }
    }
}
