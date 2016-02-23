using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Sharp.Pipeline.Extensions
{
    public static class PipelineExtensions
    {
        public static void ConfigureFromFile(this IPipelineBuilder app, string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                throw new FileNotFoundException($"Unable to locate the specified file {fileName}");
            }
            var rawJson = File.OpenText(fileName).ReadToEnd();
            app.ConfigureFromJson(rawJson);
        }

        public static void ConfigureFromJson(this IPipelineBuilder app, string json)
        {
            foreach (var t in GetConfiguredTasks(json))
            {
                var type = GetConfiguredType(t);
                if (type != null)
                {
                    app.UseMiddleware(type);
                }
            }
        }

        private static Type GetConfiguredType(PipelineTask task)
        {
            var fullName = task.AssemblyName + "." + task.TypeName;
            var assembly = Assembly.Load(new AssemblyName(task.AssemblyName));
            var type = assembly.GetType(task.TypeName) ?? assembly.GetType(fullName);
            return type;
        }

        private static IEnumerable<PipelineTask> GetConfiguredTasks(string rawJson)
        {
            if (String.IsNullOrEmpty(rawJson))
            {
                yield break;
            }
            var tasks = new List<PipelineTask>();
            var serializer = new DataContractJsonSerializer(typeof(List<PipelineTask>));
            using(var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rawJson)))
            {
                tasks = (List<PipelineTask>)serializer.ReadObject(ms);
            }
            foreach (var t in tasks)
            {
                yield return t;
            }
        }
    }
}
