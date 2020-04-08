using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockyHockey
{
    /// <summary>
    /// Klasse, die Beispiele für C#-Sprachfeatures enthält
    /// </summary>
    public class FeatureExamples
    {
        /// <summary>
        /// Konstruktor
        /// Hier wird ein EventHandler an das ConstructorCallEvent angehängt.
        /// Danach wird das Event aufgerufen.
        /// </summary>
        public FeatureExamples()
        {
            ConstructorCallEvent += OnConstructorCall;
            ConstructorCallEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Öffentliches Event, an das sich EventHandler anhängen können
        /// </summary>
        public event EventHandler ConstructorCallEvent;

        /// <summary>
        /// Beispiel für Arten der Objektinitialisierung
        /// </summary>
        public void ObjectInitialization()
        {
            // Alte Schreibweise
            var vector = new GenericVectorExample<float>();
            vector.X = 5;
            vector.Y = 5;

            // Neue Schreibweise
            var vector2 = new GenericVectorExample<float> { X = 5, Y = 5};

            Func<int, string> func = (int t) => t.ToString();
            Func<int, string> func2 = t => t.ToString();
        }

        /// <summary>
        /// privater EventHandler
        /// </summary>
        /// <param name="sender">entspricht dem Aufrufer des Events, der beim Aufruf übergeben werden muss</param>
        /// <param name="e">zusätzliche Eventargumente, die Informationen mitliefern können</param>
        private void OnConstructorCall(object sender, EventArgs e)
        {
            Console.WriteLine("Diese Methode wird beim Auslösen des Events aufgerufen.");
        }

        /// <summary>
        /// Beispielmethode, die einen neu gestarteten Task zzurückgibt
        /// </summary>
        /// <returns></returns>
        private Task DoSomething()
        {
            return Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine("Hi");
                }
            });
        }

        /// <summary>
        /// Methode, die asynchron auf einen anderen Methodenaufruf wartet
        /// </summary>
        /// <returns></returns>
        public async Task CallDoSomething()
        {
            await DoSomething().ConfigureAwait(false);
            Console.WriteLine("DoSomething wurde aufgerufen.");
        }

        /// <summary>
        /// Beispielmethode, zur neuen Stringinterpolation
        /// </summary>
        public void StringInterpolation()
        {
            var x = 42;
            var y = DateTime.Now;
            string text = $"Hans ist {x} und hat am {y} Geburtstag.";
        }

        /// <summary>
        /// Beispiel für das Anwenden einer Linq-erweiterungsmethode
        /// </summary>
        public void LinqExample()
        {
            var myList = new List<string>();
            // Holt das Erste Element aus der Aufzählung, dessen Stringlänge 5 ist
            var myFirstElement = myList.FirstOrDefault(t => t.Length == 5);
        }
    }
}
