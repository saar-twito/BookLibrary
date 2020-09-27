using BookProject2.Model.Abstract;
using BookProject2.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookProject2.Model.Classes
{
    public class ClassManager
    {
        public ArtAndDesign ArtAndDesign { get; private set; }
        public Kid Kid { get; private set; }
        public Fiction Fiction { get; private set; }
        public Dictionary<string, Book> GlobalBooksDictionary { get; set; }

        public ClassManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            GlobalBooksDictionary = new Dictionary<string, Book>();
            ArtAndDesign = new ArtAndDesign(this);
            Fiction = new Fiction(this);
            Kid = new Kid(this);
        }

        public bool IsBookUnique(Tuple<Category, string, string, int> bundleForNewBook)
        {
            switch (bundleForNewBook.Item1)
            {
                case Category.kids:
                    return CheckBookExistence(Kid, bundleForNewBook);

                case Category.Fiction:
                    return CheckBookExistence(Fiction, bundleForNewBook);

                case Category.ArtAndDesign:
                    return CheckBookExistence(ArtAndDesign, bundleForNewBook);
            }
            return true;
        }

        // existence
        public bool CheckBookExistence(Book book, Tuple<Category, string, string, int> bundleForNewBook)
        {
            if (IsBookNameUnique(book, bundleForNewBook))
            {
                _ = book.CreateAnewBook(bundleForNewBook);
                return true;
            }
            return false;
        }

        private bool IsBookNameUnique(Book book, Tuple<Category, string, string, int> bundleForNewBook) =>
            book.PrivateObservableBook.FirstOrDefault(b => b.NameOfBook == bundleForNewBook.Item3) == null;
    }
}