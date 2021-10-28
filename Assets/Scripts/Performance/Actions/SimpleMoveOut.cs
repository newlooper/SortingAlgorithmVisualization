// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Performance.Actions
{
    public class SimpleMoveOut : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var from = step.Left;
            var to   = step.Right;
            Tabernacle.Image.Enqueue( step );
            
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            await CubeController.Move( GameManager.Cubes[from], new[]
            {
                new Pace( new Vector3( to * Config.HorizontalGap, 0, Config.OutDistance ), step.Pace.MovingMaterial )
            } );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }
}