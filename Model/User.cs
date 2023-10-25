using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WpfApp2.Model
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string family;
        ObservableCollection<Book> userBooks;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChanged("Family");
            }
        }
        public ObservableCollection<Book> UserBooks
        {
            get
            {
                if (userBooks == null)
                {
                    userBooks = new ObservableCollection<Book>();
                }
                return userBooks;
            }
            set
            {
                userBooks = value;
                OnPropertyChanged("UserBooks");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
