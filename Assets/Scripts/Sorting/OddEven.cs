namespace Sorting
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
                    var step = new PerformanceQueue.Step( null, i, i + 1 );
                    if ( arr[i] > arr[i + 1] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, i + 1 ) );
                        sorted = false;
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }

                for ( var i = 0; i < arr.Length - 1; i += 2 )
                {
                    var step = new PerformanceQueue.Step( null, i, i + 1 );
                    if ( arr[i] > arr[i + 1] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, i + 1 ) );
                        sorted = false;
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }
            }
        }
    }
}