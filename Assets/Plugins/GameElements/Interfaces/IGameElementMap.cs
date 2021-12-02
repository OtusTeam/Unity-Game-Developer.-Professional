namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a dictionary of elements.</para>
    /// </summary>
    public interface IGameElementMap<in K, in V>
    {
        /// <summary>
        ///     <para>Adds an element into the dictionary.</para>
        /// </summary>
        bool AddElement(K key, V element);

        /// <summary>
        ///     <para>Removes an element from the dictionary.</para>
        /// </summary>
        bool RemoveElement(K key);

        /// <summary>
        ///     <para>Returns an element.</para>
        /// </summary>
        T GetElement<T>(K key);

        /// <summary>
        ///     <para>Tries to get an element from the dictionary.</para>
        /// </summary>
        bool TryGetElement<T>(K key, out T element);
    }
}