using Performance;

namespace Sorting
{
    public static class Quick
    {
        public static void QuickSort( int[] numbers, int left, int right )
        {
            if ( left >= right ) return;

            var middleValue = numbers[( left + right ) / 2];
            var cursorLeft  = left - 1;
            var cursorRight = right + 1;
            while ( true )
            {
                while ( numbers[++cursorLeft] < middleValue )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, right ) );
                }

                while ( numbers[--cursorRight] > middleValue )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight ) );
                }

                if ( cursorLeft >= cursorRight )
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight ) );
                    break;
                }

                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( cursorLeft, cursorRight ) );
                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwap( numbers.Clone() as int[], cursorLeft, cursorRight ) );
                ( numbers[cursorLeft], numbers[cursorRight] ) = ( numbers[cursorRight], numbers[cursorLeft] );
                PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( numbers.Clone() as int[], cursorLeft, cursorRight ) );
            }

            QuickSort( numbers, left, cursorLeft - 1 );
            QuickSort( numbers, cursorRight + 1, right );
        }

        public static void Sort( int[] numbers )
        {
            QuickSort( numbers, 0, numbers.Length - 1 );
        }
    }
}