namespace RockyHockey.Common
{
    /// <summary>
    /// Klasse, die einen 2D-Vektor repräsentiert
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// X-Achse
        /// </summary>
        public GameFieldPosition Position { get; set; }

        /// <summary>
        /// X-Length
        /// </summary>
        public GameFieldPosition Direction { get; set; }
    }
}
