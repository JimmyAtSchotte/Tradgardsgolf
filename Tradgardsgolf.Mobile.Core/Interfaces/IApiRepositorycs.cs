using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tradgardsgolf.Mobile.Core.Interfaces
{
    public interface IApiRepository
    {
        void CreateApiClient(string baseUrl);
        Task<bool> Authenticate();
        Task<bool> IsAuthenticated();

        Task<IEnumerable<string>> GetCourses();

    }
}
