using System;
using System.Collections.Generic;


namespace Sharp.Pipeline
{
    public interface IPipelineContext : IDictionary<string, object>
    {
        bool ContinueOnException { get; set; }

        Exception LastException { get; set; }

        T GetProperty<T>(string key);

        void SetProperty<T>(string key, T value);
    }
}
