using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public class DirectoryExplorer : Component
    {
        private List<Component> components = new List<Component>();
        public DirectoryExplorer(string name, int ID) : base(name, ID)
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
    }
}
