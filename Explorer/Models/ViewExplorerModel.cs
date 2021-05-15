using Explorer.Services.Linker;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;

namespace Explorer.Models
{
    //Можно было создать сервис но пока создал так дабы не усложнять
    public class ViewExplorerModel : IViewExplorerModel
    {
        public static List<Component> AllComponents = new List<Component>();
        public static List<Component> GroupedComponents = new List<Component>();

        public ViewExplorerModel()
        {
        }
        public void AddGroupedComponents()
        {
            foreach (var folder in AllComponents)
            {

                if (!(folder is DirectoryExplorer)&& folder.parentID==null)
                {
                    //Вывести в отдельный метод или в метод расширения 
                    var coincidenceList = GroupedComponents.Where(c => c.Id == folder.Id && c.GetType() == folder.GetType());
                    if (coincidenceList.Count() > 0)
                        continue;
                    GroupedComponents.Add(folder);
                    continue;
                }
                var childs = AllComponents.Where(child => child.parentID == int.Parse(folder.Id));
                foreach(var child in childs)
                {
                    DirectoryExplorer fold = (DirectoryExplorer)folder;
                    var coincidenceList2 = fold.GiveComponentsFromDir().Where(c => c.Id == folder.Id && c.GetType() == folder.GetType());
                    if (coincidenceList2.Count() > 0)
                        continue;
                    folder.Add(child);
                    folder.SetIsHaveChild(true);
                }
                var coincidenceList3 = GroupedComponents.Where(c => c.Id == folder.Id && c.GetType() == folder.GetType());
                if (coincidenceList3.Count() > 0)
                    continue;
                GroupedComponents.Add(folder);
            }
            for (int i = 0; i < GroupedComponents.Count; i++)
            {
                var g = GroupedComponents[i];
                if (g.parentID != null)
                {
                    GroupedComponents.RemoveAt(i);
                    --i;
                }
            }
        }
        public void AddAllComponentsFromDB(ref ExplorerContext context)
        {
            if (AllComponents.Count==0)
            {
                foreach (var folder in context.Folders)
                    AllComponents.Add(new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID));
                foreach (var file in context.Files)
                    AllComponents.Add(new FileExplorer(file.Name, file.ID, file.FoldersModelID));
            }
            else
            {
                foreach (var folder in context.Folders)
                {
                    DirectoryExplorer de = new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID);
                    var coincidenceList = AllComponents.Where(c => c.Id == de.Id);
                    if (coincidenceList.Count()>0)
                        continue;
                    else
                        AllComponents.Add(de);
                }
                foreach (var file in context.Files)
                {
                    FileExplorer fe = new FileExplorer(file.Name, file.ID, file.FoldersModelID);
                    var coincidenceList = AllComponents.Where(c => c.Id == fe.Id);
                    if (coincidenceList.Count() > 0)
                        continue;
                    else
                        AllComponents.Add(fe);
                }
            }
        }

  
    }
}
