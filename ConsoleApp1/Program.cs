// 'using' Direktiven importieren Namensräume, damit die darin definierten Typen ohne
// vollqualifizierten Namen verwendet werden können.
using System; // Grundlegende Typen und Hilfsklassen (z. B. Console, String, Math)
using System.Collections.Generic; // Generische Sammlungen (z. B. List<T>, Dictionary<K,V>)
using System.Linq; // LINQ-Erweiterungsmethoden für Abfragen über Sammlungen
using System.Text; // Textverarbeitungsklassen wie StringBuilder und Encoding
using System.Threading.Tasks; // Arten für asynchrone Programmierung (Task / Task<T>)

// Ein Namespace gruppiert verwandte Typen und verhindert Namenskonflikte.
namespace ConsoleApp1
{
    // 'internal' bedeutet, dass die Klasse nur innerhalb derselben Assembly sichtbar ist.
    // Die Klasse `Program` dient hier als Container für die statische `Main`-Methode,
    // welche der Einstiegspunkt der Konsolenanwendung ist.
    internal class Program
    {
        static void Main(string[] args)
        {
            // UTF-8 für das Zeichen '∈' sicherstellen
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Hello World, prüfe Zugehörigkeit zu ℕ (ℕ = {1,2,3,...}).");

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Es wurde kein Argument übergeben.");
                return;
            }

            Console.WriteLine("Übergebene Argumente:");
            // Liste zum Sammeln aller erfolgreich geparsten Integer-Argumente.
            // - Enthält alle geparsten Werte (auch solche < 1), da die Zugehörigkeit
            //   zu ℕ erst beim Ausgeben geprüft wird.
            // - Dient später zur weiteren Auswertung (z. B. Berechnung der Gaußschen
            //   Summe, falls genau ein positives Integer-Argument vorliegt).
            var numbers = new List<int>();

            for (int i = 0; i < args.Length; i++)
            {
                string a = args[i];
                Console.Write($"Argument [{i}]: {a} → ");

                if (int.TryParse(a, out int n))
                {
                    if (n >= 1)
                        Console.WriteLine($"{n} ∈ ℕ");
                    else
                        Console.WriteLine($"{n} ∉ ℕ");

                    numbers.Add(n);
                }
                else
                {
                    Console.WriteLine($"{a} ist kein Integer → {a} ∉ ℕ");
                }
            }

            // Wenn genau ein Integer-Argument vorliegt, Gaußsche Summe berechnen
            var validInts = numbers.Where(x => x >= 1).ToList();
            if (validInts.Count == 1 && numbers.Count == 1)
            {
                int n = validInts[0];
                long gaussSum = (long)n * (n + 1) / 2;
                Console.WriteLine($"Gaußsche Summe 1..{n} = {gaussSum}");
            }
        }
    }
}
