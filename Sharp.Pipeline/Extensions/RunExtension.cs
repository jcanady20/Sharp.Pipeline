using System;

namespace Sharp.Pipeline
{
    public static class RunExtension
    {
        public static void Run(this IPipelineBuilder app, PipelineDelegate handler)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            app.Use(_ => handler);
        }
    }
}
