using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface ISpot
    {
        Task OnlySpot();

        Task SetName(string name);
    }
}