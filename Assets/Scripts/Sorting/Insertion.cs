﻿using Performance;

namespace Sorting
{
    public class Insertion
    {
        public static void Sort( int[] arr )
        {
            for ( var i = 0; i < arr.Length - 1; i++ )
            {
                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForJumpOut( i + 1 ) );
                var k = i + 1;
                for ( var j = i + 1; j > 0; j-- )
                {
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCompare( j - 1 ) );
                    if ( arr[j - 1] > arr[j] )
                    {
                        PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSwapCopy( arr.Clone() as int[], j, j - 1 ) );
                        ( arr[j - 1], arr[j] ) = ( arr[j], arr[j - 1] );
                        k = j - 1;
                        PerformanceQueue.Rewind.Add( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], j, j - 1, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                    }
                }

                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForJumpIn( k ) );
            }
        }
    }
}