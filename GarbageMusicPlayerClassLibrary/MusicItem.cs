namespace GarbageMusicPlayerClassLibrary
{
    /// <summary>
    /// 음악의 정보를 저장하는 자료구조입니다.
    /// </summary>
    public class MusicItem
    {
        public string name;
        public string path;

        public TagLib.File file;

        public MusicItem prev;
        public MusicItem next;

        public MusicItem()
        {
            this.name = null;
            this.path = null;

            this.prev = null;
            this.next = null;
        }

        public MusicItem(string name, string path)
        {
            this.name = name;
            this.path = path;

            file = TagLib.File.Create(this.path);

            this.prev = null;
            this.next = null;
        }

        public MusicItem(MusicItem item)
        {
            this.name = item.name;
            this.path = item.path;

            file = TagLib.File.Create(this.path);

            this.prev = null;
            this.next = null;
        }
    }
}
