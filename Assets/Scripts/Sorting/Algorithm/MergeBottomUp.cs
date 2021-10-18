// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;

namespace Sorting.Algorithm
{
    public class MergeBottomUp
    {
        public static void Sort( int[] arr )
        {
            var orderedArr = new int[arr.Length];
            for ( var i = 2; i < arr.Length * 2; i *= 2 )
            {
                for ( var j = 0; j < ( arr.Length + i - 1 ) / i; j++ )
                {
                    var LEFT               = i * j;
                    var MIDDLE             = LEFT + i / 2 >= arr.Length ? ( arr.Length - 1 ) : ( LEFT + i / 2 );
                    var RIGHT              = i * ( j + 1 ) - 1 >= arr.Length ? ( arr.Length - 1 ) : ( i * ( j + 1 ) - 1 );
                    int nextAuxiliaryIndex = LEFT, left = LEFT, mid = MIDDLE;
                    while ( left < MIDDLE && mid <= RIGHT )
                    {
                        if ( arr[left] < arr[mid] )
                        {
                            orderedArr[nextAuxiliaryIndex++] = arr[left++];
                        }
                        else
                        {
                            orderedArr[nextAuxiliaryIndex++] = arr[mid++];
                        }
                    }

                    while ( left < MIDDLE )
                        orderedArr[nextAuxiliaryIndex++] = arr[left++];
                    while ( mid <= RIGHT )
                        orderedArr[nextAuxiliaryIndex++] = arr[mid++];

                    Array.Copy( orderedArr, LEFT, arr, LEFT, RIGHT - LEFT + 1 );
                }
            }
        }
    }
}