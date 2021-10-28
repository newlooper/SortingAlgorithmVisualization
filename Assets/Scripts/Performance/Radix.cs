// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Performance
{
    public partial class Step
    {
        public static Step CreateStepForRedixPick( int from, int back, int bucket, string key = "RadixPick" )
        {
            var step = new Step
            {
                Left = from,
                Right = back,
                Bucket = bucket,
                PerformanceEffect = PerformanceEffect.RadixPick,
                CodeLineKey = key,
                Pace = new Pace( null, Config.DefaultCube )
            };
            return step;
        }

        public static Step CreateStepForRadixBack( int[] snapshot, string key = "Copy" )
        {
            var step = new Step
            {
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.RadixBack,
                CodeLineKey = key,
                Pace = new Pace( null, Config.DefaultCube )
            };
            return step;
        }
    }
}