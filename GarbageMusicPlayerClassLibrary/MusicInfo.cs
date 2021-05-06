using System.Drawing;
using System.IO;

namespace GarbageMusicPlayerClassLibrary
{
    public class MusicInfo
    {
        public string path;
        public string title;
        public string comment;
        public string lyrics;

        public Bitmap AlbumImage { get; set; }

        // Contructor
        public MusicInfo()
        {
            this.title = null;
            this.path = null;

            this.AlbumImage = null;

            this.comment = null;
        }
        public MusicInfo(string filepath)
        {
            this.path = filepath;

            InitializeTitle();
            InitializeComment();
            InitializeAlbumImage();
            InitializeLyrics();
        }
        public MusicInfo(string name, string filepath)
        {
            this.path = filepath;
            InitializeTitleWithDefaultName(name);
            InitializeComment();
            InitializeAlbumImage();
            InitializeLyrics();
        }
        public MusicInfo(MusicInfo item)
        {
            this.path = item.path;

            InitializeTitle();
            InitializeComment();
            InitializeAlbumImage();
            InitializeLyrics();
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
                this.comment = this.comment.Replace("\\n", "\n");
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
        private void InitializeLyrics()
        {
            TagLib.File file = TagLib.File.Create(path);
            if(file.Tag.Lyrics != null)
            {
                this.lyrics = file.Tag.Lyrics;
            }
        }
        public void Dispose()
        {
            if (AlbumImage != null)
            {
                AlbumImage.Dispose();
            }
        }

        ~MusicInfo()
        {
            this.Dispose();
        }
    }
}
