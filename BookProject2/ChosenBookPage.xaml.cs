using BookProject2.Model.Abstract;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookProject2
{
    public sealed partial class ChosenBookPage : Page
    {
        public Book Current { get; set; }

        public ChosenBookPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Current = (Book)e.Parameter;
            Time.Text = DateTime.Now.ToString();
        }
    }
}