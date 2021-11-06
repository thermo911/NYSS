using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Notebook.Entities;


namespace Notebook
{
    public class Manager
    {

        private static Entities.Notebook _notebook = new();
        static void Main(string[] args)
        {
            string command;
            bool isRunning = true;
            PrintUsage();

            while (isRunning)
            {
                Console.Write(">> ");
                command = Console.ReadLine();
                int id;
                string[] tokens = Regex.Split(command, @"\s+");

                switch (tokens[0])
                {
                    case "new":
                        try
                        {
                            var note = EditNote();
                            _notebook.AddNote(note);
                            Console.WriteLine("Successfully created!");
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine("Invalid input!");
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "all":
                        foreach (var note in _notebook.Notes)
                            Console.WriteLine(note);
                        break;

                    case "edit":
                        if (tokens.Length != 2 || !int.TryParse(tokens[1], out id))
                        {
                            Console.WriteLine("usage: edit <id>");
                            break;
                        }

                        try
                        {
                            Note note = _notebook.GetNoteById(id);
                            EditNote(note);
                            Console.WriteLine("Successfully edited!");
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine($"note with id {id} not exists");
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine("Invalid input!");
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "print":
                        if (tokens.Length != 2 || !int.TryParse(tokens[1], out id))
                        {
                            Console.WriteLine("usage: print <id>");
                            break;
                        }

                        try
                        {
                            Note note = _notebook.GetNoteById(id);
                            PrintNote(note);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine($"note with id {id} not exists");
                        }
                        break;

                    case "delete":
                        if (tokens.Length != 2 || !int.TryParse(tokens[1], out id))
                        {
                            Console.WriteLine("usage: delete <id>");
                            break;
                        }

                        try
                        { 
                            _notebook.DeleteNote(id);
                            Console.WriteLine("Successfully deleted!");
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine($"note with id {id} not exists");
                        }

                        break;

                    case "exit":
                        isRunning = false;
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tall         - prints all existing notes");
            Console.WriteLine("\tnew         - opens editor to create new note");
            Console.WriteLine("\tedit <id>   - opens editor to edit existing note");
            Console.WriteLine("\tdelete <id> - deletes existing note");
            Console.WriteLine("\texit        - logout");
        }

        private static void PrintNote(Note note)
        {
            Console.WriteLine($"Name:        {note.Name}");
            Console.WriteLine($"Surname:     {note.Surname}");
            Console.WriteLine($"Middle name: {note.MiddleName}");
            Console.WriteLine($"Phone:       {note.Phone}");
            Console.WriteLine($"Country:     {note.Country}");
            Console.WriteLine($"Position:    {note.Position}");
            Console.WriteLine($"Company:     {note.Company}");
            Console.WriteLine($"Birthday:    {note.Birthday}");
            Console.WriteLine($"Other:       {note.Other}");
        }
        private static Note EditNote(Note note = null)
        {
            string name;
            string surname;
            string middleName;
            string phone;
            string country;
            string position;
            string company;
            string birthday;
            string other;

            bool editExists = note != null;

            Console.WriteLine(!editExists ? "Editing new note" : $"Editing note with id {note.Id}");

            Console.Write("Name*: ");
            name = Console.ReadLine();

            Console.Write("Surname*: ");
            surname = Console.ReadLine();

            Console.Write("Middle name: ");
            middleName = Console.ReadLine();

            Console.Write("Phone*: ");
            phone = Console.ReadLine();

            Console.Write("Country*: ");
            country = Console.ReadLine();

            Console.Write("Position: ");
            position = Console.ReadLine();

            Console.Write("Company: ");
            company = Console.ReadLine();

            Console.Write("Birthday: ");
            birthday = Console.ReadLine();

            Console.Write("Other: ");
            other = Console.ReadLine();


            note ??= new Note(name, surname, phone, country);

            if (editExists)
            {
                note.Name = name;
                note.Surname = surname;
                note.Phone = phone;
                note.Country = country;
            }
            
            note.MiddleName = middleName;
            note.Position = position;
            note.Company = company;
            note.Birthday = birthday;
            note.Other = other;

            return note;
        }
    }
}
