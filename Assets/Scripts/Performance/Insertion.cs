// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator JumpOut( int from, Step step )
        {
            var cube   = GameManager.Cubes[from];
            var target = cube.transform.position + new Vector3( 0, 0, -1f );
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            yield return Move( cube, new[] {new Pace( target, step.Pace.MovingMaterial )} );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
            // yield return new WaitForSeconds( DefaultDelay / _speed.value );
        }

        private static IEnumerator JumpIn( Step step )
        {
            var cube   = GameManager.Cubes[step.Left];
            var target = new Vector3( step.Left * Config.HorizontalGap, 0, 0f );
            yield return Move( cube, new[] {new Pace( target, step.Pace.MovingMaterial )} );
            SetPillarMaterial( cube, Resources.Load<Material>( "Materials/CubeSelectedBlue" ) );
            // yield return new WaitForSeconds( DefaultDelay / _speed.value );
        }

        private static IEnumerator SwapRelay( int left, int right, Step step )
        {
            var cubes      = GameManager.Cubes;
            var cubeSorted = Resources.Load<Material>( "Materials/CubeSelectedBlue" );

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );

            ////////////////////
            // 绑定移动前的固定位置
            var newLeft  = cubes[left].transform.position + new Vector3( Config.HorizontalGap, 0, 0 );
            var newRight = cubes[right].transform.position + new Vector3( -Config.HorizontalGap, 0, 0 );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            ////////////
            /// 展示移动效果
            var nodeA = Move( cubes[right].gameObject, new[]
            {
                new Pace( newRight, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            } );
            var nodeB = Move( cubes[left].gameObject, new[]
            {
                new Pace( newLeft, step.Pace.MovingMaterial )
            } );
            _instance.StartCoroutine( nodeA );
            _instance.StartCoroutine( nodeB );

            while ( nodeA.MoveNext() | nodeB.MoveNext() )
                yield return null;

            ////////////
            /// 移动效果完成，撤销移动样式
            SetPillarMaterial( cubes[left], cubeSorted );
            SetPillarMaterial( cubes[right], cubeSorted );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }

    public partial class Step
    {
        public static Step CreateStepForCompare( int index, string key = "Compare" )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.SelectOne,
                CodeLineKey = key,
                Pace = new Pace(
                    Resources.Load<Material>( "Materials/CubeSelected" ),
                    Resources.Load<Material>( "Materials/CubeSelectedBlue" ) )
            };
            return step;
        }

        public static Step CreateStepForJumpOut( int from, string key = "For" )
        {
            var step = new Step
            {
                Left = from,
                PerformanceEffect = PerformanceEffect.JumpOut,
                CodeLineKey = key,
                Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            };
            return step;
        }

        public static Step CreateStepForJumpIn( int insert )
        {
            var step = new Step
            {
                Left = insert,
                PerformanceEffect = PerformanceEffect.JumpIn,
                Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            };
            return step;
        }

        public static Step CreateStepForSwapRelay( int[] snapshot, int left, int right, string key = "Swap" )
        {
            var step = new Step
            {
                Left = left,
                Right = right,
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.SwapRelay,
                CodeLineKey = key,
                Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeInMoving" ) )
            };
            return step;
        }
    }
}