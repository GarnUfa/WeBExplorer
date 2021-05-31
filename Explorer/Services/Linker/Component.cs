using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI.Fluent;
using Kendo.Mvc.UI;
using Explorer.Models;

namespace Explorer.Services.Linker
{
    public abstract class Component : TreeViewItemModel
    {
        public int? parentID;
        public List<FileExtensionsModel> FileExtensList;
        public Component(string name, int ID, int? parentID, List<FileExtensionsModel> FileExtensList, bool HasChildren = false)
        {
            this.parentID = parentID;
            this.Id = ID.ToString();
            this.Text = name;
            this.HasChildren = HasChildren;
            this.FileExtensList = FileExtensList;
        }

        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Rename(string newName) { }
        public virtual void SetIsHaveChild(bool isHave) { }
        public virtual void SetComponentExtension() { }

        public override bool Equals(object obj)
        {
            return obj is Component component &&
                   Enabled == component.Enabled &&
                   Expanded == component.Expanded &&
                   Encoded == component.Encoded &&
                   Selected == component.Selected &&
                   Text == component.Text &&
                   SpriteCssClass == component.SpriteCssClass &&
                   Id == component.Id &&
                   Url == component.Url &&
                   ImageUrl == component.ImageUrl &&
                   HasChildren == component.HasChildren &&
                   Checked == component.Checked &&
                   EqualityComparer<List<TreeViewItemModel>>.Default.Equals(Items, component.Items) &&
                   EqualityComparer<IDictionary<string, string>>.Default.Equals(HtmlAttributes, component.HtmlAttributes) &&
                   EqualityComparer<IDictionary<string, string>>.Default.Equals(ImageHtmlAttributes, component.ImageHtmlAttributes) &&
                   EqualityComparer<IDictionary<string, string>>.Default.Equals(LinkHtmlAttributes, component.LinkHtmlAttributes) &&
                   parentID == component.parentID &&
                   EqualityComparer<List<FileExtensionsModel>>.Default.Equals(FileExtensList, component.FileExtensList);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Enabled);
            hash.Add(Expanded);
            hash.Add(Encoded);
            hash.Add(Selected);
            hash.Add(Text);
            hash.Add(SpriteCssClass);
            hash.Add(Id);
            hash.Add(Url);
            hash.Add(ImageUrl);
            hash.Add(HasChildren);
            hash.Add(Checked);
            hash.Add(Items);
            hash.Add(HtmlAttributes);
            hash.Add(ImageHtmlAttributes);
            hash.Add(LinkHtmlAttributes);
            hash.Add(parentID);
            hash.Add(FileExtensList);
            return hash.ToHashCode();
        }
    }
}
