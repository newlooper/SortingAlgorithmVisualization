// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class Heap
    {
        public static void Sort( int[] arr )
        {
            CompleteBinaryTree.BuildTree();
            var n = arr.Length;
            for ( var i = n / 2; i >= 0; i-- )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                Heapify( arr, n - 1, i );
            }

            for ( var i = n - 1; i > 0; i-- )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                // swap last element of the max-heap with the first element
                PerformanceQueue.Course.Add( Step.CreateStepForHeapSwap( arr.Clone() as int[], 0, i ) );
                ( arr[i], arr[0] ) = ( arr[0], arr[i] );
                PerformanceQueue.Rewind.Add(
                    Step.CreateStepForHeapSwap( arr.Clone() as int[], 0, i, "Swap",
                        PerformanceQueue.Course.Count - 1 ) );

                // exclude the last element from the heap and rebuild the heap 
                Heapify( arr, i - 1, 0 );
            }
        }

        // Heapify function is used to build the max heap
        // max heap has maximum element at the root which means
        // first element of the array will be maximum in max heap
        private static void Heapify( int[] arr, int n, int i )
        {
            var max   = i;
            var left  = 2 * i + 1;
            var right = 2 * i + 2;

            // if the left element is greater than root
            if ( left <= n && arr[left] > arr[max] )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "IF" ) );
                max = left;
            }

            // if the right element is greater than root
            if ( right <= n && arr[right] > arr[max] )
            {
                max = right;
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "IF2" ) );
            }

            // if the max is not i
            if ( max != i )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForHeapSwap( arr.Clone() as int[], i, max, "Swap2" ) );
                ( arr[i], arr[max] ) = ( arr[max], arr[i] );
                PerformanceQueue.Rewind.Add(
                    Step.CreateStepForHeapSwap( arr.Clone() as int[], i, max, "Swap",
                        PerformanceQueue.Course.Count - 1 ) );
                // Recursively Heapify the affected sub-tree
                Heapify( arr, n, max );
            }
        }
    }
}