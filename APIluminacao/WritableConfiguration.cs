using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces.Configuration;
using System;
using System.IO;

namespace APIluminacao
{
    public class WritableConfiguration<T> : IWritableConfiguration<T> where T : class, new()
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IOptionsMonitor<T> _options;
        private readonly IConfigurationRoot _configuration;
        private readonly string _section;
        private readonly string _file;

        public WritableConfiguration(IWebHostEnvironment environment, IOptionsMonitor<T> options, IConfigurationRoot configuration, string section, string file)
        {
            _environment = environment;
            _options = options;
            _configuration = configuration;
            _section = section;
            _file = file;
        }

        public T Value => _options.CurrentValue;
        public T Get(string name) => _options.Get(name);

        public void Update(Action<T> applyChanges)
        {
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(_file);
            var physicalPath = fileInfo.PhysicalPath;

            // Se o arquivo não existir, cria um object novo para permitir a criação do arquivo
            var jObject = fileInfo.Exists ? JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath)) : new JObject();
            var sectionObject = jObject!.TryGetValue(_section, out JToken? section) && section != null ? JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

            applyChanges(sectionObject!);

            jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));

            File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));

            _configuration.Reload();
        }
    }
}
