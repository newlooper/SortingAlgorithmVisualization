// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using Performance;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public GameObject btnPlay;
        public GameObject btnPause;
        public GameObject stepText;

        private static ProgressBar _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            stepText.GetComponent<Text>().text = GetComponent<Slider>().value +
                                                 " / " + PerformanceQueue.Rewind.Count +
                                                 " <size=10>Swap &| Write</size>";
        }

        public void OnDrag( PointerEventData eventData )
        {
            StartCoroutine( PauseAt( (int)GetComponent<Slider>().value ) );
        }

        public void OnPointerDown( PointerEventData eventData )
        {
            StartCoroutine( PauseAt( (int)GetComponent<Slider>().value ) );
        }

        public static void SwitchPlayPause()
        {
            _instance.btnPlay.SetActive( true );
            _instance.btnPause.SetActive( false );
        }

        public IEnumerator PauseAt( int rewindCursor )
        {
            btnPlay.SetActive( true );
            btnPause.SetActive( false );

            CubeController.canPlay = false;
            Time.timeScale = 10;
            yield return new WaitUntil( () => !CubeController.inPlay );
            Time.timeScale = 0;
            CubeController.canPlay = true;

            if ( PerformanceQueue.Rewind.Count == 0 )
            {
                yield break;
            }

            if ( rewindCursor == PerformanceQueue.Rewind.Count )
            {
                GameManager.GenObjectsFromArray( PerformanceQueue.Rewind[rewindCursor - 1].Snapshot );
                CubeController.index = PerformanceQueue.Course.Count;
                switch ( PerformanceQueue.Rewind[rewindCursor - 1].Algorithm )
                {
                    case "Heap":
                        CompleteBinaryTree.BuildTree();
                        break;
                }
            }
            else
            {
                var step = PerformanceQueue.Rewind[rewindCursor];
                GameManager.GenObjectsFromArray( PerformanceQueue.Course[step.Cursor].Snapshot );
                CubeController.index = step.Cursor;
                switch ( step.Algorithm )
                {
                    case "Heap":
                        CompleteBinaryTree.BuildTree();
                        break;
                }
            }
        }
    }
}