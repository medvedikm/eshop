﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class FileUpload
    {
        string RootPath;
        string ContentType;
        string FolderName;

        public FileUpload(string rootPath, string contentType, string folderName)
        {
            this.RootPath = rootPath;
            this.ContentType = contentType;
            this.FolderName = folderName;
        }

        public bool CheckFileContent(IFormFile iFormFile)
        {
            return iFormFile != null && iFormFile.ContentType.ToLower().Contains(ContentType);
        }

        public bool CheckFileLength(IFormFile iFormFile)
        {
            return iFormFile.Length > 0 && iFormFile.Length < 2_000_000;
        }
        public async Task <string> FileUploadAsync(IFormFile iFormFile)
        {
            string filePathOutput = String.Empty;
            var img = iFormFile;

            if (CheckFileContent(iFormFile) && CheckFileLength(iFormFile))
            {
                var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                var fileExtension = Path.GetExtension(img.FileName);
                var fileNameGenerated = Path.GetRandomFileName();

                var fileRelative = Path.Combine(ContentType + "s", FolderName, fileName + fileExtension);
                var filePath = Path.Combine(RootPath, fileRelative);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                filePathOutput = $"/{fileRelative}";
            }

            return filePathOutput;
        }
    }
}
