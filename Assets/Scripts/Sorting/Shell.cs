// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Performance;

namespace Sorting
{
    public class Shell
    {
        public static void Sort( int[] arr )
        {
            var length = arr.Length;
            for ( var step = length / 2; step >= 1; step /= 2 )
            {
                PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For" ) );
                for ( var i = step; i < length; i++ )
                {
                    PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "For2" ) );
                    var temp = arr[i];
                    var j    = i - step;
                    while ( j >= 0 && arr[j] > temp )
                    {
                        PerformanceQueue.Course.Add( Step.CreateStepForCodeLine( "While" ) );
                        PerformanceQueue.Course.Add( Step.CreateStepForSelectTwo( j, j + step ) );
                        PerformanceQueue.Course.Add( Step.CreateStepForSimpleSwap( arr.Clone() as int[], j, j + step ) );
                        var swappedArr = arr.Clone() as int[];
                        ( swappedArr[j], swappedArr[j + step] ) = ( swappedArr[j + step], swappedArr[j] );
                        PerformanceQueue.Rewind.Add( Step.CreateStepForSimpleSwap( swappedArr, j, j + step, "Swap", PerformanceQueue.Course.Count - 1 ) );
                        arr[j + step] = arr[j];
                        j -= step;
                    }

                    arr[j + step] = temp;
                }
            }
        }
    }
}