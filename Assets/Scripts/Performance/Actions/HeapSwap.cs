// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Performance.Actions
{
    public class HeapSwap : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var left        = step.Left;
            var right       = step.Right;
            var cubes       = GameManager.Cubes;
            var cubeDefault = Config.DefaultCube;

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );
            CompleteBinaryTree.treeNodes.Swap( left, right );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            
            ////////////////////
            // 绑定移动前的固定位置
            var posOne = CompleteBinaryTree.treeNodes[left].transform.position;
            var posTwo = CompleteBinaryTree.treeNodes[right].transform.position;

            var distancePillar = Math.Abs( right - left ) * Config.HorizontalGap;
            var nodeSpeed      = Vector3.Distance( posOne, posTwo ) / ( distancePillar / CubeController.speed.value );

            ////////////
            /// 展示移动效果
            await UniTask.WhenAll( CubeController.Move( CompleteBinaryTree.treeNodes[left],
                    new[] {new Pace( posTwo, step.Pace.MovingMaterial, nodeSpeed )} ),
                CubeController.Move( CompleteBinaryTree.treeNodes[right],
                    new[] {new Pace( posOne, step.Pace.MovingMaterial, nodeSpeed )} ),
                CubeController.SwapTwoObjectPosition( cubes[left], cubes[right], step.Pace )
            );

            ////////////
            /// 移动效果完成，撤销移动样式
            CubeController.SetPillarMaterial( cubes[left], cubeDefault );
            CubeController.SetPillarMaterial( cubes[right], cubeDefault );
            CubeController.SetPillarMaterial( CompleteBinaryTree.treeNodes[left], cubeDefault );
            CubeController.SetPillarMaterial( CompleteBinaryTree.treeNodes[right], cubeDefault );
            
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );

            Interlocked.Increment( ref CubeController.rewindIndex );
        }
    }
}