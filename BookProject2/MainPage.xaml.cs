using BookProject2.Model.Abstract;
using BookProject2.Model.Classes;
using BookProject2.Model.Enum;
using BookProject2.Static;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BookProject2
{
    public sealed partial class MainPage : Page
    {
        public ClassManager ClassManager { get; private set; }
        public int? BookIndex { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            ClassManager = new ClassManager();
            GoToPage(typeof(HomePage));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GetChosenListBoxByName(sender))
            {
                case "ArtAndDesign":
                    GoToPage(typeof(ArtAndDesignPage));
                    break;

                case "Kid":
                    GoToPage(typeof(ChildPage));
                    break;

                case "Fiction":
                    GoToPage(typeof(FictionPage));
                    break;

                case "Home":
                    GoToPage(typeof(HomePage));
                    break;

                case "AddBooks":
                    GoToPage(typeof(AddBooks));
                    break;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) =>
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        private static string GetChosenListBoxByName(object sender) =>
            ((ListBoxItem)(sender as ListBox).SelectedItem).Name;

        private void GoToPage(Type page) =>
            _ = MyFrame.Navigate(page, this);

        public async Task WhenClickOnTakeMeButton(ObservableCollection<Book> bookList, Book bookClicked)
        {
            if (!(bookClicked is null))
            {
                Book book = bookList.FirstOrDefault(b => b.NameOfBook == bookClicked.NameOfBook);
                if (CanUserBorrowBook(book))
                {
                    book.Stock--;
                    MyFrame.Navigate(typeof(ChosenBookPage), book);
                }
                else
                    await InfoMessage.BookCopiesIsEmptyAsync();
            }
            else
                await InfoMessage.UnselectedBookErrorAsync();
        }

        private bool CanUserBorrowBook(Book book) =>
            !(book is null) && book.Stock - 1 >= 0;

        private async void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (IsArgumentsAreOk(sender, args))
            {
                if (ClassManager.GlobalBooksDictionary.ContainsKey(args.QueryText))
                {
                    Book book = ClassManager.GlobalBooksDictionary[args.QueryText];
                    switch (book.Category)
                    {
                        case Category.kids:
                            BookIndex = ClassManager.Kid.PrivateObservableBook.IndexOf(book);
                            GoToPage(typeof(ChildPage));
                            break;

                        case Category.Fiction:
                            BookIndex = ClassManager.Fiction.PrivateObservableBook.IndexOf(book);
                            GoToPage(typeof(FictionPage));
                            break;

                        case Category.ArtAndDesign:
                            BookIndex = ClassManager.ArtAndDesign.PrivateObservableBook.IndexOf(book);
                            GoToPage(typeof(ArtAndDesignPage));
                            break;
                    }
                }
                else
                    await InfoMessage.KeyDoNotExistsErrorAsync();
            }
        }

        private bool IsArgumentsAreOk(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) =>
                 !(sender is null && args.ChosenSuggestion is null) && args.QueryText != string.Empty;

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) =>
            FilterBookList();

        private void FilterBookList() =>
            SearchAutoSuggestBox.ItemsSource = ClassManager.GlobalBooksDictionary.Keys.Where(p => p.Contains(SearchAutoSuggestBox.Text, StringComparison.OrdinalIgnoreCase));
    }
}