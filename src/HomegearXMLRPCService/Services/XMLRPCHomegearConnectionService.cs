using System;
using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using HomegearLib.RPC;
using HomegearLib;
using System.Security.Authentication;
using HomegearXMLRPCService.CallbackHandlers;
using Microsoft.Extensions.Logging;

namespace HomegearXMLRPCService.Services
{
    /// <summary>
    /// Connects the middleware to the Homegear Server via the RPC library HomegearLib
    /// It creates a callback server so that Homegear Server can send events to the middleware
    /// about actions that the middleware needs to take.
    /// </summary>
    public class XMLRPCHomegearConnectionService : IHomegearConnectionService
    {
        private Homegear _homegear;
        private RPCController _homegearController;

        private EventHandlerFactory _eventLoggerFactory;
        private readonly ILogger<XMLRPCHomegearConnectionService> _logger;

        public XMLRPCHomegearConnectionService(
            Homegear homegear, 
            RPCController rpcController, 
            ILightSwitchesPersistenceService lightSwitchesPersistence, 
            IDoorWindowSensorPersistenceService doorWindowSensorActivityPersistence,
            IExternalWallSocketsPersistenceService externalWallSocketPersistenceService,
            ILogger<XMLRPCHomegearConnectionService> logger,
            IDevicesService<LightSwitchModel> lightSwitchesService)
        {
            _homegearController = rpcController;
            _homegear = homegear;
            _logger = logger;

            _homegearController.ServerConnected += rpc_serverConnected;

            //this.homegear.Reloaded += homegear_OnReloaded;
            //this.homegear.ConnectError += homegear_OnConnectError;
            _homegear.ReloadRequired += homegear_OnReloadRequired;
            _homegear.DeviceReloadRequired += homegear_OnDeviceReloadRequired;
            _homegear.DeviceVariableUpdated += homegear_OnDeviceVariableUpdated;

            _eventLoggerFactory = new EventHandlerFactory();

            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.LightSwitch, LightSwitchVariables.STATE, new LightSwitchEventHandler(lightSwitchesPersistence));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.DoorWindowMagneticSensor, DoorWindowSensorVariables.STATE, new DoorWindowSensorStateEventHandler(doorWindowSensorActivityPersistence, lightSwitchesService));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.DoorWindowMagneticSensor, DoorWindowSensorVariables.LOWBAT, new DoorWindowSensorLowBatteryEventHandler(doorWindowSensorActivityPersistence));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.ExternalWallSocket, ExternalWallSocketVariables.CURRENT, new ExternalWallSocketHandler(externalWallSocketPersistenceService));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.ExternalWallSocket, ExternalWallSocketVariables.VOLTAGE, new ExternalWallSocketHandler(externalWallSocketPersistenceService));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.ExternalWallSocket, ExternalWallSocketVariables.FREQUENCY, new ExternalWallSocketHandler(externalWallSocketPersistenceService));
            _eventLoggerFactory.RegisterEventLogger(HomegearDeviceTypes.ExternalWallSocket, ExternalWallSocketVariables.ENERGY_COUNTER, new ExternalWallSocketHandler(externalWallSocketPersistenceService));

        }

        /// <summary>
        /// The current status of the connection to Homegear
        /// </summary>
        /// <returns>The <see cref="Abstractions.Models.HomegearStatusModel"/> that contains the current status of the connection</returns>
        public HomegearStatusModel GetStatus()
        {
            return new HomegearStatusModel
            {
                IsConnected = _homegearController.IsConnected
            };
        }

        void rpc_serverConnected(RPCServer sender, CipherAlgorithmType cipherAlgorithm, Int32 cipherStrength)
        {
            ReadOnlyDictionary<int, UpdateResult> update = _homegear.GetUpdateStatus().Results;
            _logger.LogInformation("Incomming connection from Homegear. Cipher algorithm: {0}, cipher strength: {1}.", cipherAlgorithm.ToString(), cipherStrength.ToString());
        }

        void homegear_OnDeviceVariableUpdated(Homegear sender, Device device, Channel channel, Variable variable)
        {
            try
            {
                _logger.LogDebug("Update event received for variable {0} for device id {1} with value {2}.", variable.Name, device.TypeID, variable.BooleanValue);

                dynamic variableValue = "";
                if (variable.StringValue != null && variable.StringValue != "")
                {
                    variableValue = variable.StringValue;
                }
                else if (variable.DoubleValue != 0)
                {
                    variableValue = variable.DoubleValue;
                }
                else if (variable.IntegerValue != 0)
                {
                    variableValue = variable.IntegerValue;
                }

                _eventLoggerFactory.GetEventLoggerFor((HomegearDeviceTypes)device.TypeID, variable.Name).LogEvent(device.ID, variable.Name, variableValue);
            }
            catch (KeyNotFoundException exception)
            {
                _logger.LogDebug("No handler found for variable {0} with id {1}. Exception message: {1}", variable.Name, device.TypeID, exception.Message);
            }
        }

        void homegear_OnDeviceReloadRequired(Homegear sender, Device device, Channel channel, DeviceReloadType reloadType)
        {
            if (reloadType == DeviceReloadType.Full)
            {
                //WriteLog("Reloading device " + device.ID.ToString() + ".");
                //Finish all operations on the device and then call:
                device.Reload();
            }
            else if (reloadType == DeviceReloadType.Metadata)
            {
                //WriteLog("Reloading metadata of device " + device.ID.ToString() + ".");
                //Finish all operations on the device's metadata and then call:
                device.Metadata.Reload();
            }
            else if (reloadType == DeviceReloadType.Channel)
            {
                //WriteLog("Reloading channel " + channel.Index + " of device " + device.ID.ToString() + ".");
                //Finish all operations on the device's channel and then call:
                channel.Reload();
            }
            else if (reloadType == DeviceReloadType.Variables)
            {
                //WriteLog("Device variables were updated: Device type: \"" + device.TypeString + "\", ID: " + device.ID.ToString() + ", Channel: " + channel.Index.ToString());
                //WriteLog("Reloading variables of channel " + channel.Index + " and device " + device.ID.ToString() + ".");
                //Finish all operations on the channels's variables and then call:
                channel.Variables.Reload();
            }
            else if (reloadType == DeviceReloadType.Links)
            {
                //WriteLog("Device links were updated: Device type: \"" + device.TypeString + "\", ID: " + device.ID.ToString() + ", Channel: " + channel.Index.ToString());
                //WriteLog("Reloading links of channel " + channel.Index + " and device " + device.ID.ToString() + ".");
                //Finish all operations on the channels's links and then call:
                channel.Links.Reload();
            }
            else if (reloadType == DeviceReloadType.Team)
            {
                //WriteLog("Device team was updated: Device type: \"" + device.TypeString + "\", ID: " + device.ID.ToString() + ", Channel: " + channel.Index.ToString());
                //WriteLog("Reloading channel " + channel.Index + " of device " + device.ID.ToString() + ".");
                //Finish all operations on the device's channel and then call:
                channel.Reload();
            }
            else if (reloadType == DeviceReloadType.Events)
            {
                //WriteLog("Device events were updated: Device type: \"" + device.TypeString + "\", ID: " + device.ID.ToString() + ", Channel: " + channel.Index.ToString());
                //WriteLog("Reloading events of device " + device.ID.ToString() + ".");
                //Finish all operations on the device's events and then call:
                device.Events.Reload();
            }
        }

        void homegear_OnReloadRequired(Homegear sender, ReloadType reloadType)
        {
            if (reloadType == ReloadType.Full)
            {
                //WriteLog("Received reload required event. Reloading.");
                //Finish all operations on the Homegear object and then call:
                _homegear.Reload();
            }
            else if (reloadType == ReloadType.SystemVariables)
            {
                //WriteLog("Reloading system variables.");
                //Finish all operations on the system variables and then call:
                _homegear.SystemVariables.Reload();
            }
            else if (reloadType == ReloadType.Events)
            {
                //WriteLog("Reloading timed events.");
                //Finish all operations on the timed events and then call:
                _homegear.TimedEvents.Reload();
            }
        }


        //void homegear_OnConnectError(Homegear sender, string message, string stackTrace)
        //{
        //    Console.Write("Error connecting to Homegear: " + message + "\r\nStacktrace: " + stackTrace);
        //}

        //void homegear_OnReloaded(Homegear sender)
        //{
        //    Console.Write("Reload complete. Received " + sender.Devices.Count + " devices.");


        //    //Now we can start working with the Homegear object
        //}
    }
}
