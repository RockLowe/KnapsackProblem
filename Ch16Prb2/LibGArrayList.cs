/*****************************************************************/
/*                                                               */
/*  Course: CIS 350 - Data Structures                            */
/*                                                               */
/*  Library: LibGArrayList.CS                                    */
/*                                                               */
/*  Programmer: Dr. Oakes                                        */
/*                                                               */
/*  Purpose: Create LibGArrayList namespace library (DLL).       */
/*                                                               */
/*  Class: GArrayList<T> : IEnumerable where T : IComparable<T>  */
/*                                                               */
/*****************************************************************/

using System;
using System.Windows.Forms;
using System.Collections;

/**************************************************************/
/* Begin namespace LibGArrayList                              */
/**************************************************************/  
namespace LibGArrayList
{
  
  /*********************************************************************/
  /* Begin class GArrayList<T> : IEnumerable where T : IComparable<T>  */
  /*********************************************************************/  
  public class GArrayList<T> : IEnumerable where T : IComparable<T>  
  {
    public delegate int CompareDelegate(T data1, T data2);
    
    private int capacity;
    private int count;
    private T[] items;
  
    public GArrayList()  // Default constructor
    {
      this.capacity = 1;
      this.count    = 0;
      this.items    = new T[1];
    }

    private GArrayList(int capacityValue)  // Initializing constructor
    {
      this.capacity = capacityValue;
      this.count    = 0;
      this.items    = new T[capacity];
    }

    public GArrayList(GArrayList<T> sourceList)  // Copy constructor
    {
      this.Copy(sourceList);
    }

    public int Count  // Define read-only Count property
    {
      get 
      {  
        return this.count;
      }
    }

    public bool Empty  // Return true (false) if list is (not) empty
    {
      get
      {
        return (this.count==0);
      }
    }

    public T this[int index]  // Define read/write this indexer
    { 
      get  // Implements the retieve at capability
      {  
        if ((index>=1) && (index<=this.count))
         return this.items[index-1];
        else
        {
          ProcessError(String.Format("GArrayList this[] Get index must be between 1 and {0}",this.count));
          return default(T);
        }
      }
      set  // Implements the replace at capability
      {
        if ((index>=1) && (index<=this.count))
          this.items[index-1] = value;
        else
          ProcessError(String.Format("GArrayList this[] Set index must be between 1 and {0}",this.count));
      }
    }

    public IEnumerator GetEnumerator()    // IEnumerable Interface Implementation:
    {                                     //   Declaration of the GetEnumerator() method,
      for (int i=1; i<=this.count; i++)   //   whick is needed to give meaning to the "foreach"
        yield return this[i];             //   control construct.
    }                                         

    public void Clear()  // Remove all list elements
    {
      this.capacity = 1;
      this.count    = 0;
      this.items    = new T[1];
    }

    public void Copy(GArrayList<T> sourceList)  
    {
      this.Clear();
      if (sourceList.count>0)
        if (sourceList[1] is ICloneable)  // Perform deep copy
          foreach (ICloneable data in sourceList)
            this.Add((T) data.Clone());
        else if (default(T)==null)   
          ProcessError("Use of the GArrayList Copy method on a list of a reference type requires\n" +
                       "that the type implement the ICloneable interface.");
        else  // Perform shallow copy
          foreach (T data in sourceList)
            this.Add(data);
    }

    public GArrayList<T> Clone()
    {
      return new GArrayList<T>(this); 
    }

    private void IncreaseCapacity() 
    {
      GArrayList<T> tempList = new GArrayList<T>(2*this.capacity);

      foreach (T data in this)
        tempList.Add(data);
      this.capacity = tempList.capacity;
      this.count    = tempList.count;
      this.items    = tempList.items;
    }

    public void Add(T data)
    {
      this.InsertAt(this.count + 1, data);
    }

    public void InsertAt(int position, T data)
    {
      int i;

      if ((position>=1) && (position<=this.count+1))
      {
        if (this.count==this.capacity)
          this.IncreaseCapacity();
        this.count++;
        for (i=this.count; i>position; i--)
          this[i] = this[i-1];
        this[position] = data;
      }
      else
        ProcessError(String.Format("GArrayList InsertAt index must be between 1 and {0}",this.count+1));
    }

    public T RemoveAt(int position)
    {
      int i;
      T   data=default(T);

      if ((position>=1) && (position<=this.count))
      {
        data = this[position];
        for (i=position; i<this.count; i++)
          this[i] = this[i+1]; 
        this.count--;
      }
      else
        ProcessError(String.Format("GArrayList RemoveAt index must be between 1 and {0}",this.count));
      return data;
    }

