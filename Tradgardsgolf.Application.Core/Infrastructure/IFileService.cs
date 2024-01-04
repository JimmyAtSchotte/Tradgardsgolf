using System.Threading.Tasks;

namespace Tradgardsgolf.Core.Infrastructure;

public interface IFileService
{
    Task<byte[]> Get(string fileName);
    Task Save(string filename, byte[] bytes);
    
    Task Delete(string filename);
}