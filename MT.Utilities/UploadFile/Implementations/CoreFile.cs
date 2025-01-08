using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MT.Utilities.UploadFile.Interfaces;

namespace MT.Utilities.UploadFile.Implementations
{
	public class CoreFile : IMyFile
	{
		private readonly IFormFile _file;
		public CoreFile(IFormFile file)
		{
			_file = file;
		}

		public string FileName => _file.FileName;

		public long ContentLength => _file.Length;

		public string ContentType => _file.ContentType;

		public void SaveFile(string path)
		{
			using (var stream = new FileStream(path, FileMode.Create))
			{
				_file.CopyTo(stream);
			}
		}
	}
}
