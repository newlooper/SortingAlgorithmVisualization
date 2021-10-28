// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Cysharp.Threading.Tasks;
using Performance.Fixed;
using UnityEngine;

namespace Performance.Actions
{
    public class MoveToDigitsBucket : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            var cube   = GameManager.Cubes[step.Left];
            var bucket = step.Bucket;
            DigitsBucket.buckets[bucket].Push( step.Left );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            await CubeController.MoveAndScale( cube,
                new Vector3( Digits.Positions[bucket].x, DigitsBucket.buckets[bucket].Count * Config.VerticalGap, Digits.Positions[bucket].z ),
                Vector3.one,
                step );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }
}