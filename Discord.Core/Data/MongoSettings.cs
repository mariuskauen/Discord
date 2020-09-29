using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Data
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
