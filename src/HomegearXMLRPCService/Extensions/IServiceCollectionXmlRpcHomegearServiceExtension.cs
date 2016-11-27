using Microsoft.Extensions.DependencyInjection;
using HomegearLib;
using HomegearLib.RPC;
using Microsoft.Extensions.Configuration;

namespace HomegearXMLRPCService.Extensions
{
    public static class IServiceCollectionXmlRpcHomegearServiceExtension
    {
        public static IServiceCollection AddXMLRPCHomegear(this IServiceCollection services, IConfigurationSection connectionConfig)
        {
            RPCController homegearController = new RPCController(
                connectionConfig.GetValue<string>("IP"),
                connectionConfig.GetValue<int>("Port"),
                connectionConfig.GetValue<string>("EventListenerHostname"),
                connectionConfig.GetValue<string>("EventListenerIP"),
                connectionConfig.GetValue<int>("EventListenerPort"));

            Homegear homegear = new Homegear(homegearController, true);

            services.AddSingleton<RPCController>(homegearController);
            services.AddSingleton<Homegear>(homegear);

            return services;
        }
    }
}