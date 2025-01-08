using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Utilities.UploadFile.Interfaces;
using System.Web;

namespace MT.Utilities.UploadFile.Implementations
{
	public class MVC5File : IMyFile
	{
		private readonly HttpPostedFileBase _file;

		public MVC5File(HttpPostedFileBase file)
		{
			_file = file;
		}

		public string FileName => _file.FileName;

		public long ContentLength => _file.ContentLength;

		public string ContentType => _file.ContentType;

		public void SaveFile(string path)
		{
			_file.SaveAs(path);
		}
	}
}
