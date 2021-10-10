using Performance;

namespace Sorting
{
    public class Comb
    {
        static int GetNextGap( int gap )
        {
            // The "shrink factor", empirically shown to be 1.3
            gap = ( gap * 10 ) / 13;
            if ( gap < 1 )
            {
                return 1;
            }

            return gap;
        }

        public static void Sort( int[] arr )
        {
            var length = arr.Length;
            var gap    = length;

            // We initialize this as true to enter the while loop.
            var swapped = true;

            while ( gap != 1 || swapped )
            {
                gap = GetNextGap( gap );

                // Set swapped as false.  Will go to true when two values are swapped.
                swapped = false;

                // Compare all elements with current gap 
                for ( var i = 0; i < length - gap; i++ )
                {
                    var step = new PerformanceQueue.Step( null, i, i + gap );
                    if ( arr[i] > arr[i + gap] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        // Swap
                        ( arr[i], arr[i + gap] ) = ( arr[i + gap], arr[i] );

                        PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, i + gap ) );
                        swapped = true;
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }
            }
        }
    }
}