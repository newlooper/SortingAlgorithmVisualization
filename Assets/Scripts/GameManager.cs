using System.Collections.Generic;
using Performance;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cubeContainer;
    public Slider     min;
    public Slider     max;
    public Slider     count;
    public GameObject space;
    public GameObject spaceCopy;

    public static List<GameObject> Cubes { get; private set; } = new List<GameObject>();

    public static int[] Numbers { get; private set; } = { };

    public void GenObjects()
    {
        Numbers = GetUniqueRandomArray( (int)min.value, (int)max.value, (int)count.value );
        GenObjectsFromArray( Numbers );
    }

    public void GenObjectsFromArray( int[] arr )
    {
        Destroy( GameObject.Find( "Space(Clone)" ) );
        var parent = Instantiate( space );
        Cubes = new List<GameObject>( arr.Length );
        for ( var i = 0; i < arr.Length; i++ )
        {
            var cube = Instantiate( cubeContainer, new Vector3( i * 1.5f, 0f, 0f ), Quaternion.identity, parent.transform );
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