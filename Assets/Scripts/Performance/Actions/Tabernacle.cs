// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Concurrent;

namespace Performance.Actions
{
    public static class Tabernacle
    {
        public static readonly ConcurrentQueue<Step> Image = new ConcurrentQueue<Step>();
    }
}