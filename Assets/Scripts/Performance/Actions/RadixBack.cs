// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Performance.Actions
{
    public class RadixBack : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            Interlocked.Increment( ref CubeController.rewindIndex );
            
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            
            var i = 0;
            foreach ( var queue in DigitsBucket.buckets )
                while ( queue.Count > 0 && CubeController.runLevel > 0 )
                {
                    var idx  = queue.Pop();
                    var cube = GameManager.Cubes[idx];
                    await CubeController.MoveAndScale( cube,
                        new Vector3( i++ * Config.HorizontalGap, 0, 0 ),
                        new Vector3( 1, cube.GetComponent<CubeController>().Value * Config.CubeScale, 1 ),
                        step );
                }

            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
            
            GameManager.GenObjectsFromArray( step.Snapshot );
        }
    }
}