using System.Collections;
using System.Collections.Generic;

namespace GarbageMusicPlayerClassLibrary
{
    public class MusicCurrentList : MusicList, IEnumerable<MusicItem>, IEnumerator<MusicItem>
    {
        public MusicCurrentList() : base()
        {
            Current = null;
        }

        public override void Insert(MusicItem item)
        {
            base.Insert(item);
            if (Current == null)
                Current = base.GetHead();
        }

        public override void Delete(int idx)
        {
            base.Delete(idx);
            if(idx == 0)
                Current = base.GetHead();
        }

        public MusicItem GetCurrent()
        {
            return Current;
        }

        public MusicItem GetPrev()
        {
            Current = Current.prev;
            if (Current == null)
                Current = base.GetTail();
            return Current;
        }

        public MusicItem GetNext()
        {
            if (Current == null && GetHead() == null)
                return null;

            Current = Current.next;
            if (Current == null)
                Current = GetHead();
            return Current;
        }

        public MusicItem Current { get; private set; }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            if (GetNext() == null)
            {
                Reset();
                return false;
            }
            else 
                return true;
        }

        public void Reset()
        {
            Current = base.GetHead();
        }

        public void Dispose()
        {

        }

        public IEnumerator<MusicItem> GetEnumerator()
        {
            MusicItem tmp;
            for (tmp = base.GetHead(); tmp != null; tmp = tmp.next)
            {
                yield return tmp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            MusicItem tmp;
            for (tmp = base.GetHead(); tmp != null; tmp = tmp.next)
            {
                yield return tmp;
            }
        }
    }
}
