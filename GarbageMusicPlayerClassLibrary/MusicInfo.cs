using System.Drawing;
using System.IO;

namespace GarbageMusicPlayerClassLibrary
{
    public class MusicInfo
    {
        public string path;
        public string title;
        public string comment;

        public Bitmap AlbumImage
        {
            get
            {
                return _albumImage;
            }
            set
            {
                _albumImage = value;
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
            private set
            {
                _blurredImage = value;
            }
        }

        private Bitmap _albumImage;
        private Bitmap _blurredImage;

        // Contructor
        public MusicInfo()
        {
            this.title = null;
            this.path = null;

            this.AlbumImage = null;
            this.BlurredImage = null;

            this.comment = null;
        }
        public MusicInfo(string filepath)
        {
            this.path = filepath;

            InitializeTitle();
            InitializeComment();
            InitializeAlbumImage();
        }
        public MusicInfo(string name, string filepath)
        {
            this.path = filepath;
            InitializeTitleWithDefaultName(name);
            InitializeComment();
            InitializeAlbumImage();
        }
        public MusicInfo(MusicInfo item)
        {
            this.path = item.path;

            InitializeTitle();
            InitializeComment();
            InitializeAlbumImage();
        }

        private void InitializeTitle()
        {
            TagLib.File file = TagLib.File.Create(path);
            if (file.Tag.Title != null)
            {
                this.title = file.Tag.Title;
            }
            else
            {
                string[] paths = path.Split(new char[] { '\\' });
                this.title = paths[paths.Length - 1];
            }
        }
        private void InitializeTitleWithDefaultName(string defaultName)
        {
            TagLib.File file = TagLib.File.Create(path);
            if (file.Tag.Title != null)
            {
                this.title = file.Tag.Title;
            }
            else
            {
                this.title = defaultName;
            }
        }

        private void InitializeComment()
        {
            TagLib.File file = TagLib.File.Create(path);
            if (file.Tag.Comment != null)
            {
                this.comment = file.Tag.Comment;
            }
            else
            {
                this.comment = "";
            }
        }
        private void InitializeAlbumImage()
        {
            TagLib.File file = TagLib.File.Create(path);
            if (file.Tag.Pictures.Length != 0)
            {
                this.AlbumImage = new Bitmap(Image.FromStream(new MemoryStream(file.Tag.Pictures[0].Data.Data)));
            }
        }

        ~MusicInfo()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if(_albumImage != null)
            {
                _albumImage.Dispose();
            }
            if(_blurredImage != null)
            {
                _blurredImage.Dispose();
            }
        }
    }
}
