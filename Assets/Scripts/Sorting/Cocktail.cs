// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class Cocktail
    {
        public static void Sort( int[] arr )
        {
            var swapped = true;
            var start   = 0;
            var end     = arr.Length;

            while ( swapped )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "While" ) );
                // reset the swapped flag on entering the
                // loop, because it might be true from a
                // previous iteration.
                swapped = false;

                // loop from bottom to top same as
                // the bubble sort
                for ( var i = start; i < end - 1; ++i )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( i, i + 1 ) );
                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1 ) );
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                        swapped = true;
                    }
                }

                // if nothing moved, then array is sorted.
                if ( swapped == false )
                    break;

                // otherwise, reset the swapped flag so that it
                // can be used in the next stage
                swapped = false;

                // move the end point back by one, because
                // item at the end is in its rightful spot
                end -= 1;

                // from top to bottom, doing the
                // same comparison as in the previous stage
                for ( var i = end - 1; i >= start; i-- )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( i, i + 1, "Selected2" ) );
                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap2" ) );

                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                        swapped = true;
                    }
                }

                // increase the starting point, because
                // the last stage would have moved the next
                // smallest number to its rightful spot.
                start += 1;
            }
        }
    }
}