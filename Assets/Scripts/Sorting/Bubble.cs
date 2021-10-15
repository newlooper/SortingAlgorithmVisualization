using Performance;

namespace Sorting
{
    public static class Bubble
    {
        public static void Sort( int[] arr )
        {
            var n = arr.Length;
            for ( var i = 0; i < n - 1; i++ )
            {
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForCodeLine( "For" ) );
                for ( var j = 0; j < n - i - 1; j++ )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( j, j + 1 ) );

                    if ( arr[j] > arr[j + 1] )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], j, j + 1 ) );
                        ( arr[j], arr[j + 1] ) = ( arr[j + 1], arr[j] );
                        PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], j, j + 1 ) );
                    }
                }
            }
        }
    }
}