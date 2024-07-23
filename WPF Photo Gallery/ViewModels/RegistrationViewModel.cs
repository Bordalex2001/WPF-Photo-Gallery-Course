using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using WPF_Photo_Gallery.Commands;
using WPF_Photo_Gallery.Models;
using WPF_Photo_Gallery.Views;

namespace WPF_Photo_Gallery.ViewModels
{
    internal class RegistrationViewModel : ViewModelBase
    {
        private static readonly DependencyProperty UsernameProperty;
        private static readonly DependencyProperty PasswordProperty;
        private static readonly DependencyProperty ConfirmPasswordProperty;
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty SurnameProperty;
        private static readonly DependencyProperty UsersProperty;

        static RegistrationViewModel()
        {
            UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(RegistrationViewModel));
            PasswordProperty = DependencyProperty.Register("Password", typeof(SecureString), typeof(RegistrationViewModel));
            ConfirmPasswordProperty = DependencyProperty.Register("Confirm password", typeof(SecureString), typeof(RegistrationViewModel));
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(RegistrationViewModel));
            SurnameProperty = DependencyProperty.Register("Surname", typeof(string), typeof(RegistrationViewModel));
            UsersProperty = DependencyProperty.Register("Users", typeof(ObservableCollection<User>), typeof(RegistrationViewModel));
        }

        public RegistrationViewModel() 
        {
            //Users = LoadUsersFromXml();
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); (SignUpCommand as RelayCommand).RaiseCanExecuteChanged(); }
        }

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); (SignUpCommand as RelayCommand).RaiseCanExecuteChanged(); }
        }

        public SecureString ConfirmPassword
        {
            get { return (SecureString)GetValue(ConfirmPasswordProperty);}
            set { SetValue(ConfirmPasswordProperty, value); (SignUpCommand as RelayCommand).RaiseCanExecuteChanged(); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); (SignUpCommand as RelayCommand).RaiseCanExecuteChanged(); }
        }

        public string Surname
        {
            get { return (string)GetValue(SurnameProperty); }
            set { SetValue(SurnameProperty, value); (SignUpCommand as RelayCommand).RaiseCanExecuteChanged(); }
        }

        public ObservableCollection<User> Users
        {
            get { return (ObservableCollection<User>)GetValue(UsersProperty); }
            set { SetValue(UsersProperty, value); }
        }

        private RelayCommand _signUpCommand;

        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                {
                    _signUpCommand = new RelayCommand(SignUp, CanSignUp);
                }
                return _signUpCommand;
            }
        }

        private bool CanSignUp(object par)
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                //MessageBox.Show("Please enter an username");
                return false;
            }
            if (Username.Length < 6)
            {
                //MessageBox.Show("Username must have at least 6 characters and at most 50");
                return false;
            }
            if (Password == null)
            {
                //MessageBox.Show("Please enter a password");
                return false;
            }
            if (Password.Length < 8)
            {
                //MessageBox.Show("Password must have at least 8 characters and at most 32");
                return false;
            }
            if (ConfirmPassword == null)
            {
                //MessageBox.Show("Please confirm your password");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                //MessageBox.Show("Please enter a name");
                return false;
            }
            if (Name.Length < 2)
            {
                //MessageBox.Show("Name must have at least 2 characters and at most 15");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Surname))
            {
                //MessageBox.Show("Please enter an surname");
                return false;
            }
            if (Surname.Length < 3)
            {
                //MessageBox.Show("Surname must have at least 3 characters and at most 30");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SignUp(object par)
        {
            if (Username.Length > 50)
            {
                MessageBox.Show("Username must have at least 6 characters and at most 50");
            }
            if (UserExists(Username))
            {
                MessageBox.Show("User with this username already exists");
            }
            if (Password.Length > 32)
            {
                MessageBox.Show("Password must have at least 8 characters and at most 32");
            }
            if (ConfirmPassword != Password)
            {
                MessageBox.Show("Passwords does not match to confirm");
            }
            if (Name.Length > 15)
            {
                MessageBox.Show("Name must have at least 2 characters and at most 15");
            }
            if (Surname.Length > 30)
            {
                MessageBox.Show("Surname must have at least 3 characters and at most 30");
            }

            User newUser = new User
            {
                Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                Name = Name,
                Surname = Surname
            };
            
            Users.Add(newUser);
            SaveUsersToXml(Users);
            
            MessageBox.Show("User registered successfully.");
            
            OpenLoginWindow(newUser);
        }

        private void SaveUsersToXml(ObservableCollection<User> users)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (TextWriter writer = new StreamWriter("Databases/Users.xml"))
            {
                serializer.Serialize(writer, users);
            }
        }

        private ObservableCollection<User> LoadUsersFromXml()
        {
            /*if (!File.Exists("Databases/Users.xml"))
            {
                return new ObservableCollection<User>();
            }*/

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (FileStream stream = new FileStream("Databases/Users.xml", FileMode.Open))
            {
                return serializer.Deserialize(stream) as ObservableCollection<User>;
            }
        }

        private bool UserExists(string username)
        {
            ObservableCollection<User> users = LoadUsersFromXml();
            return users.Any(user => user.Username == username);
        }

        private void OpenLoginWindow(User user)
        {
            if (Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(window => window.IsActive) is Window registrationWindow)
            {
                registrationWindow.Close();
            }

            //Login loginWindow = new Login(user);
            //loginWindow.Show();
        }

        public RelayCommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(Cancel, CanCancel);
                }
                return _cancelCommand;
            }
        }

        private bool CanCancel(object par)
        {
            return true;
        }

        private void Cancel(object par) 
        {
            if (Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive) is Window registrationWindow)
            {
                registrationWindow.Close();
            }
        }
    }
}