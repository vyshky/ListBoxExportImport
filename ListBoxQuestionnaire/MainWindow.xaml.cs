using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using UserLib;

namespace ListBoxQuestionnaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public static ObservableCollection<string> _users = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            ListBoxUser.ItemsSource = _users;
            InitXmlFile();
        }

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            new AddUserForm().ShowDialog();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUser.SelectedIndex != -1)
            {
                _users.RemoveAt(ListBoxUser.SelectedIndex);
            }
        }


        private void ExportToTxt_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();

            try
            {
                using (StreamWriter sw = new StreamWriter(path + "/user.txt", false, System.Text.Encoding.Default))
                {
                    foreach (var user in _users)
                    {
                        sw.WriteLineAsync(user);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportOfTxt_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            try
            {
                using (StreamReader sw = new StreamReader(path + "/user.txt", System.Text.Encoding.Default))
                {
                    string? line = string.Empty;
                    while ((line = sw.ReadLine()) != null)
                    {
                        _users.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlDeclaration XmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(XmlDec);

            XmlElement usersRootNode = xmlDoc.CreateElement("root_users"); // создание корневого элемента root_users
            usersRootNode.SetAttribute("name", "office");          // Атрибуты для корневого элемента
            xmlDoc.AppendChild(usersRootNode);

            XmlElement userNode = xmlDoc.CreateElement("EXAMPLE_USER"); // Создание пустого юзера c атрибутом name
            userNode.SetAttribute("name", "Александр");


            XmlElement fieldName = xmlDoc.CreateElement("name");  // Создаю поля для Юзера и заполняю данными : имя, фамилия ,email , телефон
            userNode.AppendChild(fieldName);
            fieldName.InnerText = "Александр";

            XmlElement fieldSurname = xmlDoc.CreateElement("surname");
            userNode.AppendChild(fieldSurname);
            fieldSurname.InnerText = "Дедюхин";


            XmlElement fieldEmail = xmlDoc.CreateElement("email");
            userNode.AppendChild(fieldEmail);
            fieldEmail.InnerText = "alwex@rambler.ru";

            XmlElement fieldPhone = xmlDoc.CreateElement("phone");
            userNode.AppendChild(fieldPhone);
            fieldPhone.InnerText = "89090143212";

            usersRootNode.AppendChild(userNode); // запись объекта в корневой элемент
            xmlDoc.Save("users.xml");
        }

        private void ExportToXml_Click(object sender, RoutedEventArgs e)
        {
            if (_users.Count == 0)
            {
                return;
            }

            foreach (string user in _users)
            {
                AddToXml(user);
            }
        }

        private void AddToXml(string users)
        {            
            string[] user = users.Split(";");
            if (user.Length < 4) return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");
            XmlElement? xRoot = xmlDoc.DocumentElement; // получаем корневой элемент

            if (xRoot == null) return;

            // создаем новый элемент user
            XmlElement userNode = xmlDoc.CreateElement("user");
            userNode.SetAttribute("name", user[0]);

            XmlElement name = xmlDoc.CreateElement("name");
            userNode.AppendChild(name);
            name.InnerText = user[0];

            XmlElement surname = xmlDoc.CreateElement("surname");
            userNode.AppendChild(surname);
            surname.InnerText = user[1];

            XmlElement email = xmlDoc.CreateElement("email");
            userNode.AppendChild(email);
            email.InnerText = user[2];

            XmlElement phone = xmlDoc.CreateElement("phone");
            userNode.AppendChild(phone);
            phone.InnerText = user[3];

            xRoot.AppendChild(userNode);  // запись объекта "user" в корневой элемент "root_user"
            xmlDoc.Save("users.xml");     // сохранение в файл
        }

        private void ImportOfXml_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");
            XmlElement? xRoot = xmlDoc.DocumentElement; // получаем корневой элемент
            if (xRoot == null) return;
            XmlNodeList userListNode = xRoot.ChildNodes;
            string name = string.Empty;

            foreach (XmlNode node in userListNode)
            {
                string user = string.Empty;
                var fields = node.ChildNodes;
                foreach (XmlNode field in fields)
                {
                    user += field.InnerText + ";";
                }
                _users.Add(user);
            }            
        }
    }
}
