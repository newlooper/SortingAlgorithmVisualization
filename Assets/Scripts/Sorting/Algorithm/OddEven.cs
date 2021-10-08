namespace Sorting.Algorithm
{
    public class OddEven
    {
        public static void Sort( int[] arr )
        {
            var sorted = false;
            while ( !sorted )
            {
                sorted = true;
                for ( var i = 1; i < arr.Length - 1; i += 2 )
                {
                    if ( arr[i] > arr[i + 1] )
                    {
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        sorted = false;
                    }
                }

                for ( var i = 0; i < arr.Length - 1; i += 2 )
                {
                    if ( arr[i] > arr[i + 1] )
                    {
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );

                        sorted = false;
                    }
                }
            }
        }
    }
}