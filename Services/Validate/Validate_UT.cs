﻿using System;
using System.Text;
using System.Collections.Generic;
using SW.Helpers;
using SW.Services.Validate;
using Test_SW.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Test_SW_sdk_45.Services.Validate_Test
{
    [TestClass]
    public class Validate_UT
    {
        [TestMethod]
        public void ValidateXML_UT_Ok()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            ValidateXmlResponse response = validate.ValidateXml(xml.ToString());
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.statusCodeSat), "N - 601: La expresión impresa proporcionada no es válida.");
        }
        [TestMethod]
        public void Validate_Test_ValidateXMLError()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = "";
            ValidateXmlResponse response = validate.ValidateXml(xml);
            Assert.IsTrue(response.status == "error"
                && !string.IsNullOrEmpty(response.status), "Error al leer el documento XML. La estructura del documento no es un Xml valido y/o la codificación del documento no es UTF8. Root element is missing.");
        }
        [TestMethod]
        public void Validate_Test_Lrfc()
        {
            var build = new BuildSettings();
            var rfc = build.Rfc;
            Validate validate = new Validate(build.Url, build.User, build.Password);
            ValidateLrfcResponse response = validate.ValidateLrfc(rfc);
            Assert.IsTrue(response.status == "success"
                && response.data.contribuyenteRFC == build.Rfc);
        }
        [TestMethod]
        public void Validate_Test_Lco()
        {
            var build = new BuildSettings();
            var noCertificado = build.noCertificado;
            Validate validate = new Validate(build.Url, build.User, build.Password);
            ValidateLcoResponse response = validate.ValidateLco(noCertificado);
            Assert.IsTrue(response.status == "success"
                && response.data.noCertificado == build.noCertificado);
        }

        private object GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }


    }
}
