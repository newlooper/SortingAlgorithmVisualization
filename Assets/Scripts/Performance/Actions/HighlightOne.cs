// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using Cysharp.Threading.Tasks;

namespace Performance.Actions
{
    public class HighlightOne : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var index = step.Left;
            var cubes = GameManager.Cubes;
            
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            
            CubeController.SetPillarMaterial( cubes[index], step.Pace.SelectedMaterial );
            await UniTask.Delay( TimeSpan.FromSeconds( step.Lifetime / CubeController.speed.value ) );
            CubeController.SetPillarMaterial( cubes[index], step.Pace.MovingMaterial );
            
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }
}