// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class Gnome
    {
        public static void GnomeSort( int[] arr, int length )
        {
            var index = 0;

            while ( index < length )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "While" ) );
                // if there is no pot next to the gnome, he is done.
                if ( index == 0 ) // if the gnome is at the start of the line...
                    index++; // he steps forward

                PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( index, index - 1 ) );

                // if the pots next to the gnome are in the correct order...
                if ( arr[index] >= arr[index - 1] )
                {
                    index++; // he goes to the next pot
                }
                else
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], index - 1, index ) );
                    // if the pots are in the wrong order, he switches them.
                    ( arr[index], arr[index - 1] ) = ( arr[index - 1], arr[index] );
                    PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], index - 1, index, "Swap",
                        PerformanceQueue.Course.Count - 1 ) );
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