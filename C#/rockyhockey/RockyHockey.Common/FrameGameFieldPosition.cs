namespace RockyHockey.Common
{
    /// <summary>
    /// GameFieldPosition with the number of the frame it was recorded
    /// </summary>
    public class FrameGameFieldPosition : GameFieldPosition
    {
        /// <summary>
        /// Number of the frame, the position has been recorded
        /// </summary>
        public int FrameNumber { get; set; }
    }
}
