using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FolderViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly string Folder = "D:\\Labs\\WebApiForLabs\\FolderViewer\\FolderToView";

        [HttpGet("get-directory")]
        public async Task<IActionResult> GetDirectory()
        {
            DirectoryTree tree = new DirectoryTree(Folder);

            return Ok(tree);
        }

        [HttpDelete("{path}")]
        public async Task<IActionResult> Delete(string path)
        {
            if (path.Contains(Folder) == false)
                throw new InvalidOperationException("Нельзя удалить путь вне " + Folder);

            if (Directory.Exists(path))
                Directory.Delete(path, true);
            else if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            DirectoryTree tree = new DirectoryTree(Folder);
            return Ok(tree);
        }

        [HttpPut("change-name/{oldFullName}/{newName}")]
        public async Task<IActionResult> ChangeName(string oldFullName, string newName)
        {
            if (oldFullName.Contains(Folder) == false)
                throw new InvalidOperationException("Нельзя изменить путь вне " + Folder);

            oldFullName = oldFullName.TrimEnd(' ', '\\');
            string newFullName = oldFullName.Substring(0, oldFullName.LastIndexOf("\\") + 1) + newName;

            if (Directory.Exists(oldFullName))
                Directory.Move(oldFullName, newFullName);
            else if (System.IO.File.Exists(oldFullName))
                System.IO.File.Move(oldFullName, newFullName);

            DirectoryTree tree = new DirectoryTree(Folder);
            return Ok(tree);
        }

        [HttpPost("create/{objectFlag}/{fullName}")]
        public async Task<IActionResult> Create(bool objectFlag, string fullName)
        {
            if (fullName.Contains(Folder) == false)
                throw new InvalidOperationException("Нельзя создать путь вне " + Folder);

            if (objectFlag)
                Directory.CreateDirectory(fullName);
            else
                System.IO.File.Create(fullName);

            DirectoryTree tree = new DirectoryTree(Folder);
            return Ok(tree);
        }
    }
}
