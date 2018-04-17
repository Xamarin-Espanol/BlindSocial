using System.IO;
using System.Threading.Tasks;

namespace BlindSocial
{
    public interface IBlobStorage
    {
        Task<string> PerformBlobOperation(Stream stream);
    }
}
