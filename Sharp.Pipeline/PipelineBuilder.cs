using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp.Pipeline
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly IList<Func<PipelineDelegate, PipelineDelegate>> _components = new List<Func<PipelineDelegate, PipelineDelegate>>();

        public PipelineBuilder()
        {
        }

        public PipelineDelegate Build()
        {
            //  If Nothing is in the pipeline return empty delegate method;
            PipelineDelegate app = context =>
            {
                return Task.FromResult(0);
            };

            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            return app;
        }

        public IPipelineBuilder Use(Func<PipelineDelegate, PipelineDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }
    }
}
