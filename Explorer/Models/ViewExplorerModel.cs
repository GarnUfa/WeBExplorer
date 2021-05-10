using Explorer.Services.Linker;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Models
{
    public class ViewExplorerModel
    {
        public List<Component> components = new List<Component>();
        ExplorerContext context { get; set; }
        public ViewExplorerModel(ExplorerContext context)
        {
            this.context = context;
        }

        private void qwer(ExplorerContext ec)
        {
            foreach (var folder in ec.Folders)
            {
                DirectoryExplorer component = new DirectoryExplorer(folder.Name, folder.ID);
                components.Add(component);
            }
            foreach (var file in ec.Files)
            {
                int fileID = file.ID;
                string fileName = file.Name;
                int? parentID = file.FoldersModelID;
                if (parentID != null)
                {
                    var fold = components.Where(t => t.ID == parentID && t is DirectoryExplorer).ToList()[0];
                    fold.Add(new FileExplorer(fileName, fileID));
                }
                else
                {
                    components.Add(new FileExplorer(fileName, fileID));
                }
            }
        }
    }
}
