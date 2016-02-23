using System;
using System.Threading.Tasks;

namespace Sharp.Pipeline
{
    public static class UseExtension
    {
        public static IPipelineBuilder Use(this IPipelineBuilder app, Func<IPipelineContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }
    }
}
