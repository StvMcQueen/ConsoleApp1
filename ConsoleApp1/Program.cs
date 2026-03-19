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
            // Console.OutputEncoding bestimmt die Zeichenkodierung, die beim Schreiben in die Konsole benutzt wird.
            // Standardkonfigurationen können in manchen Umgebungen eine andere Kodierung haben (z. B. OEM-Codepage),
            // wodurch Sonderzeichen wie '∈' nicht korrekt dargestellt würden.
            Console.OutputEncoding = Encoding.UTF8;

            // Kurze Einleitung, was das Programm macht.
            // Die Ausgabe ist rein informativ und hilft beim Testen / Debuggen.
            Console.WriteLine("Hello World, prüfe Zugehörigkeit zu ℕ (ℕ = {1,2,3,...}).");

            // Schutz gegen null-Array oder leere Argumentliste:
            // - `args` kann null sein, wenn ein Aufrufer oder Test dies explizit so macht.
            // - Häufig ist args jedoch mindestens ein leeres Array; trotzdem ist die Nullprüfung defensiv.
            // Wenn keine Argumente übergeben wurden, wird ein Hinweis ausgegeben und die Anwendung beendet.
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Es wurde kein Argument übergeben.");
                return; // Beendet Main, damit kein weiterer Code ausgeführt wird.
            }

            Console.WriteLine("Übergebene Argumente:");

            // Liste zum Sammeln aller erfolgreich geparsten Integer-Argumente.
            // - Enthält alle geparsten Werte (auch solche < 1), da die Zugehörigkeit
            //   zu ℕ erst beim Ausgeben geprüft wird.
            // - Dient später zur weiteren Auswertung (z. B. Berechnung der Gaußschen
            //   Summe, falls genau ein positives Integer-Argument vorliegt).
            //
            // Syntax/Struktur:
            // - `var numbers = new List<int>();`
            //   * `var` ist ein Compiler-Schlüsselwort für Typinferenz; der tatsächliche Typ hier ist `List<int>`.
            //   * `List<int>` ist eine generische Liste, die Werte des Typs `int` speichert.
            //   * `new List<int>()` ruft den Standardkonstruktor auf und erzeugt eine leere Liste.
            var numbers = new List<int>();

            // Iteriere über alle übergebenen Argumente
            // Die klassische for-Schleife wird verwendet, damit der Index `i` für die Ausgabe verfügbar ist.
            for (int i = 0; i < args.Length; i++)
            {
                // Lokale Variable für das aktuelle Argument (als String).
                string a = args[i];

                // Ausgabe mit Formatstring: Index und roher Argument-String.
                // $-Strings erlauben Einbettung von Ausdrücken wie {i} und {a}.
                Console.Write($"Argument [{i}]: {a} → ");

                // Versuche, den String in einen Integer zu parsen.
                // int.TryParse gibt einen bool zurück (Erfolg/Misserfolg) und liefert bei Erfolg den geparsten int in `n`.
                // Vorteile gegenüber int.Parse:
                // - Keine Exception bei ungültigem Format (TryParse ist sicherer für Benutzereingaben).
                if (int.TryParse(a, out int n))
                {
                    // Prüfe, ob der geparste Wert zur Menge der natürlichen Zahlen ℕ gehört.
                    // In diesem Programm wird ℕ als {1,2,3,...} verstanden (ohne 0).
                    if (n >= 1)
                        Console.WriteLine($"{n} ∈ ℕ"); // n gehört zu ℕ
                    else
                        Console.WriteLine($"{n} ∉ ℕ"); // n ist Integer, aber nicht positiv (z. B. 0 oder negativ)

                    // Füge den geparsten int der Liste hinzu, unabhängig davon, ob er >= 1 ist.
                    // Die Entscheidung, nur positive Werte später zu validieren, wird an anderer Stelle getroffen.
                    numbers.Add(n);
                }
                else
                {
                    // Falls das Argument kein gültiger Integer ist, erfolgt eine entsprechende Ausgabe.
                    // Das Argument wird nicht der `numbers`-Liste hinzugefügt.
                    Console.WriteLine($"{a} ist kein Integer → {a} ∉ ℕ");
                }
            }

            // LINQ-Ausdruck zur Filterung aller Zahlen, die >= 1 sind (gültige ℕ-Elemente).
            // .Where(x => x >= 1) erzeugt eine IEnumerable<int>, ToList() erstellt daraus eine neue List<int>.
            // `validInts` enthält nur die positiven Integer aus `numbers`.
            var validInts = numbers.Where(x => x >= 1).ToList();

            // Ursprüngliche Bedingung war stricter (prüfte, dass genau ein Integer-Argument insgesamt übergeben wurde).
            // Die auskommentierte Variante (siehe Datei oben) ist bewusst beibehalten worden; hier entscheiden wir
            // uns für die einfachere Bedingung: wenn genau ein positives Integer vorliegt, wird die Gaußsche Summe berechnet.
            // Hinweis zur Semantik:
            // - Wenn `numbers.Count > 1` aber nur ein Element >= 1 ist, wird trotzdem die Summe berechnet.
            // - Falls das ursprüngliche Verhalten (genau ein Integer-Argument insgesamt) gewünscht ist, kann die
            //   auskommentierte Prüfung wiederhergestellt:
            //   if (validInts.Count == 1 && numbers.Count == 1)
            if (validInts.Count == 1)
            {
                // Hole das einzige gültige n
                int n = validInts[0];

                // Berechnung der Gaußschen Summenformel: 1 + 2 + ... + n = n*(n+1)/2
                // Wichtig: Typkonvertierung zu long, um Überlauf zu vermeiden, wenn n groß ist.
                // - Beispiel: bei n = 65536 kann int * int eventuell über int.MaxValue hinausgehen.
                // - Durch das Casten von `n` zu long wird die Multiplikation in 64-Bit-Arithmetik ausgeführt.
                long gaussSum = (long)n * (n + 1) / 2;

                // Ergebnis ausgeben
                Console.WriteLine($"Gaußsche Summe 1..{n} = {gaussSum}");
            }
            else
            {
                // Fallback-Ausgabe, wenn keine eindeutige Bedingung für die Summenberechnung erfüllt ist.
                Console.WriteLine("Es wurde kein oder mehr als ein gültiges Integer-Argument übergeben, daher wird die Gaußsche Summe nicht berechnet.");
            }
        }
    }
}
