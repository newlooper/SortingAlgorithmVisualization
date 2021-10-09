using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    private static CubeController _instance;
    private static Slider         _speed;
    private static Slider         _progress;

    private void Awake()
    {
        _instance = this;
        _speed = GameObject.Find( "SliderSpeed" ).GetComponent<Slider>();
        _progress = GameObject.Find( "Progress" ).GetComponent<Slider>();
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
                case PerformanceQueue.PerformanceEffect.Default:
                    yield return SwapWithIndex( step.Left, step.Right, step.Snapshot );
                    if ( step.Snapshot != null )
                    {
                        _progress.value++;
                        _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                    }

                    break;
                case PerformanceQueue.PerformanceEffect.Copy:
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
                case PerformanceQueue.PerformanceEffect.Default:
                    yield return SwapWithIndex( step.Right, step.Left, step.Snapshot );
                    ( GameManager.Numbers[step.Left], GameManager.Numbers[step.Right] ) = ( GameManager.Numbers[step.Right], GameManager.Numbers[step.Left] );
                    _progress.value--;
                    _progress.GetComponentInChildren<Text>().text = _progress.value.ToString();
                    break;
                case PerformanceQueue.PerformanceEffect.Copy:
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

    private static IEnumerator SwapWithIndex( int left, int right, int[] snapshot )
    {
        var cubes        = GameManager.Cubes;
        var cubeSelected = Resources.Load<Material>( "Materials/CubeSelected" );
        var cubeDefault  = Resources.Load<Material>( "Materials/Cube" );

        SetPillarMaterial( cubes[left], cubeSelected );
        SetPillarMaterial( cubes[right], cubeSelected );
        yield return new WaitForSeconds( 1f / _speed.value );

        if ( snapshot == null )
        {
            SetPillarMaterial( cubes[left], cubeDefault );
            SetPillarMaterial( cubes[right], cubeDefault );
            yield break;
        }

        /////////////////////////////////////////////////////////////////
        /// 1、对于就地交换数值的排序算法，被排序数组的索引(也就是各个元素的位置)是不变的
        /// 2、排序算法所操作的数组，与界面上展示用的 List<GameObject> 是两个独立的集合
        /// 若在界面上移动原始对象，虽然看上去变更了顺序，但其实只是对象的 transform 变了，
        /// 而 GameManager.Cubes 中的元素位置并没有变化，随着排序算法的进行，展示效果将出现错误
        /// 要解决该问题，可以：
        /// 一、让 GameManager.Cubes 中的元素排列、界面上的对象位置排列、以及被排序数组内容时刻都保持一致，但此举势必导致大量的对象销毁和重建

        /// 二、排序时的移动效果并不通过操作原始 GameObject 展示，只要把值即将交换的两个对象进行克隆，让克隆的对象展示移动效果即可
        /// 如下所示：
        /// 克隆本应该被移动的两个对象，该时刻的信息完全一致
        var leftClone  = Instantiate( cubes[left].gameObject, cubes[left].transform.position, Quaternion.identity );
        var rightClone = Instantiate( cubes[right].gameObject, cubes[right].transform.position, Quaternion.identity );
        // Debug.Log( $"{left}[{snapshot[left]}] <-> {right}[{snapshot[right]}]" );

        ////////////////////////////
        /// 移动克隆的对象之前隐藏原始对象
        cubes[left].SetActive( false );
        cubes[right].SetActive( false );

        ////////////////////////////
        /// 交换原始对象的显示数值，虽然交换，但暂时不可见
        var tmp = snapshot[left];
        cubes[left].GetComponent<CubeController>().SetValue( snapshot[right] );
        cubes[right].GetComponent<CubeController>().SetValue( tmp );

        ////////////
        /// 用克隆对象展示移动效果
        yield return SwapTwoObjectPosition( leftClone, rightClone );

        ////////////
        /// 移动效果完成，恢复原始对象的显示
        SetPillarMaterial( cubes[left], cubeDefault );
        SetPillarMaterial( cubes[right], cubeDefault );

        cubes[left].SetActive( true );
        cubes[right].SetActive( true );
    }

    private static IEnumerator SwapTwoObjectPosition( GameObject one, GameObject two )
    {
        var cubeInMoving = Resources.Load<Material>( "Materials/CubeInMoving" );
        SetPillarMaterial( one, cubeInMoving );
        SetPillarMaterial( two, cubeInMoving );

        _instance.StartCoroutine( MoveInHigh( one, two.transform.position ) );
        return MoveInLow( two, one.transform.position );
    }

    private static IEnumerator MoveInHigh( GameObject mvObj, Vector3 target )
    {
        var relay1 = mvObj.transform.position + new Vector3( 0, 0, 1.5f );
        var relay2 = target + new Vector3( 0, 0, 1.5f );
        return Move( mvObj, target, relay1, relay2 );
    }

    private static IEnumerator MoveInLow( GameObject mvObj, Vector3 target )
    {
        var relay1 = mvObj.transform.position + new Vector3( 0, 0, -1.5f );
        var relay2 = target + new Vector3( 0, 0, -1.5f );
        return Move( mvObj, target, relay1, relay2 );
    }

    private static IEnumerator Move( GameObject from, Vector3 target, Vector3 relay1, Vector3 relay2 )
    {
        while ( from.transform.position != relay1 )
        {
            from.transform.position = Vector3.MoveTowards( from.transform.position, relay1, _speed.value * Time.deltaTime );
            yield return 0;
        }

        while ( from.transform.position != relay2 )
        {
            from.transform.position = Vector3.MoveTowards( from.transform.position, relay2, _speed.value * Time.deltaTime );
            yield return 0;
        }

        while ( from.transform.position != target )
        {
            from.transform.position = Vector3.MoveTowards( from.transform.position, target, _speed.value * Time.deltaTime );
            yield return 0;
        }

        Destroy( from ); // 展示完效果就自行销毁
    }
}