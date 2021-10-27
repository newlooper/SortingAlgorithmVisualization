// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.Linq;
using Lean.Localization;
using Performance;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameObject  _space;
    private static GameObject  _spacePrefab;
    private static GameObject  _cubePrefab;
    private static GameObject  _codeLinePanel;
    private static GameObject  _menu;
    private static GameManager _instance;
    public         Slider      min;
    public         Slider      max;
    public         Slider      count;

    public static MyList<GameObject> Cubes { get; private set; } = new MyList<GameObject>();

    public static int[] Numbers { get; private set; } = { };

    private void Awake()
    {
        _spacePrefab = Resources.Load<GameObject>( "Prefabs/Space" );
        _cubePrefab = Resources.Load<GameObject>( "Prefabs/CubeContainer" );
        _codeLinePanel = GameObject.FindWithTag( "CodeLinePanel" );
        _menu = GameObject.Find( "SliderMenu" );
        _instance = this;
    }

    private void Start()
    {
        GenObjects();
    }

    private void Update()
    {
        if ( Input.GetKeyUp( KeyCode.C ) ) _codeLinePanel.SetActive( !_codeLinePanel.activeSelf );

        if ( Input.GetKeyUp( KeyCode.M ) ) _menu.GetComponent<SliderMenu>().ShowHideMenu();

        if ( Input.GetKeyUp( KeyCode.L ) )
        {
            var currentLanguage = GameObject.Find( "LeanLocalization" ).GetComponent<LeanLocalization>().CurrentLanguage;
            var names           = LeanLocalization.CurrentLanguages.Keys.ToList();
            var nextIndex       = ( names.IndexOf( currentLanguage ) + 1 ) % LeanLocalization.CurrentLanguages.Count;
            LeanLocalization.SetCurrentLanguageAll( names[nextIndex] );
        }
    }

    public void GenObjects()
    {
        Rest();
        CompleteBinaryTree.ClearTree();
        GenObjectsFromArray( GetUniqueRandomArray( (int)min.value, (int)max.value, (int)count.value ), false );
    }

    public static void GenObjectsFromArray( int[] arr, bool reuse = true )
    {
        if ( reuse )
        {
            Reuse( arr );
            return;
        }

        Destroy( _space );
        _space = Instantiate( _spacePrefab );

        Numbers = arr;
        Cubes = new MyList<GameObject>( Numbers.Length );

        for ( var i = 0; i < Numbers.Length; i++ )
        {
            var cube = Instantiate( _cubePrefab,
                new Vector3( i * Config.HorizontalGap, 0f, 0f ),
                Quaternion.identity, _space.transform );
            cube.GetComponent<CubeController>().SetValue( Numbers[i] );
            Cubes.Add( cube );
        }
    }

    private static void Reuse( int[] arr )
    {
        Numbers = arr;
        for ( var i = 0; i < Numbers.Length; i++ )
        {
            Cubes[i].transform.position = new Vector3( i * Config.HorizontalGap, 0f, 0f );
            Cubes[i].GetComponent<CubeController>().SetValue( Numbers[i] );
            CubeController.SetPillarMaterial( Cubes[i], Config.DefaultCube );
        }
    }

    private static int[] GetUniqueRandomArray( int minNum, int maxNum, int count )
    {
        if ( maxNum - minNum < count )
        {
            maxNum = count + minNum;
            _instance.max.value = maxNum;
        }

        var result         = new int[count];
        var numbersInOrder = new List<int>();
        for ( var x = minNum; x < maxNum; x++ ) numbersInOrder.Add( x );

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
        GameObject.Find( "Algorithm" ).GetComponent<Dropdown>().interactable = enable;
    }

    public static void Rest()
    {
        PerformanceQueue.Course.Clear();
        PerformanceQueue.Rewind.Clear();
        CubeController.courseIndex = 0;
        CubeController.rewindIndex = 0;
        CubeController.inPlay = false;
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

        public int Count => _list.Count;

        public T this[ int index ]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public void Add( T obj )
        {
            _list.Add( obj );
        }

        public void Clear()
        {
            _list.Clear();
        }

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
    }
}