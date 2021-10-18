// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Gnome
    {
        public static void GnomeSort( int[] arr, int length )
        {
            var index = 0;

            while ( index < length )
            {
                // if there is no pot next to the gnome, he is done.
                if ( index == 0 ) // if the gnome is at the start of the line...
                {
                    index++; // he steps forward
                }

                // if the pots next to the gnome are in the correct order...
                if ( arr[index] >= arr[index - 1] )
                {
                    index++; // he goes to the next pot
                }
                else
                {
                    // if the pots are in the wrong order, he switches them.
                    ( arr[index], arr[index - 1] ) = ( arr[index - 1], arr[index] );
                    index--;
                }
            }
        }

        public static void Sort( int[] arr )
        {
            GnomeSort( arr, arr.Length );
        }
    }
}