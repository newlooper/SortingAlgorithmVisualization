using System.Collections.Generic;
using Performance;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    public         Slider     min;
    public         Slider     max;
    public         Slider     count;
    private static GameObject _space;
    private static GameObject _cubeContainer;

    private void Awake()
    {
        _space = Resources.Load<GameObject>( "Prefabs/Space" );
        _cubeContainer = Resources.Load<GameObject>( "Prefabs/CubeContainer" );
    }

    private void Start()
    {
        GenObjects();
    }

    public static MyList<GameObject> Cubes { get; private set; } = new MyList<GameObject>();

    public static int[] Numbers { get; private set; } = { };

    public void GenObjects()
    {
        Destroy( GameObject.Find( "TreeContainer(Clone)" ) );
        CompleteBinaryTree.List.Clear();
        GenObjectsFromArray( GetUniqueRandomArray( (int)min.value, (int)max.value, (int)count.value ) );
        // GameObject.Find( "Main Camera" ).transform.LookAt( Cubes[Cubes.Count / 2].transform );
    }

    public static void GenObjectsFromArray( int[] arr )
    {
        Numbers = arr;
        Destroy( GameObject.Find( "Space(Clone)" ) );

        var parent = Instantiate( _space );
        Cubes = new MyList<GameObject>( arr.Length );
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

    public static void EnableButtons( bool enable )
    {
        GameObject.Find( "Gen" ).GetComponent<Button>().interactable = enable;
        GameObject.Find( "Sort" ).GetComponent<Button>().interactable = enable;
        GameObject.Find( "Rewind" ).GetComponent<Button>().interactable = enable;
        GameObject.Find( "Algorithm" ).GetComponent<Dropdown>().interactable = enable;
    }

    public class MyList<T>
    {
        private readonly List<T> _list;

        public MyList( int length )
        {
            _list = new List<T>( length );
        }

        public MyList()
        {
            _list = new List<T>();
        }

        public void Add( T obj )
        {
            _list.Add( obj );
        }

        public void Clear()
        {
            _list.Clear();
        }

        public int Count => _list.Count;

        public void Swap( int firstIndex, int secondIndex )
        {
            if ( firstIndex == secondIndex ) return;

            var left  = firstIndex < secondIndex ? firstIndex : secondIndex;
            var right = firstIndex > secondIndex ? firstIndex : secondIndex;

            var leftElement  = _list[left];
            var rightElement = _list[right];

            _list.RemoveAt( right );
            _list.Insert( right, leftElement );

            _list.RemoveAt( left );
            _list.Insert( left, rightElement );
        }

        public T this[ int index ]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}