// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class OddEven
    {
        public static void Sort( int[] arr )
        {
            var sorted = false;
            while ( !sorted )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "While" ) );
                sorted = true;
                for ( var i = 1; i < arr.Length - 1; i += 2 )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( i, i + 1 ) );
                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1 ) );
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                        sorted = false;
                    }
                }

                for ( var i = 0; i < arr.Length - 1; i += 2 )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( i, i + 1, "Selected2" ) );
                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap2" ) );
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], i, i + 1, "Swap",
                            PerformanceQueue.Course.Count - 1 ) );
                        sorted = false;
                    }
                }
            }
        }
    }
}