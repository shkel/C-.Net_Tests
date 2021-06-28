namespace GenericsLib
{
    public class Node<U>
    {
        private U _data;
        private Node<U> _next;

        public Node(U data, Node<U> next)
        {
            _data = data;
            _next = next;
        }

        public U GetData()
        {
            return _data;
        }

        public Node<U> GetNext()
        {
            return _next;
        }

        public Node<U> SetNext(Node<U> item)
        {
            return _next = item;
        }
    }
}
