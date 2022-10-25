using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DirectoryTree : IEnumerable<DirectoryTree>
    {
        public readonly List<DirectoryTree> Branches = new List<DirectoryTree>();
        public int Level;
        public string Name;
        public string Path = "";

        public DirectoryTree(string key)
        {
            Name = "ROOT";
            Level = -1;
            Branches.Add(new DirectoryTree
            {
                Name = key,
                Level = 0,
                Path = key + "\\"
            });
        }

        public DirectoryTree()
        {
            Name = "ROOT";
            Level = -1;
        }

        public void Add(string key)
        {
            if (Branches.Any(b => b.Name == key))
                return;

            Branches.Add(new DirectoryTree
            {
                Name = key,
                Level = Level + 1,
                Path = Path + key + "\\"
            });
        }

        public IEnumerator<DirectoryTree> GetEnumerator()
        {
            var stack = new Stack<DirectoryTree>();
            foreach (var item in Branches)
                stack.Push(item);

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                yield return current;

                foreach (var directoryTree in current.Branches)
                    stack.Push(directoryTree);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}