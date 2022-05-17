using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace APIluminacao.Extensions
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureListen(this KestrelServerOptions options)
        {
            options.ListenAnyIP(5000);
        }
    }
}
