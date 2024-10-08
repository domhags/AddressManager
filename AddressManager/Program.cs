using System;
using System.Text.RegularExpressions;
using AddressManager.Interfaces;
using AddressManager.Models;
using AddressManager.Services;
using AddressManager.Utils;

class Program
{
    static void Main(string[] args)
    {
        IAddressBook addressManager = new AddressManagerServices();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("==== AddressManager ====");
            Console.WriteLine("1. Add an address");
            Console.WriteLine("2. Find an address");
            Console.WriteLine("3. Remove an address");
            Console.WriteLine("4. Show all addresses");
            Console.WriteLine("5. Quit");
            Console.WriteLine("Please choose an option. ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Adresse hinzufügen
                    AddAddress(addressManager);
                    break;
                case "2":
                    // Adresse suchen
                    FindAddressById(addressManager);
                    break;
                case "3":
                    // Adresse entfernen
                    RemoveAddressById(addressManager);
                    break;
                case "4":
                    // Adressen auflisten
                    ListAddresses(addressManager);
                    break;
                case "5":
                    // Programm beenden
                    exit = true;
                    Console.WriteLine("Program is closing...");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    public static void AddAddress(IAddressBook addressManager)
    {
        // Eingabe für den Vornamen
        Console.Write("Please enter the first name: ");
        string firstName = Console.ReadLine();
        if (!ValidationUtils.IsValidName(firstName))
        {
            Console.WriteLine("The first name is invalid. Please only use letters.");
            return;
        }

        // Eingabe für den Nachnamen
        Console.Write("Please enter the last name: ");
        string lastName = Console.ReadLine();
        if (!ValidationUtils.IsValidName(lastName))
        {
            Console.WriteLine("The last name is invalid. Please only use letters.");
            return;
        }

        // Eingabe für die Straße
        Console.Write("Please enter the street: ");
        string street = Console.ReadLine();
        if (!ValidationUtils.IsValidStreet(street))
        {
            Console.WriteLine("The street is invalid. Please only use letters and allowed special characters.");
            return;
        }

        // Eingabe für die Postleitzahl
        Console.Write("Please enter the postal code: ");
        string postalCode = Console.ReadLine();
        if (!ValidationUtils.IsValidPostalCode(postalCode))
        {
            Console.WriteLine("The postal code is invalid. Please enter a valid postal code (4 Numbers for Austria).");
            return;
        }

        // Eingabe für die Stadt
        Console.Write("Please enter the city: ");
        string city = Console.ReadLine();
        if (!ValidationUtils.IsValidCity(city))
        {
            Console.WriteLine("The city is invalid. Please only use letters.");
            return;
        }

        // Eingabe für die Telefonnummer
        Console.Write("Please enter the phone number: ");
        string phoneNumber = Console.ReadLine();
        if (!ValidationUtils.IsValidPhoneNumber(phoneNumber))
        {
            Console.WriteLine("The phone number has an invalid format. (xxxx/xxxxxxx)");
            return;
        }

        try
        {
            // Versucht Objekt hinzufügen
            addressManager.AddAddress(firstName, lastName, street, postalCode, city, phoneNumber);
        }
        catch (ArgumentException ex)
        {
            // Fehler beim Hinzufügen
            Console.WriteLine($"Error adding the address {ex.Message}");
        }
    }

    public static void RemoveAddressById(IAddressBook addressManager)
    {
        // Liste der Adressen anzeigen
        ListAddresses(addressManager);
        Console.Write("Please enter the ID to remove the address: ");

        // Einlesen der Id
        string id = Console.ReadLine();

        // Validierung der Eingabe
        if (int.TryParse(id, out int addressId))
        {
            // Überprüfen, ob die ID gültig ist
            if (addressId <= 0) // Stellen Sie sicher, dass die ID positiv ist
            {
                Console.WriteLine("The ID must be a positive number.");
                return;
            }

            // Überprüfen, ob die Adresse existiert
            Address? address = addressManager.FindAddressById(addressId);

            if (address != null) // Wenn die Adresse gefunden wurde
            {
                addressManager.RemoveAddressById(addressId); // Entferne die Adresse
                Console.WriteLine($"The address with the ID {addressId} has been removed.");
            }
            else
            {
                Console.WriteLine("Address with the given ID does not exist.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
        }
    }



    public static void FindAddressById(IAddressBook addressManager)
    {
        Console.Write("Please enter the ID to find the address: ");
        string id = Console.ReadLine();

        // Validierung der Eingabe
        if (int.TryParse(id, out int addressId))
        {
            // Überprüfen, ob die Adresse existiert
            Address? address = addressManager.FindAddressById(addressId); // Methode zum Finden der Adresse

            if (address != null)
            {
                // Adresse wurde gefunden, Informationen anzeigen
                address.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Address with the given ID does not exist.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
        }
    }


    public static void ListAddresses(IAddressBook addressManager)
    {
        var addresses = addressManager.GetAllAddresses();
        if (!addresses.Any()) // Überprüft ob Adressen vorhanden sind
        {
            Console.WriteLine("No addresses found.");
        }
        else
        {
            Console.WriteLine("ID   | First Name      | Last Name       | Street                         | City            | Postal Code | Phone Number");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            // LINQ Ausdruck, um die Adressen in der gewünschten Form zu formatieren mithilfe des Selects
            var formattedAddresses = addresses.Select(address => $"{address.Id,-4} | {address.FirstName,-15} | {address.LastName,-15} | {address.Street,-30} | {address.City,-15} | {address.PostalCode,-11} | {address.PhoneNumber,-10}");
            
            // Ausgabe aller formatierten Adressen
            foreach (var formattedAddress in formattedAddresses)
            {
                Console.WriteLine(formattedAddress);
            }
        }
    }

}
