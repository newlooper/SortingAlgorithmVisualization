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
                for ( var j = 0; j < ( arr.Length + i - 1 ) / i; j++ )
                {
                    var LEFT               = i * j;
                    var MIDDLE             = LEFT + i / 2 >= arr.Length ? ( arr.Length - 1 ) : ( LEFT + i / 2 );
                    var RIGHT              = i * ( j + 1 ) - 1 >= arr.Length ? ( arr.Length - 1 ) : ( i * ( j + 1 ) - 1 );
                    int nextAuxiliaryIndex = LEFT, left = LEFT, mid = MIDDLE;
                    while ( left < MIDDLE && mid <= RIGHT )
                    {
                        if ( arr[left] < arr[mid] )
                        {
                            PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( left, nextAuxiliaryIndex ) );
                            orderedArr[nextAuxiliaryIndex++] = arr[left++];
                        }
                        else
                        {
                            PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( mid, nextAuxiliaryIndex ) );
                            orderedArr[nextAuxiliaryIndex++] = arr[mid++];
                        }
                    }

                    while ( left < MIDDLE )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( left, nextAuxiliaryIndex ) );
                        orderedArr[nextAuxiliaryIndex++] = arr[left++];
                    }

                    while ( mid <= RIGHT )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForPickAuxiliary( mid, nextAuxiliaryIndex ) );
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