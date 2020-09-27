using BookProject2.Model.Enum;
using BookProject2.Static;
using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookProject2
{
    public sealed partial class AddBooks : Page
    {
        public MainPage MainPage { get; private set; }
        public Category Category { get; private set; } = Category.None;

        public AddBooks()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GetChosenComboBoxByName(sender))
            {
                case "kids":
                    Category = Category.kids;
                    break;

                case "ArtAndDesign":
                    Category = Category.ArtAndDesign;
                    break;

                case "Fiction":
                    Category = Category.Fiction;
                    break;
            }
        }

        // chosen
        private static string GetChosenComboBoxByName(object sender) =>
            ((ComboBoxItem)((ComboBox)sender).SelectedItem).Name;

        protected override void OnNavigatedTo(NavigationEventArgs e) =>
            MainPage = (MainPage)e.Parameter;

        private async void AddNewBook_Click(object sender, RoutedEventArgs e)
        {
            int bookCopies = 0;
            if (IsNewBookParametwersOk(ref bookCopies))
            {
                if (IsNamingConventionOk())
                {
                    if (MainPage.ClassManager.IsBookUnique(GetNewBookData(bookCopies)))
                        await InfoMessage.BookSuccessfullyAddedAsync();
                    else
                        await InfoMessage.BookAlreadyInTheCategoryAsync();

                    ResetTextBoxAndComboBox();
                }
                else
                    await InfoMessage.NamingErrorAsync();
            }
            else
                await InfoMessage.EmptyFieldErrorAsync();
        }

        private Tuple<Category, string, string, int> GetNewBookData(int booksNumber) =>
            Tuple.Create(Category, NameOfAuthor.Text, NameOfBook.Text, booksNumber);

        private bool IsNamingConventionOk() =>
            Regex.IsMatch(NameOfAuthor.Text, @"^[a-zA-Z]+$") && Regex.IsMatch(NameOfBook.Text, @"^[a-zA-Z]+$");

        private bool IsNewBookParametwersOk(ref int booksNumber)
        {
            return !(NameOfAuthor.Text == string.Empty
                     || string.IsNullOrWhiteSpace(NameOfAuthor.Text)
                     || NameOfBook.Text == string.Empty
                     || string.IsNullOrWhiteSpace(NameOfAuthor.Text)
                     || Category == Category.None)
                     && int.TryParse(NumberOfBook.Text, out booksNumber);
        }

        private void ResetTextBoxAndComboBox()
        {
            NameOfBook.Text = string.Empty;
            NameOfAuthor.Text = string.Empty;
            NumberOfBook.Text = string.Empty;
        }
    }
}