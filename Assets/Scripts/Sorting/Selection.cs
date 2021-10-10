﻿using Performance;

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
                // find the minimum element in unsorted array
                var min = i;
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForMin( i ) );
                for ( var j = i + 1; j < n; j++ )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelection( j, min ) );
                    if ( arr[j] < arr[min] )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForChangeMin( min, j ) );
                        min = j;
                    }
                }

                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForUnSelection( i ) );
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForUnSelection( n - 1 ) );
                if ( min == i )
                {
                    continue;
                }

                PerformanceQueue.Course.Enqueue( new PerformanceQueue.Step( arr.Clone() as int[], i, min ) );
                // swap the found minimum element with the first element
                ( arr[min], arr[i] ) = ( arr[i], arr[min] );

                PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, min ) );
            }
        }
    }
}