using BookProject2.Model.Abstract;
using BookProject2.Model.Classes;
using BookProject2.Model.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookProject2.Model
{
    public class ArtAndDesign : Book
    {
        public ClassManager ClassManager { get; set; }

        public ArtAndDesign(ClassManager classManager)
        {
            ClassManager = classManager;
            GetBooks();
        }

        public ArtAndDesign(Category category, string nameOfAotor, string nameOfBook, int stock)
            : base(category, nameOfAotor, nameOfBook, stock)
        {
        }

        public ArtAndDesign(Category category, string nameOfAotor, string nameOfBook, int stock, string imageUri)
            : base(category, nameOfAotor, nameOfBook, stock, imageUri)
        {
        }

        public override void GetBooks()
        {
            var artAndDesign1 = new ArtAndDesign(Category.ArtAndDesign, "Saar", "100 Interiors Around the World", 5, "BookImages/Art And Design/100 Interiors Around the World.png");
            var artAndDesign2 = new ArtAndDesign(Category.ArtAndDesign, "Novin", "20th Century Photography", 3, "BookImages/Art And Design/20th Century Photography.png");
            var artAndDesign3 = new ArtAndDesign(Category.ArtAndDesign, "Ofri", "Masterpieces of British Design", 0, "BookImages/Art And Design/Masterpieces of British Design.png");
            var artAndDesign4 = new ArtAndDesign(Category.ArtAndDesign, "Lenny", "Missoni Art Colour", 10, "BookImages/Art And Design/Missoni Art Colour.png");
            var artAndDesign5 = new ArtAndDesign(Category.ArtAndDesign, "Lenny", "Movies Of The 2000'S", 0, "BookImages/Art And Design/Movies Of The 2000'S.png");

            // UI
            Task a = Task.Run(() =>
            {
                PrivateObservableBook = new ObservableCollection<Book>() { artAndDesign1, artAndDesign2, artAndDesign3, artAndDesign4, artAndDesign5 };
            });

            // Global Search
            Task b = Task.Run(() =>
            {
                ClassManager.GlobalBooksDictionary.Add(artAndDesign1.NameOfBook, artAndDesign1);
                ClassManager.GlobalBooksDictionary.Add(artAndDesign2.NameOfBook, artAndDesign2);
                ClassManager.GlobalBooksDictionary.Add(artAndDesign3.NameOfBook, artAndDesign3);
                ClassManager.GlobalBooksDictionary.Add(artAndDesign4.NameOfBook, artAndDesign4);
                ClassManager.GlobalBooksDictionary.Add(artAndDesign5.NameOfBook, artAndDesign5);
            });

            Task.WaitAll(a, b);
        }

        public override Book CreateAnewBook(Tuple<Category, string, string, int> bundleForNewBook)
        {
            var artAndDesign = new ArtAndDesign(category: bundleForNewBook.Item1,
                                                nameOfAotor: bundleForNewBook.Item2,
                                                nameOfBook: bundleForNewBook.Item3,
                                                bundleForNewBook.Item4,
                                                imageUri: DefultBookImage);
            AddNewBookToLists(artAndDesign);
            return artAndDesign;
        }

        public override void AddNewBookToLists(Book newBook)
        {
            PrivateObservableBook.Add(newBook);
            ClassManager.GlobalBooksDictionary.Add(newBook.NameOfBook, newBook);
        }
    }
}