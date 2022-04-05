using System;
using System.Collections;
using System.Collections.Generic;
namespace LibraryProject
{
    public class Program
    {
        public interface IBag<T> : IEnumerable<T>
        {
            /// <summary>
            /// Add an item to the bag. <c>null</c> items are ignored.
            /// </summary>
            void Pack(T item);
            /// <summary>
            /// Remove the item from the bag at the given index.
            /// </summary>
            /// <returns>The item that was removed.</returns>
            T Unpack(int index);
        }
        /// <summary>
        ///  ILibrary : IReadOnlyCollection<Book>
        /// </summary>
        public interface ILibrary : IReadOnlyCollection<Book>
        {
            // public int Count { get; }
            /// <summary>
            /// Add a Book to the library.
            /// </summary>
            void Add(string title, string firstName, string lastName, int numberOfPages);
            /// <summary>
            /// Remove a Book from the library with the given title.
            /// </summary>
            /// <returns>The Book, or null if not found.</returns>
            Book Borrow(string title);
            /// <summary>
            /// Return a Book to the library.
            /// </summary>
            void Return(Book book);
        }
        public class Book
        {
            public string Title { get; set; }
            public string authorFirstname { get; set; }
            public string authorLastname { get; set; }
            public int numberOfpages { get; set; }
        }
        /// <summary>
        /// Library Class.
        /// </summary>
        public class Library : ILibrary
        {
            private Dictionary<string, Book> libraryBook = new Dictionary<string, Book>();
            public int Count
            {
                get
                {
                    return libraryBook.Count;
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
            public IEnumerator<Book> GetEnumerator()
            {
                return (IEnumerator<Book>)GetEnumerator();
            }
            public void printLibrary()
            {
                Dictionary<string, Book>.ValueCollection values = libraryBook.Values;
                foreach (Book element in values)
                {
                    Console.WriteLine($"Title: {element.Title},\nFirst Name: {element.authorFirstname},\nLast Name: {element.authorLastname},\nNumber Of Pages: {element.numberOfpages}.\n");
                }
            }
            public void Add(string title, string firstName, string lastName, int numberOfPages)
            {
                Book myBook = new Book();
                try
                {
                    myBook.Title = title;
                    myBook.authorFirstname = firstName;
                    myBook.authorLastname = lastName;
                    myBook.numberOfpages = numberOfPages;
                    libraryBook.Add(myBook.Title, myBook);
                }
                catch (ArgumentException e)
                {
                    myBook.Title += $"{libraryBook.Count}";
                    Console.WriteLine(e.Message);
                    Console.WriteLine($"New title has been added with a title of {myBook.Title}\n");
                    libraryBook.Add(myBook.Title, myBook);
                }
            }
            public Book Borrow(string title)
            {
                Book book;
                if (libraryBook.TryGetValue(title, out book))
                {
                    libraryBook.Remove(title);
                    return book;
                }
                else
                {
                    return null;
                }
            }
            public void Return(Book book)
            {
                libraryBook.Add(book.Title, book);
            }
        }
        public class Backpack<T> : IBag<T>
        {
            private List<T> items = new List<T>();
            public IEnumerator<T> GetEnumerator()
            {
                return (IEnumerator<T>)GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
            public void Pack(T item)
            {
                if (item != null)
                {
                    items.Add(item);
                    Console.WriteLine($"Added {item.GetType()}");
                }
            }
            public T Unpack(int index)
            {
                try
                {
                    T item = items[index];
                    items.RemoveAt(index);
                    Console.WriteLine("Removed item.");
                    return item;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Item index was not found!");
                }
                return default(T);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Library lib = new Library();
            lib.Add("Test 1", "Bashar", "Alrefae", 600);
            lib.Add("Test 2", "Osama", "Alzaghal", 322);
            lib.Add("Test 3", "Fahad", "Ahmad", 111);
            lib.Add("Test 4", "Zaid", "Borini", 456);
            lib.printLibrary();
            Book x = lib.Borrow("Test 1");
            lib.printLibrary();
            lib.Return(x);
            lib.printLibrary();
            x = lib.Borrow("Test 2");
            lib.printLibrary();
            lib.Return(x);
            lib.printLibrary();
            Backpack<Book> items = new Backpack<Book>();
            items.Pack(x);
            items.Unpack(0);
            items.Unpack(0);
        }
    }
}