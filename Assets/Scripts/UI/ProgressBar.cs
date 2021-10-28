// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Threading;
using Cysharp.Threading.Tasks;
using Lean.Localization;
using Performance;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        private static ProgressBar _instance;
        public         GameObject  btnPlay;
        public         GameObject  btnPause;
        public         GameObject  stepText;
        public         GameObject  courseProgress;
        public         GameObject  courseStepText;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            GetComponent<Slider>().value = CubeController.rewindIndex;
            GetComponent<Slider>().maxValue = PerformanceQueue.Rewind.Count;
            stepText.GetComponent<Text>().text = CubeController.rewindIndex +
                                                 " / " + PerformanceQueue.Rewind.Count + " <size=10>" +
                                                 LeanLocalization.GetTranslationText( "UI.ProgressBar.Modification" ) +
                                                 "</size>";

            courseProgress.GetComponent<Slider>().value = CubeController.courseIndex;
            courseProgress.GetComponent<Slider>().maxValue = PerformanceQueue.Course.Count;
            courseStepText.GetComponent<Text>().text = CubeController.courseIndex +
                                                       " / " + PerformanceQueue.Course.Count + " <size=10>" +
                                                       LeanLocalization.GetTranslationText( "UI.ProgressBar.TotalSteps" ) +
                                                       "</size>";
        }

        public void OnDrag( PointerEventData eventData )
        {
            PauseAt( (int)GetComponent<Slider>().value );
        }

        public void OnPointerDown( PointerEventData eventData )
        {
            PauseAt( (int)GetComponent<Slider>().value );
        }

        public static void SetPlayerButtonStatus( int status )
        {
            switch ( status )
            {
                case 0: // 停止播放时的状态
                    _instance.btnPlay.SetActive( true );
                    _instance.btnPause.SetActive( false );
                    break;
                case 1: // 播放中的状态
                    _instance.btnPlay.SetActive( false );
                    _instance.btnPause.SetActive( true );
                    break;
            }
        }

        public async void PauseAt( int rewindCursor )
        {
            SetPlayerButtonStatus( 0 );

            CubeController.runLevel = 0;
            Time.timeScale = 10;
            await UniTask.WaitUntil( () => !CubeController.inPlay );
            Time.timeScale = 0;
            CubeController.runLevel = 2;

            if ( PerformanceQueue.Rewind.Count == 0 ) return;

            CubeController.rewindIndex = rewindCursor;

            if ( rewindCursor == PerformanceQueue.Rewind.Count )
            {
                var step = PerformanceQueue.Rewind[rewindCursor - 1];
                GameManager.GenObjectsFromArray( step.Snapshot );
                Interlocked.Exchange( ref CubeController.courseIndex, PerformanceQueue.Course.Count );
                Ending( PerformanceQueue.Rewind[rewindCursor - 1].Algorithm );
            }
            else
            {
                var step = PerformanceQueue.Rewind[rewindCursor];
                GameManager.GenObjectsFromArray( PerformanceQueue.Course[step.Cursor].Snapshot );
                Interlocked.Exchange( ref CubeController.courseIndex, step.Cursor );
                Ending( step.Algorithm );
            }

            if ( rewindCursor == 0 ) CubeController.courseIndex = 0;
        }

        private void Ending( string algorithm )
        {
            switch ( algorithm )
            {
                case "Heap":
                    CompleteBinaryTree.ResetTree();
                    break;
                case "Radix":
                    break;
            }
        }
    }
}