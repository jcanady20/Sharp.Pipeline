using System;
using System.Threading.Tasks;

namespace Sharp.Pipeline.Example.Sample
{
    public class SimplePipeline : PipelineBase
    {
        protected override void OnAppConfigure(IPipelineBuilder app)
        {
            app.UseMiddleware<WaitMiddleware>();

            app.Use(async (context, next) => {
                Console.WriteLine("In the Begining");
                await next.Invoke();
                var val = context.GetProperty<int>("TheAnswer");
                Console.WriteLine($"At the End {val}");
            });

            app.UseMiddleware<SimpleMiddleware>();

            app.Run(async context => {
                var val = context.GetProperty<int>("TheAnswer");
                await Task.FromResult(0);
                Console.WriteLine($"{val}");
            });
        }
    }
}
