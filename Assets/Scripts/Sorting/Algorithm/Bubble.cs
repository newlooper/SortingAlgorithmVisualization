// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Bubble
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;
            for ( var i = 0; i < n - 1; i++ )
            {
                for ( var j = 0; j < n - i - 1; j++ )
                {
                    if ( arr[j] > arr[j + 1] )
                    {
                        ( arr[j], arr[j + 1] ) = ( arr[j + 1], arr[j] );
                    }
                }
            }
        }
    }
}