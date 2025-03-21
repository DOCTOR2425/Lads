using System.IO;

namespace FolderViewer
{
    public class DirectoryTree
    {
        public string Name { get; set; }
        public List<DirectoryTree> Children { get; set; } = new List<DirectoryTree>();

        public DirectoryTree() { }

        public DirectoryTree(string name)
        {
            Name = name;

            BuildTree(this, name);
        }

        private void BuildTree(DirectoryTree node, string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                var childNode = new DirectoryTree() { Name = Path.GetFileName(directory) };
                node.Children.Add(childNode);
                BuildTree(childNode, directory);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                node.Children.Add(new DirectoryTree() { Name = Path.GetFileName(file) });
            }
        }
    }
}
