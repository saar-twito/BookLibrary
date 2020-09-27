using BookProject2.Model.Abstract;
using BookProject2.Model.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookProject2.Model.Classes
{
    public class Kid : Book
    {
        public ClassManager ClassManager { get; set; }

        public Kid(ClassManager classManager)
        {
            ClassManager = classManager;
            GetBooks();
        }

        public Kid(Category category, string nameOfAotor, string nameOfBook, int stock)
            : base(category, nameOfAotor, nameOfBook, stock)
        {
        }

        public Kid(Category category, string nameOfAotor, string nameOfBook, int stock, string imageUri)
            : base(category, nameOfAotor, nameOfBook, stock, imageUri)
        {
        }

        public override void GetBooks()
        {
            var kid1 = new Kid(Category.kids, "A", "Hello Kitty's Fabulous Things", 25, "BookImages/Child/Hello Kitty's Fabulous Things.png");
            var kid2 = new Kid(Category.kids, "B", "My First Hebrew Word Book", 32, "BookImages/Child/My First Hebrew Word Book.png");
            var kid3 = new Kid(Category.kids, "C", "Sammy Spider's First Mitzvah", 15, "BookImages/Child/Sammy Spider's First Mitzvah.png");
            var kid4 = new Kid(Category.kids, "D", "The Giving Tree", 1, "BookImages/Child/The Giving Tree.png");
            var kid5 = new Kid(Category.kids, "E", "The Recruit", 2, "BookImages/Child/The Recruit.png");

            // UI
            Task a = Task.Run(() =>
            {
                PrivateObservableBook = new ObservableCollection<Book>() { kid1, kid2, kid3, kid4, kid5 };
            });

            // Global Search
            Task b = Task.Run(() =>
            {
                ClassManager.GlobalBooksDictionary.Add(kid1.NameOfBook, kid1);
                ClassManager.GlobalBooksDictionary.Add(kid2.NameOfBook, kid2);
                ClassManager.GlobalBooksDictionary.Add(kid3.NameOfBook, kid3);
                ClassManager.GlobalBooksDictionary.Add(kid4.NameOfBook, kid4);
                ClassManager.GlobalBooksDictionary.Add(kid5.NameOfBook, kid5);
            });

            Task.WaitAll(a, b);
        }

        public override Book CreateAnewBook(Tuple<Category, string, string, int> bundleForNewBook)
        {
            var newKid = new Kid(bundleForNewBook.Item1,
                                   bundleForNewBook.Item2,
                                   bundleForNewBook.Item3,
                                   bundleForNewBook.Item4,
                                   DefultBookImage);

            AddNewBookToLists(newKid);
            return newKid;
        }

        public override void AddNewBookToLists(Book newBook)
        {
            PrivateObservableBook.Add(newBook);
            ClassManager.GlobalBooksDictionary.Add(newBook.NameOfBook, newBook);
        }
    }
}