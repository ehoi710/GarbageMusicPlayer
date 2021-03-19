using System.Collections.Generic;

namespace GarbageMusicPlayerClassLibrary
{
    public class MusicList : List<MusicInfo>
    {
        private int current;

        // Constructor
        public MusicList()
        {
            current = -1;
        }

        public int GetCurrent()
        {
            if (base.Count == 0)
                current = -1;

            return current;
        }
        public void SetCurrent(int idx)
        {
            if (base.Count == 0)
                current = -1;
            else {
                if (idx > base.Count)
                    current = base.Count - 1;
                else if (idx < 0)
                    current = 0;
                else
                    current = idx;
            }
        }

        public new MusicInfo this[int index]
        {
            get
            {
                if (index < 0 || base.Count <= index)
                    return null;

                return base[index];
            }
        }
        public MusicInfo GetCurrentItem()
        {
            return this[current];
        }

        // Override function
        public new void RemoveAt(int idx)
        {
            MusicInfo delInfo = this[idx];
            delInfo.Dispose();
            base.RemoveAt(idx);
        }

        // Move
        public void MovePrev()
        {
            if (base.Count == 0) return;

            if (current == 0)
            {
                current = base.Count - 1;
            }
            else
            {
                current--;
            }
        }
        public void MoveNext()
        {
            if (base.Count == 0) return;

            if (current == base.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
        }
    }
}
