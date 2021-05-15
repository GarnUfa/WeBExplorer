using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public interface IViewExplorerModel
    {
        public void AddGroupedComponents();
        public void AddAllComponentsFromDB(ref ExplorerContext context);

    }
}
