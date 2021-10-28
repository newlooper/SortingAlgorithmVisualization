// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class Selection
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;

            // one by one move boundary of unsorted subarray
            for ( var i = 0; i < n - 1; i++ )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                // find the minimum element in unsorted array
                var min = i;
                PerformanceQueue.Course.Add( Step.CreateStepForMin( i ) );
                for ( var j = i + 1; j < n; j++ )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForChangeSelection( j, min ) );
                    if ( arr[j] < arr[min] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSelectNewMin( min, j ) );
                        min = j;
                    }
                }

                PerformanceQueue.Course.Add( Step.CreateStepForUnSelection( i ) );
                PerformanceQueue.Course.Add( Step.CreateStepForUnSelection( n - 1 ) );
                if ( min == i ) continue;

                PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, min ) );
                // swap the found minimum element with the first element
                ( arr[min], arr[i] ) = ( arr[i], arr[min] );
                PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, min, "Swap",
                    PerformanceQueue.Course.Count - 1 ) );
            }
        }
    }
}