namespace BankBranches.Services.Interface
{
    public interface IFileService
    {
        bool SaveFile(string civilId, byte[] file); //function

        byte[] LoadFile(string civilId);

    }

    public class FileSystemFileService : IFileService
    {
        public byte[] LoadFile(string civilId)
        {
            throw new NotImplementedException();
        }

        public bool SaveFile(string civilId, byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
