using BookProject2.Model.Abstract;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookProject2
{
    public sealed partial class FictionPage : Page
    {
        public MainPage MainPage { get; private set; }

        public FictionPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage = (MainPage)e.Parameter;
            if (MainPage.BookIndex.HasValue)
            {
                SelectBookOnScreen();
                AssignChosenBookToCurrentBook();
            }
        }

        private void AssignChosenBookToCurrentBook() =>
            MainPage.ClassManager.Fiction.CurrentBook = GetChosenBook();

        private void SelectBookOnScreen() =>
            FictionGridView.SelectedItem = GetChosenBook();

        private Book GetChosenBook() =>
            MainPage.ClassManager.Fiction.PrivateObservableBook[MainPage.BookIndex.Value];

        private void FictionGridView_ItemClick(object sender, ItemClickEventArgs e) =>
            MainPage.ClassManager.Fiction.CurrentBook = (Book)e.ClickedItem;

        private async void TakeMe_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> bookList = MainPage.ClassManager.Fiction.PrivateObservableBook;
            await MainPage.WhenClickOnTakeMeButton(bookList, MainPage.ClassManager.Fiction.CurrentBook);
        }
    }
}