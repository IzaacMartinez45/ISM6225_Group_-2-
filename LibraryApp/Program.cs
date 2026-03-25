using System;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Create books
        Book book1 = new Book("The Art of Data Strategy", "Liam Reynolds", "ISBN111", 4);
        Book book2 = new Book("Business Insights with AI", "Olivia Carter", "ISBN222", 3);
        Book book3 = new Book("Analytics in Action", "Nathan Brooks", "ISBN333", 6);

        // Create students
        Student student1 = new Student("Akhil", "akhil@usf.edu", "S001", "Business Analytics", 2026);
        Student student2 = new Student("Sandeep", "sandeep@usf.edu", "S002", "Information Systems", 2025);

        // Create staff
        Staff staff1 = new Staff("Grandon Gill", "grandon@usf.edu", "ST001", "Librarian", "Library Services");

        // Add books to library
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        // Add patrons to library
        library.AddPatron(student1);
        library.AddPatron(student2);
        library.AddPatron(staff1);

        // Display initial books
        library.DisplayBooks();

        // Display patrons
        library.DisplayPatrons();

        // Borrow books
        Console.WriteLine("Borrowing Books...");

        if (book2.BorrowBook())
        {
            Console.WriteLine("Sandeep borrowed 'Business Insights with AI'");
        }
        else
        {
            Console.WriteLine("Business Insights with AI is unavailable.");
        }

        if (book3.BorrowBook())
        {
            Console.WriteLine("Akhil borrowed 'Analytics in Action'");
        }
        else
        {
            Console.WriteLine("Analytics in Action is unavailable.");
        }

        Console.WriteLine();

        // Display updated books
        Console.WriteLine("Books after borrowing:");
        library.DisplayBooks();
    }
}