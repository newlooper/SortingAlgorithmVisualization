using Performance;

namespace Sorting
{
    public class Gnome
    {
        public static void GnomeSort( int[] arr, int length )
        {
            var index = 0;

            while ( index < length )
            {
                // if there is no pot next to the gnome, he is done.
                if ( index == 0 ) // if the gnome is at the start of the line...
                {
                    index++; // he steps forward
                }

                PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSelectTwo( index, index - 1 ) );

                // if the pots next to the gnome are in the correct order...
                if ( arr[index] >= arr[index - 1] )
                {
                    index++; // he goes to the next pot
                }
                else
                {
                    PerformanceQueue.Course.Enqueue( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], index, index - 1 ) );
                    // if the pots are in the wrong order, he switches them.
                    ( arr[index], arr[index - 1] ) = ( arr[index - 1], arr[index] );
                    PerformanceQueue.Rewind.Push( PerformanceQueue.Step.CreateStepForSwap( arr.Clone() as int[], index, index - 1 ) );
                    index--;
                }
            }
        }

        public static void Sort( int[] arr )
        {
            GnomeSort( arr, arr.Length );
        }
    }
}