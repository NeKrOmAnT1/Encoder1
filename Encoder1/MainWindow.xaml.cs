using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Crypto.AES;
using System.IO;

namespace Encoder1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn_process_Click(object sender, RoutedEventArgs e)
        {
            AES aes = new AES(tb_key.Text);
            if (rb_crypt.IsChecked.Value)
            {
                tb_result.Text = aes.Encrypt(tb_content.Text);
            }
            else if (rb_decript.IsChecked.Value)
            {
                tb_result.Text = aes.Decrypt(tb_content.Text);
            }
            aes.Dispose();


        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Shifr shifr = new Shifr
            {
                Key = tb_key.Text,
                Content = tb_content.Text,
                Result = tb_result.Text
            };
            tb_key.Text = shifr.Key;
            tb_content.Text = shifr.Content;
            tb_result.Text = shifr.Result;
            string json = JsonSerializer.Serialize<Shifr>(shifr);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Json files(*.json)|*.json|All files(*.*)|*.*";
            saveFile.ShowDialog();
            if (string.IsNullOrEmpty(saveFile.FileName))
            {
                return;
            }
            File.WriteAllText(saveFile.FileName, json);
        }
        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Json files(*.json)|*.json|All files(*.*)|*.*";
            openFile.ShowDialog();
            string json1 = File.ReadAllText(openFile.FileName);
            if (string.IsNullOrEmpty(openFile.FileName))
            {
                return;
            }
            Shifr shifr = JsonSerializer.Deserialize<Shifr>(json1);
            tb_key.Text = shifr.Key;
            tb_content.Text = shifr.Content;
            tb_result.Text = shifr.Result;


        }
    }
}

