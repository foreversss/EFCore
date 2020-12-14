using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Tools.Cache
{
    public class CacheProvider
    {
        /// <summary>
        /// 是否使用Redis
        /// </summary>
        public bool _isUseRedis { get; set; }

        /// <summary>
        /// 是否使用Redis
        /// </summary>
        public string _connectionString { get; set; }

        /// <summary>
        /// Redis实例名称   
        public string _instanceName { get; set; }


        //锁
        private static readonly object Locker = new object();

        private static CacheProvider _cacheProvider = null;

        public static CacheProvider cacheProvider
        {
            get
            {
                if (_cacheProvider == null)
                {
                    lock (Locker)
                    {
                        if (_cacheProvider == null)
                        {
                            _cacheProvider = new CacheProvider();

                        }
                    }
                }
                return _cacheProvider;
            }
        }
        public void InitConnect(IConfiguration Configuration)
        {
            this._isUseRedis = Configuration.GetSection("UseRedis").Value == "True" ? true : false;
            this._connectionString = Configuration.GetSection("Redis_ConnectionString").Value;
            this._instanceName = Configuration.GetSection("Redis_InstanceName").Value;
        }
    }
}
