using Logic.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}
