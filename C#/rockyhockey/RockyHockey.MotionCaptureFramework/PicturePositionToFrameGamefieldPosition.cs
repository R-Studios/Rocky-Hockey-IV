using RockyHockey.Common;

namespace RockyHockey.MotionCaptureFramework
{
    /// <summary>
    /// Converter to convert between positions of cameras and the game field position
    /// </summary>
    public static class PicturePositionToFrameGamefieldPosition
    {
        private static int playfieldWidthInPx = Config.Instance.GameFieldSize.Width;
        private static int playfieldHeightInPx = Config.Instance.GameFieldSize.Height;
        private const int offsetLeftSide = 36;
        private const int offsetBottomSide = 212;

        /// <summary>
        /// convert from camera position to gamefield position
        /// </summary>
        /// <param name="cameraPosition"></param>
        /// <returns>GameFieldPosition</returns>
        public static FrameGameFieldPosition ConvertCameraToFrameGameFieldPosition(FrameGameFieldPosition cameraPosition)
        {
            return new FrameGameFieldPosition
            {
                //Y = Config.Instance.GameFieldSize.Width - cameraPosition.X,
                //X = (cameraPosition.Y),
                //TODO: use complete image as soon as you combine both camera mats
                //X = Config.Instance.Camera1GameFieldHeight - cameraPosition.Y,
                //Y = cameraPosition.X,
                //X = playfieldHeightInPx - (cameraPosition.Y - offsetBottomSide),
                Y = cameraPosition.X,
                X = Config.Instance.Camera1.FieldSize.Height - cameraPosition.Y,
                FrameNumber = cameraPosition.FrameNumber
            };
        }
    }
}