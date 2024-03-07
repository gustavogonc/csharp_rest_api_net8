using RestWithASP_NET8Udemy.Data.VO;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
