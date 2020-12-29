using System;
using System.Collections.Generic;
using System.IO;

namespace GarbageMusicPlayerClassLibrary
{
    /// <summary>
    /// subList와 musicList로 구성된 파일 저장 자료구조입니다.
    /// </summary>
    public class MusicTree
    {
        readonly List<MusicTree> subList;
        readonly List<MusicItem> musicList;

        public string path;
        public string dirName;

        public bool isLoaded;

        public MusicTree(string name, string path)
        {
            this.subList = new List<MusicTree>();
            this.musicList = new List<MusicItem>();
            this.dirName = name;
            this.path = path;

            this.isLoaded = false;
        }

        public virtual MusicItem Insert(MusicItem item)
        {
            musicList.Add(item);

            return item;
        }

        public MusicTree Insert(MusicTree list)
        {
            subList.Add(list);

            return list;
        }

        public MusicTree GetSubListByPath(String path)
        {
            MusicTree Current = this;
            if (Current.path.Equals(path))
                return this;

            while(true)
            {
                if (Current.subList.Count == 0)
                    return null;
                foreach(MusicTree sub in Current.subList)
                {
                    if(path.Equals(sub.path))
                    {
                        return sub;
                    }
                    if(path.Contains(sub.path))
                    {
                        Current = sub;
                    }
                }
            }
        }

        public MusicTree GetSubList(int i)
        {
            int idx = 0;
            foreach (MusicTree item in subList)
            {
                if (idx == i) return item;
                idx++;
            }
            return null;
        }

        public List<MusicTree> GetSubList()
        {
            return subList;
        }

        public MusicItem GetMusicItem(int i)
        {
            int idx = 0;
            foreach(MusicItem item in musicList)
            {
                if (idx == i) return item;
                idx++;
            }
            return null;
        }

        public List<MusicItem> GetMusicList()
        {
            return musicList;
        }

        public MusicTree Refresh(int depth = 2)
        {
            if (depth == 0)
                return this;
            if(depth == 2)
                this.isLoaded = true;

            subList.Clear();
            musicList.Clear();

            DirectoryInfo info = new DirectoryInfo(path);

            DirectoryInfo[] dirs = info.GetDirectories();
            FileInfo[] files = info.GetFiles();

            foreach(DirectoryInfo dir in dirs)
            {
                Insert(new MusicTree(dir.Name, dir.FullName).Refresh(depth - 1));
                if (depth == 1) break;
            }
            foreach (FileInfo file in files)
            {
                if (Path.GetExtension(file.FullName).Equals(".mp3"))
                    Insert(new MusicItem(file.Name, file.FullName));
                if (depth == 1) break;
            }

            return this;
        }
    }
}
