using Performance;

namespace Sorting
{
    public class Cocktail
    {
        public static void Sort( int[] arr )
        {
            var swapped = true;
            var start   = 0;
            var end     = arr.Length;

            while ( swapped )
            {
                // reset the swapped flag on entering the
                // loop, because it might be true from a
                // previous iteration.
                swapped = false;

                // loop from bottom to top same as
                // the bubble sort
                for ( var i = start; i < end - 1; ++i )
                {
                    var step = new PerformanceQueue.Step( null, i, i + 1 );
                    if ( arr[i] > arr[i + 1] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, i + 1 ) );
                        swapped = true;
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }

                // if nothing moved, then array is sorted.
                if ( swapped == false )
                    break;

                // otherwise, reset the swapped flag so that it
                // can be used in the next stage
                swapped = false;

                // move the end point back by one, because
                // item at the end is in its rightful spot
                end -= 1;

                // from top to bottom, doing the
                // same comparison as in the previous stage
                for ( var i = end - 1; i >= start; i-- )
                {
                    var step = new PerformanceQueue.Step( null, i, i + 1 );
                    if ( arr[i] > arr[i + 1] )
                    {
                        step.Snapshot = arr.Clone() as int[];
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( new PerformanceQueue.Step( arr.Clone() as int[], i, i + 1 ) );
                        swapped = true;
                    }

                    PerformanceQueue.Course.Enqueue( step );
                }

                // increase the starting point, because
                // the last stage would have moved the next
                // smallest number to its rightful spot.
                start += 1;
            }
        }
    }
}