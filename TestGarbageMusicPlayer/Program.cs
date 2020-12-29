using System;
using System.Threading;
using GarbageMusicPlayerClassLibrary;

namespace TestGarbageMusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryFileManager manager = DirectoryFileManager.GetInstance();
            MusicPlayer musicPlayer = MusicPlayer.GetInstance();
            int idx = 0;
            int listidx = 1;
            string inst;

            MusicTree list = manager.GetFileList(@"T:\ELx'sM\SOIM\", new MusicTree(@"SOIM"));
            MusicTree curr = list;
            list.Print();

            while (true)
            {
                inst = Console.ReadLine();
                string[] vs = inst.Split(' ');
                musicPlayer.Stop();

                try
                {
                    if (vs[0].Equals("reset"))
                    {
                        curr = list;
                        Console.Clear();
                        curr.Print();
                        continue;
                    }
                    else if(vs[0].Equals("sublist"))
                    {
                        listidx = Convert.ToInt32(vs[1]);
                        curr = curr.GetSubList(listidx);
                        Console.Clear();
                        curr.Print();
                        continue;
                    }
                    else if (vs[0].Equals("break"))
                    {
                        break;
                    }
                    else if (vs[0].Equals("play"))
                    {
                        idx = Convert.ToInt32(vs[1]);

                        musicPlayer.SetReader(curr.GetIdx(idx));
                        Console.WriteLine(curr.GetIdx(idx).name);

                        musicPlayer.Play(TimeSpan.FromSeconds(0.0));
                    }
                    else if(vs[0].Equals("volume"))
                    {
                        musicPlayer.SetVolume((float)Convert.ToInt32(vs[1]) / 100);
                    }
                }
                catch
                {
                    Console.WriteLine("Wrong input");
                }
            }
        }
    }
}
