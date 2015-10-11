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
using System.IO;

namespace Gallery
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> extensions = new List<string>();
        List<Folder> folders = new List<Folder>();
        List<Image> images = new List<Image>();
        Rectangle selectedRect;
        Folder selectedFold;
        
        public MainWindow()
        {
            InitializeComponent();

            extensions.Add(".jpg");
            extensions.Add(".jpeg");
            extensions.Add(".bmp");
            extensions.Add(".png");
            extensions.Add(".gif");
            extensions.Add(".tiff");

            combo.Items.Add("all");
            combo.Items.Add("1");
            combo.Items.Add("2");
            combo.Items.Add("3");
            combo.Items.Add("4");
            combo.Items.Add("5");

            Dictionary<DirectoryInfo, FileInfo> dictFileInfos = new Dictionary<DirectoryInfo, FileInfo>();

            DirectoryInfo dI = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            List<DirectoryInfo> DI = new List<DirectoryInfo>();
            //fSI.AddRange(dI.GetFiles().ToList());
            DI.AddRange(dI.GetDirectories().ToList());
            //MessageBox.Show(DI[0].FullName.ToString());
            //fSI.AddRange(dI.GetFiles().ToList());

            int numOfFolder = -1;
            foreach (DirectoryInfo dITmp in DI)
            {
                List<FileInfo> fSI = new List<FileInfo>();
                fSI.AddRange(dITmp.GetFiles().ToList());

                bool init = false;
                foreach(FileInfo fSITmp in fSI)
                {
                    if (extensions.Contains(fSITmp.Extension))
                    {
                        if (!init)
                        {
                            numOfFolder++;
                            init = true;
                            folders.Add(new Folder(dITmp, fSITmp));
                            //folders.Add(new Folder(dITmp));
                        }
                        else
                            folders[numOfFolder].AddImage(fSITmp);
                    }
                }
            }

            listBox.ItemsSource = folders;
            listBox.DisplayMemberPath = "NameFolder";
            listBox.SelectedValuePath = "It";

            listBox.SelectedIndex = 0;
            //MessageBox.Show("Ky");

        }

        private Image FileInfoToImage(FileInfo fI)
        {
            BitmapImage bmpImage;
            Image myImage;

            bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(fI.FullName, UriKind.Absolute);
            bmpImage.EndInit();
            myImage = new Image();
            myImage.Source = bmpImage;

            return myImage;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackWithRadio.IsEnabled = false;
            properties.IsExpanded = false;
            //images.Clear();

            //List<FileInfo> fITmp = new List<FileInfo>();

            //images.AddRange(folders.IndexOf((DirectoryInfo)(listBox.SelectedValue))
            selectedFold = (Folder)e.AddedItems[0];
            //fold.Images.Clear();
            List<FileInfo> fSI = new List<FileInfo>();
            fSI.AddRange(selectedFold.DInfo.GetFiles().ToList());

            //foreach (FileInfo fSITmp in fSI)
            //{
            //    if (extensions.Contains(fSITmp.Extension))
            //    {
            //        fold.AddImage(fSITmp);
            //    }
            //}

            wrap.Children.Clear();
            images.Clear();
            //stack2.Visibility = System.Windows.Visibility.Hidden;
            //wrap.ItemHeight = 100;
            //wrap.ItemWidth = 100;

            for (int i = 0; i < selectedFold.Images.Count; i++)
            {
                if (File.Exists(selectedFold.Images[i].FileImage.FullName))
                {
                    images.Add(FileInfoToImage(selectedFold.Images[i].FileImage));
                    //imgSc.FileImage.Width = 100;
                    //imgSc.Image.Height = 100;
                    //imgSc.Image.Margin = new Thickness(5);

                    //MessageBox.Show(imgSc.Image.IsMeasureValid.ToString());
                    //imgSc.Image.Measure(new Size(100, 100));
                    //MessageBox.Show(imgSc.Image.IsMeasureValid.ToString());
                    //MessageBox.Show(imgSc.Image.ActualWidth.ToString());

                    Rectangle rect = new Rectangle();
                    rect.Margin = new Thickness(10);

                    //rect.MinWidth = imgSc.Image.Width;
                    //rect.MinHeight = imgSc.Image.Height;

                    //rect.Width = imgSc.Image.ActualWidth;
                    //rect.Height = imgSc.Image.ActualHeight;

                    if (images[i].Source.Width >= images[i].Source.Height)
                    {
                        rect.Width = 100;
                        rect.Height = images[i].Source.Height / images[i].Source.Width * 100;
                    }
                    else
                    {
                        rect.Height = 100;
                        rect.Width = images[i].Source.Width / images[i].Source.Height * 100;
                    }

                    rect.Fill = new ImageBrush(images[i].Source);
                    rect.MouseDown += rect_MouseDown;
                    //rect.StrokeThickness = 5;
                    //rect.Stroke = Brushes.Blue;

                    wrap.Children.Add(rect);

                }

                else
                {
                    selectedFold.Images.Remove(selectedFold.Images[i]);
                    i--;
                }
            }

            wrap.Visibility = System.Windows.Visibility.Visible;
        }

        void rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedRect != null)
            {
                selectedRect.Stroke = null;
            }
            stackWithRadio.IsEnabled = true;

            properties.IsExpanded = true;
            ((Rectangle)sender).StrokeThickness = 5;
            ((Rectangle)sender).Stroke = Brushes.Blue;

            selectedRect = ((Rectangle)sender);

            int num = wrap.Children.IndexOf(selectedRect);

            if (File.Exists(selectedFold.Images[num].FileImage.FullName))
            {
                _1.IsChecked = false;
                _2.IsChecked = false;
                _3.IsChecked = false;
                _4.IsChecked = false;
                _5.IsChecked = false;

                switch (selectedFold.Images[num].Score)
                {
                    case 1: { _1.IsChecked = true; break; }
                    case 2: { _2.IsChecked = true; break; }
                    case 3: { _3.IsChecked = true; break; }
                    case 4: { _4.IsChecked = true; break; }
                    case 5: { _5.IsChecked = true; break; }
                    default: { break; }
                }

                size.Text = string.Format("{0} kB", (selectedFold.Images[num].FileImage.Length / 1024).ToString());
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(selectedFold.Images[num].FileImage.FullName, UriKind.Absolute);
                bmp.EndInit();
                extention.Text = string.Format("{0}x{1}", ((int)(bmp.Width)).ToString(), ((int)(bmp.Height)).ToString());
            }
            else
            {
                selectedFold.Images.RemoveAt(num);
                ((WrapPanel)selectedRect.Parent).Children.Remove(selectedRect);
                images.RemoveAt(num);
            }
            //if (e.ClickCount == 2)
                //MessageBox.Show("dvsv");
        }

        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            slider.Width = e.NewSize.Width - 170;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(((RadioButton)sender).Content.ToString());
            int num = wrap.Children.IndexOf(selectedRect);
            selectedFold.Images[num].Score = int.Parse(((RadioButton)sender).Content.ToString());
            
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedRect != null)
            {
                selectedRect.Stroke = null;
            }
            stackWithRadio.IsEnabled = false;
            size.Text = "";
            extention.Text = "";
            properties.IsExpanded = false;

            switch (combo.SelectedItem.ToString())
            {
                case "all":
                    {
                        foreach (UIElement UIE in wrap.Children)
                        {
                            UIE.Visibility = System.Windows.Visibility.Visible;
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < wrap.Children.Count; i++)
                        {
                            if (selectedFold.Images[i].Score == int.Parse(combo.SelectedItem.ToString()))
                            {
                                wrap.Children[i].Visibility = System.Windows.Visibility.Visible;
                            }
                            else
                                wrap.Children[i].Visibility = System.Windows.Visibility.Collapsed;
                        }
                        break;
                    }
            }
        }
    }
}