using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This code is from https://www.yuanmas.com/info/obynEEW4aK.html ,very thanks.
/// </summary>
namespace Native.Tool
{
    public class StrongString
    {
        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>左边内容</returns>
        public static string GetLeft(string str, string s,bool ignoreCase = true)
        {
            try
            {
                int index;
                if (ignoreCase == true)
                {
                    index = str.ToLower().IndexOf(s.ToLower());
                }
                else
                {
                    index = str.IndexOf(s);
                }

                if (index == -1)
                {
                    return "";
                }
                string temp = str.Substring(0, index);
                return temp;
            }
            catch
            {
                return "";
            }
            
        }

        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>右边内容</returns>
        public static string GetRight(string str, string s, bool ignoreCase = true)
        {
            try
            {
                int index;
                if (ignoreCase == true)
                {
                    index = str.ToLower().IndexOf(s.ToLower());
                }
                else
                {
                    index = str.IndexOf(s);
                }

                if (index == -1)
                {
                    return "";
                }
                string temp = str.Substring(index + s.Length, str.Length - s.Length - index);
                return temp;
            }
            catch
            {
                return "";
            }
            
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>返回中间文本内容</returns>
        public static string Between(string str, string leftstr, string rightstr)
        {
            try {
                int i = str.IndexOf(leftstr) + leftstr.Length;
                string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
                return temp;
            }
            catch
            {
                return "";
            }
            
        }

        /// <summary>
        /// 取文本中间到List集合
        /// </summary>
        /// <param name="str">文本字符串</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>List集合</returns>
        public List<string> BetweenArr(string str, string leftstr, string rightstr)
        {
            List<string> list = new List<string>();
            int leftIndex = str.IndexOf(leftstr);//左文本起始位置
            int leftlength = leftstr.Length;//左文本长度
            int rightIndex = 0;
            string temp = "";
            while (leftIndex != -1)
            {
                rightIndex = str.IndexOf(rightstr, leftIndex + leftlength);
                if (rightIndex == -1)
                {
                    break;
                }
                temp = str.Substring(leftIndex + leftlength, rightIndex - leftIndex - leftlength);
                list.Add(temp);
                leftIndex = str.IndexOf(leftstr, rightIndex + 1);
            }
            return list;
        }

        /// <summary>
        /// 指定文本倒序
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns>倒序文本</returns>
        public static string StrReverse(string str)
        {
            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                sb.Append(chars[chars.Length - 1 - i]);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 检查部分值是否存在Value中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="partValue"></param>
        /// <returns></returns>
        public static bool IsExist(string str, string value,char separator)
        {
            string[] tmp = str.Split(separator);
            for (int i = 0; i < tmp.Length; i++)
            {
                if (value == tmp[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static string EncryQqid(long uid)
        {
            StringBuilder tmp = new StringBuilder(uid.ToString());
            for (int i = 1; i <= 3; i++)
            {
                tmp = tmp.Replace(uid.ToString().Substring((tmp.Length / 2) + i - 2, 1), "*", (tmp.Length / 2) + i - 2, 1);
            }
            return tmp.ToString();
        }

        public static string EncryQqid(string uid)
        {
            StringBuilder tmp = new StringBuilder(uid);
            for (int i = 1; i <= 3; i++)
            {
                tmp = tmp.Replace(uid.Substring((tmp.Length / 2) + i - 2, 1), "*", (tmp.Length / 2) + i - 2, 1);
            }
            return tmp.ToString();
        }
    }
}
