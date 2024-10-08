using AddressManager.Utils;

namespace AddressManager.Models
{
    public abstract class Person // Abstrakte Klasse
    {
        public string FirstName { get; private set; } // Setter private da die Werte nicht verändert werden
        public string LastName { get; private set; }

        // Konstruktor
        public Person(string firstName, string lastName)
        {
            SetFirstName(firstName); // Aufruf der Setter Methoden
            SetLastName(lastName);
        }

        // Setter für den Vornamen
        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) // darf nicht leer oder nur aus Leerzeichen bestehen
            {
                throw new ArgumentException("The first name must not be empty.", nameof(firstName));
            }
            if (!ValidationUtils.IsValidName(firstName)) // Regex-Überprüfung
            {
                throw new ArgumentException("The first name contains invalid characters.", nameof(firstName));
            }
            FirstName = firstName;
        }

        // Setter für den Nachnamen
        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("The last name must not be empty.", nameof(lastName));
            }
            if (!ValidationUtils.IsValidName(lastName)) // Regex-Überprüfung
            {
                throw new ArgumentException("The last name contains invalid characters.", nameof(lastName));
            }
            LastName = lastName;
        }

        // Abstrakte Methode
        public abstract void DisplayInfo();
    }
}
