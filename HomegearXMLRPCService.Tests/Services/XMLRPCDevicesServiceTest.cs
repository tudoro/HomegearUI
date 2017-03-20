using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomegearLib;
using HomegearLib.RPC;
using HomegearXMLRPCService.Services;
using System.Collections.Generic;
using Abstractions.Models;

namespace HomegearXMLRPCService.Tests.Services
{
    [TestClass]
    public class XMLRPCDevicesServiceTest
    {
        Homegear _homegear;
        XMLRPCDevicesService _devicesService;

        public XMLRPCDevicesServiceTest()
        {
            RPCController _rpc = new RPCController("localhost", 2001, "alvin", "0.0.0.0", 4002);
            _homegear = new Homegear(_rpc, true);
            _devicesService = new XMLRPCDevicesService(_homegear);
        }

        [TestMethod]
        public void TestGetAll()
        {
            List<HomegearDeviceModel> devicesList = (List<HomegearDeviceModel>)_devicesService.GetAll();
            Assert.AreEqual(devicesList.Count, 0);

            _homegear.Devices.Add("123");

            Assert.AreEqual(devicesList.Count, 1);
        }
    }
}
