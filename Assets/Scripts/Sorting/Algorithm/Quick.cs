// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Quick
    {
        public static void QuickSort( int[] arr, int left, int right )
        {
            if ( left >= right ) return;

            var middleValue = arr[( left + right ) / 2];
            var cursorLeft  = left - 1;
            var cursorRight = right + 1;
            while ( true )
            {
                while ( arr[++cursorLeft] < middleValue ) ;

                while ( arr[--cursorRight] > middleValue ) ;

                if ( cursorLeft >= cursorRight )
                    break;

                ( arr[cursorLeft], arr[cursorRight] ) = ( arr[cursorRight], arr[cursorLeft] );
            }

            QuickSort( arr, left, cursorLeft - 1 );
            QuickSort( arr, cursorRight + 1, right );
        }

        public static void Sort( int[] arr )
        {
            QuickSort( arr, 0, arr.Length - 1 );
        }
    }
}