namespace Sharp.Pipeline.Example.Sample
{
    public class SimpleMiddleware : MiddlewareBase
    {
        public SimpleMiddleware(PipelineDelegate next) : base(next)
        { }

        protected override void OnInvoke(IPipelineContext context)
        {
            context.SetProperty<int>("TheAnswer", 42);
        }
    }
}
