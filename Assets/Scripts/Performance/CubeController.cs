// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections;
using Performance.ProducerConsumer;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public partial class CubeController : MonoBehaviour
    {
        private static          CubeController       _instance;
        private static          Slider               _speed;
        private static          Slider               _progress;
        public static           bool                 inPlay;
        public static           bool                 canPlay = true;
        public static           bool                 oneStep;
        public static           int                  index;
        public static           float                Gap          { get; private set; }
        private static          float                DefaultDelay { get; set; }
        private static readonly FixedSizeQueue<Step> FixedBox = new FixedSizeQueue<Step>( 1 );

        private void Awake()
        {
            _instance = this;
            _speed = GameObject.Find( "SliderSpeed" ).GetComponent<Slider>();
            _progress = GameObject.Find( "Progress" ).GetComponent<Slider>();
            Gap = 1.5f;
            DefaultDelay = 1f;
        }

        private void SetButtonText( string str )
        {
            var buttons = GetComponentsInChildren<Button>();
            foreach ( var btn in buttons )
            {
                btn.GetComponentInChildren<Text>().text = str;
            }
        }

        public void SetValue( int value )
        {
            transform.Find( "Cube" ).transform.localScale = new Vector3( 1, value / 2f, 1 );
            SetButtonText( value.ToString() );
        }

        public static IEnumerator Play()
        {
            if ( inPlay )
            {
                yield break;
            }

            inPlay = true;

            CodeDictionary.inPlay = true;
            GameManager.EnableButtons( false );
            _progress.maxValue = PerformanceQueue.Rewind.Count;
            var totalSteps = PerformanceQueue.Course.Count;

            while ( canPlay && index < totalSteps )
            {
                var step = PerformanceQueue.Course[index];
                if ( FixedBox.Enqueue( step ) )
                {
                    yield return Show( step );
                    index++;
                    if ( oneStep )
                    {
                        Time.timeScale = 0;
                        yield return null;
                    }

                    DropStep();
                }
                else
                {
                    yield return null;
                }
            }

            inPlay = false;
            ProgressBar.SwitchPlayPause();
            CodeDictionary.inPlay = false;
            GameManager.EnableButtons( true );
        }

        public static void DropStep()
        {
            FixedBox.Dequeue( out var dropStep );
        }

        private static IEnumerator Show( Step step )
        {
            switch ( step.PerformanceEffect )
            {
                case PerformanceEffect.SelectTwo:
                    yield return HighlightTwoWithIndex( step.Left, step.Right, step );
                    break;
                case PerformanceEffect.Swap:
                    yield return SwapWithIndex( step.Left, step.Right, step );
                    _progress.value++;
                    break;
                case PerformanceEffect.SelectOne:
                    yield return HighlightOneWithIndex( step.Left, step );
                    break;
                case PerformanceEffect.UnSelectOne:
                    yield return HighlightOneWithIndex( step.Left, step, 0 );
                    break;
                case PerformanceEffect.ChangeSelection:
                    yield return HighlightSelectionWithIndex( step.Left, step );
                    break;
                case PerformanceEffect.NewMin:
                    yield return HighlightChange( step.Left, step.Right, step );
                    break;
                case PerformanceEffect.JumpOut:
                    yield return JumpOut( step.Left, step );
                    break;
                case PerformanceEffect.JumpIn:
                    yield return JumpIn( step );
                    break;
                case PerformanceEffect.SwapCopy:
                    yield return SwapCopy( step.Left, step.Right, step );
                    _progress.value++;
                    break;
                case PerformanceEffect.Auxiliary:
                    yield return SimpleMove( step.Left, step.Right, step );
                    break;
                case PerformanceEffect.AuxiliaryBack:
                    yield return AuxiliaryBack( step );
                    _progress.value++;
                    break;
                case PerformanceEffect.MergeHistory:
                    _progress.value++;
                    break;
                case PerformanceEffect.SwapHeap:
                    yield return SwapHeapWithIndex( step.Left, step.Right, step );
                    _progress.value++;
                    break;
                case PerformanceEffect.CodeLine:
                    CodeDictionary.AddMarkLine( step.CodeLineKey );
                    yield return new WaitForSeconds( DefaultDelay / _speed.value );
                    CodeDictionary.RemoveMarkLine( step.CodeLineKey );
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SetPillarMaterial( GameObject go, Material mat )
        {
            go.transform.Find( "Cube" ).transform.Find( "Pillar" ).GetComponent<MeshRenderer>().material = mat;
        }

        private static IEnumerator Move( GameObject from, Pace[] paces )
        {
            foreach ( var pace in paces )
            {
                var speed = _speed.value;
                if ( pace.Speed != 0 )
                {
                    speed = pace.Speed;
                }

                SetPillarMaterial( from, pace.MovingMaterial );

                while ( from.transform.position != pace.Target )
                {
                    from.transform.position = Vector3.MoveTowards(
                        from.transform.position,
                        pace.Target,
                        speed * Time.deltaTime );
                    yield return null;
                }
            }
        }
    }
}