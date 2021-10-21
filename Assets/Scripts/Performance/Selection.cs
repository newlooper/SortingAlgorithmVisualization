// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator HighlightOneWithIndex( int index, Step step, float lifetime = 1f )
        {
            var cubes = GameManager.Cubes;
            CodeDictionary.AddMarkLine( step.CodeLineKey );
            SetPillarMaterial( cubes[index], step.Pace.SelectedMaterial );

            yield return new WaitForSeconds( lifetime / _speed.value );

            SetPillarMaterial( cubes[index], step.Pace.MovingMaterial );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator HighlightSelectionWithIndex( int index, Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeSelected = step.Pace.SelectedMaterial;
            var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            SetPillarMaterial( cubes[index], cubeSelected );
            if ( index - 1 > step.Right )
            {
                SetPillarMaterial( cubes[index - 1], cubeDefault );
            }

            yield return new WaitForSeconds( Config.DefaultDelay / _speed.value );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }

        private static IEnumerator HighlightChange( int oldMin, int newMin, Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeSelected = step.Pace.SelectedMaterial;
            var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );

            CodeDictionary.AddMarkLine( step.CodeLineKey );
            SetPillarMaterial( cubes[oldMin], cubeDefault );
            SetPillarMaterial( cubes[newMin], cubeSelected );

            yield return new WaitForSeconds( Config.DefaultDelay / _speed.value );
            CodeDictionary.RemoveMarkLine( step.CodeLineKey );
        }
    }

    public partial class Step
    {
        public static Step CreateStepForMin( int index, string key = "Selected" )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.SelectOne,
                CodeLineKey = key,
                Pace = new Pace(
                    Resources.Load<Material>( "Materials/CubeSelectedRed" ),
                    Resources.Load<Material>( "Materials/CubeSelectedRed" ) )
            };
            return step;
        }

        public static Step CreateStepForChangeMin( int oldIndex, int newIndex, string key = "Selected3" )
        {
            var step = new Step
            {
                Left = oldIndex,
                Right = newIndex,
                PerformanceEffect = PerformanceEffect.NewMin,
                CodeLineKey = key,
                Pace = new Pace( Resources.Load<Material>( "Materials/CubeSelectedRed" ), null )
            };
            return step;
        }

        public static Step CreateStepForSelection( int index, int currentMin, string key = "Selected2" )
        {
            var step = new Step
            {
                Left = index,
                Right = currentMin,
                PerformanceEffect = PerformanceEffect.ChangeSelection,
                CodeLineKey = key,
                Pace = new Pace( Resources.Load<Material>( "Materials/CubeSelected" ), null )
            };
            return step;
        }

        public static Step CreateStepForUnSelection( int index )
        {
            var step = new Step
            {
                Left = index,
                PerformanceEffect = PerformanceEffect.UnSelectOne,
                Pace = new Pace(
                    Resources.Load<Material>( "Materials/Cube" ),
                    Resources.Load<Material>( "Materials/Cube" ) )
            };
            return step;
        }
    }
}