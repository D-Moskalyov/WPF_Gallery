using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gallery
{
    class Folder
    {
        DirectoryInfo dInfo;
        Folder it;
        List<ImageWithScore> images;
        string nameFolder;

        internal List<ImageWithScore> Images
        {
            get { return images; }
            set { images = value; }
        }

        internal Folder It
        {
            get { return it; }
            set { it = value; }
        }

        public string NameFolder
        {
            get { return nameFolder; }
            set { nameFolder = value; }
        }

        public DirectoryInfo DInfo
        {
            get { return dInfo; }
            set { dInfo = value; }
        }

        public Folder(DirectoryInfo dI, FileInfo fI)
        {
            dInfo = dI;
            images = new List<ImageWithScore>();
            nameFolder = dInfo.Name;
            it = this;
            images.Add(new ImageWithScore(fI));
        }

        public void AddImage(FileInfo fI)
        {
            images.Add(new ImageWithScore(fI));
        }
    }
}
