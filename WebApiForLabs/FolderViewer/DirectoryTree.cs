namespace FolderViewer
{
    public class DirectoryTree
    {
        public string Name { get; set; }
        public List<DirectoryTree>? Children { get; set; }

        public DirectoryTree() { }

        public DirectoryTree(string name)
        {
            Name = name;
            Children = new List<DirectoryTree>();

            BuildTree(this, name);
        }

        private void BuildTree(DirectoryTree node, string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                DirectoryTree childNode = new DirectoryTree()
                {
                    Name = Path.GetFileName(directory),
                    Children = new List<DirectoryTree>()
                };
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
