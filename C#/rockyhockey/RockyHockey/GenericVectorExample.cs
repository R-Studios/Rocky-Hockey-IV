namespace RockyHockey
{
    /// <summary>
    /// Beispielklasse für Generika
    /// </summary>
    /// <typeparam name="T">Datentyp der X- bzw. Y-Achse</typeparam>
    public class GenericVectorExample<T>
    {
        /// <summary>
        /// X-Achse
        /// </summary>
        public T X { get; set; }

        /// <summary>
        /// Y-Achse
        /// </summary>
        public T Y { get; set; }
    }
}
