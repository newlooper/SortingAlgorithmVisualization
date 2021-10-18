// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Performance
{
    public partial class Step
    {
        public static Step CreateStepForCodeLine( string key )
        {
            var step = new Step
            {
                CodeLineKey = key,
                PerformanceEffect = PerformanceEffect.CodeLine
            };
            return step;
        }
    }
}