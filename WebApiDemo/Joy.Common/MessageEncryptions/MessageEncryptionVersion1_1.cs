//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:41:09.
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
    /// 数据加解密 des
    /// </summary>
    public class MessageEncryptionVersion1_1 : IMessageEnCryption
    {
        public static readonly string KEY = "fHil/4]0";
        public string Decode(string content)
        {
            return content.DecryptDES(KEY);
        }

        public string Encode(string content)
        {
            return content.EncryptDES(KEY);
        }
    }
}
