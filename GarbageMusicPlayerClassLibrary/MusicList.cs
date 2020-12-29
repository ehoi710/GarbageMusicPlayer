namespace GarbageMusicPlayerClassLibrary
{
    public class MusicList
    {
        private MusicItem head;
        private MusicItem tail;

        private int MusicCount;

        public MusicList()
        {
            head = null;
            tail = null;

            MusicCount = 0;
        }

        public MusicItem GetItem(int index)
        {
            MusicItem tmp = head;
            for (int i = 0; i < index; i++)
            {
                if (tmp == null)
                    return null;
                tmp = tmp.next;
            }
            return tmp;
        }

        public MusicItem this[int i]
        {
            get { return GetItem(i); }
        }

        public MusicItem GetHead()
        {
            return head;
        }

        public MusicItem GetTail()
        {
            return tail;
        }

        public int GetCount()
        {
            return MusicCount;
        }

        public MusicItem Find(int idx)
        {
            if (idx >= MusicCount)
                return null;
            else
            {
                int i = 0;
                MusicItem tmp;
                for (tmp = head; i < idx; i++)
                {
                    tmp = tmp.next;
                }
                return tmp;
            }
        }

        public virtual void Insert(MusicItem item)
        {
            if (head == null)
            {
                item.prev = null;
                item.next = null;

                head = item;
                tail = item;
            }
            else
            {
                tail.next = item;

                item.prev = tail;
                item.next = null;

                tail = item;
            }

            MusicCount++;
        }

        public virtual void Delete(int idx)
        {
            MusicItem deleteItem = Find(idx);
            if (deleteItem == null)
                return;
            if (MusicCount == 1)
            {
                head = tail = null;
            }
            else if (idx == 0)
            {
                deleteItem.next.prev = null;
                head = head.next;
            }
            else if (idx == MusicCount - 1)
            {
                deleteItem.prev.next = null;
                tail = tail.prev;
            }
            else
            {
                deleteItem.next.prev = deleteItem.prev;
                deleteItem.prev.next = deleteItem.next;
            }
            MusicCount--;
        }

        public void Clear()
        {
            while(MusicCount != 0)
            {
                Delete(0);
            }
        }
    }
}
