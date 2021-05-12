using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public class DirectoryExplorer : Component
    {
        public List<Component> components = new List<Component>();
        public DirectoryExplorer(string name, int ID, int? parentID) : base(name, ID, parentID)
        {

        }
        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override void Rename(string newName)
        {
            this.name = newName;
        }

        public override TreeViewItemFactory View(TreeViewItemFactory item)
        {
            item.Add().Text(this.name).SpriteCssClasses("folder");
            return item;
        }
    }
}
