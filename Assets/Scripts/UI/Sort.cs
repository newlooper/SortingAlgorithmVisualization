// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Sort : MonoBehaviour
    {
        public static string     className;
        public        GameObject playBar;
        public        GameObject algorithm;

        public void DoSort()
        {
            if ( GameManager.Numbers.Length < 2 ) return;

            PrintArray( GameManager.Numbers, " <- Original" ); // log original array

            var algDropdown = algorithm.GetComponent<Dropdown>();
            className = algDropdown.options[algDropdown.value].text;

            var cloneForPureSort = GameManager.Numbers.Clone() as int[];
            var sortOnly = new Thread( () =>
            {
                CallSortByClassName( "Sorting.Algorithm." + className, cloneForPureSort );
                PrintArray( cloneForPureSort, " <- After pure sorting" );
            } );
            sortOnly.Start(); // sort only for testing real performance of current algorithm

            GameManager.Rest();
            GameManager.GenObjectsFromArray( GameManager.Numbers, false );
            CallSortByClassName( "Sorting." + className, GameManager.Numbers ); // sorting visualization

            PrintArray( GameManager.Numbers, " <- After visual sorting" );

            playBar.GetComponent<PlayBar>().Play();
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
                BindingFlags.Public | BindingFlags.Static );
            func.Invoke( null, new object[] {numbers} );
        }
    }
}