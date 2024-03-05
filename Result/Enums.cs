namespace Result;

/// <summary>
/// Enumeration of the ways operations on collections might fail
/// </summary>
public enum CollectionError
{
    /// <summary>
    /// Indicates the collection was null.
    /// </summary>
    IsNull,
    
    /// <summary>
    /// Indicates the collection was expected to have item(s) but was empty.
    /// </summary>
    IsEmpty,
    
    /// <summary>
    /// Indicates the collection had multiple matching items where one was expected.
    /// </summary>
    MultipleMatchingItems,
    
    /// <summary>
    /// Indicates the collection had no matching items where one (or more) was expected.
    /// </summary>
    NoMatchingItems,
}