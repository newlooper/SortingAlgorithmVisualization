namespace Sorting
{
    public static class Bubble
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;
            for ( var i = 0; i < n - 1; i++ )
            {
                for ( var j = 0; j < n - i - 1; j++ )
                {
                    var step = new PerformanceQueue.Step( null, j, j + 1 );

                    if ( arr[j] > arr[j + 1] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        ( arr[j], arr[j + 1] ) = ( arr[j + 1], arr[j] );
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }
            }
        }
    }
}