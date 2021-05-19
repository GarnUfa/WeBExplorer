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
                if (folder is FileExplorer)
                    continue;
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
        //Собирает в единый список все компоненты дерева (папки и файлы), не добавляя повторные компоненты
        //Можно переделать через два форича но я пока не стал
        public void AddAllComponentsFromDB(ref ExplorerContext context)
        {
            FileExtensionsModel extensionsModel = new FileExtensionsModel();
            if (AllComponents.Count==0)
            {
                foreach (var folder in context.Folders)
                {
                    if (folder.ParentID is null)
                    {
                        //Эти цифры тоже не правильно, непонятно откуда что берется, опять же надо выносить в отдельный метод поиск расширения
                        // 2 и 3 это ИД расширения в БД в зависимости от заполнености папки
                        var fileExten = context.FileExtensions.Where(fe => fe.ID == 2).ToList()[0];
                        extensionsModel = fileExten;
                    }
                    else
                    {
                        var fileExten = context.FileExtensions.Where(fe => fe.ID == 3).ToList()[0];
                        extensionsModel = fileExten;
                    }

                    AllComponents.Add(new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID, extensionsModel));
                }
                foreach (var file in context.Files)
                {
                    extensionsModel = context.FileExtensions.Where(fe => fe.ID == file.FileExtensionsModelID).ToList()[0];
                    AllComponents.Add(new FileExplorer(file.Name, file.ID, file.FoldersModelID, extensionsModel));
                }
            }
            else
            {
                foreach (var folder in context.Folders)
                {
                    //Повторяющийся код - надо вынести в отдельный метод
                    if (folder.ParentID is null)
                    {
                        //Эти цифры тоже не правильно, непонятно откуда что берется, опять же надо выносить в отдельный метод поиск расширения
                        // 2 и 3 это ИД расширения в БД в зависимости от заполнености папки
                        var fileExten = context.FileExtensions.Where(fe => fe.ID == 2).ToList()[0];
                        extensionsModel = fileExten;
                    }
                    else
                    {
                        var fileExten = context.FileExtensions.Where(fe => fe.ID == 3).ToList()[0];
                        extensionsModel = fileExten;
                    }
                    DirectoryExplorer de = new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID, extensionsModel);
                    var coincidenceList = AllComponents.Where(c => c.Id == de.Id);
                    if (coincidenceList.Count()>0)
                        continue;
                    else
                        AllComponents.Add(de);
                }
                foreach (var file in context.Files)
                {
                    extensionsModel = context.FileExtensions.Where(fe => fe.ID == file.FileExtensionsModelID).ToList()[0];
                    FileExplorer fe = new FileExplorer(file.Name, file.ID, file.FoldersModelID, extensionsModel);
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
