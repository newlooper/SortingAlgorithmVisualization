using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Performance
{
    public static class PerformanceQueue
    {
        public static readonly ConcurrentQueue<Step> Course = new ConcurrentQueue<Step>();
        public static readonly Stack<Step>           Rewind = new Stack<Step>();

        public class Step
        {
            public Step( int[] snapshot, int left, int right )
            {
                Left = left;
                Right = right;
                Snapshot = snapshot;
                PerformanceEffect = snapshot == null ? PerformanceEffect.Selected : PerformanceEffect.Swap;

                Pace = new CubeController.Pace(
                    Resources.Load<Material>( "Materials/CubeSelected" ),
                    Resources.Load<Material>( "Materials/CubeInMoving" ) );
            }

            public Step( int[] snapshot, int left, int right, PerformanceEffect performanceEffect )
            {
                Left = left;
                Right = right;
                Snapshot = snapshot;
                PerformanceEffect = performanceEffect;
            }

            public int Left { get; set; }

            public int Right { get; set; }

            private int[] _snapshot;

            public int[] Snapshot
            {
                get => _snapshot;
                set
                {
                    PerformanceEffect = PerformanceEffect.Swap;
                    _snapshot = value;
                }
            }

            public PerformanceEffect PerformanceEffect { get; set; }

            public CubeController.Pace Pace { get; set; }
        }

        public enum PerformanceEffect
        {
            Swap,
            Selected,
            Copy,
        }
    }
}