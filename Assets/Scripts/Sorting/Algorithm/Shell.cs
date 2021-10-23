// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

namespace Sorting.Algorithm
{
    public class Shell
    {
        public static void Sort( int[] arr )
        {
            var length = arr.Length;
            for ( var step = length / 2; step >= 1; step /= 2 )
            {
                for ( var i = step; i < length; i++ )
                {
                    var temp = arr[i];
                    var j    = i - step;
                    while ( j >= 0 && arr[j] > temp )
                    {
                        arr[j + step] = arr[j];
                        j -= step;
                    }

                    arr[j + step] = temp;
                }
            }
        }
    }
}