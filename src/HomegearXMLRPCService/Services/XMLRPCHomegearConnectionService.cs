﻿using System;
using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using HomegearLib.RPC;
using HomegearLib;
using System.Security.Authentication;

namespace HomegearXMLRPCService.Services
{
    /// <summary>
    /// Connects the middleware to the Homegear Server via the RPC library HomegearLib
    /// It creates a callback server so that Homegear Server can send events to the middleware
    /// about actions that the middleware needs to take.
    /// </summary>
    public class XMLRPCHomegearConnectionService : IHomegearConnectionService
    {
        private Homegear homegear;
        private RPCController homegearController;

        public XMLRPCHomegearConnectionService(Homegear homegear, RPCController rpcController)
        {
            this.homegearController = rpcController;
            this.homegearController.ServerConnected += rpc_serverConnected;
            this.homegear = homegear;

            //this.homegear.Reloaded += homegear_OnReloaded;
            //this.homegear.ConnectError += homegear_OnConnectError;
            this.homegear.ReloadRequired += homegear_OnReloadRequired;
            this.homegear.DeviceReloadRequired += homegear_OnDeviceReloadRequired;
            this.homegear.DeviceVariableUpdated += homegear_OnDeviceVariableUpdated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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