using System.Collections.Concurrent;
using UnityEngine;

public class PerformanceQueue
{
    public static readonly ConcurrentQueue<Step> Course = new ConcurrentQueue<Step>();

    public class Step
    {
        public Step( int[] current, int left, int right )
        {
            Left = left;
            Right = right;
            Snapshot = current;
        }

        public int Left { get; set; }

        public int Right { get; set; }

        public int[] Snapshot { get; set; }
    }
}