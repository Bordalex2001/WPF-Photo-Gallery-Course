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
using System.Windows.Shapes;
using WPF_Photo_Gallery.Models;
using WPF_Photo_Gallery.ViewModels;

namespace WPF_Photo_Gallery.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login() 
        { 
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        /*public Login(User user)
        {
            InitializeComponent();
            //DataContext = new LoginViewModel(user);
        }*/
    }
}