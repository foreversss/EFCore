using EFCore.Tools.Attributes;
using System;

namespace ConsoleApp1
{
    class Program
    {
        [LoggerInterceptor]
        static void Main(string[] args)
        {
            GetUser();
        }

        [LoggerInterceptor]
        public static void GetUser()
        {
            Console.WriteLine("获取用户信息业务逻辑");
        }
    }
}
