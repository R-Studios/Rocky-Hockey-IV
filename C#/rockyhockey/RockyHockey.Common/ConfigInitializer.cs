using System.Drawing;

namespace RockyHockey.Common
{
    /// <summary>
    /// Contains the method to initialize the config
    /// </summary>
    public static class ConfigInitializer
    {
        /// <summary>
        /// Initializes the config
        /// </summary>
        /// <param name="objectSerializer">object serializer to read the config file</param>
        public static void InitializeConfig(ObjectSerializer objectSerializer)
        {
            var config = objectSerializer.DeSerializeObject<Config>();
            if (config == null)
            {
                Config.Instance.GameFieldSize = new Size(423, 238);
                Config.Instance.FrameRate = 187;
                Config.Instance.GameDifficulty = Difficulties.hard;
                Config.Instance.ImaginaryAxePosition = 320;
                Config.Instance.Camera1Index = 0;
                Config.Instance.Camera2Index = 1;
                Config.Instance.Tolerance = 5;
                Config.Instance.MaxBatVelocity = 0.4;
                Config.Instance.RestPositionDivisor = 16;
                Config.Instance.Camera1GameFieldWidth = 320;
                Config.Instance.Camera1GameFieldHeight = 240;
                Config.Instance.Camera1OffsetFromTop = 0;
                Config.Instance.Camera1OffsetFromLeft = 0;
                Config.Instance.Camera1UpperLeftPositionX = 0;
                Config.Instance.Camera1UpperLeftPositionY = 0;
                Config.Instance.Camera1UpperRightPositionX = 320;
                Config.Instance.Camera1UpperRightPositionY = 0;
                Config.Instance.Camera1LowerLeftPositionX = 0;
                Config.Instance.Camera1LowerLeftPositionY = 240;
                Config.Instance.Camera1LowerRightPositionX = 320;
                Config.Instance.Camera1LowerRightPositionY = 240;
                Config.Instance.Camera2GameFieldWidth = 320;
                Config.Instance.Camera2GameFieldHeight = 240;
                Config.Instance.Camera2OffsetFromTop = 0;
                Config.Instance.Camera2OffsetFromLeft = 0;
                Config.Instance.Camera2UpperLeftPositionX = 0;
                Config.Instance.Camera2UpperLeftPositionY = 0;
                Config.Instance.Camera2UpperRightPositionX = 320;
                Config.Instance.Camera2UpperRightPositionY = 0;
                Config.Instance.Camera2LowerLeftPositionX = 0;
                Config.Instance.Camera2LowerLeftPositionY = 240;
                Config.Instance.Camera2LowerRightPositionX = 320;
                Config.Instance.Camera2LowerRightPositionY = 240;
                objectSerializer.SerializeObject(Config.Instance);
            }
            else
            {
                Config.Instance.GameFieldSize = config.GameFieldSize;
                Config.Instance.FrameRate = config.FrameRate;
                Config.Instance.GameDifficulty = config.GameDifficulty;
                Config.Instance.ImaginaryAxePosition = config.ImaginaryAxePosition;
                Config.Instance.Camera1Index = config.Camera1Index;
                Config.Instance.Camera2Index = config.Camera2Index;
                Config.Instance.Tolerance = config.Tolerance;
                Config.Instance.MaxBatVelocity = config.MaxBatVelocity;
                Config.Instance.RestPositionDivisor = config.RestPositionDivisor;
                Config.Instance.Camera1GameFieldWidth = config.Camera1GameFieldWidth;
                Config.Instance.Camera1GameFieldHeight = config.Camera1GameFieldHeight;
                Config.Instance.Camera1OffsetFromTop = config.Camera1OffsetFromTop;
                Config.Instance.Camera1OffsetFromLeft = config.Camera1OffsetFromLeft;
                Config.Instance.Camera1UpperLeftPositionX = config.Camera1UpperLeftPositionX;
                Config.Instance.Camera1UpperLeftPositionY = config.Camera1UpperLeftPositionY;
                Config.Instance.Camera1UpperRightPositionX = config.Camera1UpperRightPositionX;
                Config.Instance.Camera1UpperRightPositionY = config.Camera1UpperRightPositionY;
                Config.Instance.Camera1LowerLeftPositionX = config.Camera1LowerLeftPositionX;
                Config.Instance.Camera1LowerLeftPositionY = config.Camera1LowerLeftPositionY;
                Config.Instance.Camera1LowerRightPositionX = config.Camera1LowerRightPositionX;
                Config.Instance.Camera1LowerRightPositionY = config.Camera1LowerRightPositionY;
                Config.Instance.Camera2GameFieldWidth = config.Camera2GameFieldWidth;
                Config.Instance.Camera2GameFieldHeight = config.Camera2GameFieldHeight;
                Config.Instance.Camera2OffsetFromTop = config.Camera2OffsetFromTop;
                Config.Instance.Camera2OffsetFromLeft = config.Camera2OffsetFromLeft;
                Config.Instance.Camera2UpperLeftPositionX = config.Camera2UpperLeftPositionX;
                Config.Instance.Camera2UpperLeftPositionY = config.Camera2UpperLeftPositionY;
                Config.Instance.Camera2UpperRightPositionX = config.Camera2UpperRightPositionX;
                Config.Instance.Camera2UpperRightPositionY = config.Camera2UpperRightPositionY;
                Config.Instance.Camera2LowerLeftPositionX = config.Camera2LowerLeftPositionX;
                Config.Instance.Camera2LowerLeftPositionY = config.Camera2LowerLeftPositionY;
                Config.Instance.Camera2LowerRightPositionX = config.Camera2LowerRightPositionX;
                Config.Instance.Camera2LowerRightPositionY = config.Camera2LowerRightPositionY;     
            }
        }
    }
}
