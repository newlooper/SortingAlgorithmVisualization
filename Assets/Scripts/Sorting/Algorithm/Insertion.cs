// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Insertion
    {
        public static void Sort( int[] arr )
        {
            for ( var i = 0; i < arr.Length - 1; i++ )
            {
                for ( var j = i + 1; j > 0; j-- )
                {
                    if ( arr[j - 1] > arr[j] )
                    {
                        ( arr[j - 1], arr[j] ) = ( arr[j], arr[j - 1] );
                    }
                }
            }
        }
    }
}