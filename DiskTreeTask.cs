using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public static List<string> Solve(List<string> input)
        {
            var tree = ContainTree(input);
            var treeInOrder = GetTreeInOrder(tree);
            var d = GetListNamesDirectories(treeInOrder);
            return GetListNamesDirectories(treeInOrder);
        }

        private static List<string> GetListNamesDirectories(List<DirectoryTree> treeInOrder)
        {
            return treeInOrder.Select(t => new string(' ', t.Level) + t.Name).ToList();
        }


        private static List<DirectoryTree> GetTreeInOrder(DirectoryTree tree)
        {
            var list = new List<DirectoryTree>();
            if (tree.Branches.Count == 0)
                return list;
            var stack = new Stack<DirectoryTree>();
            foreach (var item in tree.Branches.OrderByDescending(t => t.Name, StringComparer.Ordinal))
                stack.Push(item);

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                list.Add(current);
                if (current.Branches.Count == 0)
                    continue;
                foreach (var item in current.Branches.OrderByDescending(t => t.Name, StringComparer.Ordinal))
                    stack.Push(item);
            }
            return list;
        }

        private static DirectoryTree ContainTree(List<string> input)
        {
            var tree = new DirectoryTree();
            foreach (var array in input.Select(s => s.Split('\\')))
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (i == 0)
                    {
                        tree.Add(array[i]);
                        continue;
                    }
                    var currentTree = tree.FirstOrDefault(b => b.Path == string.Join("\\", array.Take(i).ToArray())+ "\\");
                    if (currentTree != null)
                        currentTree.Add(array[i]);
                }
            }
            return tree;
        }

        private static DirectoryTree FindTree(string[] array, DirectoryTree tree)
        {
            for (var i = 0; i < array.Length; i++)
            {
                foreach (var item in tree.Where(t => t.Level == i))
                {
                    if (item.Name == array[i])
                    {
                        if (FindTree(array.Skip(1).ToArray(), item) == null)
                            return item;
                        FindTree(array.Skip(1).ToArray(), item);
                    }
                }
            }
            return null;
        }
    }
}