using BookProject2.Model.Abstract;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookProject2
{
    public sealed partial class ChildPage : Page
    {
        private MainPage MainPage { get; set; }

        public ChildPage()
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
      MainPage.ClassManager.Kid.CurrentBook = GetChosenBook();

        private void SelectBookOnScreen() =>
            KidsGridView.SelectedItem = GetChosenBook();

        private Book GetChosenBook() =>
            MainPage.ClassManager.Kid.PrivateObservableBook[MainPage.BookIndex.Value];

        private void MyGridView_ItemClick(object sender, ItemClickEventArgs e) =>
              MainPage.ClassManager.Kid.CurrentBook = (Book)e.ClickedItem;

        private async void TakeMe_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> bookList = MainPage.ClassManager.Kid.PrivateObservableBook;
            await MainPage.WhenClickOnTakeMeButton(bookList, MainPage.ClassManager.Kid.CurrentBook);
        }
    }
}