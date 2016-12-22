namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Denotes the type of comparison performed by <see cref="ThresholdCompare"/>.
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// Success = <see cref="IAbstractDie"/> result is greater than the reference value.
        /// </summary>
        Greater,

        /// <summary>
        /// Success = <see cref="IAbstractDie"/> result is greater than or equal to the reference value.
        /// </summary>
        GreaterOrEqual,

        /// <summary>
        /// Success = <see cref="IAbstractDie"/> result is less than the reference value.
        /// </summary>
        Smaller,

        /// <summary>
        /// Success = <see cref="IAbstractDie"/> result is less than or equal to the reference value.
        /// </summary>
        SmallerOrEqual
    }
}