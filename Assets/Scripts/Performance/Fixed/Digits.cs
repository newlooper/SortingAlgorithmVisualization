// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Performance.Fixed
{
    public class Digits : MonoBehaviour
    {
        public GameObject digitsBox;

        public static readonly List<Vector3> Positions = new List<Vector3>();

        // Start is called before the first frame update
        private void Start()
        {
            var i = 0;
            foreach ( Transform digit in digitsBox.transform )
            {
                digit.localScale = new Vector3( 3, 3, 3 );
                digit.position += new Vector3( i++ * 3f, 0, -3f );
                digit.GetComponent<MeshRenderer>().material.color = Color.Lerp( Color.cyan, Color.blue, i / 10.0f );
                Positions.Add( digit.transform.position );
            }
        }

        // Update is called once per frame
        private void Update()
        {
            digitsBox.SetActive( Sort.className == "Radix" );
        }
    }
}