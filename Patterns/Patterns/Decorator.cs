using System;
using System.Linq;
using System.Runtime.Caching;

namespace Patterns
{
    class Config
    {
        ConfigReader configReader;

        public Config()
        {
            InitConfig();
        }

        public string GetSettingValue(string name)
        {
            return configReader?.GetSetting(name);
        }

        private void InitConfig()
        {
            var readers = "Cache,Environment,Database";
            var readerList = readers.Split(',').Reverse();
            foreach (var reader in readerList)
            {
                switch (reader)
                {
                    case "Cache":
                        configReader = new ConfigReader.CacheReader(configReader);
                        continue;
                    case "Environment":
                        configReader = new ConfigReader.EnvironmentReader(configReader);
                        continue;
                    case "Database":
                        configReader = new ConfigReader.DatabaseReader(configReader);
                        continue;
                }
            }
        }
    }

    abstract class ConfigReader
    {
        public abstract string GetSetting(string name);

        public abstract class ReaderDecorator : ConfigReader
        {
            ConfigReader reader;

            public ReaderDecorator(ConfigReader reader)
            {
                this.reader = reader;
            }

            public override string GetSetting(string name)
            {
                return reader.GetSetting(name);
            }
        }

        public class CacheReader : ReaderDecorator
        {
            private MemoryCache cache = new MemoryCache("Config");
            private TimeSpan cacheDuration = TimeSpan.FromSeconds(60);
            public CacheReader(ConfigReader reader) : base(reader)
            {
            }

            public override string GetSetting(string name)
            {
                var value = cache.Get(name);
                if (value == null)
                {
                    value = base.GetSetting(name);
                    cache.Add(name, value, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.Add(cacheDuration) });
                }

                return (string)value;
            }
        }

        public class EnvironmentReader : ReaderDecorator
        {
            public EnvironmentReader(ConfigReader reader) : base(reader)
            {
            }

            public override string GetSetting(string name)
            {
                return Environment.GetEnvironmentVariable(name) ?? base.GetSetting(name);
            }
        }

        public class DatabaseReader : ReaderDecorator
        {
            const string dbValue = "value from db";
            public DatabaseReader(ConfigReader reader) : base(reader)
            {
            }

            public override string GetSetting(string name)
            {
                return dbValue ?? base.GetSetting(name);
            }
        }
    }




    //-------------------------------------------------------------------------------------------------------------
    abstract class Beverage
    {
        public abstract string Name { get; }
        public abstract int Cost();
    }

    class Coffee : Beverage
    {
        public override string Name
        {
            get { return "Coffee"; }
        }

        public override int Cost()
        {
            return 10;
        }
    }


    abstract class Decorator : Beverage
    {
        protected Beverage beverage;
        public Decorator(Beverage beverage)
        {
            this.beverage = beverage;
        }
    }

    class Milk : Decorator
    {
        public Milk(Beverage beverage) : base(beverage) { }

        public override string Name
        {
            get { return beverage.Name + "+" + "Milk"; }
        }

        public override int Cost()
        {
            return beverage.Cost() + 2;
        }
    }

    class Cream : Decorator
    {
        public Cream(Beverage beverage)
            : base(beverage) { }

        public override string Name
        {
            get { return beverage.Name + "+" + "Cream"; }
        }

        public override int Cost()
        {
            return beverage.Cost() + 3;
        }
    }

}
