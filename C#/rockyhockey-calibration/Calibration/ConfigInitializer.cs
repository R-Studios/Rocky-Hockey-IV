using System;

namespace Calibration
{
    public static class ConfigInitializer
    {
            /// <summary>
            /// Initializess the config
            /// </summary>
            /// <param name="objectSerializer">object serializer to read the config file</param>
            public static void InitializeConfig(ObjectSerializer objectSerializer)
            {
                var config = objectSerializer.DeSerializeObject<Config>();
                if (config != null)
                {
                    Config.Instance.LastDirectoryUsed = config.LastDirectoryUsed;
                }
                else
                {
                    Config.Instance.LastDirectoryUsed = string.Empty;
                    objectSerializer.SerializeObject(Config.Instance);
            }
            }
        }
    }


