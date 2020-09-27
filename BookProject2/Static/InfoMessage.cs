using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BookProject2.Static
{
    internal static class InfoMessage
    {
        public static async Task EmptyFieldErrorAsync()
        {
            _ = await new ContentDialog()
            {
                Title = "Empty Field Error",
                Content = "You have one or more empty fields",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task BookAlreadyInTheCategoryAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Book Existence Error",
                Content = "You cant add two books with the same name in the same category",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task BookSuccessfullyAddedAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Adding Book Operation",
                Content = "A book was successfully added ✅",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task BookCopiesIsEmptyAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Inventory Error",
                Content = "No copies are available",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task UnselectedBookErrorAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Unselected Book Error",
                Content = "You must select a book in order to take it",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task NamingErrorAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Naming Error",
                Content = "The name of the book and author must to contain only alphabets",
                PrimaryButtonText = "OK"
            }.ShowAsync();
        }

        public static async Task KeyDoNotExistsErrorAsync()
        {
            _ = await new ContentDialog
            {
                Title = "Item Doesn't Exists Error",
                Content = "The item that you are looking for doesn't exists, are you sure you write it correct?",
                PrimaryButtonText = "Try again"
            }.ShowAsync();
        }
    }
}