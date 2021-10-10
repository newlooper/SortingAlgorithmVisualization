using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Performance
{
    public static partial class PerformanceQueue
    {
        public static readonly ConcurrentQueue<Step> Course = new ConcurrentQueue<Step>();
        public static readonly Stack<Step>           Rewind = new Stack<Step>();

        public partial class Step
        {
            private Step()
            {
            }

            public Step( int[] snapshot, int left, int right )
            {
                Left = left;
                Right = right;
                Snapshot = snapshot;
                PerformanceEffect = snapshot == null ? PerformanceEffect.SelectTwo : PerformanceEffect.Swap;

                Pace = new Pace(
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

            public Pace Pace { get; set; }
        }

        public enum PerformanceEffect
        {
            Swap,
            SelectOne,
            SelectTwo,
            NewMin,
            ChangeSelection,
            UnSelectOne,
            Copy,
        }

        public class Pace
        {
            public Pace( Material selectedMaterial, Material movingMaterial )
            {
                SelectedMaterial = selectedMaterial;
                MovingMaterial = movingMaterial;
            }

            public Pace( Vector3 target, Material movingMaterial )
            {
                Target = target;
                MovingMaterial = movingMaterial;
            }

            public Vector3 Target { get; set; }

            public Material MovingMaterial   { get; set; }
            public Material SelectedMaterial { get; set; }
        }
    }
}