using Performance;

namespace Sorting
{
    public class Insertion
    {
        public static void Sort( int[] arr )
        {
            for ( var i = 0; i < arr.Length - 1; i++ )
            {
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSorted( i ) );

                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForJumpOut( i + 1 ) );
                for ( var j = i + 1; j > 0; j-- )
                {
                    if ( arr[j - 1] > arr[j] )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwapCopy( arr.Clone() as int[], j, j - 1 ) );
                        ( arr[j - 1], arr[j] ) = ( arr[j], arr[j - 1] );
                        PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], j, j - 1 ) );
                    }
                }

                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForJumpIn() );
            }
        }
    }
}