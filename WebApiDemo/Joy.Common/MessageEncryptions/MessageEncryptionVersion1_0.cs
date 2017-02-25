//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:40:51.
//  Version 1.0
//  JonneyDong [mailto:jonneydong@gmail.com]
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Common
{
    /// <summary>
    /// 加解密 只做 base64
    /// </summary>
    public class MessageEncryptionVersion1_0 : IMessageEnCryption
    {
        public string Decode(string content)
        {
            return content?.DecryptBase64();
        }

        public string Encode(string content)
        {
            return content.EncryptBase64();
        }
    }
}
