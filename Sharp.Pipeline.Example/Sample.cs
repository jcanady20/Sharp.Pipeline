namespace Sharp.Pipeline.Example
{
    public partial class Program
    {
        [System.ComponentModel.Description("Sample Pipeline")]
        static void sample()
        {
            var pl = new Sample.SimplePipeline();
            var context = pl.CreateContext();
            var task = pl.ProcessPipeline(context);
            task.Wait();
        }
    }
}
