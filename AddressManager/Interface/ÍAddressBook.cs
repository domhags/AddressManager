using AddressManager.Models;

namespace AddressManager.Interfaces
{
    internal interface IAddressBook
    {
        // Methode zum Hinzufügen einer neuen Adresse
        public void AddAddress(string firstName, string lastName, string street, string postalCode, string city, string phoneNumber);

        // Methode zum Entfernen einer Adresse basierend auf der Telefonnummer
        public void RemoveAddressById(int id);

        // Methode zum Suchen einer Adresse basierend auf dem Index
        public Address? FindAddressById(int id);

        // Methode zum Anzeigen aller Addressen IEnumerable = Sammlung von Objekten - besser zum iterieren
        public IEnumerable<Address> GetAllAddresses();
    }
}
