using Microsoft.AspNetCore.StaticFiles;
using System.ComponentModel.DataAnnotations;

namespace WebApiForLabs.Stuff
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;
		private readonly FileExtensionContentTypeProvider _contentTypeProvider;

		public AllowedExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
			_contentTypeProvider = new FileExtensionContentTypeProvider();
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

				if (!_extensions.Contains(extension) || file.Length == 0)
				{
					return new ValidationResult($"Разрешены только не пустые файлы с расширениями: " +
						$"{string.Join(", ", _extensions)}.");
				}

				if (!_contentTypeProvider.TryGetContentType(file.FileName, out string? mimeType))
				{
					return new ValidationResult($"Не удалось определить MIME-тип файла.");
				}

				if (!IsMimeTypeValidForExtension(mimeType, extension))
				{
					return new ValidationResult($"Файл имеет недопустимый MIME-тип для расширения {extension}.");
				}
				return ValidationResult.Success;
			}
			return new ValidationResult($"Разрешены только не пустые файлы с расширениями: " +
				$"{string.Join(", ", _extensions)}.");
		}

		private bool IsMimeTypeValidForExtension(string mimeType, string extension)
		{
			return _contentTypeProvider.Mappings.ContainsKey(extension) &&
				   _contentTypeProvider.Mappings[extension] == mimeType;
		}
	}
}
