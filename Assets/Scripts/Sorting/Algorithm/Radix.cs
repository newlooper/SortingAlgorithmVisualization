// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Radix
    {
        public static void Sort( int[] arr )
        {
            var n   = arr.Length;
            var max = arr[0];

            // find largest element in the Array
            for ( var i = 1; i < n; i++ )
            {
                if ( max < arr[i] )
                    max = arr[i];
            }

            // Counting sort is performed based on place. 
            // like ones place, tens place and so on.
            for ( var place = 1; max / place > 0; place *= 10 )
                CountingSort( arr, place );
        }

        static void CountingSort( int[] arr, int place )
        {
            var   n      = arr.Length;
            var output = new int[n];

            // range of the number is 0-9 for each place considered.
            int[] freq = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            // count number of occurrences in freq array
            for ( var i = 0; i < n; i++ )
                freq[( arr[i] / place ) % 10]++;

            // Change freq[i] so that freq[i] now contains actual 
            // position of this digit in output[] 
            for ( var i = 1; i < 10; i++ )
                freq[i] += freq[i - 1];

            // Build the output array 
            for ( var i = n - 1; i >= 0; i-- )
            {
                output[freq[( arr[i] / place ) % 10] - 1] = arr[i];
                freq[( arr[i] / place ) % 10]--;
            }

            // Copy the output array to the input Array, Now the Array will 
            // contain sorted array based on digit at specified place
            for ( var i = 0; i < n; i++ )
                arr[i] = output[i];
        }
    }
}