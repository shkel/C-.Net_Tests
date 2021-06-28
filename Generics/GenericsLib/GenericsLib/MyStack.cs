using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsLib
{
    public class MyStack<T> : IEnumerable<T>
    {
        private Node<T> _top;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Push(T item)
        {
            if (_top == null)
            {
                _top = new Node<T>(item, null);
            }
            else
            {
                var next =  new Node<T>(item, _top);
                _top = next;
            }
        }

        public T Pop()
        {
            T result;
            if (!TryPop(out result))
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return result;
        }

        public T Peek()
        {
            T result;
            if (!TryPeek(out result))
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return result;
        }

        public int Count()
        {
            int cnt = 0;
            if (_top != null)
            {
                cnt++;
                var next = _top.GetNext();
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
            item = _top.GetData();
            return _top != null;
        }

        public bool TryPop(out T item)
        {
            if (_top != null)
            {
                item = _top.GetData();
                _top = _top.GetNext();
                return true;
            }
            else
            {
                item = default;
                return false;
            }
        }

        public class Enumerator : IEnumerator<T>
        {
            private MyStack<T> _stack;
            private Node<T> _current;

            public Enumerator(MyStack<T> stack)
            {
                _stack = stack;
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
                    _current = _stack._top;
                }
                else
                {
                    _current = _current.GetNext();
                }
                return _current != null;
            }

            public void Reset()
            {
                _current = _stack._top;
            }
        }
    }
}
