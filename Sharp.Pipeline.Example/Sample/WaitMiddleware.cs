using System;

namespace Sharp.Pipeline.Example.Sample
{
    public class WaitMiddleware : MiddlewareBase
    {
        public WaitMiddleware(PipelineDelegate next) : base(next)
        { }

        protected override void OnInvoke(IPipelineContext context)
        {
            Console.WriteLine("Processing Middleware");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
        }
    }
}
