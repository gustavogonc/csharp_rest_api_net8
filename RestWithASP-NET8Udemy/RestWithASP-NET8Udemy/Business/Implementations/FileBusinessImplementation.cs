﻿using RestWithASP_NET8Udemy.Data.VO;

namespace RestWithASP_NET8Udemy.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UplodDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO FileDetail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);

                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    FileDetail.DocumentName = docName;
                    FileDetail.DocType = fileType;
                    FileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + FileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    
                        await file.CopyToAsync(stream);
                }
            }

            return FileDetail;
        }
        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();

            foreach(IFormFile file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }
            return list;
        }


    }
}
