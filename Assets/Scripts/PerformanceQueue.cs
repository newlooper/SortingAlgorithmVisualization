using System.Collections.Concurrent;
using System.Collections.Generic;

public static class PerformanceQueue
{
    public static readonly ConcurrentQueue<Step> Course  = new ConcurrentQueue<Step>();
    public static readonly Stack<Step>           Rewind = new Stack<Step>();

    public class Step
    {
        public Step( int[] snapshot, int left, int right )
        {
            Left = left;
            Right = right;
            Snapshot = snapshot;
            PerformanceEffect = PerformanceEffect.Default;
        }

        public Step( int left, int right, int[] snapshot, PerformanceEffect performanceEffect )
        {
            Left = left;
            Right = right;
            Snapshot = snapshot;
            PerformanceEffect = performanceEffect;
        }

        public int Left { get; set; }

        public int Right { get; set; }

        public int[] Snapshot { get; set; }

        public PerformanceEffect PerformanceEffect { get; set; }
    }

    public enum PerformanceEffect
    {
        Default,
        Copy,
    }
}