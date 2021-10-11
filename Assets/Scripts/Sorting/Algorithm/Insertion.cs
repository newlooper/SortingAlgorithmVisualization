namespace Sorting.Algorithm
{
    public class Insertion
    {
        public static void Sort( int[] arr )
        {
            for ( var i = 0; i < arr.Length - 1; i++ )
            {
                for ( var j = i + 1; j > 0; j-- )
                {
                    if ( arr[j - 1] > arr[j] )
                    {
                        ( arr[j - 1], arr[j] ) = ( arr[j], arr[j - 1] );
                    }
                }
            }
        }
    }
}