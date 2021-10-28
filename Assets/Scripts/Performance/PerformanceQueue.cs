// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Performance
{
    public static class PerformanceQueue
    {
        public static readonly List<Step> Course = new List<Step>();
        public static readonly List<Step> Rewind = new List<Step>();
    }

    public partial class Step
    {
        private Step()
        {
        }

        public int Left { get; set; }

        public int Right { get; set; }

        public int[] Snapshot { get; set; }

        public PerformanceEffect PerformanceEffect { get; set; }

        public Pace Pace { get; set; }

        public string CodeLineKey { get; private set; }
        public int    Cursor      { get; private set; }
        public string Algorithm   { get; private set; }
        public int    Bucket      { get; private set; }

        public float Lifetime { get; private set; } = Config.DefaultDelay;

        public static Step CreateStepForSelectTwo( int left, int right, string key = "Selected" )
        {
            var step = new Step
            {
                Left = left,
                Right = right,
                PerformanceEffect = PerformanceEffect.SelectTwo,
                CodeLineKey = key,
                Pace = new Pace(
                    Config.YellowCube,
                    null )
            };
            return step;
        }

        public static Step CreateStepForSimpleSwap( int[] snapshot, int left, int right, string key = "Swap", int cursor = -1 )
        {
            var step = new Step
            {
                Left = left,
                Right = right,
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.SimpleSwap,
                CodeLineKey = key,
                Cursor = cursor,
                Pace = new Pace(
                    Config.YellowCube,
                    Config.GreenCube )
            };
            return step;
        }

        public static Step CreateStepForHeapSwap( int[] snapshot, int left, int right, string key = "Swap", int cursor = -1 )
        {
            var step = new Step
            {
                Left = left,
                Right = right,
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.HeapSwap,
                CodeLineKey = key,
                Cursor = cursor,
                Pace = new Pace(
                    Config.YellowCube,
                    Config.GreenCube ),
                Algorithm = Sort.className
            };
            return step;
        }
    }

    public class Pace
    {
        public Pace( Material selectedMaterial, Material movingMaterial )
        {
            SelectedMaterial = selectedMaterial;
            MovingMaterial = movingMaterial;
        }

        public Pace( Vector3 target, Material movingMaterial )
        {
            Target = target;
            MovingMaterial = movingMaterial;
        }

        public Pace( Vector3 target, Material movingMaterial, float speed = 0 )
        {
            Target = target;
            MovingMaterial = movingMaterial;
            Speed = speed;
        }

        public Vector3 Target { get; set; }

        public Material MovingMaterial   { get; set; }
        public Material SelectedMaterial { get; set; }
        public float    Speed            { get; set; }
    }
}