#cnblogs
http://www.cnblogs.com/jonneydong/p/WebApi_Encryption.html

#git on oschina
https://git.oschina.net/jonneydong/Webapi_Encryption

#Webapi_Encryption

WEBAPI实现通讯加密

Base64加密 和DES加密

单元测试
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
