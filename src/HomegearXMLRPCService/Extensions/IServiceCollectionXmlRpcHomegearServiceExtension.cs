using Microsoft.Extensions.DependencyInjection;
using HomegearLib;
using HomegearLib.RPC;
using Microsoft.Extensions.Configuration;

namespace HomegearXMLRPCService.Extensions
{
    /// <summary>
    /// Creates extension methods for the <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionXmlRpcHomegearServiceExtension
    {
        /// <summary>
        /// Used at application startup in order to add the <see cref="Homegear"/> and <see cref="RPCController"/> services.
        /// These services will establish a connection to the Homegear server and will create a bidirectional communication channel to 
        /// and from Homegear server that will be used in the other <see cref="HomegearXMLRPCService"/> services.
        /// </summary>
        /// <param name="services">The current services collection</param>
        /// <param name="connectionConfig">Provides the information to connect to the Homegear server</param>
        /// <returns></returns>
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