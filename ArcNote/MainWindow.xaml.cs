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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;


namespace ArcNote
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;
        }
        private void Move_MouseMove(object sender, MouseEventArgs e) //移動窗口

        {

            if (e.LeftButton == MouseButtonState.Pressed)

            {

                this.DragMove();

            }

        }
        bool boldstate = false;//加粗狀態
        private void InputRichTextBox_TextBoldEnable(object sender, EventArgs e)//啟用加粗
        {
            
            InputRichTextBox.FontWeight = FontWeights.Bold;
            boldstate = true;
        }
        private void InputRichTextBox_TextBoldDisable(object sender, EventArgs e)//禁用加粗
        {

            InputRichTextBox.FontWeight = FontWeights.Normal;
            boldstate = false;
        }
        bool italicstate = false;
        private void InputRichTextBox_TextItalicEnable(object sender, EventArgs e)//啓用意大利斜體
        {
            InputRichTextBox.FontStyle = FontStyles.Italic;
            italicstate = true;
        }
        private void InputRichTextBox_TextItalicDisable(object sender, EventArgs e)//禁用意大利斜體
        {
            InputRichTextBox.FontStyle = FontStyles.Normal;
            italicstate = false;
        }
        private void InputRichTextBox_OpenFile(object sender, RoutedEventArgs e) //打開檔案
        {
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            string fileName = "Note.xml";
            path = Environment.ExpandEnvironmentVariables(path);
            System.IO.Directory.CreateDirectory(path);
            path = System.IO.Path.Combine(path, fileName);
            if (System.IO.File.Exists(path))
            {
                TextRange range;
                FileStream File;
                range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd);
                File = new FileStream(path, FileMode.Open);
                range.Load(File, DataFormats.Xaml) ;
                File.Close();
            }
            else
            {
                TextRange range;
                FileStream file;
                range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentStart);
                file = new FileStream(path, FileMode.Create);
                range.Save(file, DataFormats.Xaml);
                MessageBox.Show("無記事檔案，現已建立佔位空檔案", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void InputRichTextBox_SaveFile(object sender, RoutedEventArgs e) //存爲檔案
        {
            if (boldstate == true)//臨時去加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Normal;
            }
            if (italicstate == true)//臨時去意大利體
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Normal;
            }
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            string fileName = "Note.xml";
            path = Environment.ExpandEnvironmentVariables(path);
            System.IO.Directory.CreateDirectory(path);
            path = System.IO.Path.Combine(path, fileName);
            TextRange range;
            FileStream file;
            range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd);
            file = new FileStream(path, FileMode.Create);
            range.Save(file, DataFormats.Xaml);
            file.Close();
            if (boldstate == true)//恢復加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Bold;
            }
            if (italicstate == true)//恢復意大利體
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Italic;
            }
            MessageBox.Show("檔案已儲存", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void InputRichTextBox_DeleteFile(object sender, RoutedEventArgs e)
        {
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            string fileName = "Note.xml";
            path = Environment.ExpandEnvironmentVariables(path);
            path = System.IO.Path.Combine(path, fileName);
            System.IO.File.Delete(path);
            path = System.IO.Path.Combine(folderName, "ArcNote");
            path = Environment.ExpandEnvironmentVariables(path);
            System.IO.Directory.Delete(path);
            MessageBox.Show("檔案已刪除", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Exit(object sender, RoutedEventArgs e) //退出
        {
            Application.Current.Shutdown();
        }
    }
}
