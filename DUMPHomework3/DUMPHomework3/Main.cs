using DUMPHomework3.Classes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml;
var timerHasFinished = false;
List<int> listOfCallStatus = new();
Random random = new Random();
string[] callStatusOptions = { "u tijeku", "propusten", "zavrsen" };
var submenuContact = "";
var backingUp = "";
Dictionary<Contact, List<Call>> mainDict = new();
void main()
{
    Console.Clear();
    Console.WriteLine("MENU:\n1 - Ispis svih kontakata\n2 - Dodavanje novih kontakata u imenik\n3 - Brisanje kontakata iz imenika\n4 - Editiranje preference kontakata\n5 - Upravljanje kontaktom koje otvara podmenu sa funkcionalnostima\n6 - Ispis svih poziva\n7 - Izlaz iz aplikacije");
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        switch (choice)
        {
            case 1:
                printAllContacts();
                break;
            case 2:
                addContact();
                break;
            case 3:
                deleteContact();
                break;
            case 4:
                editContactPreference();
                break;
            case 5:
                contactFunctions();
                break;
            case 6:
                printAllCals();
                break;
            case 7:
                Environment.Exit(0);
                break;
            default:
                main();
                break;
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("MENU:\n1 - Ispis svih kontakata\n2 - Dodavanje novih kontakata u imenik\n3 - Brisanje kontakata iz imenika\n4 - Editiranje preference kontakata\n5 - Upravljanje kontaktom koje otvara podmenu sa sljedecim funkcionalnostima:");
        main();
    }
}
void printAllCals()
{
    Console.Clear();
    foreach (var key in mainDict)
    {
        var kvp = key.Key;
        var numberOfContact = (int)kvp.GetType().GetProperty("NumberOfContact").GetValue(kvp);
        string nameOfContact = (string)kvp.GetType().GetProperty("NameOfContact").GetValue(kvp);
        string preferenceOfContact = (string)kvp.GetType().GetProperty("PreferenceOfContact").GetValue(kvp);
        Console.WriteLine($"Broj: {numberOfContact}, Ime: {nameOfContact}, Preferenca: {preferenceOfContact}"); 
        foreach (var call in mainDict[key.Key])
        {
            Console.WriteLine($"Datum uspostave: {call.DateOfCall}, Status: {call.StatusOfCall}");
        }
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void printAllContacts() 
{
    Console.Clear();
    Console.WriteLine("Ovo su svi vasi kontakti");
    foreach (var kvp in mainDict)
    {
        var key = kvp.Key;
        var numberOfContact = (int)key.GetType().GetProperty("NumberOfContact").GetValue(key);
        string nameOfContact = (string)key.GetType().GetProperty("NameOfContact").GetValue(key);
        string preferenceOfContact = (string)key.GetType().GetProperty("PreferenceOfContact").GetValue(key);
        Console.WriteLine($"Broj: {numberOfContact}, Ime: {nameOfContact}, Preferenca: {preferenceOfContact}");
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void addContact()
{
    Console.Clear();
    Console.WriteLine($"Unesite broj kontakta");
    string numberOfContactString = Console.ReadLine();
    if (int.TryParse(numberOfContactString, out int numberOfContact))
    {
    }
    else if (numberOfContactString == "back")
    {
        main();
    }
    else
    {
        addContact();
    }
    Console.WriteLine("Unesite ime kontakta");
    string nameOfContact = Console.ReadLine();
    Console.WriteLine("Unesite preferencu kontakta (favorit, normalan, blokiran)");
    string preferenceOfContact = Console.ReadLine();
    if (nameOfContact != "back")
    {
        Contact newKey = new Contact(numberOfContact, nameOfContact, preferenceOfContact);
        bool intPropertyExists = mainDict.Keys.Any(key => key.NumberOfContact == newKey.NumberOfContact);
        if(!intPropertyExists) 
        {
            mainDict.Add(new Contact(numberOfContact, nameOfContact, preferenceOfContact), new List<Call>());
        }
        else
        {
            addContact();
        }
    }
    main();
}
void deleteContact() 
{
    Console.Clear();
    Console.WriteLine("Ovo su svi vasi kontakti:");
    foreach (var kvp in mainDict)
    {
        var key = kvp.Key;
        var numberOfContact = (int)key.GetType().GetProperty("NumberOfContact").GetValue(key);
        string nameOfContact = (string)key.GetType().GetProperty("NameOfContact").GetValue(key);
        string preferenceOfContact = (string)key.GetType().GetProperty("PreferenceOfContact").GetValue(key);
        Console.WriteLine($"Broj: {numberOfContact}, Ime: {nameOfContact}, Preferenca: {preferenceOfContact}");
    }
    Console.WriteLine("Odaberite koji kontakt zelite izbrisati po imenu:");
    var deleteContact = Console.ReadLine();
    if (deleteContact.ToString() == "back")
    {
        main();
    }
    else
    {
        Contact keyToRemove = mainDict.Keys.FirstOrDefault(key => key.NameOfContact == deleteContact);
        mainDict.Remove(keyToRemove);
        Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
        backingUp = Console.ReadLine();
        main();
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void editContactPreference() 
{
    Console.Clear();
    Console.WriteLine("Ovo su svi vasi kontakti");
    foreach (var kvp in mainDict)
    {
        var key = kvp.Key;
        var numberOfContact = (int)key.GetType().GetProperty("NumberOfContact").GetValue(key);
        string nameOfContact = (string)key.GetType().GetProperty("NameOfContact").GetValue(key);
        string preferenceOfContact = (string)key.GetType().GetProperty("PreferenceOfContact").GetValue(key);
        Console.WriteLine($"Broj: {numberOfContact}, Ime: {nameOfContact}, Preferenca: {preferenceOfContact}");
    }
    Console.WriteLine("Odaberite kojem kontaktu zelite izmijeniti preferencu ('favorit', 'normalan', 'blokiran'):");
    var contactName = Console.ReadLine();
    if (contactName.ToString() == "back")
    {
        main();
    }
    else
    {
        foreach (var contact in mainDict.Keys)
        {
            if (contact.NameOfContact == contactName)
            {
                Console.Write("Unesite novu preferencu:");
                string newPreference = Console.ReadLine();
                contact.PreferenceOfContact = newPreference;
            }
        }
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void contactFunctions() 
{
    Console.Clear();
    Console.WriteLine("Ovo su svi vasi kontakti");
    foreach (var kvp in mainDict)
    {
        var key = kvp.Key;
        var numberOfContact = (int)key.GetType().GetProperty("NumberOfContact").GetValue(key);
        string nameOfContact = (string)key.GetType().GetProperty("NameOfContact").GetValue(key);
        string preferenceOfContact = (string)key.GetType().GetProperty("PreferenceOfContact").GetValue(key);
        Console.WriteLine($"Broj: {numberOfContact}, Ime: {nameOfContact}, Preferenca: {preferenceOfContact}");
    }
    Console.WriteLine("Odaberite kojim kontaktom zelite upravljati sa submenuom");
    submenuContact = Console.ReadLine();
    if (submenuContact.ToString() == "back")
    {
        main();
    }
    else
    {
        foreach (var contact in mainDict)
        {
            if (contact.Key.NameOfContact == submenuContact)
            {
                Console.Clear();
                Console.WriteLine("Ovo je podmenu funkcija nad tim kontaktom:");
                Console.WriteLine("1 - Ispis svih poziva sa tim kontaktom poredan od vremenski najnovijeg prema najstarijem\n2 - Kreiranje novog poziva\n3 - Izlaz iz podmenua");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            printCalls();
                            break;
                        case 2:
                            makeACall();
                            break;
                        case 3:
                            main();
                            break;
                        default:
                            main();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("MENU:\n1 - Ispis svih kontakata\n2 - Dodavanje novih kontakata u imenik\n3 - Brisanje kontakata iz imenika\n4 - Editiranje preference kontakata\n5 - Upravljanje kontaktom koje otvara podmenu sa sljedecim funkcionalnostima:");
                    main();
                }
            }
        }
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void printCalls() 
{
    foreach (var key in mainDict)
    {
        if (key.Key.NameOfContact == submenuContact)
        {
            foreach(var call in mainDict[key.Key])
            {
                Console.WriteLine($"Datum uspostave: {call.DateOfCall}, Status: {call.StatusOfCall}");
            }
        }
    }
    Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
    backingUp = Console.ReadLine();
    main();
}
void timerFinished()
{
    timerHasFinished = true;
}
void makeACall() 
{
    foreach (var key in mainDict)
    {
        if (key.Key.NameOfContact == submenuContact)
        {
            if (key.Key.PreferenceOfContact == "blokiran")
            {
                contactFunctions();
            }
            int randomStatusIndex = random.Next(callStatusOptions.Length);
            string callStatus = callStatusOptions[randomStatusIndex];
            mainDict[key.Key].Add(new Call(callStatus));
            if (callStatus == "propusten")
            {
                Console.WriteLine("Vas poziv je propusten");
                Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
                backingUp = Console.ReadLine();
                contactFunctions();
            }
            if (callStatus == "zavrsen")
            {
                Console.WriteLine("Vas poziv je zavrsen");
                Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
                backingUp = Console.ReadLine();
                contactFunctions();
            }
            else
            {
                timerHasFinished = false;
                Random random = new Random();
                int randomDuration = random.Next(1, 21) * 1000;
                Timer timer = new(state => timerFinished(), null, randomDuration, Timeout.Infinite);
                Console.WriteLine("Vas poziv je u tijeku");
                while (!timerHasFinished)
                {

                }
                Console.WriteLine("Vas poziv je zavrsio");
                Console.WriteLine("Napisite bilo sto i pritisnite tipku enter kada zelite ici natrag");
                backingUp = Console.ReadLine();
                contactFunctions();
            }
                contactFunctions();
        }
    }
}
main();