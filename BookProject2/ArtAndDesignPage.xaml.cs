using BookProject2.Model.Abstract;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookProject2
{
    public sealed partial class ArtAndDesignPage : Page
    {
        public MainPage MainPage { get; set; }

        public ArtAndDesignPage()
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
        MainPage.ClassManager.ArtAndDesign.CurrentBook = GetChosenBook();

        private void SelectBookOnScreen() =>
            ArtAndDesignGridView.SelectedItem = GetChosenBook();

        private Book GetChosenBook() =>
            MainPage.ClassManager.ArtAndDesign.PrivateObservableBook[MainPage.BookIndex.Value];

        private void ArtAndDesignGridView_ItemClick(object sender, ItemClickEventArgs e) =>
             MainPage.ClassManager.ArtAndDesign.CurrentBook = (Book)e.ClickedItem;

        private async void TakeMe_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> bookList = MainPage.ClassManager.ArtAndDesign.PrivateObservableBook;
            await MainPage.WhenClickOnTakeMeButton(bookList, MainPage.ClassManager.ArtAndDesign.CurrentBook);
        }
    }
}