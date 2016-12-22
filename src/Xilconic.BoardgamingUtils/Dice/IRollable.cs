namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Interface for an object that can be rolled for a result.
    /// </summary>
    /// <typeparam name="T">The rolled result type.</typeparam>
    public interface IRollable<T> where T : struct
    {
        /// <summary>
        /// Rolls for a randomly generated result value.
        /// </summary>
        /// <returns>The rolled result.</returns>
        T Roll();
    }
}
