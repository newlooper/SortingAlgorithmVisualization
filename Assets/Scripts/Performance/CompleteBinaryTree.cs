using System;
using UnityEngine;

namespace Performance
{
    public class CompleteBinaryTree : MonoBehaviour
    {
        private static GameObject                     _treeContainer;
        public static  GameManager.MyList<GameObject> List = new GameManager.MyList<GameObject>();

        public static void BuildTree()
        {
            ClearTree();
            _treeContainer = Instantiate( Resources.Load<GameObject>( "Prefabs/TreeContainer" ) );

            const int storeyHeight = 3;
            var       heapSize     = GameManager.Cubes.Count;
            var       k            = (int)( Math.Floor( Math.Log( heapSize, 2 ) ) + 1 );
            List = new GameManager.MyList<GameObject>( heapSize );

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
                    node.transform.Find( "Cube" ).transform.localScale = new Vector3( 1, 1, 1 );
                    List.Add( node );
                }
            }

            var lineMaterial = new Material( Shader.Find( "Sprites/Default" ) );

            for ( var i = 1; i < List.Count; i++ )
            {
                var node    = List[i];
                var parent  = List[(int)( Math.Floor( i - 1f ) / 2 )];
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

        public static void ClearTree()
        {
            Destroy( _treeContainer );
        }
    }
}