using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator SimpleMove( int from, int to, PerformanceQueue.Step step )
        {
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            Image.Enqueue( from );
            yield return Move( GameManager.Cubes[from], new[]
            {
                new PerformanceQueue.Pace( new Vector3( to * Gap, 0, -2f ), step.Pace.MovingMaterial )
            } );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator AuxiliaryBack( PerformanceQueue.Step step )
        {
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            while ( Image.TryDequeue( out var idx ) )
            {
                var cube = GameManager.Cubes[idx];
                if ( cube.transform.position.z != 0 )
                {
                    yield return Move( cube, new[]
                    {
                        new PerformanceQueue.Pace( cube.transform.position + new Vector3( 0, 0, 2f ), step.Pace.MovingMaterial )
                    } );
                }
            }

            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
            GameManager.GenObjectsFromArray( step.Snapshot );
        }
    }

    public static partial class PerformanceQueue
    {
        public partial class Step
        {
            public static Step CreateStepForPickAuxiliary( int from, int to, string key = "Pick" )
            {
                var step = new Step
                {
                    Left = from,
                    Right = to,
                    PerformanceEffect = PerformanceEffect.Auxiliary,
                    CodeLineKey = key,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForAuxiliaryBack( int[] snapshot, string key = "Copy" )
            {
                var step = new Step
                {
                    Snapshot = snapshot,
                    PerformanceEffect = PerformanceEffect.AuxiliaryBack,
                    CodeLineKey = key,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForMerge( int[] snapshot, int cursor = -1 )
            {
                var step = new Step
                {
                    Snapshot = snapshot,
                    Cursor = cursor,
                    PerformanceEffect = PerformanceEffect.MergeHistory
                };
                return step;
            }
        }
    }
}