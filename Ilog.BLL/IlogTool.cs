using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilog.BLL
{
    public class IlogTool
    {
        #region  发送手机短信

        /// <summary>
        /// 功能描述：发送手机短信
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="mobileNumber">手机号码</param>
        /// <param name="content">短信内容</param>
        /// <returns>0发送失败 1发送成功</returns>
        public static int SendMobile(string mobileNumber, string content, ref int urlstate)
        {
            int resultCount = ILog.DAL.IlogTool.SendMobile(mobileNumber, content, ref urlstate);
            return resultCount;

        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 按照中文字节长度截取字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="len">截取长度</param>
        /// <returns></returns>
        public static string GetUnicodeSubString(string str, int startIndex, int len)
        {
            string result = string.Empty;// 最终返回的结果
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = startIndex;// 记录读取进度
            int pos = startIndex + len;// 记录截取位置

            for (int i = 0; i < charLen; i++)
            {
                if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                    byteCount += 2;
                else// 按英文字符计算加1
                    byteCount += 1;
                if (byteCount > len)// 超出时只记下上一个有效位置
                {
                    pos = i;
                    break;
                }
                else if (byteCount == len)// 记下当前位置
                {
                    pos = i + 1;
                    break;
                }
            }

            if (pos >= 0)
                result = str.Substring(startIndex, pos);
            return result;

        }
        #endregion


    }
}
