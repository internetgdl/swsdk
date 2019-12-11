﻿using System;
using System.Collections.Generic;
using System.Linq;
using Test_SW.Helpers;
using SW.Services.Csd;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_SW_sdk_45.Services.Csd
{
    [TestClass]
    public class Csd_Test_45
    {
        [TestMethod]
        public void Csd_Test_45_UploadCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.UploadMyCsd(build.Cer, build.Key, build.CerPassword, "stamp", true);
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Csd_Test_45_UploadCsd_EmptyCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.UploadMyCsd("", build.Key, build.CerPassword, "stamp", true);
            Assert.IsTrue(response.message == "El certificado o llave privada vienen vacios" && response.status == "error");
        }
    }
}
