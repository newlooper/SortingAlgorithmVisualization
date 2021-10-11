using Performance;

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
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( i, i + 1 ) );

                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], i, i + 1 ) );
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], i, i + 1 ) );
                        sorted = false;
                    }
                }

                for ( var i = 0; i < arr.Length - 1; i += 2 )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( i, i + 1 ) );

                    if ( arr[i] > arr[i + 1] )
                    {
                        PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], i, i + 1 ) );
                        ( arr[i], arr[i + 1] ) = ( arr[i + 1], arr[i] );
                        PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], i, i + 1 ) );
                        sorted = false;
                    }
                }
            }
        }
    }
}