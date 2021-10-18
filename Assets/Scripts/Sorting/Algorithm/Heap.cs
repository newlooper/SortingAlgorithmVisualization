// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Heap
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;
            for ( var i = n / 2; i >= 0; i-- )
            {
                Heapify( arr, n - 1, i );
            }

            for ( var i = n - 1; i > 0; i-- )
            {
                // swap last element of the max-heap with the first element
                ( arr[i], arr[0] ) = ( arr[0], arr[i] );

                // exclude the last element from the heap and rebuild the heap 
                Heapify( arr, i - 1, 0 );
            }
        }

        // Heapify function is used to build the max heap
        // max heap has maximum element at the root which means
        // first element of the array will be maximum in max heap
        static void Heapify( int[] arr, int n, int i )
        {
            var max   = i;
            var left  = 2 * i + 1;
            var right = 2 * i + 2;

            // if the left element is greater than root
            if ( left <= n && arr[left] > arr[max] )
            {
                max = left;
            }

            // if the right element is greater than root
            if ( right <= n && arr[right] > arr[max] )
            {
                max = right;
            }

            // if the max is not i
            if ( max != i )
            {
                ( arr[i], arr[max] ) = ( arr[max], arr[i] );
                // Recursively Heapify the affected sub-tree
                Heapify( arr, n, max );
            }
        }
    }
}