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
        private static List<Component> AllComponents = new List<Component>();
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
                    //можно вынести в отдельный метод вроде GroupedComponentsAdded, что бы вкладывать туда все, что нужно сделать\добавить перед основным add  
                    folder.SetComponentExtension();
                    GroupedComponents.Add(folder);
                    continue;
                }
                if (folder is FileExplorer)
                    continue;
                var childs = AllComponents.Where(child => child.parentID == int.Parse(folder.Id));
                foreach(var child in childs)
                {
                    DirectoryExplorer fold = (DirectoryExplorer)folder;
                    var coincidenceList2 = fold.GiveComponentsFromDir().Where(c => c.Id == folder.Id && c.GetType() == folder.GetType());
                    if (coincidenceList2.Count() > 0)
                        continue;
                    //можно вынести в отдельный метод вроде GroupedComponentsAdded, что бы вкладывать туда все, что нужно сделать\добавить перед основным add  
                    child.SetComponentExtension();
                    folder.Add(child);
                    folder.SetIsHaveChild(true);
                }
                var coincidenceList3 = GroupedComponents.Where(c => c.Id == folder.Id && c.GetType() == folder.GetType());
                if (coincidenceList3.Count() > 0)
                    continue;
                folder.SetComponentExtension();
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
        //Собирает в единый список все компоненты дерева (папки и файлы), не добавляя повторные компоненты
        //Можно переделать через два форича но я пока не стал
        public List<Component> AddAllComponentsFromDB(ExplorerContext context)
        {
            List<FileExtensionsModel> FileExtensList = context.FileExtensions.ToList();
            if (AllComponents.Count==0)
            {
                AllComponents.Add(new DirectoryExplorer("Root", 0, null, FileExtensList));
                foreach (var folder in context.Folders)
                {
                    AllComponents.Add(new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID, FileExtensList));
                }
                foreach (var file in context.Files)
                {
                    AllComponents.Add(new FileExplorer(file.Name, file.ID, file.FoldersModelID, FileExtensList, file.FileExtensionsModelID));
                }
                return AllComponents;
            }
            else
            {
                foreach (var folder in context.Folders)
                {
                    DirectoryExplorer de = new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID, FileExtensList);
                    var coincidenceList = AllComponents.Where(c => c.Id == de.Id);
                    if (coincidenceList.Count()>0)
                        continue;
                    else
                        AllComponents.Add(de);
                }
                foreach (var file in context.Files)
                {
                    FileExplorer fe = new FileExplorer(file.Name, file.ID, file.FoldersModelID, FileExtensList, file.FileExtensionsModelID);
                    var coincidenceList = AllComponents.Where(c => c.Id == fe.Id);
                    if (coincidenceList.Count() > 0)
                        continue;
                    else
                        AllComponents.Add(fe);
                }
                return AllComponents;
            }
        }
    }
}
