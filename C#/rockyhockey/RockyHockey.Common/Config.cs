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
        private Config() { }

        private static Config instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Config Instance => instance ?? (instance = new Config());

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
        /// Index of the first camera
        /// </summary>
        public int Camera1Index { get; set; }

        /// <summary>
        /// Index of the second camera
        /// </summary>
        public int Camera2Index { get; set; }

        /// <summary>
        /// Maximum velocity of the bat
        /// </summary>
        public double MaxBatVelocity { get; set; }

        /// <summary>
        /// Determines the Bat Rest Position on the X-Axis
        /// </summary>
        public double RestPositionDivisor { get; set; }

        /// <summary>
        /// GameFieldWidth in px for Camera 1
        /// </summary>
        public int Camera1GameFieldWidth { get; set; }

        /// <summary>
        /// GameFieldHeigth in px for Camera 1
        /// </summary>
        public int Camera1GameFieldHeight { get; set; }

        /// <summary>
        /// Offset from top side for camera 1
        /// </summary>
        public int Camera1OffsetFromTop { get; set; }

        /// <summary>
        /// Offset from left side for camera 1
        /// </summary>
        public int Camera1OffsetFromLeft { get; set; }

        /// <summary>
        /// x position for upper left position for warping for camera 1
        /// </summary>
        public int Camera1UpperLeftPositionX { get; set; }

        /// <summary>
        /// y position for upper left position for warping for camera 1
        /// </summary>
        public int Camera1UpperLeftPositionY { get; set; }

        /// <summary>
        /// x position for upper right position for warping for camera 1
        /// </summary>
        public int Camera1UpperRightPositionX { get; set; }

        /// <summary>
        /// y position for upper right position for warping for camera 1
        /// </summary>
        public int Camera1UpperRightPositionY { get; set; }

        /// <summary>
        /// x position for lower left position for warping for camera 1
        /// </summary>
        public int Camera1LowerLeftPositionX { get; set; }

        /// <summary>
        /// y position for lower left position for warping for camera 1
        /// </summary>
        public int Camera1LowerLeftPositionY { get; set; }

        /// <summary>
        /// x position for lower right position for warping for camera 1
        /// </summary>
        public int Camera1LowerRightPositionX { get; set; }

        /// <summary>
        /// y position for lower right position for warping for camera 1
        /// </summary>
        public int Camera1LowerRightPositionY { get; set; }

        /// <summary>
        /// GameFieldHeigth in px for Camera 2
        /// </summary>
        public int Camera2GameFieldWidth { get; set; }

        /// <summary>
        /// GameFieldHeigth in px for Camera 2
        /// </summary>
        public int Camera2GameFieldHeight { get; set; }

        /// <summary>
        /// Offset from top side for camera 2
        /// </summary>
        public int Camera2OffsetFromTop { get; set; }

        /// <summary>
        /// Offset from top side for camera 2
        /// </summary>
        public int Camera2OffsetFromLeft { get; set; }

        /// <summary>
        /// x position for upper left position for warping for camera 2
        /// </summary>
        public int Camera2UpperLeftPositionX { get; set; }

        /// <summary>
        /// y position for upper left position for warping for camera 2
        /// </summary>
        public int Camera2UpperLeftPositionY { get; set; }

        /// <summary>
        /// x position for upper right position for warping for camera 2
        /// </summary>
        public int Camera2UpperRightPositionX { get; set; }

        /// <summary>
        /// y position for upper right position for warping for camera 2
        /// </summary>
        public int Camera2UpperRightPositionY { get; set; }

        /// <summary>
        /// x position for lower left position for warping for camera 2
        /// </summary>
        public int Camera2LowerLeftPositionX { get; set; }

        /// <summary>
        /// y position for lower left position for warping for camera 2
        /// </summary>
        public int Camera2LowerLeftPositionY { get; set; }

        /// <summary>
        /// x position for lower right position for warping for camera 2
        /// </summary>
        public int Camera2LowerRightPositionX { get; set; }

        /// <summary>
        /// y position for lower right position for warping for camera 2
        /// </summary>
        public int Camera2LowerRightPositionY { get; set; }
    }
}
