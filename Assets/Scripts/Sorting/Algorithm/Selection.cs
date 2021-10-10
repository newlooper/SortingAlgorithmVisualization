namespace Sorting.Algorithm
{
    public class Selection
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;

            // one by one move boundary of unsorted subarray
            for ( var i = 0; i < n - 1; i++ )
            {
                // find the minimum element in unsorted array
                var min = i;
                for ( var j = i + 1; j < n; j++ )
                {
                    if ( arr[j] < arr[min] )
                    {
                        min = j;
                    }
                }

                // swap the found minimum element with the first element
                ( arr[min], arr[i] ) = ( arr[i], arr[min] );
            }
        }
    }
}