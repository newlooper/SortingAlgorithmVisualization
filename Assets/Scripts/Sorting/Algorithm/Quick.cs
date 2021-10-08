namespace Sorting.Algorithm
{
    public class Quick
    {
        public static void QuickSort( int[] numbers, int left, int right )
        {
            if ( left >= right ) return;

            var middleValue = numbers[( left + right ) / 2];
            var cursorLeft  = left - 1;
            var cursorRight = right + 1;
            while ( true )
            {
                while ( numbers[++cursorLeft] < middleValue ) ;

                while ( numbers[--cursorRight] > middleValue ) ;

                if ( cursorLeft >= cursorRight )
                    break;

                ( numbers[cursorLeft], numbers[cursorRight] ) = ( numbers[cursorRight], numbers[cursorLeft] );
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