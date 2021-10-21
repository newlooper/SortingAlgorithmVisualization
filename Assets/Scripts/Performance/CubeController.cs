// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections;
using System.Threading;
using Performance.ProducerConsumer;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public partial class CubeController : MonoBehaviour
    {
        private static         CubeController       _instance;
        private static         Slider               _speed;
        public static          bool                 inPlay;
        public static          bool                 canPlay = true;
        public static          bool                 inAction;
        public static          int                  courseIndex;
        public static          int                  rewindIndex;
        public static readonly FixedSizeQueue<Step> FixedBox = new FixedSizeQueue<Step>( 1 );
        public                 Transform            scaler;
        private                int                  _value;

        private void Awake()
        {
            _instance = this;
            _speed = GameObject.Find( "SliderSpeed" ).GetComponent<Slider>();
            scaler = transform.Find( "Cube" );
        }

        private void SetButtonText( string str )
        {
            var buttons = GetComponentsInChildren<Button>();
            foreach ( var btn in buttons ) btn.GetComponentInChildren<Text>().text = str;
        }

        public void SetValue( int value )
        {
            scaler.localScale = new Vector3( 1, value * Config.CubeScale, 1 );
            SetButtonText( value.ToString() );
            _value = value;
        }

        public static IEnumerator Play()
        {
            inPlay = true;
            CodeDictionary.inPlay = true;
            GameManager.EnableButtons( false );

            var totalSteps = PerformanceQueue.Course.Count;
            while ( canPlay && courseIndex < totalSteps )
                if ( FixedBox.Dequeue( out var step ) )
                {
                    inAction = true;
                    Interlocked.Increment( ref courseIndex );
                    yield return Show( step );
                    inAction = false;
                }
                else
                {
                    yield return null;
                }

            FixedBox.Dequeue( out var none );
            inPlay = false;
            CodeDictionary.inPlay = false;
            GameManager.EnableButtons( true );
            ProgressBar.SetPlayerButtonStatus( 0 );
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
                    Interlocked.Increment( ref rewindIndex );
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
                    Interlocked.Increment( ref rewindIndex );
                    break;
                case PerformanceEffect.MergePick:
                    yield return SimpleMove( step.Left, step.Right, step );
                    break;
                case PerformanceEffect.MergeBack:
                    yield return AuxiliaryBack( step );
                    Interlocked.Increment( ref rewindIndex );
                    break;
                case PerformanceEffect.MergeHistory:
                    Interlocked.Increment( ref rewindIndex );
                    break;
                case PerformanceEffect.SwapHeap:
                    yield return SwapHeapWithIndex( step.Left, step.Right, step );
                    Interlocked.Increment( ref rewindIndex );
                    break;
                case PerformanceEffect.CodeLine:
                    CodeDictionary.AddMarkLine( step.CodeLineKey );
                    yield return new WaitForSeconds( Config.DefaultDelay / _speed.value );
                    CodeDictionary.RemoveMarkLine( step.CodeLineKey );
                    break;
                case PerformanceEffect.RadixPick:
                    yield return MoveToBucket( step );
                    break;
                case PerformanceEffect.RadixBack:
                    Interlocked.Increment( ref rewindIndex );
                    yield return RadixBack( step );
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
                if ( pace.Speed != 0 ) speed = pace.Speed;

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