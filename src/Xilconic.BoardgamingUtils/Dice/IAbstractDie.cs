using System.Diagnostics.Contracts;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Represents an abstract die that can be rolled for a result according to its probability distribution.
    /// </summary>
    public interface IAbstractDie : IRollable<int>, IDiscreteIntegerRandomVariable { }
}
