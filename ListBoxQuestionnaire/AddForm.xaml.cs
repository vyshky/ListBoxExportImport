using System.Windows;
using UserLib;

namespace ListBoxQuestionnaire
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddUserForm : Window
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                FirstName = TextFirstName.Text,
                LastName = TextLastName.Text,
                Email = TextEmail.Text,
                Phone = TextPhone.Text
            };
            MainWindow._users.Add(user.ToString());
            MessageBox.Show($"Вы добавили :\n{TextFirstName.Text}\n{TextLastName.Text}\n{TextEmail.Text}\n{TextPhone.Text}");
        }

    }
}
