using System.Collections.Generic;

public interface Displayable<T> : BasicDisplayable
{
    List<T> items { get; }
}