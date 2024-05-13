using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IAuthTokenService
{
    // this is used for secure api

    // I've tried to stay away from using async methods
    // in order to keep things as simple as possible
    // but in this case we have to make calls to a bunch
    // of async methods that don't have synchronous equivalents
    // so it is much easier to just make this method async as well
    Task<string?> GetTokenAsync(string username, string password);
}
