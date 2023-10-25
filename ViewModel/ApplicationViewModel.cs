using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WpfApp2.Model;
using System.Windows;


namespace WpfApp2.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {

        private User userList;
        private Book bookList;
        private Book userbooklist;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Book> Books { get; set; }

        public User UserList
        {
            get { return userList; }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
                OnPropertyChanged("UserBook");
            }
        }


        public Book BookList
        {
            get { return bookList; }
            set
            {
                bookList = value;
                OnPropertyChanged("BookList");
            }
        }

        public Book UserBookList
        {
            get { return userbooklist; }
            set
            {
                userbooklist = value;
                OnPropertyChanged("UserBookList");
            }
        }



        private RelayComand addCommand;
        public RelayComand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayComand(obj =>
                  {
                      if (UserList == null || BookList == null)
                      {
                          MessageBox.Show("Пожалуйста, выберите пользователя и книгу.", "Ошибка");
                      }
                 
                      else
                      {
                          if (BookList.Count == 0) MessageBox.Show("Книги закончились. Невозможно выдать книгу.", "Ошибка");
                          else
                          {
                              UserList.UserBooks.Insert(0, BookList);
                              BookList.Count--;

                          }
                      }
                  }));
            }
        }





        private RelayComand removeCommand;
        public RelayComand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayComand(obj =>
                  {
                      if (UserList == null || UserBookList == null)
                      {
                          MessageBox.Show("Пожалуйста, выберите пользователя и книгу пользователя.", "Ошибка");
                      }
                    
                      else
                      {
                          UserBookList.Count++;
                          UserList.UserBooks.Remove(UserBookList);
                      }
                  }));
            }
        }



        public ObservableCollection<Book> SearchBooksBD
        {
            get
            {
                if (searchBook != null)
                {
                    return new ObservableCollection<Book>(Books.Where(q => q.Author.Contains(SearchBook, StringComparison.OrdinalIgnoreCase) ||
                    q.Name.Contains(SearchBook, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    return Books;
                }
            }
        }



        private string searchBook;
        public string SearchBook
        {
            get { return searchBook; }
            set
            {
                searchBook = value;
                OnPropertyChanged("SearchBook");
                OnPropertyChanged("SearchBooksBD");
            }
        }



        public ObservableCollection<Book> UserBook
        {
            get
            {

                if (UserList != null)
                {
                    return UserList.UserBooks;
                }
                else return new ObservableCollection<Book>();
            }
        }


        public ObservableCollection<User> SearchUserBD
        {
            get
            {
                if (searchUser != null)
                {
                    return new ObservableCollection<User>(Users.Where(q => q.Name.Contains(SearchUser, StringComparison.OrdinalIgnoreCase) ||
                    q.Family.Contains(SearchUser, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    return Users;
                }
            }
        }

        private string searchUser;
        public string SearchUser
        {
            get { return searchUser; }
            set
            {
                searchUser = value;
                OnPropertyChanged("SearchUser");
                OnPropertyChanged("SearchUserBD");
            }
        }



        public ApplicationViewModel()
        {

            Books = new ObservableCollection<Book>
                {
                    new Book { Author = "Лев Толстой", Name = "Война и мир",  Age = 1869, Count = 5 },
                    new Book { Author = "Лев Толстой", Name = "Война и мир2",  Age = 1869, Count = 5 },
                    new Book { Author = "Фёдор Достоевский", Name = "Преступление и наказание", Age = 1866 , Count = 10 },
                    new Book { Author = "Харпер Ли", Name = "Убить пересмешника", Age = 1960 , Count = 7 },
                    new Book { Author = "Гордость и предубеждение", Name = "Гордость и предубеждение", Age = 1813 , Count = 8 },
                };

            Users = new ObservableCollection<User>
            {
                new User { Id = 1, Name = "Иван", Family = "Иванов", UserBooks = { Books[0] } },
                new User { Id = 2, Name = "Антон", Family = "Кабачков",UserBooks = { Books[0] } },
                new User { Id = 3, Name = "Аркадий", Family = "Табунов",UserBooks = { Books[0] } },
                 new User { Id = 4, Name = "Алексей", Family = "Волков", UserBooks = { Books[0] } },
            };

          



        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }





}
