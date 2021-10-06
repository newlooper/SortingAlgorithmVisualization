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
                    PerformanceQueue.Course.Enqueue( new PerformanceQueue.Step( null, cursorLeft, right ) );
                }

                while ( numbers[--cursorRight] > middleValue )
                {
                    PerformanceQueue.Course.Enqueue( new PerformanceQueue.Step( null, cursorLeft, cursorRight ) );
                }

                if ( cursorLeft >= cursorRight )
                {
                    PerformanceQueue.Course.Enqueue( new PerformanceQueue.Step( null, cursorLeft, cursorRight ) );
                    break;
                }

                var step = new PerformanceQueue.Step( numbers.Clone() as int[], cursorLeft, cursorRight );

                ( numbers[cursorLeft], numbers[cursorRight] ) = ( numbers[cursorRight], numbers[cursorLeft] );
                PerformanceQueue.Course.Enqueue( step );
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