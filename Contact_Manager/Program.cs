using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager
{
    public class Program
    {
        private static List<Contact> contacts = new List<Contact>();

        static void Main(string[] args)
        {
            ContactsManager();
        }

        public static void ContactsManager()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine();
                Console.Write("Welcome to the Contact Manager\n" +
                              "1. Add a contact\n" +
                              "2. Remove a contact\n" +
                              "3. View all contacts\n" +
                              "4. Search for a contact\n" +
                              "5. Clear all contacts\n" +
                              "6. Exit\n" +
                              "Enter your choice (1-6): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter a new contact name: ");
                        string nameAdd = Console.ReadLine();
                        Console.Write("Enter contact category: ");
                        string categoryAdd = Console.ReadLine();

                        try
                        {
                            AddContact(nameAdd, categoryAdd);
                            Console.WriteLine("Contact was added successfully for your contact list.");
                        }
                        catch (InvalidOperationException invalidOex)
                        {
                            Console.WriteLine($"Error: {invalidOex.Message}");
                        }
                        break;

                    case "2":
                        Console.Write("Enter a contact name that you want to remove it: ");
                        string nameRemove = Console.ReadLine();

                        try
                        {
                            RemoveContact(nameRemove);
                            Console.WriteLine("Contact removed successfully.");
                        }
                        catch (KeyNotFoundException KNFex)
                        {
                            Console.WriteLine($"Error: {KNFex.Message}");
                        }
                        break;

                    case "3":
                        var allContacts = ViewAllContacts();
                        Console.WriteLine("All contacts:");
                        for (int i = 0; i < allContacts.Count; i++)
                        {
                            Console.WriteLine(allContacts[i]);
                        }
                        break;

                    case "4":
                        Console.Write("Enter contact name you want to search: ");
                        string nameSearch = Console.ReadLine();
                        string searchR = SearchContact(nameSearch);
                        Console.WriteLine(searchR);
                        break;

                    case "5":
                        ClearContacts();
                        Console.WriteLine("All contacts was cleared sucessfully.");
                        break;

                    case "6":
                        exitRequested = true;
                        Console.WriteLine("Exiting the Contact Manager.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                        break;
                }
            }
        }

        public static List<Contact> AddContact(string name, string category)
        {
            ValidateContactName(name);

            if (contacts.Any(c => c.Name == name))
                throw new InvalidOperationException("Contact already exists.");

            contacts.Add(new Contact(name, category));
            return new List<Contact>(contacts);
        }

        public static List<Contact> RemoveContact(string name)
        {
            var contact = FindContactByName(name);
            contacts.Remove(contact);
            return new List<Contact>(contacts);
        }

        public static List<Contact> ViewAllContacts() => new List<Contact>(contacts);

        public static string SearchContact(string name)
        {
            var contact = FindContactByName(name, throwIfNotFound: false);
            return contact != null ? contact.ToString() : "Contact not found.";
        }

        public static void ClearContacts() => contacts.Clear();

        private static void ValidateContactName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Contact name cannot be empty.");
        }

        private static Contact FindContactByName(string name, bool throwIfNotFound = true)
        {
            var contact = contacts.FirstOrDefault(c => c.Name == name);
            if (throwIfNotFound && contact == null)
                throw new KeyNotFoundException("Contact not found.");
            return contact;
        }
    }
}