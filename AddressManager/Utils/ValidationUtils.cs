using System.Text.RegularExpressions;

namespace AddressManager.Utils
{
    public static class ValidationUtils // Hilfsklasse für die Regex-Überprüfungen - static weil nur Hilfsfunktionen
    {
        public static bool IsValidName(string name)
        {
            string pattern = @"^[A-Za-zÄÖÜäöüß]+$"; // Erlaubt nur Buchstaben und Umlaute
            return Regex.IsMatch(name, pattern);
        }

        public static bool IsValidStreet(string street)
        {
            string pattern = @"^[a-zA-Z0-9ß\s.-]+$"; // Erlaubt Buchstaben, Zahlen, Leerzeichen, Bindestriche und Punkte
            return Regex.IsMatch(street, pattern);
        }

        public static bool IsValidCity(string city)
        {
            string pattern = @"^[a-zA-ZäöüÄÖÜß\s-]+$"; // Erlaubt Buchstaben, Leerzeichen und Bindestriche
            return Regex.IsMatch(city, pattern);
        }

        public static bool IsValidPostalCode(string postalCode)
        {
            string pattern = @"^\d{4}$"; // Vier Ziffern (österreichische Postleitzahl)
            return Regex.IsMatch(postalCode, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^[\d\s\-()/]+$"; // Erlaubt Ziffern, Leerzeichen, Bindestriche, Klammern , Schrägstrich und Pluszeichen
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
