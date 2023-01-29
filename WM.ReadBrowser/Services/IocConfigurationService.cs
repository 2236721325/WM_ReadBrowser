using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.ReadBrowser.Services
{
    public class IocConfigurationService : IMauiInitializeService
    {
        /// <summary>
        /// Initialize services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void Initialize(IServiceProvider services)
        {
            Ioc.Default.ConfigureServices(services);
        }
    }
}
