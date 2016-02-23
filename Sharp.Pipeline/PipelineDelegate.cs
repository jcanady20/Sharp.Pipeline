using System.Threading.Tasks;

namespace Sharp.Pipeline
{
    public delegate Task PipelineDelegate(IPipelineContext context);
}
