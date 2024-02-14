namespace Narumikazuchi;

internal class StringReferenceList
{
    internal void Add(String item)
    {
        Optional<String> value = String.IsInterned(str: item);
        if (value.HasValue is true)
        {
            for (Int32 index = 0;
                 index < m_Count;
                 index++)
            {
                if (ReferenceEquals(objA: item,
                                    objB: m_Items[index]) is true)
                {
                    return;
                }
            }

            this.EnsureCapacity(capacity: m_Count + 1);
            m_Items[m_Count] = value.Value;
            m_Count++;
        }
        else
        {
            this.EnsureCapacity(capacity: m_Count + 1);
            m_Items[m_Count] = String.Intern(str: item);
            m_Count++;
        }
    }

    internal Boolean Contains(String item)
    {
        Optional<String> value = String.IsInterned(str: item);
        if (value.HasValue is false)
        {
            return false;
        }

        for (Int32 index = 0;
             index < m_Count;
             index++)
        {
            if (ReferenceEquals(objA: item,
                                objB: m_Items[index]) is true)
            {
                return true;
            }
        }

        return false;
    }

    internal Optional<String> Find(String item)
    {
        Optional<String> value = String.IsInterned(str: item);
        if (value.HasValue is false)
        {
            return default;
        }

        for (Int32 index = 0;
             index < m_Count;
             index++)
        {
            if (ReferenceEquals(objA: item,
                                objB: m_Items[index]) is true)
            {
                return value.Value;
            }
        }

        return default;
    }

    private void EnsureCapacity(Int32 capacity)
    {
        if (m_Items.Length <= capacity)
        {
            Int32 newCapacity = m_Items.Length * 2;
            String[] newArray = new String[newCapacity];
            Array.Copy(sourceArray: m_Items,
                       destinationArray: newArray,
                       length: m_Count);
            m_Items = newArray;
        }
    }

    private String[] m_Items = new String[4];
    private Int32 m_Count = 0;
}