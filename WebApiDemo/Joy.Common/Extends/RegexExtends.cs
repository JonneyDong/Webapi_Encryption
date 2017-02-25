//==============================================================
//  Copyright (C) 2017 JonneyDong Inc. All rights reserved.
//
//==============================================================
//  Create by JonneyDong at 2017/2/25 12:42:37.
//  Version 1.0
//  JonneyDong [mailto:jonneydong@gmail.com]
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Joy.Common
{
    public static class RegexExtends
    {
        /// <summary>
        /// 返回指定 匹配项
        /// <para>默认返点 索引1内容</para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string Match(this string source, string regex, int index = 1)
        {
            return Regex.Match(source, regex).Groups[index].Value;
        }
    }
}
