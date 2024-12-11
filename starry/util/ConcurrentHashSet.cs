using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace starry;

/// <summary>
/// why tf isnt this in System.Collections.Concurrent. i forgot where i stole this from i stole this 4 minutes ago
/// </summary>
public class ConcurrentHashSet<T> : ICollection<T>, IReadOnlyCollection<T> {
    // This class wraps our keys and serves only to provide support for the special case
    // where the item is null which is supported in a HashSet<T> but not as a key in a dictionary
    class Item(T value) {
        public T Value { get; } = value;
    }

    // We also have to wrap the comparer since the generic types of the 
    // item and underlying dictionary are different
    class ItemComparer(IEqualityComparer<T> comparer) : IEqualityComparer<Item> {
        readonly IEqualityComparer<T> _comparer = comparer;

        public bool Equals(Item? x, Item? y)
        {
            if (x == null || y == null) return false;
            return _comparer.Equals(x.Value, y.Value);
        }

        public int GetHashCode(Item obj) => _comparer.GetHashCode(obj.Value!);
    }

    readonly ConcurrentDictionary<Item, byte> _dictionary;

    public ConcurrentHashSet() => _dictionary = new ConcurrentDictionary<Item, byte>(
        new ItemComparer(EqualityComparer<T>.Default));

    public ConcurrentHashSet(IEnumerable<T> collection)
    {
        _dictionary = new ConcurrentDictionary<Item, byte>(
            collection.Select(x => new KeyValuePair<Item, byte>(new Item(x), byte.MinValue)),
            new ItemComparer(EqualityComparer<T>.Default));
    }

    public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
        _dictionary = new ConcurrentDictionary<Item, byte>(
            collection.Select(x => new KeyValuePair<Item, byte>(new Item(x), byte.MinValue)),
            new ItemComparer(comparer));
    }

    public ConcurrentHashSet(IEqualityComparer<T> comparer) => _dictionary =
        new ConcurrentDictionary<Item, byte>(new ItemComparer(comparer));

    public bool Add(T item) => _dictionary.TryAdd(new Item(item), byte.MinValue);

    // IEnumerable, IEnumerable<T>

    public IEnumerator<T> GetEnumerator() => _dictionary.Keys.Select(x => x.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // IReadOnlyCollection<T>

    public int Count => _dictionary.Count;

    // ICollection<T>

    void ICollection<T>.Add(T item) => ((IDictionary<Item, byte>) _dictionary).Add(
        new Item(item), byte.MinValue);

    public void Clear() => _dictionary.Clear();

    public bool Contains(T item) => _dictionary.ContainsKey(new Item(item));

    public void CopyTo(T[] array, int arrayIndex) => 
        _dictionary.Keys.Select(x => x.Value).ToArray().CopyTo(array, arrayIndex);

    public bool Remove(T item) => _dictionary.TryRemove(new Item(item), out byte value);

    public bool IsReadOnly => false;
}