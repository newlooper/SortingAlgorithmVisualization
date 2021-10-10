using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Performance;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class Sort : MonoBehaviour
    {
        public void DoSort()
        {
            if ( GameManager.Numbers.Length < 2 ) return;
            GameObject.Find( "Gen" ).GetComponent<Button>().enabled = false;
            GameObject.Find( "Sort" ).GetComponent<Button>().enabled = false;
            GameObject.Find( "Rewind" ).GetComponent<Button>().enabled = false;

            PrintArray( GameManager.Numbers, " <- Original" ); // log original array

            var algDropdown = GameObject.Find( "Algorithm" ).GetComponent<Dropdown>();
            var className   = algDropdown.options[algDropdown.value].text;

            var cloneForPureSort = GameManager.Numbers.Clone() as int[];
            var sortOnly = new Thread( () =>
            {
                CallSortByClassName( "Sorting.Algorithm." + className, cloneForPureSort );
                PrintArray( cloneForPureSort, " <- After pure sorting" );
            } );
            sortOnly.Start(); // sort only for testing real performance of current algorithm

            PerformanceQueue.Rewind.Clear();
            CallSortByClassName( "Sorting." + className, GameManager.Numbers ); // sorting visualization
            PrintArray( GameManager.Numbers, " <- After visual sorting" );
            StartCoroutine( CubeController.Play() );
        }

        public void Rewind()
        {
            GameObject.Find( "Gen" ).GetComponent<Button>().enabled = false;
            GameObject.Find( "Sort" ).GetComponent<Button>().enabled = false;
            GameObject.Find( "Rewind" ).GetComponent<Button>().enabled = false;
            StartCoroutine( CubeController.Rewind() );
        }

        public static void PrintArray( IEnumerable<int> arr, string postfix = "", string prefix = "" )
        {
            var sb = new StringBuilder();

            foreach ( var i in arr )
            {
                sb.Append( i );
                sb.Append( " " );
            }

            Debug.Log( prefix + sb + postfix );
        }

        private static void CallSortByClassName( string classname, IEnumerable numbers )
        {
            var type = Type.GetType( classname );
            var func = type.GetMethod( "Sort",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static );
            func.Invoke( null, new object[] {numbers} );
        }
    }
}