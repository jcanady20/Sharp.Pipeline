using System.Threading.Tasks;

namespace Sharp.Pipeline
{
    public abstract class PipelineBase
    {
        protected readonly PipelineDelegate _app;

        protected PipelineBase()
        {
            IPipelineBuilder builder = new PipelineBuilder();
            builder = Configure(builder);
            _app = builder.Build();
        }

        private IPipelineBuilder Configure(IPipelineBuilder app)
        {
            OnAppConfigure(app);
            return app;
        }

        protected abstract void OnAppConfigure(IPipelineBuilder app);

        public virtual IPipelineContext CreateContext()
        {
            return new PipelineContext();
        }

        public async Task ProcessPipeline(IPipelineContext context)
        {
            await _app(context);
        }
    }
}
