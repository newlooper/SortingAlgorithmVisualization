// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using System.Collections.Generic;
using Performance.Fixed;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static Stack<int>[] _buckets;

        public static void ResetBuckets()
        {
            _buckets = new[]
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


        private static IEnumerator MoveToBucket( Step step )
        {
            var cube   = GameManager.Cubes[step.Left];
            var bucket = step.Bucket;
            _buckets[bucket].Push( step.Left );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            yield return MoveAndScale( cube,
                new Vector3( Digits.Positions[bucket].x, _buckets[bucket].Count * Config.VerticalGap, Digits.Positions[bucket].z ),
                Vector3.one,
                step );

            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator RadixBack( Step step )
        {
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            var i = 0;
            foreach ( var queue in _buckets )
                while ( queue.Count > 0 && canPlay )
                {
                    var idx  = queue.Pop();
                    var cube = GameManager.Cubes[idx];
                    yield return MoveAndScale( cube,
                        new Vector3( i++ * Config.HorizontalGap, 0, 0 ),
                        new Vector3( 1, cube.GetComponent<CubeController>()._value * Config.CubeScale, 1 ),
                        step );
                }

            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
            GameManager.GenObjectsFromArray( step.Snapshot );
        }

        private static IEnumerator MoveAndScale( GameObject cube, Vector3 target, Vector3 targetScale, Step step )
        {
            var startPos   = cube.transform.position;
            var scaler     = cube.GetComponent<CubeController>().scaler;
            var startScale = scaler.localScale;
            var distance   = Vector3.Distance( startPos, target );
            SetPillarMaterial( cube, step.Pace.MovingMaterial );

            var i = 0f;
            while ( i < 1f && canPlay )
            {
                var rate = _speed.value / distance;
                i += Time.deltaTime * rate;

                cube.transform.position = Vector3.Lerp( startPos, target, i );
                scaler.localScale = Vector3.Lerp( startScale, targetScale, i );

                yield return null;
            }
        }
    }

    public partial class Step
    {
        public static Step CreateStepForRedixPick( int from, int back, int bucket, string key = "RadixPick" )
        {
            var step = new Step
            {
                Left = from,
                Right = back,
                Bucket = bucket,
                PerformanceEffect = PerformanceEffect.RadixPick,
                CodeLineKey = key,
                Pace = new Pace( null, Config.DefaultCube )
            };
            return step;
        }

        public static Step CreateStepForRadixBack( int[] snapshot, string key = "Copy" )
        {
            var step = new Step
            {
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.RadixBack,
                CodeLineKey = key,
                Pace = new Pace( null, Config.DefaultCube )
            };
            return step;
        }
    }
}