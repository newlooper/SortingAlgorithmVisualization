// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Performance.Actions
{
    public class JumpIn : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var cube   = GameManager.Cubes[step.Left];
            var target = new Vector3( step.Left * Config.HorizontalGap, 0, 0f );
            await CubeController.Move( cube, new[] {new Pace( target, step.Pace.MovingMaterial )} );
            CubeController.SetPillarMaterial( cube, Config.BlueCube );
        }
    }
}