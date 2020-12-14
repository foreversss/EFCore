using EFCore.Tools.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EFCore.Tools.Extensions
{
    public static class NetCoreDIModuleRegister
    {
        public static void AutoRegisterService(this IServiceCollection services, Type ImplementationType)
        {
            //获取类型的 UseDIAttribute 属性 对应的对象
            UseDIAttribute attr = ImplementationType.GetCustomAttribute(typeof(UseDIAttribute)) as UseDIAttribute;
            ////获取类实现的所有接口
            //Type[] types = ImplementationType.GetInterfaces();
            List<Type> types = attr.GetTargetTypes();
            var lifetime = attr.Lifetime;
            //遍历类实现的每一个接口
            foreach (var t in types)
            {
                //将类注册为接口的实现-----但是存在一个问题，就是担心 如果一个类实现了IDisposible接口 担心这个类变成了这个接口的实现
                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(t, ImplementationType, lifetime);
                services.Add(serviceDescriptor);
            }
        }

        /// <summary>
        /// 根据程序集的名字获取程序集中所有的类型集合
        /// </summary>
        /// <param name="AssemblyName">程序集名字</param>
        /// <returns>类型集合</returns>
        public static Type[] GetTypesByAssemblyName(String AssemblyName)
        {
            Assembly assembly = Assembly.Load(AssemblyName);
            return assembly.GetTypes();
        }


        #region 将程序集中的所有符合条件的类型全部注册到 IServiceCollection 中 重载1
        /// <summary>
        /// 将程序集中的所有符合条件的类型全部注册到 IServiceCollection 中
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="AassemblyName">程序集名字</param>
        public static void AutoRegisterServicesFromAssembly(this IServiceCollection services, string AassemblyName)
        {
            //根据程序集的名字 获取程序集中所有的类型
            Type[] types = GetTypesByAssemblyName(AassemblyName);
            //过滤上述程序集 首先按照传进来的条件进行过滤 接着要求Type必须是类，而且不能是抽象类
            IEnumerable<Type> _types = types.Where(t => t.IsClass && !t.IsAbstract);
            foreach (var t in _types)
            {
                IEnumerable<Attribute> attrs = t.GetCustomAttributes();
                //遍历类的所有特性
                foreach (var attr in attrs)
                {
                    //如果在其特性中发现特性是 UseDIAttribute 特性 就将这个类注册到DI容器中去
                    //并跳出当前的循环 开始对下一个类进行循环
                    if (attr is UseDIAttribute)
                    {
                        services.AutoRegisterService(t);
                        break;
                    }
                }
            }
        }
        #endregion

        #region 将程序集中的所有符合条件的类型全部注册到 IServiceCollection 中 重载2
        /// <summary>
        /// 将程序集中的所有符合条件的类型全部注册到 IServiceCollection 中
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="AassemblyName">程序集名字</param>
        /// <param name="wherelambda">过滤类型的表达式</param>
        public static void AutoRegisterServicesFromAssembly(this IServiceCollection services,
            string AassemblyName, Func<Type, bool> wherelambda)
        {
            //根据程序集的名字 获取程序集中所有的类型
            Type[] types = GetTypesByAssemblyName(AassemblyName);
            //过滤上述程序集 首先按照传进来的条件进行过滤 接着要求Type必须是类，而且不能是抽象类
            IEnumerable<Type> _types = types.Where(wherelambda).Where(t => t.IsClass && !t.IsAbstract);
            foreach (var t in _types)
            {
                IEnumerable<Attribute> attrs = t.GetCustomAttributes();
                //遍历类的所有特性
                foreach (var attr in attrs)
                {
                    //如果在其特性中发现特性是 UseDIAttribute 特性 就将这个类注册到DI容器中去
                    //并跳出当前的循环 开始对下一个类进行循环
                    if (attr is UseDIAttribute)
                    {
                        services.AutoRegisterService(t);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
