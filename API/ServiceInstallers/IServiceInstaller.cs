using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.ServiceInstallers
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection installer, IConfiguration configuration);
    }
}
