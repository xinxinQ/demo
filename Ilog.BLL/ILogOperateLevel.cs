using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogOperateLevel
    {
        #region  增加一条范围数据
        /// <summary>
        /// 增加一条范围数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static string OperatelevelAdd(ILog.Model.ILogOperateLevel model)
        {
            StringBuilder strILogOperateLevel = new StringBuilder();

            strILogOperateLevel.Append("var strILogOperateLevelJsonObject = ");
            strILogOperateLevel.Append("({");
            strILogOperateLevel.Append("\"state\": \"" + ILog.DAL.ILogOperateLevel.OperatelevelAdd(model).ToString() + "\"");
            strILogOperateLevel.Append("})");

            return strILogOperateLevel.ToString();
        }
        #endregion
    }
}
