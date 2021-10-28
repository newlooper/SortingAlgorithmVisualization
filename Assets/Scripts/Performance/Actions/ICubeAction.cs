// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Cysharp.Threading.Tasks;

namespace Performance.Actions
{
    public interface ICubeAction
    {
        public UniTask Perform( Step step );
    }
}