    public int LinearSearch(T data)  // Locate and return the list index of data
    {                                // in the list. List can be unordered.
      int listIndex = 1;             // Uses the ICompareable<T> CompareTo() method.   
      
      while ((listIndex<=this.count) && (this[listIndex].CompareTo(data)!=0))
        listIndex++;
      if (listIndex>this.count)
          listIndex = ~listIndex;
      return listIndex;
    }

    public bool LinearContains(T data)  // Does not require that the list be ordered.
    {                                    
      return (this.LinearSearch(data)>0);
    }

    public int LinearSearch(T data, CompareDelegate compareMethod)  // Locate and return the index of data
    {                                                               // in the list. List can be unordered.
      int listIndex = 1;                                            // Uses a CompareDelegate method.                   
                                                                       
      while ((listIndex<=this.count) && (compareMethod(this[listIndex],data)!=0))
        listIndex++;
      if (listIndex>this.count)
        listIndex = ~listIndex;
      return listIndex;
    }

    public bool LinearContains(T data, CompareDelegate compareMethod)  // List can be unordered.
    {
      return (this.LinearSearch(data,compareMethod)>0);
    }
    
    public int BinarySearch(T data)                     // Locate and return the index of data
    {                                                   // in the list. List must be ordered.  
      int listIndex, lowIndex=1, highIndex=this.count;  // Uses the IComparable<T> CompareTo() method.

      listIndex = (lowIndex+highIndex)/2;
      while ((lowIndex<=highIndex) && (((IComparable<T>) this[listIndex]).CompareTo(data)!=0))
      {  
        if (this[listIndex].CompareTo(data)>0)
          highIndex = listIndex - 1;
        else 
          lowIndex = listIndex + 1;
        listIndex = (lowIndex+highIndex)/2;
      }
      if (listIndex==0)
        listIndex = 1;
      else if ((lowIndex>highIndex) && (this[listIndex].CompareTo(data)<0))  // Make listIndex point to element
        listIndex++;                                                         // that should follow data,
      if (lowIndex>highIndex)                                                // if data is to be inserted.
        listIndex = ~listIndex;
      return listIndex;
    }

    public bool BinaryContains(T data)  // List can be unordered.
    {
      return (this.BinarySearch(data)>0);
    }

    public int BinarySearch(T data, CompareDelegate compareMethod)  // Locate and return the index of data
    {                                                               // in the list. List must be ordered.
      int listIndex, lowIndex=1, highIndex=this.count;              // Uses a CompareDelegate method.

      listIndex = (lowIndex+highIndex)/2;
      while ((lowIndex<=highIndex) && (compareMethod(this[listIndex],data)!=0))
      {  
        if (compareMethod(this[listIndex],data)>0)
          highIndex = listIndex - 1;
        else 
          lowIndex = listIndex + 1;
        listIndex = (lowIndex+highIndex)/2;
      }
      if (listIndex==0)
        listIndex = 1;
      else if ((lowIndex>highIndex) && (compareMethod(this[listIndex],data)<0)) // Make listIndex point to element
        listIndex++;                                                            // that should follow data,
      if (lowIndex>highIndex)                                                   // if data is to be inserted. 
        listIndex = ~listIndex;
      return listIndex;
    }

    public bool BinaryContains(T data, CompareDelegate compareMethod)  // List must be ordered.
    {
      return (this.BinarySearch(data,compareMethod)>0);
    }

    public void Reverse()
    {
      int i;
      T   tempValue;

      for (i=1; i<=this.count/2; i++)
      {
        tempValue            = this[i];
        this[i]              = this[this.count+1-i];
        this[this.count+1-i] = tempValue;
      }
    }

    public void SelectionSort()  // Uses the IComparable<T> CompareTo() method.
    {         
      int i,j,k;
      T   temp;

      for (i=1; i<=(this.count-1); i++)
      {
        k = i;
        for (j=(i+1); j<=this.count; j++)
          if (this[j].CompareTo(this[k])<0)
            k = j;
        if (k>i)
        {
          temp    = this[k];
          this[k] = this[i];
          this[i] = temp;
        }
      }
    }

    public void SelectionSort(CompareDelegate compareMethod)  // Uses a CompareDelegate method.
    {
      int i,j,k;
      T   temp;

      for (i=1; i<=(this.count-1); i++)
      {
        k = i;
        for (j=(i+1); j<=this.count; j++)
          if (compareMethod(this[j],this[k])<0)
            k = j;
        if (k>i)
        {
          temp    = this[k];
          this[k] = this[i];
          this[i] = temp;
        }
      }
    }
    
