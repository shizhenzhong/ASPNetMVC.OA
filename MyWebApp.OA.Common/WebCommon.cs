using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyWebApp.OA.Common
{
    public class WebCommon
    {
        public static string Md5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer =Encoding.UTF8.GetBytes(str);
            byte[] md5buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (var item in md5buffer)
            {
                sb.Append(item.ToString("X2"));
                
            }

            return sb.ToString();
        } 
    }
}