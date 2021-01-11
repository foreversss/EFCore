using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Tools.Helpers
{
    /// <summary>
    /// api 返回类
    /// </summary>
    public class ExcutedResult
    {
        public bool success { get; set; }

        public string msg { get; set; }

        public object rows { get; set; }

        public int statecode { get; set; }

        public enum status
        {
            成功 = 0,
            请求失败 = 1,
            请求繁忙 = 401,
            账号密码错误 = 402,
            无访问权限或票据过期 = 403,
            添加数据失败 = 405,
            更新数据失败 = 406,
            删除数据失败 = 407
        }

        public ExcutedResult(bool success, string msg, object rows,int StateCode)
        {
            this.success = success;
            this.msg = msg;
            this.rows = rows;
            this.statecode = StateCode;
        }
        public ExcutedResult()
        {

        }
        public static ExcutedResult SuccessResult(string msg = null,int StateCode = 0)
        {
            return new ExcutedResult(true, msg, null, StateCode);
        }
        public static ExcutedResult SuccessResult(object rows, int StateCode)
        {
            return new ExcutedResult(true, null, rows, StateCode);
        }
        public static ExcutedResult SuccessResult(string msg, object rows,int StateCode)
        {
            return new ExcutedResult(true, msg, rows, StateCode);
        }
        public static ExcutedResult FailedResult(string msg, int StateCode)
        {
            return new ExcutedResult(false, msg, null, StateCode);
        }
    }

    public class PaginationResult : ExcutedResult
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount => total % pageSize == 0 ? total / pageSize : total / pageSize + 1;

        public PaginationResult(bool success, string msg, object rows,int statcode) : base(success, msg, rows, statcode)
        {
        }

        public static PaginationResult PagedResult(object rows, int total, int size, int index, int statcode)
        {
            return new PaginationResult(true, null, rows, statcode)
            {
                total = total,
                pageSize = size,
                pageIndex = index
            };
        }
    }
}
