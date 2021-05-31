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
        FileExtensionsModel extensionsModel = new FileExtensionsModel();
        public DirectoryExplorer(string name, int ID, int? parentID, List<FileExtensionsModel> FileExtensList, bool HasChildren = false) : base(name, ID, parentID, FileExtensList, HasChildren)
        {
            this.HasChildren = HasChildren;
            this.HtmlAttributes = new Dictionary<string, string> { ["id"] = "folder" };
        }
        public override void Add(Component component)
        {
            this.HasChildren = true;
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
        public override void SetComponentExtension()
        {
            if (this.FileExtensList.Count==0)
                return;
            //Цифры 2 и 3 это ИД в БД
            //Переделать было б не плохо так как стороннему наблюдателю непонятен их смысл, хотяб бы enum
            if (components.Count()>0)
            {
                extensionsModel = this.FileExtensList.Where(fe => fe.ID == 2).ToList()[0];
            }
            else
            {
                extensionsModel = this.FileExtensList.Where(fe => fe.ID == 1).ToList()[0];
            }

            this.ImageUrl = "data:image/vnd.microsoft.icon;base64," + Convert.ToBase64String(extensionsModel.Icon);
        }

    }
}
