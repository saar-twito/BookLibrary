using BookProject2.Model.Enum;
using System;
using System.Collections.ObjectModel;

namespace BookProject2.Model.Abstract
{
    public abstract class Book
    {
        public Category Category { get; private set; }
        public ObservableCollection<Book> PrivateObservableBook { get; set; }
        public Book CurrentBook { get; set; }
        public string DefultBookImage { get => "BookImages/book.png"; }
        public string ImageOfBook { get; set; }
        public string NameOfBook { get; set; }
        public string NameOfAuthor { get; set; }
        public int Stock { get; set; }

        public Book()
        {
        }

        public Book(Category category, string nameOfAotor, string nameOfBook, int stock)
        {
            NameOfAuthor = nameOfAotor;
            NameOfBook = nameOfBook;
            Stock = stock;
            Category = category;
        }

        public Book(Category category, string nameOfAotor, string nameOfBook, int stock, string imageUri)
            : this(category, nameOfAotor, nameOfBook, stock)
        {
            ImageOfBook = imageUri;
        }

        public abstract Book CreateAnewBook(Tuple<Category, string, string, int> person);

        public abstract void GetBooks();

        public abstract void AddNewBookToLists(Book newBook);
    }
}