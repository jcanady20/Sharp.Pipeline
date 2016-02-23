using System;
using System.Collections.Generic;

namespace Sharp.Pipeline
{
    public class PipelineContext : Dictionary<string, object>, IPipelineContext
    {
        public PipelineContext()
        { }

        public PipelineContext(IDictionary<string, object> dictionary) : base(dictionary)
        { }

        public bool ContinueOnException
        {
            get
            {
                return GetProperty<bool>(ContextKeys.ContinueOnFailure);
            }
            set
            {
                SetProperty(ContextKeys.ContinueOnFailure, value);
            }
        }

        public Exception LastException
        {
            get
            {
                return GetProperty<Exception>(ContextKeys.LastException);
            }
            set
            {
                SetProperty(ContextKeys.LastException, value);
            }
        }

        public T GetProperty<T>(string key)
        {
            object value;
            return this.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public void SetProperty<T>(string key, T value)
        {
            this[key] = value;
        }
    }
}
