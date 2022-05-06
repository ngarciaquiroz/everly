using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Services
{
    public interface ITinyUrl 
    {
        Task<string?> ShortenUrl(string? url);
    }
}
