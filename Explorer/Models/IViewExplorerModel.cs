using Explorer.Services.Linker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public interface IViewExplorerModel
    {
        public void AddGroupedComponents();
        public List<Component> AddAllComponentsFromDB(ExplorerContext context);

    }
}
