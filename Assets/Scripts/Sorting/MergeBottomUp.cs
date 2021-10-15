using System;
using Performance;

namespace Sorting
{
    public class MergeBottomUp
    {
        public static void Sort( int[] arr )
        {
            var orderedArr = new int[arr.Length];
            for ( var i = 2; i < arr.Length * 2; i *= 2 )
            {
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForCodeLine( "For" ) );
                for ( var j = 0; j < ( arr.Length + i - 1 ) / i; j++ )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForCodeLine( "For2" ) );
                    var LEFT               = i * j;
                    var MIDDLE             = LEFT + i / 2 >= arr.Length ? ( arr.Length - 1 ) : ( LEFT + i / 2 );
                    var RIGHT              = i * ( j + 1 ) - 1 >= arr.Length ? ( arr.Length - 1 ) : ( i * ( j + 1 ) - 1 );
                    int nextAuxiliaryIndex = LEFT, left = LEFT, mid = MIDDLE;
                    while ( left < MIDDLE && mid <= RIGHT )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForCodeLine( "While" ) );
                        if ( arr[left] < arr[mid] )
                        {
                            PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( left, nextAuxiliaryIndex ) );
                            orderedArr[nextAuxiliaryIndex++] = arr[left++];
                        }
                        else
                        {
                            PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( mid, nextAuxiliaryIndex, "Pick2" ) );
                            orderedArr[nextAuxiliaryIndex++] = arr[mid++];
                        }
                    }

                    while ( left < MIDDLE )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( left, nextAuxiliaryIndex, "Pick3" ) );
                        orderedArr[nextAuxiliaryIndex++] = arr[left++];
                    }

                    while ( mid <= RIGHT )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( mid, nextAuxiliaryIndex, "Pick4" ) );
                        orderedArr[nextAuxiliaryIndex++] = arr[mid++];
                    }

                    var step = PerformanceQueue.Step.CreateStepForMerge( arr.Clone() as int[] );

                    Array.Copy( orderedArr, LEFT, arr, LEFT, RIGHT - LEFT + 1 );
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForAuxiliaryBack( arr.Clone() as int[] ) );

                    PerformanceQueue.Course.Enqueue( step );
                    PerformanceQueue.Rewind.Push( step );
                }
            }
        }
    }
}