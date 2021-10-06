using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            var algDropdown = GameObject.Find( "Algorithm" ).GetComponent<Dropdown>();
            var className   = algDropdown.options[algDropdown.value].text;
            PrintArray( GameManager.Numbers );
            CallSortByClassName( className, GameManager.Numbers );
            PrintArray( GameManager.Numbers );
            StartCoroutine( CubeController.Play() );
        }

        private static void PrintArray( IEnumerable<int> arr )
        {
            var sb = new StringBuilder();

            foreach ( var i in arr )
            {
                sb.Append( i );
                sb.Append( ", " );
            }

            Debug.Log( sb.ToString() );
        }

        private static void CallSortByClassName( string classname, IEnumerable numbers )
        {
            var type = Type.GetType( "Sorting." + classname );
            var func = type.GetMethod( "Sort",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static );
            func.Invoke( null, new object[] {numbers} );
        }
    }
}