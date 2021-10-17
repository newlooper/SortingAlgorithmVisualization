using Performance;

namespace Sorting
{
    public static class Quick
    {
        public static void QuickSort( int[] arr, int left, int right )
        {
            if ( left >= right ) return;

            var middleValue = arr[( left + right ) / 2];
            var cursorLeft  = left - 1;
            var cursorRight = right + 1;
            while ( true )
            {
                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "While" ) );
                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "Selected" ) );
                while ( arr[++cursorLeft] < middleValue )
                {
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, right ) );
                }

                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "Selected2" ) );
                while ( arr[--cursorRight] > middleValue )
                {
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight, "Selected2" ) );
                }

                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForCodeLine( "IF" ) );
                if ( cursorLeft >= cursorRight )
                {
                    PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight, "Selected3" ) );
                    break;
                }

                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight, "Selected4" ) );
                PerformanceQueue.Course.Add( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], cursorLeft, cursorRight ) );
                ( arr[cursorLeft], arr[cursorRight] ) = ( arr[cursorRight], arr[cursorLeft] );
                PerformanceQueue.Rewind.Add( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], cursorLeft, cursorRight, "Swap",
                    PerformanceQueue.Course.Count - 1 ) );
            }

            QuickSort( arr, left, cursorLeft - 1 );
            QuickSort( arr, cursorRight + 1, right );
        }

        public static void Sort( int[] arr )
        {
            QuickSort( arr, 0, arr.Length - 1 );
        }
    }
}