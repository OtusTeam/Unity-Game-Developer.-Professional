namespace GameElements.Unity
{
    public abstract class UnityGameElementMap<K, V> : UnityGameElement, IGameElementMap<K, V>
    {
        private readonly GameElementMap<K, V> map;

        public UnityGameElementMap()
        {
            this.map = new GameElementMap<K, V>();
        }

        public bool AddElement(K key, V element)
        {
            return this.map.AddElement(key, element);
        }

        public bool RemoveElement(K key)
        {
            return this.map.RemoveElement(key);
        }

        public T GetElement<T>(K key)
        {
            return this.map.GetElement<T>(key);
        }

        public bool TryGetElement<T>(K key, out T element)
        {
            return this.map.TryGetElement(key, out element);
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            IGameElement gameElement = this.map;
            gameElement.Setup(this.GameSystem);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            IGameElement gameElement = this.map;
            gameElement.Dispose();
        }
    }
}