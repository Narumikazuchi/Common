using Narumikazuchi.Collections;

namespace Enumerators;

[TestClass]
public sealed class An_empty_enumerator
{
    [TestMethod]
    public void will_not_move_next()
    {
        EmptyEnumerator<Int32> enumerator = new();
        Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void throws_invalid_operation_exception_when_accessing_current_value()
    {
        EmptyEnumerator<Int32> enumerator = new();
        Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
    }
}