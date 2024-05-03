
using CUSTOMSOFT.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.CORE.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateJwt(ApplicationDTO application);
        string GenerateShortToken(string PrivateKey, string clientSecretKey);
    
    }
}
