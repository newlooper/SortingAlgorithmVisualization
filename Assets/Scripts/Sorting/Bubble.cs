// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public static class Bubble
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;
            for ( var i = 0; i < n - 1; i++ )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                for ( var j = 0; j < n - i - 1; j++ )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( j, j + 1 ) );

                    if ( arr[j] > arr[j + 1] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], j, j + 1 ) );
                        ( arr[j], arr[j + 1] ) = ( arr[j + 1], arr[j] );
                        PerformanceQueue.Rewind.Add(
                            Step.CreateStepForSimpleSwap(
                                arr.Clone() as int[], j, j + 1, "Swap", PerformanceQueue.Course.Count - 1 ) );
                    }
                }
            }
        }
    }
}