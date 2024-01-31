using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary;

public class ListManager<T> : IListManager<T>
{
    /// <summary>
    /// instance list
    /// </summary>
    private List<T> list;

    /// <summary>
    /// List property with read access
    /// </summary>
    public List<T> List => list;

    /// <summary>
    /// Defualt constructor
    /// Creates a new a nstance
    /// </summary>
    public ListManager() => list = new List<T>();

    /// <summary>
    /// Constructor that takes a list as parameter
    /// </summary>
    /// <param name="list"></param>
    public ListManager(List<T> list)
    {
        this.list = list;
    }

    /// <summary>
    /// Count property that returns the number of items in the collection list
    /// Only read access
    /// </summary>
    public int Count => list.Count;

    /// <summary>
    /// Method that adds an object to the collection
    /// </summary>
    /// <param name="item">A type</param>
    /// <returns>True if successful, false otherwise.</returns>
    public bool Add(T item)
    {
        if (item != null)
        {
            list.Add(item);
            return true;
        }
        return false;
    }


    /// <summary>
    /// Method that changes the value of an object at a given position in the collection
    /// </summary>
    /// <param name="type"> the object to replace an existing one at index-position </param>
    /// <param name="index">The position in the object collection in which the changes are to be made</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool ChangeAt(T type, int index)
    {
        if (CheckIndex(index))
        {
            list[index] = type;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method that checks if the index is not out of collections's range
    /// </summary>
    /// <param name="index">Input index of the postion to be checked</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool CheckIndex(int index) => (index >= 0 && index < list.Count);

    /// <summary>
    /// Method that deletes the collection
    /// </summary>
    public void DeleteAll() => list = new List<T>();

    /// <summary>
    /// Method that removes an object from the collection at a given position 
    /// </summary>
    /// <param name="index">Index to object that is to be removed</param>
    /// <returns>True if successful, false otherwise.</returns>
    public bool DeleteAt(int index)
    {
        if (CheckIndex(index))
        {
            list.RemoveAt(index);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method that removes an exciting estate
    /// </summary>
    /// <param name="estate"></param>
    public bool Remove(T type) => type != null ? list.Remove(type) : false;

    /// <summary>
    /// Method that returns an object from the collection at a given position
    /// </summary>
    /// <param name="index">input index of the position in the collection</param>
    /// <returns>True if successful, false otherwise</returns>
    public T GetAt(int index) => CheckIndex(index) ? list[index] : default;


    /// <summary>
    /// Method that returns the collection as array of string
    /// </summary>
    /// <returns>array</returns>
    public string[] ToStringArray() => list.Select(x => x?.ToString()).ToArray();

    /// <summary>
    /// method that returns the collection as list of string
    /// </summary>
    /// <returns>list</returns>
    public List<string> ToStringList() => list.Select(x => x.ToString()).ToList();


    /// <summary>
    /// Method that sorts the collection by a given property
    /// </summary>
    /// <param name="sorter"></param>
    public void Sort(IComparer<T> sorter) => list.Sort(sorter);


}


