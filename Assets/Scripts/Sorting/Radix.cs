// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;
using Performance.Actions;

namespace Sorting
{
    public class Radix
    {
        public static void Sort( int[] arr )
        {
            var n   = arr.Length;
            var max = arr[0];

            for ( var i = 1; i < n; i++ )
                if ( max < arr[i] )
                    max = arr[i];

            for ( var place = 1; max / place > 0; place *= 10 ) CountingSort( arr, place );
        }

        private static void CountingSort( int[] arr, int place )
        {
            var n      = arr.Length;
            var output = new int[n];

            int[] freq = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            for ( var i = 0; i < n; i++ ) freq[arr[i] / place % 10]++;

            for ( var i = 1; i < 10; i++ ) freq[i] += freq[i - 1];

            PerformanceQueue.Course.Add( Step.CreateStepForMerge( arr.Clone() as int[] ) );
            PerformanceQueue.Rewind.Add( Step.CreateStepForMerge( arr.Clone() as int[], PerformanceQueue.Course.Count - 1 ) );
            DigitsBucket.ResetBuckets();
            for ( var i = n - 1; i >= 0; i-- )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For5" ) );
                var digit = arr[i] / place % 10;
                PerformanceQueue.Course.Add( Step.CreateStepForRedixPick( i, freq[digit] - 1, digit ) );
                output[freq[digit] - 1] = arr[i];
                freq[digit]--;
            }

            for ( var i = 0; i < n; i++ )
                arr[i] = output[i];

            PerformanceQueue.Course.Add( Step.CreateStepForRadixBack( arr.Clone() as int[] ) );
            PerformanceQueue.Rewind.Add( Step.CreateStepForMerge( arr.Clone() as int[], PerformanceQueue.Course.Count - 1 ) );
        }
    }
}