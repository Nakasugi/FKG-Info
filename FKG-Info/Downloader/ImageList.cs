using System.Collections.Generic;

namespace FKG_Info.Downloader
{
    class ImageList
    {
        private List<DownloadedImage> Images;




        public ImageList()
        {
            Images = new List<DownloadedImage>();
        }



        public void Add(DownloadedImage dwImg)
        {
            if ((dwImg == null) || (dwImg.Image == null)) return;

            Images.Add(dwImg);
        }



        public DownloadedImage Find(string fileName)
        {
            return Images.Find(img => img.Name == fileName);
        }

        public DownloadedImage Find(string fileName, bool mobile)
        {
            return Images.Find(img => (img.Name == fileName) && (img.Mobile == mobile));
        }

        public List<DownloadedImage> FindByPartName(string partName)
        {
            return Images.FindAll(img => img.Name.Contains(partName));
        }



        public void Delete(DownloadedImage dwImage)
        {
            Images.Remove(dwImage);
            dwImage.Dispose();
        }
    }
}
