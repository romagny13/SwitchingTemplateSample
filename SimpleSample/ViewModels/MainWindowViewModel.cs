using MvvmLib.Mvvm;
using SimpleSample.Models;

namespace SimpleSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Person person;
        public Person Person
        {
            get { return person; }
            set { SetProperty(ref person, value); }
        }

        public MainWindowViewModel()
        {
            this.Person = new Person
            {
                FirstName = "Marie",
                LastName = "Bell"
            };
        }

    }
}
