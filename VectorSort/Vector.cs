using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class Vector<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extend the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity
        {
            get { return data.Length; }
        }
        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as 
        // array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), 
        //and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[Capacity + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        // This method searches for the specified object and 
        //returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                //search for element
                if (data[i].Equals(element))
                    //return zero-based index of the first occurence within the entire data structure.
                    return i;
            }
            return -1;
        }

        //Inserts new element into the data structure at the specified index.
        public void Insert(int index, T element)
        {

            if (index > Count || index < 0) throw new IndexOutOfRangeException(nameof(element));
            //If count already equals capacity, the capacity of the Vector<T> is increased by automatically reallocating
            //the internal array. The existing element are copied to the new larger array before the new element is added.
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            //If index is equal to count, element is added to the end of the data structue.
            if (index == Count) data[Count] = element;
            //Looping backwards through the array, elements are shuffled towards the end of the array.
            //The for loop is broken when it reaches the specified index.
            for (int i = Count; i > index; i--)
            {
                data[i] = data[i - 1];
            }
            //the element is inserted into the data structure at the specified index.
            data[index] = element;
            //Count is incremented to account for the newly inserted element.
            Count++;
        }

        public void Clear()
        {
            //Data structure is reassigned as a new data structure, preserving its original Capacity. Count reset to 0.
            data = new T[Capacity];
            Count = 0;
        }

        public bool Contains(T element)
        {
            //using IndexOf method, iterates through the data structure searching for the zero-based index of the specified element.
            //Returns true if found, false otherwise.
            if (IndexOf(element) > -1)
                return true;
            return false;
        }

        public bool Remove(T element)
        {
            //Using IndexOf method, declare target index of the element to be removed
            var index = IndexOf(element);
            //If the element at the target index is found in the structure, use the RemoveAt method to remove it
            //before returning true.
            if (index > -1)
            {
                RemoveAt(index);
                return true;
            }
            //Otherwise, return false.
            return false;
        }
        //Removes the element at the specified index of the structure.
        //When called to remove an item, the remaining items in the list are renumbered to replace the removed item.

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0) throw new IndexOutOfRangeException();
            //When called to remove an item, the remaining items in the list are renumbered to replace the removed item.
            //Beginning at the specified index, loop forwards through the array.
            //Items are 'shuffled to the left' by being 'over-written' (re-assigned the value of the element after).
            //Note that the loop ends at 'data[Count -1]'.
            //This method is almost the opposite of 'Insert'.
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            //The loop is broken at the index before the last index in the array. The count is decremented, and
            //the new last index in the array is reassigned the default(T) value type.
            Count--;
            data[Count] = default(T);
        }
        //Returns a string that represents the current object, suitable for display.
        public override string ToString()
        {
            //loop through each element in the data structure, 'stringifying' them via the format method,
            //and adding them to the string 'stringData'
            //finally presented as comma separated values.
            string stringData = "";
            int i = 0;
            for (i = 0; i < Count - 1; i++)
                stringData += string.Format("{0}, ", data[i]);
            stringData += string.Format("{0}", data[i]);
            return stringData;
        }
        public void Sort()
        {
            Array.Sort(data, 0, Count);
        }
        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(data, 0, Count, comparer);
        }
    }
}
