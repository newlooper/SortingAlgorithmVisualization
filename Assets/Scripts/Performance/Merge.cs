// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static readonly ConcurrentQueue<Step> Image = new ConcurrentQueue<Step>();

        private static IEnumerator SimpleMove( int from, int to, Step step )
        {
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            Image.Enqueue( step );
            yield return Move( GameManager.Cubes[from], new[]
            {
                new Pace( new Vector3( to * Config.HorizontalGap, 0, Config.OutDistance ), step.Pace.MovingMaterial )
            } );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator AuxiliaryBack( Step step )
        {
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            while ( Image.TryDequeue( out var fromTo ) )
            {
                var cube = GameManager.Cubes[fromTo.Left];
                if ( cube.transform.position.z != 0 )
                {
                    yield return Move( cube, new[]
                    {
                        new Pace( cube.transform.parent.position + new Vector3( fromTo.Right * Config.HorizontalGap, 0, 0f ), step.Pace.MovingMaterial )
                    } );
                }
            }

            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
            GameManager.GenObjectsFromArray( step.Snapshot );
        }
    }

    public partial class Step
    {
        public static Step CreateStepForPickAuxiliary( int from, int to, string key = "Pick" )
        {
            var step = new Step
            {
                Left = from,
                Right = to,
                PerformanceEffect = PerformanceEffect.MergePick,
                CodeLineKey = key,
                Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            };
            return step;
        }

        public static Step CreateStepForAuxiliaryBack( int[] snapshot, string key = "Copy" )
        {
            var step = new Step
            {
                Snapshot = snapshot,
                PerformanceEffect = PerformanceEffect.MergeBack,
                CodeLineKey = key,
                Pace = new Pace( null, Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            };
            return step;
        }

        public static Step CreateStepForMerge( int[] snapshot, int cursor = -1 )
        {
            var step = new Step
            {
                Snapshot = snapshot,
                Cursor = cursor,
                PerformanceEffect = PerformanceEffect.MergeHistory
            };
            return step;
        }
    }
}