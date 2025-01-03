using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MT.Utilities.UploadFile
{
    public interface IMyFile
    {
        string FileName { get; }
        long ContentLength { get; }
        string ContentType { get; }
    }
    public static class IMyFileExtensions
    {
        public static bool HasFile(this IMyFile file)
        {
            return !(
                file == null ||
                file.ContentLength == 0 ||
                string.IsNullOrEmpty(file.FileName)
            );
        }
    }
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
    }

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
    }
}
