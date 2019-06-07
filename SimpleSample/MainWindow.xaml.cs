using SimpleSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleSample
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel();
            this.DataContext = vm;

            C1.Content = vm;
            SwitchTemplate("DetailsTemplate");

            // set the content , can be any type (view model, ...)
            // template is used to know how to display information that is passed to him
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            SwitchTemplate("EditTemplate");
        }

        private void SwitchTemplate(string templateKey)
        {
            var template = (DataTemplate)this.Resources[templateKey];
            C1.ContentTemplate = template;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            SwitchTemplate("DetailsTemplate");
        }
    }
}
