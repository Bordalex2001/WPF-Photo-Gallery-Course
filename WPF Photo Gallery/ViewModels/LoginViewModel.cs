using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
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
    internal class LoginViewModel : ViewModelBase
    {
        private static readonly DependencyProperty UsernameProperty;
        private static readonly DependencyProperty PasswordProperty;

        static LoginViewModel()
        {
            UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(LoginViewModel));
            PasswordProperty = DependencyProperty.Register("Password", typeof(SecureString), typeof(LoginViewModel));
        }

        public LoginViewModel()
        {
            //Username = user.Username;
            //Password = user.Password;
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        private RelayCommand _signInCommand;

        public ICommand SignInCommand
        {
            get
            {
                if (_signInCommand == null)
                {
                    _signInCommand = new RelayCommand(SignIn, CanSignIn);
                }
                return _signInCommand;
            }
        }

        private bool CanSignIn(object par)
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                //MessageBox.Show("Please enter an username");
                return false;
            }
            if (Password == null)
            {
                //MessageBox.Show("Please enter a password");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SignIn(object par)
        {
            if (!ValidateUser(Username, Password))
            {
                MessageBox.Show("Incorrect username or password");
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

        private bool ValidateUser(string username, SecureString password)
        {
            ObservableCollection<User> users = LoadUsersFromXml();
            return users.Any(user => user.Username == username && user.Password == password);
        }

        private RelayCommand _cancelCommand;

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
            Window loginWindow = Application.Current.MainWindow;
            loginWindow.Close();
        }

        private RelayCommand _moveToRegisterCommand;

        public ICommand MoveToRegisterCommand
        {
            get
            {
                if (_moveToRegisterCommand == null)
                {
                    _moveToRegisterCommand = new RelayCommand(MoveToRegister, CanMoveToRegister);
                }
                return _moveToRegisterCommand;
            }
        }

        private bool CanMoveToRegister(object par)
        {
            return true;
        }

        private void MoveToRegister(object par)
        {
            Registration registrationWindow = new Registration();
            Window loginWindow = Application.Current.MainWindow;
            loginWindow.Close();
            registrationWindow.Show();
        }
    }
}