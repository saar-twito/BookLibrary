using BookProject2.Model.Abstract;
using BookProject2.Model.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookProject2.Model.Classes
{
    public class Fiction : Book
    {
        public ClassManager ClassManager { get; set; }

        public Fiction(ClassManager classManager)
        {
            ClassManager = classManager;
            GetBooks();
        }

        public Fiction(Category category, string nameOfAotor, string nameOfBook, int stock)
            : base(category, nameOfAotor, nameOfBook, stock)
        {
        }

        public Fiction(Category category, string nameOfAotor, string nameOfBook, int stock, string imageUri)
            : base(category, nameOfAotor, nameOfBook, stock, imageUri)
        {
        }

        public override void GetBooks()
        {
            var fiction1 = new Fiction(Category.Fiction, "Svetlana", "Captivated by You", 5, "BookImages/Fiction/Captivated by You.png");
            var fiction2 = new Fiction(Category.Fiction, "Chen", "Complete Dead Sea Scrolls", 4, "BookImages/Fiction/Complete Dead Sea Scrolls.png");
            var fiction3 = new Fiction(Category.Fiction, "Sandra", "Entwined With You", 8, "BookImages/Fiction/Entwined With You.png");
            var fiction4 = new Fiction(Category.Fiction, "Roy", "Gretel and the Dark", 12, "BookImages/Fiction/Gretel and the Dark.png");
            var fiction5 = new Fiction(Category.Fiction, "Roy", "The Unfinished Symphony of You and Me", 6, "BookImages/Fiction/The Unfinished Symphony of You and Me.png");

            // UI
            Task a = Task.Run(() =>
            {
                PrivateObservableBook = new ObservableCollection<Book>() { fiction1, fiction2, fiction3, fiction4, fiction5 };
            });

            // Global Search
            Task b = Task.Run(() =>
            {
                ClassManager.GlobalBooksDictionary.Add(fiction1.NameOfBook, fiction1);
                ClassManager.GlobalBooksDictionary.Add(fiction2.NameOfBook, fiction2);
                ClassManager.GlobalBooksDictionary.Add(fiction3.NameOfBook, fiction3);
                ClassManager.GlobalBooksDictionary.Add(fiction4.NameOfBook, fiction4);
                ClassManager.GlobalBooksDictionary.Add(fiction5.NameOfBook, fiction5);
            });

            Task.WaitAll(a, b);
        }

        public override Book CreateAnewBook(Tuple<Category, string, string, int> bundleForNewBook)
        {
            var newFiction = new Fiction(bundleForNewBook.Item1,
                                         bundleForNewBook.Item2,
                                         bundleForNewBook.Item3,
                                         bundleForNewBook.Item4,
                                         DefultBookImage);

            AddNewBookToLists(newFiction);
            return newFiction;
        }

        public override void AddNewBookToLists(Book newBook)
        {
            PrivateObservableBook.Add(newBook);
            ClassManager.GlobalBooksDictionary.Add(newBook.NameOfBook, newBook);
        }
    }
}