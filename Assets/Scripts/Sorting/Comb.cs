﻿using Performance;

namespace Sorting
{
    public class Comb
    {
        static int GetNextGap( int gap )
        {
            // The "shrink factor", empirically shown to be 1.3
            gap = ( gap * 10 ) / 13;
            if ( gap < 1 )
            {
                return 1;
            }

            return gap;
        }

        public static void Sort( int[] arr )
        {
            var length = arr.Length;
            var gap    = length;

            // We initialize this as true to enter the while loop.
            var swapped = true;

            while ( gap != 1 || swapped )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "While" ) );
                gap = GetNextGap( gap );

                // Set swapped as false.  Will go to true when two values are swapped.
                swapped = false;

                // Compare all elements with current gap 
                for ( var i = 0; i < length - gap; i++ )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( i, i + gap ) );
                    if ( arr[i] > arr[i + gap] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSwap( arr.Clone() as int[], i, i + gap ) );
                        // Swap
                        ( arr[i], arr[i + gap] ) = ( arr[i + gap], arr[i] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSwap( arr.Clone() as int[], i, i + gap, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                        swapped = true;
                    }
                }
            }
        }
    }
}