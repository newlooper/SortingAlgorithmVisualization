using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static readonly ConcurrentQueue<GameObject> Image = new ConcurrentQueue<GameObject>();

        private static IEnumerator JumpOut( int from, PerformanceQueue.Step step )
        {
            var cube   = GameManager.Cubes[from];
            var target = cube.transform.position + new Vector3( 0, 0, -1f );
            Image.Enqueue( cube );
            yield return Move( cube, new[] {new PerformanceQueue.Pace( target, step.Pace.MovingMaterial )} );
            // yield return new WaitForSeconds( DefaultDelay / _speed.value );
        }

        private static IEnumerator JumpIn( PerformanceQueue.Step step )
        {
            if ( Image.TryDequeue( out var cube ) )
            {
                var target = cube.transform.position + new Vector3( 0, 0, 1f );
                yield return Move( cube, new[] {new PerformanceQueue.Pace( target, step.Pace.MovingMaterial )} );
                SetPillarMaterial( cube, Resources.Load<Material>( "Materials/CubeSelectedBlue" ) );
                // yield return new WaitForSeconds( DefaultDelay / _speed.value );
            }
        }

        private static IEnumerator SwapCopy( int left, int right, PerformanceQueue.Step step )
        {
            var cubes      = GameManager.Cubes;
            var cubeSorted = Resources.Load<Material>( "Materials/CubeSelectedBlue" );

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );

            ////////////////////
            // 绑定移动前的固定位置
            var newLeft  = cubes[left].transform.position + new Vector3( Gap, 0, 0 );
            var newRight = cubes[right].transform.position + new Vector3( -Gap, 0, 0 );

            ////////////
            /// 展示移动效果
            var nodeA = Move( cubes[right].gameObject, new[]
            {
                new PerformanceQueue.Pace( newRight, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            } );
            var nodeB = Move( cubes[left].gameObject, new[]
            {
                new PerformanceQueue.Pace( newLeft, step.Pace.MovingMaterial )
            } );
            _instance.StartCoroutine( nodeA );
            _instance.StartCoroutine( nodeB );

            while ( nodeA.MoveNext() | nodeB.MoveNext() )
                yield return null;

            ////////////
            /// 移动效果完成，撤销移动样式
            SetPillarMaterial( cubes[left], cubeSorted );
            SetPillarMaterial( cubes[right], cubeSorted );
        }
    }

    public static partial class PerformanceQueue
    {
        public partial class Step
        {
            public static Step CreateStepForSorted( int index )
            {
                var step = new Step
                {
                    Left = index,
                    PerformanceEffect = PerformanceEffect.SelectOne,
                    Pace = new Pace( Resources.Load<Material>( "Materials/CubeSelectedBlue" ), null )
                };
                return step;
            }

            public static Step CreateStepForJumpOut( int from )
            {
                var step = new Step
                {
                    Left = from,
                    PerformanceEffect = PerformanceEffect.JumpOut,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForJumpIn()
            {
                var step = new Step
                {
                    PerformanceEffect = PerformanceEffect.JumpIn,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForSwapCopy( int[] snapshot, int left, int right )
            {
                var step = new Step
                {
                    Left = left,
                    Right = right,
                    Snapshot = snapshot,
                    PerformanceEffect = PerformanceEffect.SwapCopy,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeInMoving" ) )
                };
                return step;
            }
        }
    }
}