using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
    }
}