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

        public Bitmap AlbumImage;
        public Bitmap BlurredImage;

        public TagLib.File file;

        public MusicInfo()
        {
            this.title = null;
            this.path = null;

            comment = null;
            AlbumImage = null;
            BlurredImage = null;
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

            if (file.Tag.Pictures.Length != 0)
            {
                MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                AlbumImage = new Bitmap(Image.FromStream(ms));
            }
            else
                AlbumImage = null;
            BlurredImage = null;

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

            if (file.Tag.Pictures.Length != 0)
            {
                MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                AlbumImage = new Bitmap(Image.FromStream(ms));
            }
            else
                AlbumImage = null;
            BlurredImage = null;

            this.path = item.path;
        }
    }
}
