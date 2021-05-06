namespace Explorer.Models
{
    public class ViewExplorerModel
    {
        public ViewExplorerModel(FoldersModel folder, FileExtensionsModel fileExtensions, FilesModel file)
        {
            this.folder = folder;
            this.fileExtensions = fileExtensions;
            this.file = file;
        }

        FoldersModel folder { get; set; }
        FileExtensionsModel fileExtensions { get; set; }
        FilesModel file { get; set; }
    }
}