    public void Merge(GArrayList<T> leftList, GArrayList<T> rightList)  // Uses the IComparable<T> CompareTo() method.
    {
      int i, j, k;
      
      for (i=1,j=1,k=1; i<=this.count; i++)
        if ((j<=leftList.count) && (k<=rightList.Count))
          if (leftList[j].CompareTo(rightList[k])<=0)
          {
            this[i] = leftList[j];  j++; 
          }
          else
          {
            this[i] = rightList[k];  k++; 
          }
        else if (j<=leftList.count)
        {
          this[i] = leftList[j];  j++; 
        }
        else
        {
          this[i] = rightList[k];  k++; 
        }
    }
  
    public void MergeSort()  // Uses the IComparable<T> CompareTo() method to accomplish merge step.
    {                        
      int           i, midPos;
      GArrayList<T> leftList  = new GArrayList<T>(); 
      GArrayList<T> rightList = new GArrayList<T>();
      
      if (this.count<100)
        this.SelectionSort();
      else
      {
        midPos = (1+this.count)/2;
        for (i=1; i<=midPos; i++)
          leftList.Add(this[i]);
        for (i=midPos+1; i<=this.count; i++)
          rightList.Add(this[i]);
        leftList.MergeSort();
        rightList.MergeSort();
        this.Merge(leftList, rightList);
      }
    }

    public void Merge(GArrayList<T> leftList, GArrayList<T> rightList,  // Uses a CompareDelegate method.
                      CompareDelegate compareMethod)
    {                                                                   
      int i, j, k, count=leftList.Count+rightList.Count;
      
      for (i=1,j=1,k=1; i<=this.count; i++)
        if ((j<=leftList.count) && (k<=rightList.Count))
          if (compareMethod(leftList[j],rightList[k])<=0)
          {
            this[i] = leftList[j];  j++; 
          }
          else
          {
            this[i] = rightList[k];  k++; 
          }
        else if (j<=leftList.count)
        {
          this[i] = leftList[j];  j++; 
        }
        else
        {
          this[i] = rightList[k];  k++; 
        }
    }
  
    public void MergeSort(CompareDelegate compareMethod)  // Uses a CompareDelegate method to accomplish merge step.
    {
      int           i, midPos;
      GArrayList<T> leftList  = new GArrayList<T>(); 
      GArrayList<T> rightList = new GArrayList<T>();

      if (this.count<100)
        this.SelectionSort(compareMethod);
      else
      {
        midPos = (1+this.count)/2;
        for (i=1; i<=midPos; i++)
          leftList.Add(this[i]);
        for (i=midPos+1; i<=this.count; i++)
          rightList.Add(this[i]);
        leftList.MergeSort(compareMethod);
        rightList.MergeSort(compareMethod);
        this.Merge(leftList, rightList, compareMethod); 
      }
    }

    public void QuickSort()  // Uses the IComparable<T> CompareTo() method.
    {
      int i;
      T pivot;
      GArrayList<T> leftList = new GArrayList<T>();
      GArrayList<T> rightList = new GArrayList<T>();

      if (this.count < 100)
        this.SelectionSort();
      else
      {
        pivot = this[1];
          for (i = 2; i <= this.count; i++)
        {
          if (this[i].CompareTo(pivot) < 0)
            leftList.Add(this[i]);
          else
            rightList.Add(this[i]);
        }
        leftList.Add(pivot);
        leftList.QuickSort();
        rightList.QuickSort();
        this.Merge(leftList, rightList);
      }
    }

    public void QuickSort(CompareDelegate compareMethod)  // Uses a CompareDelegate method.
    {
      int i;
      T pivot;
      GArrayList<T> leftList = new GArrayList<T>();
      GArrayList<T> rightList = new GArrayList<T>();

      if (this.count < 100)
        this.SelectionSort(compareMethod);
      else
      {
        pivot = this[1];
        for (i = 2; i <= this.count; i++)
        {
          if (this[i].CompareTo(pivot) < 0)
            leftList.Add(this[i]);
          else
            rightList.Add(this[i]);
        }
        leftList.Add(pivot);
        leftList.QuickSort(compareMethod);
        rightList.QuickSort(compareMethod);
        this.Merge(leftList, rightList);
      }
    }

    private void ProcessError(string message)
    {
      MessageBox.Show(message,"Execution Abort",MessageBoxButtons.OK,MessageBoxIcon.Error);
      Application.Exit();
    }
  }  // End class GArrayList<T>

}  // End namespace LibGArrayList
