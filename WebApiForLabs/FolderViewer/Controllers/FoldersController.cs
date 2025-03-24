﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FolderViewer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoldersController : ControllerBase
	{
		private readonly string Folder = Directory.GetCurrentDirectory() + "\\FolderToView\\";

		[HttpGet("get-directory")]
		public async Task<IActionResult> GetDirectory()
		{
			DirectoryTree tree = new DirectoryTree(Folder);
			return Ok(tree);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(
			[FromQuery] string path)
		{
			string target = Folder + path;
			if (Directory.Exists(target))
				Directory.Delete(target, true);
			else if (System.IO.File.Exists(target))
				System.IO.File.Delete(target);

			DirectoryTree tree = new DirectoryTree(Folder);
			return Ok(tree);
		}

		[HttpPut]
		public async Task<IActionResult> ChangeName(
			[FromQuery] string oldName,
			[FromQuery] string newName)
		{
			oldName = Folder + oldName.TrimEnd(' ', '\\');
			string newFullName = oldName.Substring(0, oldName.LastIndexOf("\\") + 1) + newName;

			if (Directory.Exists(oldName) &&
				Directory.Exists(newFullName) == false)
				Directory.Move(oldName, newFullName);
			else if (System.IO.File.Exists(oldName) &&
				System.IO.File.Exists(newFullName) == false)
				System.IO.File.Move(oldName, newFullName);

			DirectoryTree tree = new DirectoryTree(Folder);
			return Ok(tree);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create(
			[FromQuery] bool isFolder,
			[FromQuery] string name)
		{
			if (Directory.Exists(Folder + name.Substring(0, name.LastIndexOf("\\"))) == false)
				throw new ArgumentException("Нельзя создать объект не в папке\n" + name);

			if (isFolder && Directory.Exists(name) == false)
				Directory.CreateDirectory(Folder + name);
			else if (System.IO.File.Exists(name) == false)
				System.IO.File.Create(Folder + name).Close();

			DirectoryTree tree = new DirectoryTree(Folder);
			return Ok(tree);
		}
	}
}
