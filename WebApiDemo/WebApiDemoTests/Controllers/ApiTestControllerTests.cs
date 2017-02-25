
//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:50:43.
//  Version 1.0
//  JonneyDong [mailto:jonneydong@gmail.com]
//==============================================================
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Joy.Common;
using System.Diagnostics;

namespace WebApiDemo.Controllers.Tests
{
    [TestClass()]
    public class ApiTestControllerTests
    {
        HttpClient http = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:34475/")
        };

        [TestMethod()]
        public void GetTest()
        {
            var id = 10;
            var resultSuccess = $"\"value{id}\"";
            //不加密
            Trace.WriteLine($"without encryption.");
            var url = $"api/ApiTest?id={id}";
            Trace.WriteLine($"get url : {url}");
            var response = http.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result, resultSuccess);
            Trace.WriteLine($"result : {result}");

            //使用 方案1加密
            Trace.WriteLine($"encryption case one.");

            url = $"api/ApiTest?code=" + $"id={id}".EncryptBase64().EncodeUrl();

            Trace.WriteLine($"get url : {url}");

            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("api_version", "1.0");
            response = http.GetAsync(url).Result;

            result = response.Content.ReadAsStringAsync().Result;

            Trace.WriteLine($"result : {result}");

            result = result.DecryptBase64();

            Trace.WriteLine($"DecryptBase64 : {result}");

            Assert.AreEqual(result, resultSuccess);

            //使用 方案2 加密通讯
            Trace.WriteLine($"encryption case one.");
            
            url = $"api/ApiTest?code=" + $"id={id}".EncryptDES(MessageEncryptionVersion1_1.KEY).EncodeUrl();

            Trace.WriteLine($"get url : {url}");

            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("api_version", "1.1");
            response = http.GetAsync(url).Result;

            result = response.Content.ReadAsStringAsync().Result;

            Trace.WriteLine($"result : {result}");

            result = result.DecryptDES(MessageEncryptionVersion1_1.KEY);

            Trace.WriteLine($"DecryptBase64 : {result}");

            Assert.AreEqual(result, resultSuccess);
        }
    }
}