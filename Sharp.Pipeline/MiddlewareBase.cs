using System.Threading.Tasks;
using System;

namespace Sharp.Pipeline
{
    public abstract class MiddlewareBase
    {
        protected PipelineDelegate _next;
        public MiddlewareBase(PipelineDelegate next)
        {
            _next = next;
        }

        public Task Invoke(IPipelineContext context)
        {
            try
            {
                OnInvoke(context);
            }
            catch(Exception e)
            {
                context.LastException = e;
            }
            return _next(context);
        }

        protected abstract void OnInvoke(IPipelineContext context);
    }
}
