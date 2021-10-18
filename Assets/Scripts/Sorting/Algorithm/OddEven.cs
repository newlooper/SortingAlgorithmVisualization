// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class OddEven
    {
        public static void Sort( int[] arr )
        {
            var sorted = false;
            while ( !sorted )
            {
                sorted = true;
                for ( var i = 1; i < arr.Length - 1; i += 2 )
                {
                    if ( arr[i] > arr[i + 1] )
                    {
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        sorted = false;
                    }
                }

                for ( var i = 0; i < arr.Length - 1; i += 2 )
                {
                    if ( arr[i] > arr[i + 1] )
                    {
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );

                        sorted = false;
                    }
                }
            }
        }
    }
}