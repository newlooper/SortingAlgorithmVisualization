// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
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
            btnPause.SetActive( true );
            btnPlay.SetActive( false );
            Time.timeScale = 1;
            CubeController.oneStep = false;
            StartCoroutine( CubeController.Play() );
        }

        public void Pause()
        {
            btnPlay.SetActive( true );
            btnPause.SetActive( false );
            Time.timeScale = 0;
        }

        public void StepBegin()
        {
            CubeController.oneStep = false;
            StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( 0 ) );
            progressBar.GetComponent<Slider>().value = 0;
        }

        public void StepEnd()
        {
            CubeController.oneStep = false;
            StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( PerformanceQueue.Rewind.Count ) );
            progressBar.GetComponent<Slider>().value = PerformanceQueue.Rewind.Count;
        }

        public void StepBackward()
        {
            CubeController.oneStep = false;
            progressBar.GetComponent<Slider>().value--;
            StartCoroutine( progressBar.GetComponent<ProgressBar>().PauseAt( (int)progressBar.GetComponent<Slider>().value ) );
        }

        public void StepForward()
        {
            if ( CubeController.index > PerformanceQueue.Course.Count ||
                 
                 PerformanceQueue.Rewind.Count == 0 ) return;

            btnStepForward.GetComponent<Button>().interactable = false;
            btnPlay.SetActive( true );
            btnPause.SetActive( false );

            StartCoroutine( OneStep() );
        }

        private IEnumerator OneStep()
        {
            Time.timeScale = 1;
            CubeController.oneStep = true;
            if ( !CubeController.inPlay )
            {
                StartCoroutine( CubeController.Play() );
            }

            yield return new WaitUntil( () => Time.timeScale == 0 );
            btnStepForward.GetComponent<Button>().interactable = true;
            CubeController.DropStep();
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