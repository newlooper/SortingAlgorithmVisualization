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

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );

            ////////////
            /// 展示移动效果
            yield return SwapTwoObjectPosition( cubes[left], cubes[right], step.Pace );

            ////////////
            /// 移动效果完成，撤销移动样式
            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
        }

        private static IEnumerator SwapTwoObjectPosition( GameObject one, GameObject two, Pace pace )
        {
            ////////////////////
            // 绑定移动前的固定位置
            var onePos = one.transform.position;
            var twoPos = two.transform.position;
            
            ////////////////////
            /// 分路同时移动
            _instance.StartCoroutine( MoveInHigh( one, twoPos, pace ) );
            return MoveInLow( two, onePos, pace );
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