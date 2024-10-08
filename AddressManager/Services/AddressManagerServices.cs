using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using AddressManager.Interfaces;
using AddressManager.Models;
using AddressManager.Utils;

namespace AddressManager.Services
{
    public class AddressManagerServices : IAddressBook
    {
        // Liste zum Speichern von Adressen
        private List<Address> addresses = new List<Address>();

        // Methode zum Hinzufügen einer Adresse
        public void AddAddress(string firstName, string lastName, string street, string postalCode, string city, string phoneNumber)
        {
            int newId = 1; // Standard Wert für Index 1

            // Überprüfen, ob die Telefonnummer im gültigen Format vorliegt
            if (!ValidationUtils.IsValidPhoneNumber(phoneNumber))
            {
                Console.WriteLine("The phone number has an invalid format (xxxx/xxxxxxx).");
                return;
            }

            // Überprüfen ob die Telefonnummer bereits vorhanden ist
            foreach (Address a in addresses)
            {
                if (a.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("The phone number already exists.");
                    return;
                }
            }

            // Höchste ID in der Liste suchen
            foreach (Address a in addresses)
            {
                if (a.Id >= newId) // Wenn die aktuelle ID größer oder gleich dem aktuellen newId ist
                {
                    newId = a.Id + 1; // newId auf die nächste ID erhöhen
                }
            }

            Address newAddress = new Address(firstName, lastName, newId, street, postalCode, city, phoneNumber);
            addresses.Add(newAddress);
            Console.WriteLine("The address is added successfully.");

            /* Überprüfen, ob eine Adresse mit der angegebenen Telefonnummer bereits existiert
             * LINQ Ausdruck, Any return boolean Wert
             * a = jedes Adress-Objekt, => iteriert durch addresses, überprüft Telefonnummer
            bool exists = addresses.Any(a => a.PhoneNumber == phoneNumber);
            if (exists)
            {
                throw new InvalidOperationException("Address with this phone number already exists.");
            }

            // Neue Adresse erstellen und zur Liste hinzufügen
            Address newAddress = new Address(firstName, lastName, newId, street, city, postalCode, phoneNumber);
            addresses.Add(newAddress);
            Console.WriteLine("The address is added succesfully");*/
        }

        // Methode zum Entfernen einer Adresse anhand der ID
        public void RemoveAddressById(int id)
        {
            GetAllAddresses(); // Listet alle Adressen auf mit einer ID

            // Überprüfen, ob die ID gültig ist
            if (id <= 0 || id > addresses.Count)
            {
                Console.WriteLine("The ID must be a positive number.");
                return;
            }

            // Überprüfen ob die Adresse mit der angegebenen ID vorhanden ist
            Address? addressToRemove = null;

            foreach (Address a in addresses)
            {
                if (a.Id == id)
                {
                    addressToRemove = a; // Adresse zum Entfernen
                    break; // Schleife beenden
                }
            }

            // Überprüfen, ob die Adresse gefunden wurde
            if (addressToRemove != null)
            {
                addresses.Remove(addressToRemove); // Entfernen der Adresse aus der Liste
            }
            else
            {
                // Wenn keine Adresse gefunden wurde, außerhalb der Schleife die Nachricht anzeigen
                Console.WriteLine($"The address with the ID {id} is not found.");
            }
        }

        // Address? gibt ein Objekt Adresse aus, kann aber auch "null" sein
        public Address? FindAddressById(int id)
        {
            // Überprüfen, ob die ID gültig ist
            if (id <= 0 || id > addresses.Count)
            {
                return null;
            }
            return addresses[id - 1]; // -1 da Index bei 0 anfängt
        }

        // IEnumerable = Sammlung von Objekten
        public IEnumerable<Address> GetAllAddresses()
        {
            if (addresses == null || addresses.Count == 0) // Any (irgendein Objekt vorhanden)
            {
                return Enumerable.Empty<Address>(); // Gibt eine leere Liste zurück
            }
            return addresses; // Gibt die Adressen zurück, wenn vorhanden
        }  
    }
}
