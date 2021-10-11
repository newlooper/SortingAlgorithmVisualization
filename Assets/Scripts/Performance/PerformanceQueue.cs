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

            public static Step CreateStepForSelectTwo( int left, int right )
            {
                var step = new Step
                {
                    Left = left,
                    Right = right,
                    PerformanceEffect = PerformanceEffect.SelectTwo,
                    Pace = new Pace(
                        Resources.Load<Material>( "Materials/CubeSelected" ),
                        null )
                };
                return step;
            }

            public static Step CreateStepForSwap( int[] snapshot, int left, int right )
            {
                var step = new Step
                {
                    Left = left,
                    Right = right,
                    Snapshot = snapshot,
                    PerformanceEffect = PerformanceEffect.Swap,
                    Pace = new Pace(
                        Resources.Load<Material>( "Materials/CubeSelected" ),
                        Resources.Load<Material>( "Materials/CubeInMoving" ) )
                };
                return step;
            }

            public int Left { get; set; }

            public int Right { get; set; }

            public int[] Snapshot { get; set; }

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
            JumpOut,
            JumpIn,
            SwapCopy,
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