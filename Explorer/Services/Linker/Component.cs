using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public abstract class Component
    {
        public string name;
        public int ID;

        public Component(string name, int ID)
        {
            this.name = name;
            this.ID = ID;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
    }
}
