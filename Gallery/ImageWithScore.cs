using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Gallery
{
    class ImageWithScore
    {
        int score;
        FileInfo fileImage;

        public FileInfo FileImage
        {
            get { return fileImage; }
            set { fileImage = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public ImageWithScore(FileInfo fI)
        {
            score = -1;
            fileImage = fI;
            //BitmapImage bmpImage = new BitmapImage();
            //bmpImage.BeginInit();
            //bmpImage.UriSource = new Uri(fI.FullName, UriKind.Absolute);
            //bmpImage.EndInit();

            //image = new Image();
            //image.Source = bmpImage;
        }
    }
}
