using System;
using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using HomegearLib.RPC;
using HomegearLib;
using System.Security.Authentication;
using HomegearXMLRPCService.Options;
using Microsoft.Extensions.Options;


namespace HomegearXMLRPCService.Services
{
    public class XMLRPCHomegearService : IHomegearService
    {

        private Homegear homegear;
        private RPCController homegearController;

        public XMLRPCHomegearService(IOptions<Connection> configConnection)
        {
            Connection connectionSettings = configConnection.Value;
            homegearController = new RPCController(
                connectionSettings.IP,
                connectionSettings.Port,
                connectionSettings.EventListenerHostname,
                connectionSettings.EventListenerIP,
                connectionSettings.EventListenerPort);

            homegearController.ServerConnected += rpc_serverConnected;

            homegear = new Homegear(homegearController, true);
            homegear.Reloaded += homegear_OnReloaded;
            homegear.ConnectError += homegear_OnConnectError;
            homegear.ReloadRequired += homegear_OnReloadRequired;
            homegear.DeviceReloadRequired += homegear_OnDeviceReloadRequired;
            homegear.DeviceVariableUpdated += homegear_OnDeviceVariableUpdated;
        }

        public HomegearStatusModel GetStatus()
        {
            return new HomegearStatusModel
            {
                IsConnected = homegearController.IsConnected
            };
        }

        void rpc_serverConnected(RPCServer sender, CipherAlgorithmType cipherAlgorithm, Int32 cipherStrength)
        {
            ReadOnlyDictionary<int, UpdateResult> update = homegear.GetUpdateStatus().Results;
            if (cipherAlgorithm != CipherAlgorithmType.Null) Console.Write("Incoming connection from Homegear. Cipher Algorithm: " + cipherAlgorithm.ToString() + ", Cipher Strength: " + cipherStrength.ToString());
            else Console.Write("Incoming connection from Homegear.");
        }

        void homegear_OnDeviceVariableUpdated(Homegear sender, Device device, Channel channel, Variable variable)
        {
            Console.Write("Variable updated: Device type: \"" + device.TypeString + "\", ID: " + device.ID.ToString() + ", Channel: " + channel.Index.ToString() + ", Variable Name: \"" + variable.Name + "\", Value: " + variable.ToString());
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
                homegear.Reload();
            }
            else if (reloadType == ReloadType.SystemVariables)
            {
                //WriteLog("Reloading system variables.");
                //Finish all operations on the system variables and then call:
                homegear.SystemVariables.Reload();
            }
            else if (reloadType == ReloadType.Events)
            {
                //WriteLog("Reloading timed events.");
                //Finish all operations on the timed events and then call:
                homegear.TimedEvents.Reload();
            }
        }


        void homegear_OnConnectError(Homegear sender, string message, string stackTrace)
        {
            Console.Write("Error connecting to Homegear: " + message + "\r\nStacktrace: " + stackTrace);
        }

        void homegear_OnReloaded(Homegear sender)
        {
            Console.Write("Reload complete. Received " + sender.Devices.Count + " devices.");


            //Now we can start working with the Homegear object
        }

        public IEnumerable<LightSwitchModel> GetAll()
        {
            List<LightSwitchModel> lightSwitches = new List<LightSwitchModel>();
            foreach (KeyValuePair<int, Device> device in homegear.Devices)
            {
                lightSwitches.Add(new LightSwitchModel
                {
                    Id = device.Key,
                    State = true
                });
            }

            return lightSwitches;

            //List<LightSwitchModel> lightSwitches = new List<LightSwitchModel>();
            //IEnumerable<int> connectedDevicesIds = homematic.getPeerId(3, "105");
            //foreach(int id in connectedDevicesIds)
            //{
            //    bool currentState = homematic.getValue(id, 1, "STATE");
            //    lightSwitches.Add(new LightSwitchModel
            //    {
            //        Id = id,
            //        State = currentState
            //    });

            //}
            //return lightSwitches;
        }

        public LightSwitchModel GetByPeerId(int peerId)
        {
            //bool currentState = homematic.getValue(peerId, 1, "STATE");

            bool currentState = homegear.Devices[peerId].Channels[1].Variables["STATE"].BooleanValue;
            return new LightSwitchModel
            {
                Id = peerId,
                State = currentState
            };
        }

        public void SetStateForPeerId(int peerId, bool state)
        {
            homegear.Devices[peerId].Channels[1].Variables["STATE"].BooleanValue = state;
            //homematic.setValue(peerId, 1, "STATE", state);
        }
    }
}
