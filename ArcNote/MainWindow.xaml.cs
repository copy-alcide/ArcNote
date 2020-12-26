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
        //基本功能
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
        private void Exit(object sender, RoutedEventArgs e) //退出程式
        {
            Application.Current.Shutdown();
        }
        //字型樣式
        bool boldState = false; //加粗狀態
        private void InputRichTextBox_TextBold(object sender, EventArgs e) //加粗切換
        {
            if (boldState == false)
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Bold;
                boldState = true;
            }
            else
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Normal;
                boldState = false;
            }
        }
        bool italicState = false; //意大利體狀態
        private void InputRichTextBox_TextItalic(object sender, EventArgs e) //意大利體切換
        {
            if (italicState == false)
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Italic;
                italicState = true;
            }
            else
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Normal;
                italicState = false;
            }
        }
        //多檔案存取系統
        public int slotColor = 0;//檔案槽位色彩值
        public void SlotBlue(object sender, EventArgs e)//藍色槽
        {
            slotColor = 1;
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xEA, 0xEE));
            InputRichTextBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
            InputRichTextBox.SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x5B, 0x05));
        }
        public void SlotOrange(object sender, EventArgs e)//橙色槽
        {
            slotColor = 2;
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF,0xF1,0x83,0x37));
            InputRichTextBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
            InputRichTextBox.SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xEA, 0xEE));
        }
        public void SlotRed (object sender, EventArgs e)//紅色槽
        {
            slotColor = 3;
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF,0xF9,0x4E,0x4E));
            InputRichTextBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
            InputRichTextBox.SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF,0x80,0xC3,0x2E));
        }
        public void SlotGreen(object sender, EventArgs e)//綠色槽
        {
            slotColor = 4;
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF,0xA5,0xD3,0x55));
            InputRichTextBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF,0x00,0x00,0x00));
            InputRichTextBox.SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF,0x9B,0x21,0x21));
        }
        public void SlotBlack(object sender, EventArgs e)//黑色槽
        {
            slotColor = 5;
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF,0x00,0x00,0x00));
            InputRichTextBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            InputRichTextBox.SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF,0x95,0x95,0x95));
        }
        private void InputRichTextBox_OpenFile(object sender, RoutedEventArgs e) //重開檔案
        {
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            if (slotColor == 1)
            {
                string fileName = "SlotBlue.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 2)
            {
                string fileName = "SlotOrange.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 3)
            {
                string fileName = "SlotRed.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 4)
            {
                string fileName = "SlotGreen.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 5)
            {
                string fileName = "SlotBlack.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            try
            {
                TextRange range;
                FileStream File;
                range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd);
                File = new FileStream(path, FileMode.Open);
                if (boldState == true) //去富文本框加粗
                {
                    InputRichTextBox.Document.FontWeight = FontWeights.Normal;
                    boldState = false;
                }
                if (italicState == true) //去富文本框意大利體
                {
                    InputRichTextBox.Document.FontStyle = FontStyles.Normal;
                    italicState = false;
                }
                range.Load(File, DataFormats.Xaml);
                File.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("無檔案可供重開", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void InputRichTextBox_SaveFile(object sender, RoutedEventArgs e) //存爲檔案
        {
            if (boldState == true) //臨時去富文本框加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Normal;
            }
            if (italicState == true) //臨時去富文本框意大利體
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Normal;
            }
            string folderName = @"%Appdata%";
            string path = System.IO.Path.Combine(folderName, "ArcNote");
            path = Environment.ExpandEnvironmentVariables(path);
            System.IO.Directory.CreateDirectory(path);
            if (slotColor == 1)
            {
                string fileName = "SlotBlue.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 2)
            {
                string fileName = "SlotOrange.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 3)
            {
                string fileName = "SlotRed.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 4)
            {
                string fileName = "SlotGreen.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            if (slotColor == 5)
            {
                string fileName = "SlotBlack.xml";
                path = Environment.ExpandEnvironmentVariables(path);
                path = System.IO.Path.Combine(path, fileName);
            }
            TextRange range;
            FileStream file;
            range = new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd);
            file = new FileStream(path, FileMode.Create);
            range.Save(file, DataFormats.Xaml);
            file.Close();
            if (boldState == true) //恢復富文本框加粗
            {
                InputRichTextBox.Document.FontWeight = FontWeights.Bold;
            }
            if (italicState == true) //恢復富文本框意大利體
            {
                InputRichTextBox.Document.FontStyle = FontStyles.Italic;
            }
            MessageBox.Show("檔案已儲存", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        bool allDeleteFlag = false;
        private void AllDelete(object sender, RoutedEventArgs e)//完全刪除
        {
            MessageBoxResult result = MessageBox.Show("確實要完全刪除檔案？", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                allDeleteFlag = true;
                for (slotColor = 1; slotColor <= 5; slotColor++)
                {
                    InputRichTextBox_DeleteFile(sender, e);
                }
                slotColor = 0;
                string folderName = @"%Appdata%";
                string pathDirectory = System.IO.Path.Combine(folderName, "ArcNote");
                pathDirectory = Environment.ExpandEnvironmentVariables(pathDirectory);
                System.IO.Directory.Delete(pathDirectory);
                allDeleteFlag = false;
                MessageBox.Show("檔案已完全刪除", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void InputRichTextBox_DeleteFile(object sender, RoutedEventArgs e) //刪除檔案
        {
            try
            {
                string folderName = @"%Appdata%";
                string path = System.IO.Path.Combine(folderName, "ArcNote");
                if (slotColor == 1)
                {
                    string fileName = "SlotBlue.xml";
                    path = Environment.ExpandEnvironmentVariables(path);
                    path = System.IO.Path.Combine(path, fileName);
                }
                if (slotColor == 2)
                {
                    string fileName = "SlotOrange.xml";
                    path = Environment.ExpandEnvironmentVariables(path);
                    path = System.IO.Path.Combine(path, fileName);
                }
                if (slotColor == 3)
                {
                    string fileName = "SlotRed.xml";
                    path = Environment.ExpandEnvironmentVariables(path);
                    path = System.IO.Path.Combine(path, fileName);
                }
                if (slotColor == 4)
                {
                    string fileName = "SlotGreen.xml";
                    path = Environment.ExpandEnvironmentVariables(path);
                    path = System.IO.Path.Combine(path, fileName);
                }
                if (slotColor == 5)
                {
                    string fileName = "SlotBlack.xml";
                    path = Environment.ExpandEnvironmentVariables(path);
                    path = System.IO.Path.Combine(path, fileName);
                }
                System.IO.File.Delete(path);
                if (allDeleteFlag != true)
                {
                    MessageBox.Show("檔案已刪除", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("當前無檔案可供刪除", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
