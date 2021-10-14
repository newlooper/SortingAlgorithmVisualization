using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public partial class CubeController : MonoBehaviour
    {
        private static CubeController _instance;
        private static Slider         _speed;
        private static Slider         _progress;

        public static  float Gap          { get; set; }
        private static float DefaultDelay { get; set; }

        private void Awake()
        {
            _instance = this;
            _speed = GameObject.Find( "SliderSpeed" ).GetComponent<Slider>();
            _progress = GameObject.Find( "Progress" ).GetComponent<Slider>();
            Gap = 1.5f;
            DefaultDelay = 1f;
        }

        public void SetButtonText( string str )
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
            _progress.minValue = 0;
            _progress.maxValue = PerformanceQueue.Rewind.Count;
            _progress.value = 0;
            _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();

            while ( PerformanceQueue.Course.TryDequeue( out var step ) )
            {
                switch ( step.PerformanceEffect )
                {
                    case PerformanceQueue.PerformanceEffect.SelectTwo:
                        yield return HighlightTwoWithIndex( step.Left, step.Right, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.Swap:
                        yield return SwapWithIndex( step.Left, step.Right, step );
                        _progress.value++;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    case PerformanceQueue.PerformanceEffect.SelectOne:
                        yield return HighlightOneWithIndex( step.Left, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.UnSelectOne:
                        yield return HighlightOneWithIndex( step.Left, step, 0 );
                        break;
                    case PerformanceQueue.PerformanceEffect.ChangeSelection:
                        yield return HighlightSelectionWithIndex( step.Left, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.NewMin:
                        yield return HighlightChange( step.Left, step.Right, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.JumpOut:
                        yield return JumpOut( step.Left, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.JumpIn:
                        yield return JumpIn( step );
                        break;
                    case PerformanceQueue.PerformanceEffect.SwapCopy:
                        yield return SwapCopy( step.Left, step.Right, step );
                        _progress.value++;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    case PerformanceQueue.PerformanceEffect.Auxiliary:
                        yield return SimpleMove( step.Left, step.Right, step );
                        break;
                    case PerformanceQueue.PerformanceEffect.AuxiliaryBack:
                        yield return AuxiliaryBack( step );
                        break;
                    case PerformanceQueue.PerformanceEffect.MergeHistory:
                        _progress.value++;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    case PerformanceQueue.PerformanceEffect.SwapHeap:
                        yield return SwapHeapWithIndex( step.Left, step.Right, step );
                        _progress.value++;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // _progress.interactable = true;

            GameObject.Find( "Gen" ).GetComponent<Button>().enabled = true;
            GameObject.Find( "Sort" ).GetComponent<Button>().enabled = true;
            GameObject.Find( "Rewind" ).GetComponent<Button>().enabled = true;
        }

        public static IEnumerator Rewind()
        {
            while ( PerformanceQueue.Rewind.Count > 0 )
            {
                var step = PerformanceQueue.Rewind.Pop();

                switch ( step.PerformanceEffect )
                {
                    case PerformanceQueue.PerformanceEffect.Swap:
                        yield return SwapWithIndex( step.Right, step.Left, step );
                        ( GameManager.Numbers[step.Left], GameManager.Numbers[step.Right] ) =
                            ( GameManager.Numbers[step.Right], GameManager.Numbers[step.Left] );
                        _progress.value--;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    case PerformanceQueue.PerformanceEffect.MergeHistory:
                        yield return MergeRewind( step );
                        _progress.value--;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    case PerformanceQueue.PerformanceEffect.SwapHeap:
                        yield return SwapHeapWithIndex( step.Right, step.Left, step );
                        ( GameManager.Numbers[step.Left], GameManager.Numbers[step.Right] ) =
                            ( GameManager.Numbers[step.Right], GameManager.Numbers[step.Left] );
                        _progress.value--;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            GameObject.Find( "Gen" ).GetComponent<Button>().enabled = true;
            GameObject.Find( "Sort" ).GetComponent<Button>().enabled = true;
            GameObject.Find( "Rewind" ).GetComponent<Button>().enabled = true;
        }


        private static void SetPillarMaterial( GameObject go, Material mat )
        {
            go.transform.Find( "Cube" ).transform.Find( "Pillar" ).GetComponent<MeshRenderer>().material = mat;
        }

        private static IEnumerator Move( GameObject from, PerformanceQueue.Pace[] paces )
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
                    from.transform.position = Vector3.MoveTowards( from.transform.position, pace.Target, speed * Time.deltaTime );
                    yield return null;
                }
            }
        }
    }
}