using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Sharp.Pipeline
{
    public static class UseMiddlewareExtensions
    {
        const string InvokeMethodName = "Invoke";
        public static IPipelineBuilder UseMiddleware<TMiddleware>(this IPipelineBuilder app, params object[] args)
        {
            return app.UseMiddleware(typeof(TMiddleware), args);
        }

        public static IPipelineBuilder UseMiddleware(this IPipelineBuilder app, Type middleware, params object[] args)
        {
            return app.Use(next =>
            {
                var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m => string.Equals(m.Name, InvokeMethodName, StringComparison.Ordinal)).ToArray();
                if (invokeMethods.Length > 1)
                {
                    throw new InvalidOperationException($"Ambiguous method found {InvokeMethodName}");
                }

                if (invokeMethods.Length == 0)
                {
                    throw new InvalidOperationException($"Unable to locate specified method {InvokeMethodName}");
                }

                var methodinfo = invokeMethods[0];
                if (!typeof(Task).IsAssignableFrom(methodinfo.ReturnType))
                {
                    throw new InvalidOperationException($"{InvokeMethodName} does not have a return type of {nameof(Task)}");
                }

                var parameters = methodinfo.GetParameters();
                if (parameters.Length == 0 || parameters[0].ParameterType != typeof(IPipelineContext))
                {
                    throw new InvalidOperationException($"Specified method {InvokeMethodName} does not accept {nameof(IPipelineContext)}");
                }

                var instance = Activator.CreateInstance(middleware, new[] { next }.Concat(args).ToArray());
                if (parameters.Length == 1)
                {
                    return (PipelineDelegate)methodinfo.CreateDelegate(typeof(PipelineDelegate), instance);
                }

                return context =>
                {
                    var arguments = new object[parameters.Length];
                    arguments[0] = context;
                    return (Task)methodinfo.Invoke(instance, arguments);
                };
            });
        }
    }
}
