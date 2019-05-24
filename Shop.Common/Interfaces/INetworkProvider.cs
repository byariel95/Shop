using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Interfaces
{
    public interface INetworkProvider
    {
        bool IsConnectedToWifi();
    }

}
