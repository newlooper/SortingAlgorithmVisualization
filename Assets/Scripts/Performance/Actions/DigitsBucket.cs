// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;

namespace Performance.Actions
{
    public static class DigitsBucket
    {
        public static Stack<int>[] buckets;

        public static void ResetBuckets()
        {
            buckets = new[]
            {
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>()
            };
        }
    }
}