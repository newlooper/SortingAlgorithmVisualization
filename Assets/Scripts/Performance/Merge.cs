using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator SimpleMove( int from, int to, PerformanceQueue.Step step )
        {
            Image.Enqueue( GameManager.Cubes[from] );
            yield return Move( GameManager.Cubes[from], new[]
            {
                new PerformanceQueue.Pace( new Vector3( to * Gap, 0, -2f ), step.Pace.MovingMaterial )
            } );
        }

        private static IEnumerator AuxiliaryBack( PerformanceQueue.Step step )
        {
            while ( Image.TryDequeue( out var go ) )
            {
                yield return Move( go, new[]
                {
                    new PerformanceQueue.Pace( go.transform.position + new Vector3( 0, 0, 2f ), step.Pace.MovingMaterial )
                } );
            }

            GameManager.GenObjectsFromArray( step.Snapshot );
        }

        private static IEnumerator MergeRewind( PerformanceQueue.Step step )
        {
            GameManager.GenObjectsFromArray( step.Snapshot );
            yield return new WaitForSeconds( DefaultDelay / _speed.value );
        }
    }

    public static partial class PerformanceQueue
    {
        public partial class Step
        {
            public static Step CreateStepForPickAuxiliary( int from, int to )
            {
                var step = new Step
                {
                    Left = from,
                    Right = to,
                    PerformanceEffect = PerformanceEffect.Auxiliary,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForAuxiliaryBack( int[] snapshot )
            {
                var step = new Step
                {
                    Snapshot = snapshot,
                    PerformanceEffect = PerformanceEffect.AuxiliaryBack,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForMerge( int[] snapshot )
            {
                var step = new Step
                {
                    Snapshot = snapshot,
                    PerformanceEffect = PerformanceEffect.MergeHistory
                };
                return step;
            }
        }
    }
}