using System.Collections.Generic;

namespace Performance.ProducerConsumer
{
    public class FixedSizeQueue<T>
    {
        private readonly List<T> _queue   = new List<T>();
        private readonly object  _syncObj = new object();

        private int Size { get; set; }

        public FixedSizeQueue( int size )
        {
            Size = size;
        }

        public bool Enqueue( T obj )
        {
            lock ( _syncObj )
            {
                if ( _queue.Count >= Size )
                {
                    return false;
                }

                _queue.Insert( 0, obj );
                return true;
            }
        }

        public bool Dequeue( out T result )
        {
            lock ( _syncObj )
            {
                if ( _queue.Count > 0 )
                {
                    result = _queue[0];
                    _queue.RemoveAt( 0 );
                    return true;
                }

                result = default;
                return false;
            }
        }
    }
}