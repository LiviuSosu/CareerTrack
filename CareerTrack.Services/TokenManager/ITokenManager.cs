using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public interface ITokenManager
    {
        Task DeactivateCurrentAsync();

        Task DeactivateAsync(string token);

        Task<bool> IsCurrentActiveToken();


    }
}
