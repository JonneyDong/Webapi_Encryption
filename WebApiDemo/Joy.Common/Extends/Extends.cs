//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:45:28.
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
    public static class Extends
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}
