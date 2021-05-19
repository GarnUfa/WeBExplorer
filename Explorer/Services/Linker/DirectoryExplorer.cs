using Explorer.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public class DirectoryExplorer : Component
    {
        private List<Component> components = new List<Component>();
        public DirectoryExplorer(string name, int ID, int? parentID, FileExtensionsModel extensionsModel, bool HasChildren = false) : base(name, ID, parentID,  extensionsModel, HasChildren)
        {
            this.HasChildren = HasChildren;
            this.SpriteCssClass = "folder-empty";
        }
        public override void Add(Component component)
        {
            this.HasChildren = true;
            this.SpriteCssClass = "folder-with-contents";
            if (!components.Contains(component))
            {
                components.Add(component);
                this.Items.Add(component);
            }
        }
        public List<Component> GiveComponentsFromDir()
        {
            return components;
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override void Rename(string newName)
        {
            this.Text = newName;
        }
        public override void SetIsHaveChild(bool isHave)
        {
            HasChildren = isHave;
        }

    }
}
