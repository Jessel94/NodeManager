using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeManagerCore.Helpers
{
    public class JSONtoByte
    {
        public static byte[] JsonStringToByteArray(string jsonByteString)
        {
            jsonByteString = jsonByteString.Substring(1, jsonByteString.Length - 2);
            string[] arr = jsonByteString.Split(',');
            byte[] bResult = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                bResult[i] = byte.Parse(arr[i]);
            }
            return bResult;
        }
    }
}
