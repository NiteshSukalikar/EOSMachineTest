using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay
{
    public interface IJwtAuthenticationManager
    {
       string Autheticate(string username);
    }
}
