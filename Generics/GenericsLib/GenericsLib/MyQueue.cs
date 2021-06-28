using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsLib
{
    public class MyQueue<T> : IEnumerable<T>
    {
        private Node<T> _first;
        private Node<T> _last;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Enqueue(T item)
        {
            if (_last == null)
            {
                _first = new Node<T>(item, null);
                _last = _first;
            }
            else
            {
                var next = new Node<T>(item, null);
                _last.SetNext(next);
                _last = next;
            }
        }

        public T Dequeue()
        {
            T result;
            if (!TryDequeue(out result))
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return result;
        }

        public T Peek()
        {
            T result;
            if (!TryPeek(out result))
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return result;
        }

        public int Count()
        {
            int cnt = 0;
            if (_first != null)
            {
                cnt++;
                var next = _first.GetNext();
                while (next != null)
                {
                    cnt++;
                    next = next.GetNext();
                }
            }
            return cnt;
        }

        public bool TryPeek(out T item)
        {
            item = _first.GetData();
            return _first != null;
        }

        public bool TryDequeue(out T item)
        {
            if (_first != null)
            {
                item = _first.GetData();
                _first = _first.GetNext();
                if (_first == null)
                {
                    _last = null;
                }
                return true;
            }
            else
            {
                item = default;
                return false;
            }
        }

        public void Clear()
        {
            while (_first != null)
            {
                _first = _first.GetNext();
            }
            _last = null;
        }

        public class Enumerator : IEnumerator<T>
        {
            private MyQueue<T> _queue;
            private Node<T> _current;

            public Enumerator(MyQueue<T> queue)
            {
                _queue = queue;
            }

            public T Current => _current == null ? default : _current.GetData();

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _queue._first;
                }
                else
                {
                    _current = _current.GetNext();
                }
                return _current != null;
            }

            public void Reset()
            {
                _current = _queue._first;
            }
        }
    }
}
