using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static readonly ConcurrentQueue<GameObject> Image = new ConcurrentQueue<GameObject>();

        private static IEnumerator JumpOut( int from, int to, PerformanceQueue.Step step )
        {
            var cloneFrom = Instantiate( GameManager.Cubes[from] );
            var target    = new Vector3( to * Gap, 0, 0 ) + new Vector3( 0, 0, -1f );
            GameManager.Cubes[from].SetActive( false );
            Image.Enqueue( cloneFrom );
            yield return Move( cloneFrom, new[] {new PerformanceQueue.Pace( target, step.Pace.MovingMaterial )} );
            // yield return new WaitForSeconds( 1f / _speed.value );
        }

        private static IEnumerator JumpIn( int from, int to, PerformanceQueue.Step step )
        {
            if ( Image.TryDequeue( out var cloneFrom ) )
            {
                var target = GameManager.Cubes[to].transform.position;
                yield return Move( cloneFrom, new[] {new PerformanceQueue.Pace( target, step.Pace.MovingMaterial )} );
                Destroy( cloneFrom );
                SetPillarMaterial( GameManager.Cubes[from], Resources.Load<Material>( "Materials/CubeSelectedBlue" ) );
                GameManager.Cubes[from].SetActive( true );
                // yield return new WaitForSeconds( 1f / _speed.value );
            }
        }

        private static IEnumerator SwapCopy( int left, int right, PerformanceQueue.Step step )
        {
            var cubes      = GameManager.Cubes;
            var cubeSorted = Resources.Load<Material>( "Materials/CubeSelectedBlue" );

            //////////////////////////////
            /// 克隆本应该被移动的两个对象，该时刻的信息完全一致
            var leftClone  = Instantiate( cubes[left].gameObject, cubes[left].transform.position, Quaternion.identity );
            var rightClone = Instantiate( cubes[right].gameObject, cubes[right].transform.position, Quaternion.identity );

            ////////////////////////////
            /// 移动克隆的对象之前隐藏原始对象
            cubes[left].SetActive( false );
            cubes[right].SetActive( false );

            ////////////////////////////
            /// 交换原始对象的显示数值，虽然交换，但暂时不可见
            var tmp = step.Snapshot[left];
            cubes[left].GetComponent<CubeController>().SetValue( step.Snapshot[right] );
            cubes[right].GetComponent<CubeController>().SetValue( tmp );

            ////////////
            /// 用克隆对象展示移动效果
            Image.TryPeek( out var cloneFrom );
            _instance.StartCoroutine( Move( cloneFrom, new[]
            {
                new PerformanceQueue.Pace( cloneFrom.transform.position + new Vector3( -Gap, 0, 0 ),
                    Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            } ) );

            _instance.StartCoroutine( Move( leftClone, new[]
            {
                new PerformanceQueue.Pace( cubes[right].transform.position, step.Pace.MovingMaterial )
            } ) );
            yield return Move( rightClone, new[]
            {
                new PerformanceQueue.Pace( cubes[left].transform.position, step.Pace.MovingMaterial )
            } );

            Destroy( leftClone ); // 展示完效果就销毁
            Destroy( rightClone ); // 展示完效果就销毁

            ////////////
            /// 移动效果完成，恢复原始对象的显示
            SetPillarMaterial( cubes[left], cubeSorted );
            SetPillarMaterial( cubes[right], cubeSorted );

            cubes[left].SetActive( true );
            // cubes[right].SetActive( true );
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

            public static Step CreateStepForJumpOut( int from, int to )
            {
                var step = new Step
                {
                    Left = from,
                    Right = to,
                    PerformanceEffect = PerformanceEffect.JumpOut,
                    Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
                };
                return step;
            }

            public static Step CreateStepForJumpIn( int from, int to )
            {
                var step = new Step
                {
                    Left = from,
                    Right = to,
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