using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MT.Utilities.UploadFile.Interfaces
{
	public interface IMyFile
	{
		string FileName { get; }
		long ContentLength { get; }
		string ContentType { get; }
		void SaveFile(string path);
	}
}
