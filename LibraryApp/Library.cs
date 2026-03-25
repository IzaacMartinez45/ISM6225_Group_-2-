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

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void AddPatron(Person person)
    {
        Patrons.Add(person);
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in Library:");
        foreach (Book book in Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Available Copies: {book.AvailableCopies}");
        }
        Console.WriteLine();
    }

    public void DisplayPatrons()
    {
        Console.WriteLine("Patrons in Library:");
        foreach (Person patron in Patrons)
        {
            Console.WriteLine($"Name: {patron.Name}, ID: {patron.ID}");
        }
        Console.WriteLine();
    }
}