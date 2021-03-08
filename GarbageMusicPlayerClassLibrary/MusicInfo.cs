using System.Drawing;
using System.IO;

namespace GarbageMusicPlayerClassLibrary
{
    /// <summary>
    /// 음악의 정보를 저장하는 자료구조입니다.
    /// </summary>
    public class MusicInfo
    {
        public string title;
        public string path;

        public string comment;

        public Bitmap AlbumImage
        {
            get 
            {
                if(
                    _albumImage == null &&
                    file.Tag.Pictures.Length != 0
                )
                {
                    _albumImage = new Bitmap(Image.FromStream(new MemoryStream(file.Tag.Pictures[0].Data.Data)));
                }

                return _albumImage;
            }
        }
        public Bitmap BlurredImage
        {
            get
            {
                if(
                    _blurredImage == null
                )
                {
                    _blurredImage = ImageController.BoxBlur(AlbumImage, 2);
                }

                return _blurredImage;
            }
        }

        private Bitmap _albumImage;
        private Bitmap _blurredImage;

        public TagLib.File file;

        public MusicInfo()
        {
            this.title = null;
            this.path = null;

            this._albumImage = null;
            this._blurredImage = null;

            comment = null;
        }
        public MusicInfo(string filepath)
        {
            file = TagLib.File.Create(filepath);

            if (file.Tag.Title != null)
                this.title = file.Tag.Title;
            else
            {
                string[] paths = filepath.Split(new char[] { '\\' });
                this.title = paths[paths.Length - 1];
            }

            if (file.Tag.Comment != null)
                this.comment = file.Tag.Comment.Replace("\\n", "\n");
            else
                this.comment = "";

            this.path = filepath;
        }
        public MusicInfo(string name, string filepath)
        {
            file = TagLib.File.Create(filepath);

            if (file.Tag.Title != null)
                this.title = file.Tag.Title;
            else
                this.title = name;

            if (file.Tag.Comment != null)
                this.comment = file.Tag.Comment.Replace("\\n", "\n"); 
            else
                this.comment = "";

            this.path = filepath;
        }
        public MusicInfo(MusicInfo item)
        {
            file = TagLib.File.Create(item.path);
            if (file.Tag.Title != null)
                this.title = file.Tag.Title;
            else
                this.title = item.title;

            if (file.Tag.Comment != null)
                this.comment = file.Tag.Comment.Replace("\\n", "\n");
            else
                this.comment = "";

            this.path = item.path;
        }

        ~MusicInfo()
        {
            this.Dispose();
        }

        public void Dispose()
        {

        }
    }
}
