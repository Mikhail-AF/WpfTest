using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GetMethodsFromPath (string DirectoryPath)
        {
            DirectoryInfo d = new DirectoryInfo(DirectoryPath);
            FileInfo[] Files = d.GetFiles("*.dll");

            foreach (FileInfo file in Files)
            {
                Assembly SampleAssembly = Assembly.LoadFrom(file.FullName);

                foreach (Type oType in SampleAssembly.GetTypes())
                {
                    textBox2.Text += file.Name + ": " + oType.Name + Environment.NewLine;

                    foreach (MethodInfo members in oType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic))
                        if (members.IsFamily || members.IsPublic)
                            textBox2.Text += string.Concat("    - ", members.Name, Environment.NewLine);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = textBox1.Text;
            
            if (Directory.Exists(path))
            {
                GetMethodsFromPath(textBox1.Text);
            }
            else
            {
                textBox2.Text = "Directory does not exist";
            }
        }
    }
}
