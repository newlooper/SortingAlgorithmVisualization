// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Performance.Actions
{
    public static class Context
    {
        private static readonly Dictionary<PerformanceEffect, ICubeAction> StrategiesDictionary =
            new Dictionary<PerformanceEffect, ICubeAction>();

        static Context()
        {
            StrategiesDictionary.Add( PerformanceEffect.SimpleSwap, new SimpleSwap() );
            StrategiesDictionary.Add( PerformanceEffect.SelectTwo, new HighlightTwo() );
            StrategiesDictionary.Add( PerformanceEffect.CodeLine, new CodeHighlight() );
            StrategiesDictionary.Add( PerformanceEffect.HeapSwap, new HeapSwap() );
            StrategiesDictionary.Add( PerformanceEffect.JumpOut, new JumpOut() );
            StrategiesDictionary.Add( PerformanceEffect.JumpIn, new JumpIn() );
            StrategiesDictionary.Add( PerformanceEffect.SelectOne, new HighlightOne() );
            StrategiesDictionary.Add( PerformanceEffect.RelaySwap, new RelaySwap() );
            StrategiesDictionary.Add( PerformanceEffect.MergePick, new SimpleMoveOut() );
            StrategiesDictionary.Add( PerformanceEffect.MergeBack, new SimpleMoveIn() );
            StrategiesDictionary.Add( PerformanceEffect.MergeHistory, new MergeHistory() );
            StrategiesDictionary.Add( PerformanceEffect.RadixPick, new MoveToDigitsBucket() );
            StrategiesDictionary.Add( PerformanceEffect.RadixBack, new RadixBack() );
            StrategiesDictionary.Add( PerformanceEffect.ChangeSelection, new ChangeSelection() );
            StrategiesDictionary.Add( PerformanceEffect.SelectNewMin, new HighlightChange() );
        }

        public static UniTask Execute( Step step )
        {
            return StrategiesDictionary[step.PerformanceEffect].Perform( step );
        }
    }
}