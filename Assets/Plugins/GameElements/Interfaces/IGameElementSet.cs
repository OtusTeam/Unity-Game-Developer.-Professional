namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a set of elements.</para>
    /// </summary>
    public interface IGameElementSet
    {
        /// <summary>
        ///     <para>Adds an element into the set.</para>
        /// </summary>
        bool AddElement(object element);

        /// <summary>
        ///     <para>Removes an element into the set.</para>
        /// </summary>
        bool RemoveElement(object element);

        /// <summary>
        ///     <para>Checks an element into the set.</para>
        /// </summary>
        bool ContainsElement(object element);
    }
}