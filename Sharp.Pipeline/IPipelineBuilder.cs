using System;

namespace Sharp.Pipeline
{
    public interface IPipelineBuilder
    {
        IPipelineBuilder Use(Func<PipelineDelegate, PipelineDelegate> middleware);

        PipelineDelegate Build();
    }
}
