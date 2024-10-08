using AddressManager.Utils;
using System;
using System.Text.RegularExpressions;

namespace AddressManager.Models
{
    public class Address : Person  // erbt von der Klasse Person
    {
        public int Id { get; private set; } // Setter privat, da die Werte nicht verändert werden
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string PhoneNumber { get; private set; }

        // Konstruktor mit ID
        public Address (string firstName, string lastName, int id, string street, string postalCode, string city, string phoneNumber)
            : base(firstName, lastName)
        {
            SetId(id);
            SetStreet(street);
            SetPostalCode(postalCode);
            SetCity(city);
            SetPhoneNumber(phoneNumber);
        }

        // Überladener Konstruktor ohne ID
        public Address(string firstName, string lastName, string street, string postalCode, string city, string phoneNumber)
            : base(firstName, lastName)
        {
            SetStreet(street);
            SetPostalCode(postalCode);
            SetCity(city);
            SetPhoneNumber(phoneNumber);
        }

        // Setter für die ID
        public void SetId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The id must be positive", nameof(id));
            }
            Id = id;
        }

        // Setter für die Straße
        public void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentNullException(nameof(street), "The street must not be empty.");
            }
            else if (!ValidationUtils.IsValidStreet(street)) // Regex-Überprüfung
            {
                throw new ArgumentException("The street contains invalid characters.", nameof(street));
            }
            Street = street;
        }

        // Setter für die Postleitzahl
        public void SetPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentNullException(nameof(postalCode), "The postal code must not be empty.");
            }
            if (!ValidationUtils.IsValidPostalCode(postalCode)) // Regex-Überprüfung
            {
                throw new ArgumentException("The postal code is invalid.", nameof(postalCode));
            }
            PostalCode = postalCode;
        }

        // Setter für die Stadt
        public void SetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city), "The city must not be empty.");
            }
            if (!ValidationUtils.IsValidCity(city)) // Regex-Überprüfung
            {
                throw new ArgumentException("The city contains invalid characters.", nameof(city));
            }
            City = city;
        }

        // Setter für die Telefonnummer
        public void SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber), "The phone number must not be empty.");
            }
            if (!ValidationUtils.IsValidPhoneNumber(phoneNumber)) // Regex-Überprüfung
            {
                throw new ArgumentException("The phone number is invalid.", nameof(phoneNumber));
            }
            PhoneNumber = phoneNumber;
        }

        // Die abstrakte Methode zur Ausgabe der Informationen
        public override void DisplayInfo()
        {
            Console.WriteLine($"First Name: {FirstName}, Last Name: {LastName}");
            Console.WriteLine($"Street: {Street}");
            Console.WriteLine($"Postal Code: {PostalCode}, City: {City}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
        }
    }
}
