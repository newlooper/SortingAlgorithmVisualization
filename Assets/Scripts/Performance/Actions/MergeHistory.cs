// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Performance.Actions
{
    public class MergeHistory : ICubeAction
    {
        public async UniTask Perform( Step step )
        {
            Interlocked.Increment( ref CubeController.rewindIndex );
            await UniTask.Yield();
        }
    }
}