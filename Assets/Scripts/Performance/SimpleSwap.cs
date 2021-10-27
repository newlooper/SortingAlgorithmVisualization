// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator HighlightTwoWithIndex( int left, int right, Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeDefault  = Config.DefaultCube;
            var cubeSelected = step.Pace.SelectedMaterial;
            CodeDictionary.AddMarkLine( step.CodeLineKey );

            SetPillarMaterial( cubes[left], cubeSelected );
            SetPillarMaterial( cubes[right], cubeSelected );
            yield return new WaitForSeconds( Config.DefaultDelay / _speed.value );

            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator SwapHeapWithIndex( int left, int right, Step step )
        {
            var cubes       = GameManager.Cubes;
            var cubeDefault = Config.DefaultCube;

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );
            CompleteBinaryTree.treeNodes.Swap( left, right );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            ////////////////////
            // 绑定移动前的固定位置
            var onePos = CompleteBinaryTree.treeNodes[left].transform.position;
            var twoPos = CompleteBinaryTree.treeNodes[right].transform.position;

            var distancePillar = Math.Abs( right - left ) * Config.HorizontalGap;
            var nodeSpeed      = Vector3.Distance( onePos, twoPos ) / ( distancePillar / _speed.value );

            ////////////////////
            /// 堆节点同时移动
            var nodeA = Move( CompleteBinaryTree.treeNodes[left],
                new[] {new Pace( twoPos, step.Pace.MovingMaterial, nodeSpeed )} );
            var nodeB = Move( CompleteBinaryTree.treeNodes[right],
                new[] {new Pace( onePos, step.Pace.MovingMaterial, nodeSpeed )} );

            _instance.StartCoroutine( nodeA );
            _instance.StartCoroutine( nodeB );

            ////////////
            /// 展示移动效果
            var swap = SwapTwoObjectPosition( cubes[left], cubes[right], step.Pace );
            _instance.StartCoroutine( swap );

            while ( nodeA.MoveNext() | nodeB.MoveNext() | swap.MoveNext() )
                yield return null;

            ////////////
            /// 移动效果完成，撤销移动样式
            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
            SetPillarMaterial( CompleteBinaryTree.treeNodes[left], cubeDefault );
            SetPillarMaterial( CompleteBinaryTree.treeNodes[right], cubeDefault );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator SwapWithIndex( int left, int right, Step step )
        {
            var cubes       = GameManager.Cubes;
            var cubeDefault = Config.DefaultCube;

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            ////////////////////
            /// 展示移动效果
            yield return SwapTwoObjectPosition( cubes[left], cubes[right], step.Pace );

            ////////////
            /// 移动效果完成，撤销移动样式
            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator SwapTwoObjectPosition( GameObject one, GameObject two, Pace pace )
        {
            ////////////////////
            // 绑定移动前的固定位置
            var onePos = one.transform.position;
            var twoPos = two.transform.position;

            var moveInHigh = MoveInHigh( one, twoPos, pace );
            var moveInLow  = MoveInLow( two, onePos, pace );

            ////////////////////
            /// 分路同时移动
            _instance.StartCoroutine( moveInHigh );
            _instance.StartCoroutine( moveInLow );
            while ( moveInHigh.MoveNext() | moveInLow.MoveNext() ) yield return null;
        }

        private static IEnumerator MoveInHigh( GameObject mvObj, Vector3 target, Pace pace )
        {
            return Move( mvObj, new[]
            {
                new Pace( mvObj.transform.position + new Vector3( 0, 0, Config.HorizontalGap ), pace.MovingMaterial ),
                new Pace( target + new Vector3( 0, 0, Config.HorizontalGap ), pace.MovingMaterial ),
                new Pace( target, pace.MovingMaterial )
            } );
        }

        private static IEnumerator MoveInLow( GameObject mvObj, Vector3 target, Pace pace )
        {
            return Move( mvObj, new[]
            {
                new Pace( mvObj.transform.position + new Vector3( 0, 0, -Config.HorizontalGap ), pace.MovingMaterial ),
                new Pace( target + new Vector3( 0, 0, -Config.HorizontalGap ), pace.MovingMaterial ),
                new Pace( target, pace.MovingMaterial )
            } );
        }
    }
}