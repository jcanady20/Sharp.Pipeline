namespace Sharp.Pipeline
{
    /// <summary>
    /// Used to deserialize json pipeline configuration
    /// </summary>
    public class PipelineTask
    {
        public string TaskName { get; set; }
        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
    }
}
