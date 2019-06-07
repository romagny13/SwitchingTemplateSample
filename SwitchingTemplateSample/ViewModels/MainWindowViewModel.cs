using Microsoft.Win32;
using MvvmLib.Mvvm;
using SwitchingTemplateSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SwitchingTemplateSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Person person;
        public Person Person
        {
            get { return person; }
            set { SetProperty(ref person, value); }
        }

        public ICommand SelectImageCommand { get; set; }

        public MainWindowViewModel()
        {
            this.Person = new Person
            {
                FirstName = "Marie",
                LastName = "Bell"
            };

            SelectImageCommand = new RelayCommand(SelectImage);
        }

        private void SelectImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (dialog.ShowDialog() == true)
            {
                this.Person.ImagePath = dialog.FileName;                
            }
        }
    }
}
