// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections;
using System.Threading;
using Performance;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayBar : MonoBehaviour
    {
        public  GameObject  btnPlay;
        public  GameObject  btnPause;
        public  GameObject  btnStepBegin;
        public  GameObject  btnStepEnd;
        public  GameObject  btnStepForward;
        public  GameObject  btnStepBackward;
        public  GameObject  progressBar;
        private IEnumerator _producer;

        public void Play()
        {
            Time.timeScale = 1;
            if ( PerformanceQueue.Course.Count == 0 ) return;

            ProgressBar.SetPlayerButtonStatus( 1 );

            StopProducer();
            _producer = ContinuousPlay();
            StartCoroutine( _producer );
            if ( !CubeController.inPlay )
                StartCoroutine( CubeController.Play() );
        }

        public void Pause()
        {
            Time.timeScale = 0;
            ProgressBar.SetPlayerButtonStatus( 0 );
        }

        public void StepBegin()
        {
            StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( 0 ) );
        }

        public void StepEnd()
        {
            StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( PerformanceQueue.Rewind.Count ) );
        }

        public void StepBackward()
        {
            if ( CubeController.rewindIndex > 0 )
                StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( Interlocked.Decrement( ref CubeController.rewindIndex ) ) );
        }

        public void StepForward()
        {
            if ( CubeController.courseIndex >= PerformanceQueue.Course.Count ||
                 PerformanceQueue.Rewind.Count == 0 ) return;

            ProgressBar.SetPlayerButtonStatus( 0 );
            btnStepForward.GetComponent<Button>().interactable = false;
            btnPause.GetComponent<Button>().interactable = false;
            btnPlay.GetComponent<Button>().interactable = false;

            StopProducer();
            _producer = OneStep();
            StartCoroutine( _producer );
        }

        private IEnumerator ContinuousPlay()
        {
            var totalSteps = PerformanceQueue.Course.Count;

            while ( CubeController.courseIndex < totalSteps )
            {
                var step = PerformanceQueue.Course[CubeController.courseIndex];
                if ( !CubeController.FixedBox.Enqueue( step ) ) yield return null;
            }
        }

        private IEnumerator OneStep()
        {
            CubeController.canPlay = false;
            yield return new WaitUntil( () => !CubeController.inAction );
            CubeController.canPlay = true;
            Time.timeScale = 1;
            if ( !CubeController.inPlay )
                StartCoroutine( CubeController.Play() );

            try
            {
                var step = PerformanceQueue.Course[CubeController.courseIndex];
                CubeController.FixedBox.Enqueue( step );
            }
            catch ( Exception ex )
            {
                btnStepForward.GetComponent<Button>().interactable = true;
                Debug.Log( ex.Message );
                yield break;
            }

            yield return new WaitUntil( () => !CubeController.inAction );
            btnStepForward.GetComponent<Button>().interactable = true;
            btnPause.GetComponent<Button>().interactable = true;
            btnPlay.GetComponent<Button>().interactable = true;
        }

        public void StopProducer()
        {
            if ( _producer != null ) StopCoroutine( _producer );
        }

        private void SetEnable( bool isEnabled )
        {
            btnPlay.GetComponent<Button>().interactable = isEnabled;
            btnPause.GetComponent<Button>().interactable = isEnabled;
            btnStepBegin.GetComponent<Button>().interactable = isEnabled;
            btnStepEnd.GetComponent<Button>().interactable = isEnabled;
            btnStepForward.GetComponent<Button>().interactable = isEnabled;
            btnStepForward.GetComponent<Button>().interactable = isEnabled;
            btnStepBackward.GetComponent<Button>().interactable = isEnabled;
        }
    }
}