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
            Console.WriteLine("Hello World, this is the Sum of all numbers from the given Parameters.");
            // Guard against missing command-line arguments to avoid IndexOutOfRangeException
            if (args != null && args.Length > 0)
            {
                Console.WriteLine("Übergebene Argumente:");
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"  [{i}] {args[i]}");
                }
            }
            else
            {
                Console.WriteLine("Es wurde kein Argument übergeben.");
            }
        }
    }
}
