// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Performance;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayBar : MonoBehaviour
    {
        public GameObject btnPlay;
        public GameObject btnPause;
        public GameObject btnStepBegin;
        public GameObject btnStepEnd;
        public GameObject btnStepForward;
        public GameObject btnStepBackward;
        public GameObject progressBar;

        public void Play()
        {
            Time.timeScale = 1;
            if ( PerformanceQueue.Course.Count == 0 ) return;

            ProgressBar.SetPlayerButtonStatus( 1 );
            ContinuousPlay().Forget();
            CubeController.Play().Forget();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            ProgressBar.SetPlayerButtonStatus( 0 );
        }

        public void StepBegin()
        {
            progressBar.GetComponent<ProgressBar>().PauseAt( 0 );
        }

        public void StepEnd()
        {
            progressBar.GetComponent<ProgressBar>().PauseAt( PerformanceQueue.Rewind.Count );
        }

        public void StepBackward()
        {
            if ( CubeController.rewindIndex > 0 )
                progressBar.GetComponent<ProgressBar>().PauseAt( Interlocked.Decrement( ref CubeController.rewindIndex ) );
        }

        public void StepForward()
        {
            if ( CubeController.courseIndex >= PerformanceQueue.Course.Count ||
                 PerformanceQueue.Rewind.Count == 0 ) return;

            ProgressBar.SetPlayerButtonStatus( 0 );
            btnStepForward.GetComponent<Button>().interactable = false;
            btnPause.GetComponent<Button>().interactable = false;
            btnPlay.GetComponent<Button>().interactable = false;

            OneStep().Forget();
        }

        private static async UniTask ContinuousPlay()
        {
            var totalSteps = PerformanceQueue.Course.Count;

            while ( CubeController.courseIndex < totalSteps &&
                    CubeController.runLevel > 1 )
            {
                if ( !CubeController.FixedBox.Enqueue( PerformanceQueue.Course[CubeController.courseIndex] ) ) // producer 1
                    await UniTask.Yield();
            }
        }

        private async UniTask OneStep()
        {
            CubeController.runLevel = 1;
            Time.timeScale = 1;
            await UniTask.WaitUntil( () => !CubeController.inAction );
            CubeController.runLevel = 2;

            try
            {
                var step = PerformanceQueue.Course[CubeController.courseIndex];
                CubeController.FixedBox.Enqueue( step ); // producer 2
                if ( !CubeController.inPlay ) CubeController.Play().Forget();
            }
            catch ( Exception ex )
            {
                btnStepForward.GetComponent<Button>().interactable = true;
                Debug.Log( ex.Message );
                return;
            }

            await UniTask.WaitUntil( () => !CubeController.inAction );
            btnStepForward.GetComponent<Button>().interactable = true;
            btnPause.GetComponent<Button>().interactable = true;
            btnPlay.GetComponent<Button>().interactable = true;
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