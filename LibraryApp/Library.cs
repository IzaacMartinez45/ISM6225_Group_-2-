using System;
using System.Collections.Generic;

public class Library
{
    public List<Book> Books { get; set; }
    public List<Person> Patrons { get; set; }

    public Library()
    {
        Books = new List<Book>();
        Patrons = new List<Person>();
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in Library:");

        foreach (var book in Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Available Copies: {book.AvailableCopies}");
        }
    }

    public void DisplayPatrons()
    {
        Console.WriteLine("Patrons in Library:");

        foreach (var p in Patrons)
        {
            Console.WriteLine($"Name: {p.Name}, ID: {p.ID}");
        }
    }
}