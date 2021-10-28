// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Performance.Actions
{
    public class SimpleSwap : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var cubes       = GameManager.Cubes;
            var cubeDefault = Config.DefaultCube;
            var left        = step.Left;
            var right       = step.Right;

            ////////////////////////////////
            /// 交换 List 中的元素位置
            GameManager.Cubes.Swap( left, right );
            
            CodeDictionary.AddMarkLine( step.CodeLineKey );

            ////////////////////
            /// 展示移动效果
            await CubeController.SwapTwoObjectPosition( cubes[left], cubes[right], step.Pace );

            ////////////
            /// 移动效果完成，撤销移动样式
            CubeController.SetPillarMaterial( cubes[left], cubeDefault );
            CubeController.SetPillarMaterial( cubes[right], cubeDefault );
            
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );

            Interlocked.Increment( ref CubeController.rewindIndex );
        }
    }
}