// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Performance
{
    public partial class Step
    {
        public static Step CreateStepForCompare( int index, string key = "Compare" )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.SelectOne,
                CodeLineKey = key,
                Pace = new Pace(
                    Config.YellowCube,
                    Config.BlueCube )
            };
            return step;
        }

        public static Step CreateStepForJumpOut( int from, string key = "For" )
        {
            var step = new Step
            {
                Left = from,
                PerformanceEffect = PerformanceEffect.JumpOut,
                CodeLineKey = key,
                Pace = new Pace( null, Config.RedCube )
            };
            return step;
        }

        public static Step CreateStepForJumpIn( int insert )
        {
            var step = new Step
            {
                Left = insert,
                PerformanceEffect = PerformanceEffect.JumpIn,
                Pace = new Pace( null, Config.RedCube )
            };
            return step;
        }

        public static Step CreateStepForSwapRelay( int[] snapshot, int left, int right, string key = "Swap" )
        {
            var step = new Step
            {
                Left = left,
                Right = right,
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.RelaySwap,
                CodeLineKey = key,
                Pace = new Pace( null, Config.GreenCube )
            };
            return step;
        }
    }
}