using System.Collections.Generic;

namespace GarbageMusicPlayerClassLibrary
{
    public class MusicList : List<MusicInfo>
    {
        public int Current { get; private set; }

        public MusicList()
        {
            Current = -1;
        }

        public new void Add(MusicInfo item)
        {
            base.Add(item);
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public new void Clear()
        {
            Current = -1;
            base.Clear();
        }

        public MusicInfo GetCurrent()
        {
            if (base.Count <= 0)
                Current = -1;
            if (Current < 0)
                return null;
            return base[Current];
        }

        public MusicInfo GetPrev()
        {
            if (Current <= 0)
                Current = base.Count - 1;
            else
                Current--;
            return GetCurrent();
        }

        public MusicInfo GetNext()
        {
            if (Current >= base.Count - 1)
                Current = 0;
            else
                Current++;

            return GetCurrent();
        }
    }
}
