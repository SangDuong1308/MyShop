using MyShop.BUS;
using MyShop.DTO;
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

namespace MyShop.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_Login(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;
            //MessageBox.Show($"hehe{inputUsername}, {inputPassword}");

            UserBUS userBus = new UserBUS();
            UserDTO handleApiUser = userBus.getOne(inputUsername, inputPassword);

            if (handleApiUser != null)
            {
                //MessageBox.Show("Nice try lil nig");
                bool remember = RememberMeCheckBox.IsChecked == true;
                Properties.Settings.Default.IdUser = handleApiUser.UserID;
                Properties.Settings.Default.Save();
                if (remember && inputUsername != null)
                {
                    Properties.Settings.Default.UsernameRemember = true;
                    Properties.Settings.Default.Save();
                }
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
            else
            {
                //MessageBox.Show("Invalid username or password");
                txtFailLogin.Text = "Invalid username or password";
            }

        }

        private void TextBlock_SignUp(object sender, MouseButtonEventArgs e)
        {
            SignupWindow sigupWindow = new SignupWindow();
            sigupWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.UsernameRemember)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
