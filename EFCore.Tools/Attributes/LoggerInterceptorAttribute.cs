using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Tools.Attributes
{
    public class LoggerInterceptorAttribute : AbstractInterceptorAttribute
    {
        [HandleProcessCorruptedStateExceptions]
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await context.Invoke(next);
            }
            catch (AccessViolationException ex)
            {
                context.ReturnValue = "404";
            }
            catch (Exception ex)
            {
                context.ReturnValue = "404";
            }
        }
    }
}
