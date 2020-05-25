using System;
using System.Drawing;

namespace RockyHockey.Common
{
    /// <summary>
    /// Holds all values that describe the configuration of the application
    /// </summary>
    [Serializable]
    public class Config
    {
        private Config()
        {
            GameFieldSize = new Size(423, 238);
            FrameRate = 187;
            GameDifficulty = Difficulties.hard;
            ImaginaryAxePosition = 320;
            Tolerance = 5;
            MaxBatVelocity = 0.4;
            RestPositionDivisor = 16;

            Camera1 = new CameraConfig(0);
            Camera1.FieldSize = new Size(320, 240);
            Camera1.UpperRight = new Coordinate(320, 0);
            Camera1.LowerLeft = new Coordinate(0, 240);
            Camera1.LowerRight = new Coordinate(320, 240);

            Camera2 = new CameraConfig(1);
            Camera2.FieldSize = new Size(320, 240);
            Camera2.UpperRight = new Coordinate(320, 0);
            Camera2.LowerLeft = new Coordinate(0, 240);
            Camera2.LowerRight = new Coordinate(320, 240);
        }

        private static Config instance;

        private static string configFile = "RockyHockeyConfig.xml";

        /// <summary>
        /// tries to load config from file
        /// if none exists a new config gets created
        /// </summary>
        /// <returns></returns>
        private static Config Load()
        {
            Config config = ObjectSerializer.DeserializeObject<Config>(configFile);

            if (config == null)
            {
                config = new Config();
                config.save();
            }

            return config;
        }

        /// <summary>
        /// saves / serializes config object into file
        /// </summary>
        public void save()
        {
            ObjectSerializer.SerializeObject(this, configFile);
        }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Config Instance { get; } = Load();

        /// <summary>
        /// config of camera 1
        /// </summary>
        public CameraConfig Camera1 { get; set; }

        /// <summary>
        /// config of camera 2
        /// </summary>
        public CameraConfig Camera2 { get; set; }

        /// <summary>
        /// Size of the gamefield
        /// </summary>
        public Size GameFieldSize { get; set; }

        /// <summary>
        /// Position of the imaginary axis
        /// </summary>
        public int ImaginaryAxePosition { get; set; }

        /// <summary>
        /// Tolerance in pixels
        /// </summary>
        public int Tolerance { get; set; }

        /// <summary>
        /// Framerate of the camera
        /// </summary>
        public int FrameRate { get; set; }

        /// <summary>
        /// Difficulty of the RockyHockey game
        /// </summary>
        public Difficulties GameDifficulty { get; set; }

        /// <summary>
        /// Maximum velocity of the bat
        /// </summary>
        public double MaxBatVelocity { get; set; }

        /// <summary>
        /// Determines the Bat Rest Position on the X-Axis
        /// </summary>
        public double RestPositionDivisor { get; set; }
    }
}
