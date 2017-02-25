//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:33:56.
//  Version 1.0
//  JonneyDong [mailto:jonneydong@gmail.com]
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Joy.Common
{
    /// <summary>
    ///  API消息请求处理
    /// </summary>
    public class JoyMessageHandler : MessageProcessingHandler
    {

        /// <summary>
        /// 接收到request时 处理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content.IsMimeMultipartContent())
                return request;
            // 获取请求头中 api_version版本号
            var ver = System.Web.HttpContext.Current.Request.Headers.GetValues("api_version")?.FirstOrDefault();
            // 根据api_version版本号获取加密对象, 如果为null 则不需要加密
            var encrypt = MessageEncryptionCreator.GetInstance(ver);

            if (encrypt != null)
            {
                // 读取请求body中的数据
                string baseContent = request.Content.ReadAsStringAsync().Result;
                // 获取加密的信息
                // 兼容 body: 加密数据  和 body: code=加密数据
                baseContent = baseContent.Match("(code=)*(?<code>[\\S]+)", 2);
                // URL解码数据
                baseContent = baseContent.DecodeUrl();
                // 用加密对象解密数据
                baseContent = encrypt.Decode(baseContent);

                string baseQuery = string.Empty;
                if (!request.RequestUri.Query.IsNullOrEmpty())
                {
                    // 同 body
                    // 读取请求 url query数据
                    baseQuery = request.RequestUri.Query.Substring(1);
                    baseQuery = baseQuery.Match("(code=)*(?<code>[\\S]+)", 2);
                    baseQuery = baseQuery.DecodeUrl();
                    baseQuery = encrypt.Decode(baseQuery);
                }
                // 将解密后的 URL 重置URL请求
                request.RequestUri = new Uri($"{request.RequestUri.AbsoluteUri.Split('?')[0]}?{baseQuery}");
                // 将解密后的BODY数据 重置
                request.Content = new StringContent(baseContent);
            }

            return request;
        }

        /// <summary>
        /// 处理将要向客户端response时
        /// </summary>
        /// <param name="response"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            //var isMediaType = response.Content.Headers.ContentType.MediaType.Equals(mediaTypeName, StringComparison.OrdinalIgnoreCase);
            var ver = System.Web.HttpContext.Current.Request.Headers.GetValues("api_version")?.FirstOrDefault();
            var encrypt = MessageEncryptionCreator.GetInstance(ver);
            if (encrypt != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // 返回消息 进行加密
                    var encodeResult = encrypt.Encode(result);
                    response.Content = new StringContent(encodeResult);
                }
            }

            return response;
        }

    }
}
