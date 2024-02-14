namespace Narumikazuchi;

public partial struct FilePath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static private String RemoveInvalidCharactersFrom(String value)
    {
        Optional<String> cached = s_Cached.Find(item: value);
        if (cached.HasValue is true)
        {
            return cached.Value;
        }

        Span<Char> cleaned = stackalloc Char[value.Length];
        Int32 position = 0;
        foreach (Char character in value)
        {
            if (position is 0 &&
                character is ' ')
            {
                continue;
            }

            if (character.Equals(obj: Path.VolumeSeparatorChar) is true &&
                position is not 1)
            {
                continue;
            }

            if (character is <= (Char)31
                          or (Char)34
                          or (Char)60
                          or (Char)62
                          or (Char)63
                          or (Char)124)
            {
                continue;
            }

            if (character is '\\')
            {
                cleaned[position] = '/';
                position++;
            }
            else if (character is '/' &&
                     position is > 0 &&
                     cleaned[position - 1] is '/')
            {
                continue;
            }
            else
            {
                cleaned[position] = character;
                position++;
            }
        }

        while (position is > 0 &&
               cleaned[position - 1] is ' ')
        {
            position--;
        }

        if (position is > 1 &&
            cleaned[position - 1] is '/' &&
            (Char.IsLetter(c: cleaned[0]) is false ||
            cleaned[1].Equals(obj: Path.VolumeSeparatorChar) is false))
        {
            position--;
        }

        String result = cleaned[..position].ToString();
        s_Cached.Add(item: result);
        return result;
    }

    static private readonly StringReferenceList s_Cached = new();

    IEnumerator<Char> IEnumerable<Char>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private readonly String m_Value;
}