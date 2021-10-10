using System.Collections;
using UnityEngine;

namespace Performance
{
    public partial class CubeController
    {
        private static IEnumerator HighlightOneWithIndex( int index, PerformanceQueue.Step step, float lifetime = 1f )
        {
            var cubes        = GameManager.Cubes;
            var cubeSelected = step.Pace.SelectedMaterial;

            SetPillarMaterial( cubes[index], cubeSelected );

            yield return new WaitForSeconds( lifetime / _speed.value );
        }

        private static IEnumerator HighlightSelectionWithIndex( int index, PerformanceQueue.Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeSelected = step.Pace.SelectedMaterial;
            var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );

            SetPillarMaterial( cubes[index], cubeSelected );
            if ( index - 1 > step.Right )
            {
                SetPillarMaterial( cubes[index - 1], cubeDefault );
            }

            yield return new WaitForSeconds( 1f / _speed.value );
        }

        private static IEnumerator HighlightChange( int oldMin, int newMin, PerformanceQueue.Step step )
        {
            var cubes        = GameManager.Cubes;
            var cubeSelected = step.Pace.SelectedMaterial;
            var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );

            SetPillarMaterial( cubes[oldMin], cubeDefault );
            SetPillarMaterial( cubes[newMin], cubeSelected );

            yield return new WaitForSeconds( 1f / _speed.value );
        }
    }

    public static partial class PerformanceQueue
    {
        public partial class Step
        {
            public static Step CreateStepForMin( int index )
            {
                var step = new Step
                {
                    Left = index,
                    PerformanceEffect = PerformanceEffect.SelectOne,
                    Pace = new Pace( Resources.Load<Material>( "Materials/CubeSelectedRed" ), null )
                };
                return step;
            }

            public static Step CreateStepForChangeMin( int oldIndex, int newIndex )
            {
                var step = new Step
                {
                    Left = oldIndex,
                    Right = newIndex,
                    PerformanceEffect = PerformanceEffect.NewMin,
                    Pace = new Pace( Resources.Load<Material>( "Materials/CubeSelectedRed" ), null )
                };
                return step;
            }

            public static Step CreateStepForSelection( int index, int currentMin )
            {
                var step = new Step
                {
                    Left = index,
                    Right = currentMin,
                    PerformanceEffect = PerformanceEffect.ChangeSelection,
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
                    Pace = new Pace( Resources.Load<Material>( "Materials/Cube" ), null )
                };
                return step;
            }
        }
    }
}