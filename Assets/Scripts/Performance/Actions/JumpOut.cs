// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Performance.Actions
{
    public class JumpOut : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var cube   = GameManager.Cubes[step.Left];
            var target = cube.transform.position + new Vector3( 0, 0, -1f );
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            await CubeController.Move( cube, new[] {new Pace( target, step.Pace.MovingMaterial )} );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }
}