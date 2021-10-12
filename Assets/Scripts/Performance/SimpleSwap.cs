using System.Collections;
using UnityEngine;
using Pace = Performance.PerformanceQueue.Pace;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator HighlightTwoWithIndex( int left, int right, PerformanceQueue.Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );
            var cubeSelected = step.Pace.SelectedMaterial;

            SetPillarMaterial( cubes[left], cubeSelected );
            SetPillarMaterial( cubes[right], cubeSelected );
            yield return new WaitForSeconds( DefaultDelay / _speed.value );

            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
        }

        private static IEnumerator SwapWithIndex( int left, int right, PerformanceQueue.Step step )
        {
            var cubes       = GameManager.Cubes;
            var cubeDefault = Resources.Load<Material>( "Materials/Cube" );

            /////////////////////////////////////////////////////////////////
            /// 1、对于就地交换数值的排序算法，被排序数组的索引(也就是各个元素的位置)是不变的
            /// 2、排序算法所操作的数组，与界面上展示用的 List<GameObject> 是两个独立的集合
            /// 若在界面上移动原始对象，虽然看上去变更了顺序，但其实只是对象的 transform 变了，
            /// 而 GameManager.Cubes 中的元素位置并没有变化，随着排序算法的进行，展示效果将出现错误
            /// 要解决该问题，可以：
            /// 一、让 GameManager.Cubes 中的元素排列、界面上的对象位置排列、以及被排序数组内容时刻都保持一致，但此举势必导致大量的对象销毁和重建

            /// 二、排序时的移动效果并不通过操作原始 GameObject 展示，只要把值即将交换的两个对象进行克隆，让克隆的对象展示移动效果即可
            /// 如下所示：
            /// 克隆本应该被移动的两个对象，该时刻的信息完全一致
            var leftClone  = Instantiate( cubes[left].gameObject, cubes[left].transform.position, Quaternion.identity );
            var rightClone = Instantiate( cubes[right].gameObject, cubes[right].transform.position, Quaternion.identity );
            // Debug.Log( $"{left}[{snapshot[left]}] <-> {right}[{snapshot[right]}]" );

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
            yield return SwapTwoObjectPosition( leftClone, rightClone, step.Pace );

            Destroy( leftClone ); // 展示完效果就销毁
            Destroy( rightClone ); // 展示完效果就销毁

            ////////////
            /// 移动效果完成，恢复原始对象的显示
            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );

            cubes[left].SetActive( true );
            cubes[right].SetActive( true );
        }

        private static IEnumerator SwapTwoObjectPosition( GameObject one, GameObject two, Pace pace )
        {
            _instance.StartCoroutine( MoveInHigh( one, two.transform.position, pace ) );
            return MoveInLow( two, one.transform.position, pace );
        }

        private static IEnumerator MoveInHigh( GameObject mvObj, Vector3 target, Pace pace )
        {
            return Move( mvObj, new[]
            {
                new Pace( mvObj.transform.position + new Vector3( 0, 0, Gap ), pace.MovingMaterial ),
                new Pace( target + new Vector3( 0, 0, Gap ), pace.MovingMaterial ),
                new Pace( target, pace.MovingMaterial ),
            } );
        }

        private static IEnumerator MoveInLow( GameObject mvObj, Vector3 target, Pace pace )
        {
            return Move( mvObj, new[]
            {
                new Pace( mvObj.transform.position + new Vector3( 0, 0, -Gap ), pace.MovingMaterial ),
                new Pace( target + new Vector3( 0, 0, -Gap ), pace.MovingMaterial ),
                new Pace( target, pace.MovingMaterial ),
            } );
        }
    }
}