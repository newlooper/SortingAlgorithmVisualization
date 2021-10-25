// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using UI;
using UnityEngine;

namespace Performance
{
    public class CompleteBinaryTree : MonoBehaviour
    {
        private static GameObject                     _treeContainer;
        public static  GameManager.MyList<GameObject> treeNodes = new GameManager.MyList<GameObject>();

        public static void BuildTree()
        {
            ClearTree();
            _treeContainer = Instantiate( Resources.Load<GameObject>( "Prefabs/TreeContainer" ) );

            const int storeyHeight = 3;
            var       heapSize     = GameManager.Cubes.Count;
            var       k            = (int)( Math.Floor( Math.Log( heapSize, 2 ) ) + 1 );
            treeNodes = new GameManager.MyList<GameObject>( heapSize );

            for ( int nodeIndexInHeap = 0, layer = 1; layer <= k; layer++ )
            {
                int nodesOfOneLayer;
                if ( layer == k )
                {
                    nodesOfOneLayer = (int)( heapSize - Math.Pow( 2, layer - 1 ) + 1 );
                }
                else
                {
                    nodesOfOneLayer = (int)Math.Pow( 2, layer - 1 );
                }

                var mostLeftNodePos   = (int)Math.Pow( 2, k - layer ) - 1;
                var horizontalSpacing = (int)Math.Pow( 2, k - layer + 1 );
                for ( var i = 0; i < nodesOfOneLayer; i++ )
                {
                    var node = Instantiate( GameManager.Cubes[nodeIndexInHeap++],
                        new Vector3( mostLeftNodePos + horizontalSpacing * i,
                            ( k - layer ) * storeyHeight,
                            _treeContainer.transform.position.z ),
                        Quaternion.identity );
                    node.transform.SetParent( _treeContainer.transform );
                    node.transform.Find( "Cube" ).transform.localScale = Vector3.one;
                    treeNodes.Add( node );
                }
            }

            var lineMaterial = new Material( Shader.Find( "Sprites/Default" ) );

            for ( var i = 1; i < treeNodes.Count; i++ )
            {
                var node    = treeNodes[i];
                var parent  = treeNodes[(int)( Math.Floor( i - 1f ) / 2 )];
                var lineBox = new GameObject();
                lineBox.transform.SetParent( _treeContainer.transform );
                lineBox.transform.position = node.transform.position;
                var lr = lineBox.AddComponent<LineRenderer>();
                lr.material = lineMaterial;
                lr.widthMultiplier = 0.05f;
                lr.useWorldSpace = false;
                lr.positionCount = 2;
                lr.SetPosition( 0, Vector3.zero );
                lr.SetPosition( 1, parent.transform.position - lineBox.transform.position );
            }
        }

        public static void ResetTree()
        {
            for ( var i = 0; i < treeNodes.Count; i++ )
            {
                treeNodes[i].GetComponent<CubeController>().SetValue( GameManager.Numbers[i], false );
            }
        }

        public static void ClearTree()
        {
            Destroy( _treeContainer );
        }

        private void Update()
        {
            _treeContainer.SetActive( Sort.className == "Heap" );
        }
    }
}