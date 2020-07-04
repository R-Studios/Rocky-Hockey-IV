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
            SizeRatio = 1;
            PuckRadiusMM = 5;
            PuckRadius = PuckRadiusMM / SizeRatio;
            BatRadiusMM = 5;
            BatRadius = BatRadiusMM / SizeRatio;

            Camera1 = new CameraConfig(0);
            Camera1.Resolution = new Size(320, 240);
            Camera1.UpperRight = new Coordinate(320, 0);
            Camera1.LowerLeft = new Coordinate(0, 240);
            Camera1.LowerRight = new Coordinate(320, 240);
        }

        private static string configFile = "RockyHockeyConfig.xml";

        public static Config load()
        {
            lock (configFile)
            {
                if (instance == null)
                {
                    instance = ObjectSerializer.DeserializeObject<Config>(configFile);

                    if (instance == null)
                    {
                        instance = new Config();
                        instance.save();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// saves / serializes config object into file
        /// </summary>
        public void save()
        {
            ObjectSerializer.SerializeObject(this, configFile);
        }

        private static Config instance = null;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Config Instance => instance ?? (instance = load());

        /// <summary>
        /// config of camera 1
        /// </summary>
        public CameraConfig Camera1 { get; set; }

        public PuckDetectionConfig detectionConfig { get; set; }

        /// <summary>
        /// Size of the gamefield
        /// </summary>
        public Size GameFieldSize { get; set; }

        /// <summary>
        /// Size of the gamefield in millimeters
        /// </summary>
        public Size GameFieldSizeMM { get; set; }

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
        /// radius of the puck in mm
        /// </summary>
        public double PuckRadiusMM { get; set; }

        /// <summary>
        /// radius of the puck
        /// </summary>
        public double PuckRadius { get; set; }

        /// <summary>
        /// radius of the bat in mm
        /// </summary>
        public double BatRadiusMM { get; set; }

        /// <summary>
        /// radius of the bat
        /// </summary>
        public double BatRadius { get; set; }

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
        
        /// <summary>
        /// How many millimeters of table a pixel of the game field represents (table size / game field size)
        /// </summary>
        public double SizeRatio { get; set; }
    }
}
