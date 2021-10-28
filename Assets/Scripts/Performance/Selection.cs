// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Performance
{
    public partial class Step
    {
        public static Step CreateStepForMin( int index, string key = "Selected" )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.SelectOne,
                CodeLineKey = key,
                Pace = new Pace(
                    Config.RedCube,
                    Config.RedCube )
            };
            return step;
        }

        public static Step CreateStepForSelectNewMin( int oldIndex, int newIndex, string key = "Selected3" )
        {
            var step = new Step
            {
                Left = oldIndex,
                Right = newIndex,
                PerformanceEffect = PerformanceEffect.SelectNewMin,
                CodeLineKey = key,
                Pace = new Pace( Config.RedCube, null )
            };
            return step;
        }

        public static Step CreateStepForChangeSelection( int index, int currentMin, string key = "Selected2" )
        {
            var step = new Step
            {
                Left = index,
                Right = currentMin,
                PerformanceEffect = PerformanceEffect.ChangeSelection,
                CodeLineKey = key,
                Pace = new Pace( Config.YellowCube, null )
            };
            return step;
        }

        public static Step CreateStepForUnSelection( int index )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.SelectOne,
                Lifetime = 0f,
                Pace = new Pace( Config.DefaultCube, Config.DefaultCube )
            };
            return step;
        }
    }
}