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
        bool boldState = false;//加粗狀態
        private void InputRichTextBox_TextBold(object sender, EventArgs e)//加粗切換
        {
            if (boldState == false)
            {
                InputRichTextBox.FontWeight = FontWeights.Bold;
                boldState = true;
            }
            else
            {
                InputRichTextBox.FontWeight = FontWeights.Normal;
                boldState = false;
            }
        }
        bool italicState = false;//意大利體狀態
        private void InputRichTextBox_TextItalic(object sender, EventArgs e)//意大利體切換
        {
            if (italicState == false)
            {
                InputRichTextBox.FontStyle = FontStyles.Italic;
                italicState = true;
            }
            else
            {
                InputRichTextBox.FontStyle = FontStyles.Normal;
                italicState = false;
            }
        }
        private void InputRichTextBox_OpenFile(object sender, RoutedEventArgs e) //重開檔案
        {
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            string fileName = "Note.xml";
            path = Environment.ExpandEnvironmentVariables(path);
            System.IO.Directory.CreateDirectory(path);
            path = System.IO.Path.Combine(path, fileName);
            try
            {
                TextRange range;
                FileStream File;
                range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd);
                File = new FileStream(path, FileMode.Open);
                range.Load(File, DataFormats.Xaml) ;
                File.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("無檔案可供重開", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void InputRichTextBox_SaveFile(object sender, RoutedEventArgs e) //存爲檔案
        {
            if (boldState == true)//臨時去加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Normal;
            }
            if (italicState == true)//臨時去意大利體
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
            if (boldState == true)//恢復加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Bold;
            }
            if (italicState == true)//恢復意大利體
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Italic;
            }
            MessageBox.Show("檔案已儲存", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void InputRichTextBox_DeleteFile(object sender, RoutedEventArgs e)//刪除檔案
        {
            try
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
            catch (IOException)
            {
                MessageBox.Show("當前無檔案可供刪除", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Exit(object sender, RoutedEventArgs e) //退出程式
        {
            Application.Current.Shutdown();
        }
    }
}
