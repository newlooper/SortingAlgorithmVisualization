using System.Collections.Generic;
using Performance;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public         Slider     min;
    public         Slider     max;
    public         Slider     count;
    private static GameObject _space;
    private static GameObject _cubeContainer;

    private void Awake()
    {
        _space = AssetDatabase.LoadAssetAtPath<GameObject>( "Assets/Prefabs/Space.prefab" );
        _cubeContainer = AssetDatabase.LoadAssetAtPath<GameObject>( "Assets/Prefabs/CubeContainer.prefab" );
    }

    public static List<GameObject> Cubes { get; private set; } = new List<GameObject>();

    public static int[] Numbers { get; private set; } = { };

    public void GenObjects()
    {
        GenObjectsFromArray( GetUniqueRandomArray( (int)min.value, (int)max.value, (int)count.value ) );
    }

    public static void GenObjectsFromArray( int[] arr )
    {
        Numbers = arr;
        Destroy( GameObject.Find( "Space(Clone)" ) );

        var parent = Instantiate( _space );
        Cubes = new List<GameObject>( arr.Length );
        for ( var i = 0; i < arr.Length; i++ )
        {
            var cube = Instantiate( _cubeContainer, new Vector3( i * CubeController.Gap, 0f, 0f ), Quaternion.identity, parent.transform );
            cube.GetComponent<CubeController>().SetValue( arr[i] );
            Cubes.Add( cube );
        }
    }

    private static int[] GetUniqueRandomArray( int min, int max, int count )
    {
        var result         = new int[count];
        var numbersInOrder = new List<int>();
        for ( var x = min; x < max; x++ )
        {
            numbersInOrder.Add( x );
        }

        for ( var x = 0; x < count; x++ )
        {
            var randomIndex = Random.Range( 0, numbersInOrder.Count );
            result[x] = numbersInOrder[randomIndex];
            numbersInOrder.RemoveAt( randomIndex );
        }

        return result;
    }
}