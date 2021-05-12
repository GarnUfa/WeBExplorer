﻿using Explorer.Services.Linker;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;

namespace Explorer.Models
{
    //Можно было создать сервис но пока создал так дабы не усложнять
    public class ViewExplorerModel
    {
        public static Action<TreeViewItemFactory> action12;
        public static List<Component> AllComponents = new List<Component>();
        public static List<Component> GroupedComponents = new List<Component>();
        ExplorerContext context { get; set; }
        public ViewExplorerModel( ref ExplorerContext context)
        {
            this.context = context;
            AddAllComponentsFromDB(ref context);
            AddGroupedComponents();
        }
        //private Component Grouper(Component item)
        //{

        //}
        private void AddGroupedComponents()
        {
            foreach (var folder in AllComponents)
            {
                if (!(folder is DirectoryExplorer)&& folder.parentID==null)
                {
                    GroupedComponents.Add(folder);
                    continue;
                }
                var child = AllComponents.Where(child => child.parentID == folder.ID);
                foreach(var cmp in child)
                {
                    folder.Add(cmp);
                }
                GroupedComponents.Add(folder);
            }
            for(int i=0; i< GroupedComponents.Count; i++)
            {
                var g = GroupedComponents[i];
                if (g.parentID != null)
                {
                    GroupedComponents.RemoveAt(i);
                    --i;
                }
            }
        }
        private void AddAllComponentsFromDB(ref ExplorerContext context)
        {
            foreach (var folder in context.Folders)
                AllComponents.Add(new DirectoryExplorer(folder.Name, folder.ID, folder.ParentID));
            foreach(var file in context.Files)
                AllComponents.Add(new FileExplorer(file.Name, file.ID, file.FoldersModelID));
        }
        public IEnumerable<TreeViewItemFactory> TreeViewRoot (TreeViewItemFactory treeViewItemFactory)
        {
            yield return treeViewItemFactory;
        }
  
    }
}
