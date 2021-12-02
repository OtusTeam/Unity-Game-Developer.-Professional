namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a dictionary of generic elements.</para>
    /// </summary>
    public interface IGameElementLayer
    {
        /// <summary>
        ///     <para>Adds an element into the layer.</para>
        /// </summary>
        bool AddElement(object element);

        /// <summary>
        ///     <para>Removes an element from the layer.</para>
        /// </summary>
        bool RemoveElement(object element);

        /// <summary>
        ///     <para>Returns an element of "T".</para>
        /// </summary>
        T GetElement<T>();

        /// <summary>
        ///     <para>Tries to get an element of "T".</para>
        /// </summary>
        bool TryGetElement<T>(out T element);
    }
}