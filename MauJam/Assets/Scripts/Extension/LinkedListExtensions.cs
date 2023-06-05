using System.Collections.Generic;

namespace Core._Common.Extensions
{
    public static class LinkedListExtensions
    {
        public static int IndexOf<T>(this LinkedList<T> list, T item)
        {
            int count = 0;
            for (LinkedListNode<T> node = list.First; node != null; node = node.Next, count++)
            {
                if (ReferenceEquals(node.Value, item))
                    return count;
            }
            return -1;
        }
    }
}