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
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_SignUp(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;
            string inputFullname = txtFullname.Text;
            if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(inputFullname))
            {
                txtFailSignUp.Text = "Please enter all information";
            }
            else
            {

                //AesHelper aesHelper = new AesHelper();
                //string encryptedText = aesHelper.Encrypt(inputPassword);
                //string decryptedText = aesHelper.Decrypt(encryptedText);


                UserDTO userDTO = new UserDTO
                {
                    Username = inputUsername,
                    //Password = encryptedText,
                    Password = inputPassword,
                    Fullname = inputFullname,
                };


                UserBUS userBUS = new UserBUS();

                bool signupSucess = userBUS.createUser(userDTO);
                if (signupSucess)
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    txtFailSignUp.Text = "Failed to create new account";
                }
            }
        }

        private void TextBlock_Login(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}
