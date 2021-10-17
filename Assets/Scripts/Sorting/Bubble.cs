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
                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "For" ) );
                for ( var j = 0; j < n - i - 1; j++ )
                {
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "For2" ) );
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSelectTwo( j, j + 1 ) );

                    if ( arr[j] > arr[j + 1] )
                    {
                        PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], j, j + 1 ) );
                        ( arr[j], arr[j + 1] ) = ( arr[j + 1], arr[j] );
                        PerformanceQueue.Rewind.Add(
                            PerformanceQueue.Step.CreateStepForSwap(
                                arr.Clone() as int[], j, j + 1, "Swap", PerformanceQueue.Course.Count - 1 ) );
                    }
                }
            }
        }
    }
}