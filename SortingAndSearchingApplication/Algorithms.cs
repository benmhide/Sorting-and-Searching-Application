using DataBase;
using System;
// System.Diagnostics namespace is used for the stopwatch class to time the algorithms
using System.Diagnostics;

namespace Algorithms
{
    // Sorting class for the Record array
    public class Sort
    {
        #region Sorting Algorithms
        // Implementation of the Bubble Sort Algorithm which sorts the a Record array by the property decided by the user.
        // Parameters passed to the method: Record array (to be sorted), Func Method - encapsulates a method that has one parameter and returns a value of the type specified by the TResult 
        // parameter (passed with an input parameter Type of Record and result of type dynamic, a dynamic has been used as the TResult parameter will be different datatypes depending on the 
        // property of the record used in the algorthim), an integer (to define the sorting order) and a string with the name of the property (that is used to sort the Record array)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm analysis with regards to total comparisons, swaps and time taken to complete)
        public static void BubbleSort(Record[] recordSet, Func<Record, dynamic> prop, int order, string propName)
        {
            // Method variables
            int innerLoopCount, outerLoopCount = 0, swaps = 0, comparisons = 0;
            int size = recordSet.Length, sort, lastIndex;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (lastIndex = size - 1; lastIndex > 0; lastIndex--)
            {
                // Boolean to decide if the array is sorted. If no swaps take place through a complete pass then the array is sorted and the boolean will be false
                // which will break out of the loop
                bool swapped = false;
                innerLoopCount = 0;

                for (sort = 0; sort < lastIndex; sort++)
                {
                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    if (prop(recordSet[sort + order]) > prop(recordSet[sort + (1 - order)]))
                    {
                        Swap(recordSet, (sort + order), (sort + 1 - order));
                        swapped = true;
                        swaps++;
                    }
                    innerLoopCount++;
                }
                if (!swapped)
                    break;
                outerLoopCount++;
            }
            stopwatch.Stop();
            #region Display
            // Display the records once sorted and the analysis of the algorithm once it has finished.
            ViewRecords(recordSet, propName);
            Console.WriteLine("BubbleSort Complete");
            Console.WriteLine("Sorted by {0} in {1} order", propName, sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("BubbleSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Implementation of the Bubble Sort Algorithm which sorts the a Record array by a string property
        // Parameters passed to the method: Record array (to be sorted), an integer (to define the sorting order).
        // The method uses the defined CompareStrings function of the Record class to compare strings of the region property of the records
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void BubbleSortString(Record[] recordSet, int order)
        {
            int innerLoopCount, outerLoopCount = 1, swaps = 0, comparisons = 0;
            int size = recordSet.Length, sort, lastIndex;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (lastIndex = size - 1; lastIndex > 0; lastIndex--)
            {
                bool swapped = false;
                innerLoopCount = 0;

                for (sort = 0; sort < lastIndex; sort++)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    if (Record.CompareStrings(recordSet[sort + order].Region, recordSet[sort + 1 - order].Region) < 0)
                    {
                        Swap(recordSet, (sort + order), (sort + 1 - order));
                        swapped = true;
                        swaps++;
                    }
                    innerLoopCount++;
                }
                if (!swapped)
                    break;
                outerLoopCount++;
            }
            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, "Region");
            Console.WriteLine("BubbleSort Complete");
            Console.WriteLine("Sorted by Region in {0} order", sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("BubbleSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Implementation of the Insertion Sort Algorithm which sorts the a Record array by property
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // (passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order) and a string with the name of the property (used to sort the Record array)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void InsertionSort(Record[] recordSet, Func<Record, dynamic> prop, int order, string propName)
        {
            int outerLoopCount = 0, innerLoopCount, swaps = 0, comparisons = 0;
            int size = recordSet.Length, sort, firstIndex;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (firstIndex = 0; firstIndex < size - 1; firstIndex++)
            {
                innerLoopCount = 0;

                for (sort = firstIndex + 1; sort > 0; sort--)
                {
                    if (order == 1)
                    {
                        comparisons++;
                        // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                        // The method returns the property value of the record at the specified index.
                        // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                        if (prop(recordSet[sort]) > prop(recordSet[sort - 1]))
                        {
                            Swap(recordSet, sort, sort - 1);
                            swaps++;
                        }
                        else
                            innerLoopCount++;
                    }
                    else if (order == 0)
                    {
                        comparisons++;
                        // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                        // The method returns the property value of the record at the specified index.
                        // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                        if (prop(recordSet[sort]) < prop(recordSet[sort - 1]))
                        {
                            Swap(recordSet, sort - 1, sort);
                            swaps++;
                        }
                        else
                            innerLoopCount++;
                    }
                }
                outerLoopCount++;
            }
            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, propName);
            Console.WriteLine("InsertionSort Complete");
            Console.WriteLine("Sorted by {0} in {1} order", propName, sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("InsertionSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Implementation of the Insertion Sort Algorithm which sorts the a Record array by a string property
        // Parameters passed to the method: Record array (to be sorted), an integer (to define the sorting order).
        // The method uses the defined CompareTo function of the Record class to comapre strings of the region property
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void InsertionSortString(Record[] recordSet, int order)
        {
            int outerLoopCount = 0, innerLoopCount, swaps = 0, comparisons = 0;
            int size = recordSet.Length, sort, firstIndex;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (firstIndex = 0; firstIndex < size - 1; firstIndex++)
            {
                innerLoopCount = 0;

                for (sort = firstIndex + 1; sort > 0; sort--)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    if (Record.CompareStrings(recordSet[sort - order].Region, recordSet[sort - 1 + order].Region) > 0)
                    {
                        Swap(recordSet, (sort - order), (sort - 1 + order));
                        swaps++;
                    }
                    else
                        innerLoopCount++;

                }
                outerLoopCount++;
            }
            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, "Region");
            Console.WriteLine("InsertionSort Complete");
            Console.WriteLine("Sorted by Region in {0} order", sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("InsertionSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Implementation of the Quick Sort Algorithm which sorts the a Record array by property
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // (passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order) and a string with the name of the property (used to sort the Record array)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void QuickSort(Record[] recordSet, Func<Record, dynamic> prop, int order, string propName)
        {
            int recurCallQuickSort = 0, comparisons = 0, swaps = 0;
            int size = recordSet.Length;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSort(recordSet, 0, size - 1, prop, order, propName, ref recurCallQuickSort, ref comparisons, ref swaps);

            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, propName);
            Console.WriteLine("QuickSort Complete");
            Console.WriteLine("Sorted by {0} in {1} order", propName, sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("QuickSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Recursive method for the Quick Sort Algorithm which recursively sorts the Record array.
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array)
        // an integer of the 'left' most value of the Record array, an integer of 'right' most value of the Record array and
        // referenced integers to count the recursive calls to the method, comparisons and swaps.
        private static void QuickSort(Record[] recordSet, int left, int right, Func<Record, dynamic> prop, int order, string propName,
            ref int recurCallQuickSort, ref int comparisons, ref int swaps)
        {
            int indexOne, indexTwo;
            indexOne = left;
            indexTwo = right;

            Record pivot = recordSet[(left + right) / 2];

            if (order == 1)
            {
                do
                {
                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    while ((prop(recordSet[indexOne]) > prop(pivot)) && (indexOne < right)) indexOne++;

                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    while ((prop(pivot) > prop(recordSet[indexTwo])) && (indexTwo > left)) indexTwo--;

                    if (indexOne <= indexTwo)
                    {
                        Swap(recordSet, indexOne, indexTwo);
                        swaps++;
                        indexOne++;
                        indexTwo--;
                    }
                } while (indexOne <= indexTwo);

                if (left < indexTwo)
                {
                    recurCallQuickSort++;
                    QuickSort(recordSet, left, indexTwo, prop, order, propName, ref recurCallQuickSort, ref comparisons, ref swaps);
                }
                if (indexOne < right)
                {
                    recurCallQuickSort++;
                    QuickSort(recordSet, indexOne, right, prop, order, propName, ref recurCallQuickSort, ref comparisons, ref swaps);
                }
            }

            else if (order == 0)
            {
                do
                {
                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    while ((prop(recordSet[indexOne]) < prop(pivot)) && (indexOne < right)) indexOne++;

                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    while ((prop(pivot) < prop(recordSet[indexTwo])) && (indexTwo > left)) indexTwo--;

                    if (indexOne <= indexTwo)
                    {
                        Swap(recordSet, indexOne, indexTwo);
                        swaps++;
                        indexOne++;
                        indexTwo--;
                    }
                } while (indexOne <= indexTwo);

                if (left < indexTwo)
                {
                    recurCallQuickSort++;
                    QuickSort(recordSet, left, indexTwo, prop, order, propName, ref recurCallQuickSort, ref comparisons, ref swaps);
                }
                if (indexOne < right)
                {
                    recurCallQuickSort++;
                    QuickSort(recordSet, indexOne, right, prop, order, propName, ref recurCallQuickSort, ref comparisons, ref swaps);
                }
            }
        }



        // Implementation of the Quick Sort Algorithm which sorts the a Record array by a string property
        // Parameters passed to the method: Record array (to be sorted), an integer (to define the sorting order).
        // The method uses the defined CompareTo function of the Record class to comapre strings of the region property
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void QuickSortString(Record[] recordSet, int order)
        {
            int recurCallQuickSort = 0, comparisons = 0, swaps = 0;
            int size = recordSet.Length;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSortString(recordSet, 0, size - 1, order, ref recurCallQuickSort, ref comparisons, ref swaps);

            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, "Region");
            Console.WriteLine("QuickSort Complete");
            Console.WriteLine("Sorted by Region in {0} order", sortOrder);
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("QuickSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Recursive method for the Quick Sort Algorithm which recursively sorts the Record array.
        // Parameters passed to the method: Record array (to be sorted), an integer of the 'left' most value of
        // the Record array, an integer of 'right' most value of the Record array and referenced integers to count
        // the recursive calls to the method, comparisons and swaps.
        private static void QuickSortString(Record[] recordSet, int left, int right, int order,
            ref int recurCallQuickSort, ref int comparisons, ref int swaps)
        {
            int indexOne, indexTwo;
            indexOne = left;
            indexTwo = right;

            string pivot = recordSet[(left + right) / 2].Region;
            do
            {
                if (order == 0)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    while (Record.CompareStrings(recordSet[indexOne].Region, pivot) > 0 && (indexOne < right)) indexOne++;

                    comparisons++;
                    // Calls the compare to method of the record class
                    while (Record.CompareStrings(pivot, recordSet[indexTwo].Region) > 0 && (indexTwo > left)) indexTwo--;
                }

                else if (order == 1)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    while (Record.CompareStrings(recordSet[indexOne].Region, pivot) < 0 && (indexOne < right)) indexOne++;

                    comparisons++;
                    // Calls the compare to method of the record class
                    while (Record.CompareStrings(pivot, recordSet[indexTwo].Region) < 0 && (indexTwo > left)) indexTwo--;
                }

                if (indexOne <= indexTwo)
                {
                    Swap(recordSet, indexOne, indexTwo);
                    swaps++;
                    indexOne++;
                    indexTwo--;
                }
            } while (indexOne <= indexTwo);

            if (left < indexTwo) QuickSortString(recordSet, left, indexTwo, order, ref recurCallQuickSort, ref comparisons, ref swaps);
            if (indexOne < right) QuickSortString(recordSet, indexOne, right, order, ref recurCallQuickSort, ref comparisons, ref swaps);
        }


        // Implementation of the Merge Sort Algorithm which sorts the a Record array by property
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // (passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array) and 
        // also a boolean to allow the analysis/records to be shown (this is due to the merge sort being used to sort the 
        // Record array before a binary search is performed)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void MergeSort(Record[] recordSet, Func<Record, dynamic> prop, int order, string propName, bool isForSearch)
        {
            int recurCallMergeSort = 0, recurCallMerge = 0, comparisons = 0, swaps = 0;
            int size = recordSet.Length;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Record[] temp = new Record[size];
            MergeSort(recordSet, temp, 0, size - 1, prop, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);

            stopwatch.Stop();
            #region Display
            if (!isForSearch)
            {
                ViewRecords(recordSet, propName);
                Console.WriteLine("MergeSort Complete");
                Console.WriteLine("Sorted by {0} in {1} order", propName, sortOrder);
                Console.WriteLine("Array Size: {0}", size);
                Console.WriteLine("Total Swaps: {0}", swaps);
                Console.WriteLine("Total Comaprisons: {0}", comparisons);
                Console.WriteLine("MergeSort Took: {0}", stopwatch.Elapsed);
            }
            #endregion
        }


        // Recursive method for the Merge Sort Algorithm which recursively sorts the Record array.
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array)
        // A temporary Record array, an integer of the 'lowest' most value of the Record array, an integer of 'highest' most
        // value of the Record array and referenced integers to count the recursive calls to the method, comparisons and swaps.
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        private static void MergeSort(Record[] recordSet, Record[] temp, int low, int high, Func<Record, dynamic> prop, int order,
            ref int recurCallMergeSort, ref int recurCallMerge, ref int comparisons, ref int swaps)
        {
            int numb = high - low + 1;
            int middle = low + numb / 2;
            int index;

            if (numb < 2) return;

            for (index = low; index < middle; index++)
            {
                temp[index] = recordSet[index];
            }

            recurCallMergeSort++;
            MergeSort(temp, recordSet, low, middle - 1, prop, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);

            recurCallMergeSort++;
            MergeSort(recordSet, temp, middle, high, prop, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);

            recurCallMerge++;
            Merge(recordSet, temp, low, middle, high, prop, order, ref comparisons, ref swaps);
        }


        // Merge function of the Merge Sort Algoithm used to merge data into a sorted array
        private static void Merge(Record[] recordSet, Record[] temp, int low, int middle, int high, Func<Record, dynamic> prop, int order,
            ref int comparisons, ref int swaps)
        {
            int resultIndex = low;
            int tempIndex = low;
            int destinationIndex = middle;

            while (tempIndex < middle && destinationIndex <= high)
            {
                if (order == 0)
                {
                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    if (prop(recordSet[destinationIndex]) < prop(temp[tempIndex]))
                    {
                        swaps++;
                        recordSet[resultIndex++] = recordSet[destinationIndex++];
                    }
                    else
                    {
                        swaps++;
                        recordSet[resultIndex++] = temp[tempIndex++];
                    }
                }
                else if (order == 1)
                {
                    comparisons++;
                    // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                    // The method returns the property value of the record at the specified index.
                    // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                    if (prop(recordSet[destinationIndex]) > prop(temp[tempIndex]))
                    {
                        swaps++;
                        recordSet[resultIndex++] = recordSet[destinationIndex++];
                    }
                    else
                    {
                        swaps++;
                        recordSet[resultIndex++] = temp[tempIndex++];
                    }
                }
            }
            while (tempIndex < middle)
            {
                swaps++;
                recordSet[resultIndex++] = temp[tempIndex++];
            }
        }


        // Implementation of the Merge Sort Algorithm which sorts the a Record array by property
        // Parameters passed to the method: Record array (to be sorted), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array) and 
        // also a boolean to allow the analysis/records to be shown (this is due to the merge sort being used to sort the 
        // Record array before a binary search is performed)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void MergeSort(Record[] recordSet, int order, bool isForSearch)
        {
            int recurCallMergeSort = 0, recurCallMerge = 0, comparisons = 0, swaps = 0;
            int size = recordSet.Length;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Record[] temp = new Record[size];
            MergeSort(recordSet, temp, 0, size - 1, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);

            stopwatch.Stop();
            #region Display
            if (!isForSearch)
            {
                ViewRecords(recordSet, "Region");
                Console.WriteLine("MergeSort Complete");
                Console.WriteLine("Sorted by Region in {0} order", sortOrder);
                Console.WriteLine("Array Size: {0}", size);
                Console.WriteLine("Total Swaps: {0}", swaps);
                Console.WriteLine("Total Comaprisons: {0}", comparisons);
                Console.WriteLine("MergeSort Took: {0}", stopwatch.Elapsed);
            }
            #endregion
        }


        // Recursive method for the Merge Sort Algorithm which recursively sorts the Record array.
        // Parameters passed to the method: Record array (to be sorted), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array)
        // A temporary Record array, an integer of the 'lowest' most value of the Record array, an integer of 'highest' most
        // value of the Record array and referenced integers to count the recursive calls to the method, comparisons and swaps.
        private static void MergeSort(Record[] recordSet, Record[] temp, int low, int high, int order,
            ref int recurCallMergeSort, ref int recurCallMerge, ref int comparisons, ref int swaps)
        {
            int numb = high - low + 1;
            int middle = low + numb / 2;
            int index;

            if (numb < 2) return;

            for (index = low; index < middle; index++)
            {
                temp[index] = recordSet[index];
            }

            recurCallMergeSort++;
            MergeSort(temp, recordSet, low, middle - 1, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);
            recurCallMergeSort++;
            MergeSort(recordSet, temp, middle, high, order, ref recurCallMergeSort, ref recurCallMerge, ref comparisons, ref swaps);
            recurCallMerge++;
            Merge(recordSet, temp, low, middle, high, order, ref comparisons, ref swaps);
        }


        // Merge function of the Merge Sort Algoithm used to merge data into a sorted array
        private static void Merge(Record[] recordSet, Record[] temp, int low, int middle, int high, int order,
            ref int comparisons, ref int swaps)
        {
            int resultIndex = low;
            int tempIndex = low;
            int destinationIndex = middle;

            while (tempIndex < middle && destinationIndex <= high)
            {
                if (order == 1)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    if (Record.CompareStrings(recordSet[destinationIndex].Region, temp[tempIndex].Region) < 0)
                    {
                        swaps++;
                        recordSet[resultIndex++] = recordSet[destinationIndex++];
                    }
                    else
                    {
                        swaps++;
                        recordSet[resultIndex++] = temp[tempIndex++];
                    }
                }
                else if (order == 0)
                {
                    comparisons++;
                    // Calls the compare to method of the record class
                    if (Record.CompareStrings(recordSet[destinationIndex].Region, temp[tempIndex].Region) > 0)
                    {
                        swaps++;
                        recordSet[resultIndex++] = recordSet[destinationIndex++];
                    }
                    else
                    {
                        swaps++;
                        recordSet[resultIndex++] = temp[tempIndex++];
                    }
                }

            }
            while (tempIndex < middle)
            {
                swaps++;
                recordSet[resultIndex++] = temp[tempIndex++];
            }
        }


        // Implementation of the Heap Sort Algorithm which sorts the a Record array by property
        // Parameters passed to the method: Record array (to be sorted), generic delegate Func Method
        // (passed with an input parameter of Type Record and output of type dynamic), an integer
        // (to define the sorting order), a string with the name of the property (used to sort the Record array)
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void HeapSort(Record[] recordSet, Func<Record, dynamic> prop, int order, string propName)
        {
            int recurCallHeapSort = 0, comparisons = 0, swaps = 0;
            int heapSize = recordSet.Length - 1;
            int index;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (index = heapSize / 2; index >= 0; index--)
            {
                MaxHeapify(recordSet, prop, order, heapSize, index, ref swaps, ref comparisons, ref recurCallHeapSort);
                recurCallHeapSort++;
            }

            for (index = recordSet.Length - 1; index > 0; index--)
            {
                Swap(recordSet, index, 0);
                swaps++;
                heapSize--;
                MaxHeapify(recordSet, prop, order, heapSize, 0, ref swaps, ref comparisons, ref recurCallHeapSort);
                recurCallHeapSort++;
            }

            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, propName);
            Console.WriteLine("HeapSort Complete");
            Console.WriteLine("Sorted by {0} in {1} order", propName, sortOrder);
            Console.WriteLine("Recursive Call Count: {0}", recurCallHeapSort);
            Console.WriteLine("Array Size: {0}", recordSet.Length);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("HeapSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }


        // Max Heaify method of the Heap sort algorithm
        private static void MaxHeapify(Record[] recordSet, Func<Record, dynamic> prop, int order, int heapSize, int index,
            ref int swaps, ref int comparisons, ref int recurCallHeapSort)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            if (order == 0)
            {
                comparisons++;
                // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                // The method returns the property value of the record at the specified index.
                // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                if (left < heapSize && prop(recordSet[left]) > prop(recordSet[index]))
                    largest = left;

                else
                    largest = index;

                comparisons++;
                // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                // The method returns the property value of the record at the specified index.
                // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                if (right < heapSize && prop(recordSet[right]) > prop(recordSet[largest]))
                    largest = right;

                if (largest != index)
                {
                    Swap(recordSet, index, largest);
                    swaps++;
                    MaxHeapify(recordSet, prop, order, heapSize, largest, ref swaps, ref comparisons, ref recurCallHeapSort);
                    recurCallHeapSort++;
                }
            }

            else if (order == 1)
            {
                comparisons++;
                // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                // The method returns the property value of the record at the specified index.
                // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                if (left < heapSize && prop(recordSet[left]) < prop(recordSet[index]))
                    largest = left;
                else
                    largest = index;

                comparisons++;
                // Queries the Record array and selects the property to sort according to the Func method variable 'prop' passed to the algorithm.
                // The method returns the property value of the record at the specified index.
                // Compare the two returned values from the Func method variable. The 'order' variable detemines which indexes are compared.
                if (right < heapSize && prop(recordSet[right]) < prop(recordSet[largest]))
                    largest = right;

                if (largest != index)
                {
                    Swap(recordSet, index, largest);
                    swaps++;
                    MaxHeapify(recordSet, prop, order, heapSize, largest, ref swaps, ref comparisons, ref recurCallHeapSort);
                    recurCallHeapSort++;
                }
            }
        }

        // Implementation of the Heap Sort Algorithm which sorts the a Record array by a string property
        // Parameters passed to the method: Record array (to be sorted), an integer (to define the sorting order).
        // The method uses the defined CompareTo function of the Record class to comapre strings of the region property
        //
        // (The Record array can be viewed by sort property and complete record once complete, also inculdes algorithm 
        // analysis with regards to total comparisons, swaps and time taken to complete)
        public static void HeapSort(Record[] recordSet, int order)
        {
            int recurCallHeapSort = 0, comparisons = 0, swaps = 0;
            int heapSize = recordSet.Length;
            string sortOrder = "";

            #region Order
            // Defines the sorting order of the algorithm
            if (order == 1)
                sortOrder = "Descending";
            else if (order == 0)
                sortOrder = "Ascending";
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int index;
            for (index = (heapSize - 1) / 2; index >= 0; index--)
            {
                recurCallHeapSort++;
                MaxHeapify(recordSet, heapSize, index, order, ref recurCallHeapSort, ref comparisons, ref swaps);
            }
            for (index = heapSize - 1; index > 0; index--)
            {
                Swap(recordSet, index, 0);
                heapSize--;
                recurCallHeapSort++;
                MaxHeapify(recordSet, heapSize, 0, order, ref recurCallHeapSort, ref comparisons, ref swaps);
            }

            stopwatch.Stop();
            #region Display
            ViewRecords(recordSet, "Region");
            Console.WriteLine("HeapSort Complete");
            Console.WriteLine("Sorted by Region in {0} order", sortOrder);
            Console.WriteLine("Array Size: {0}", recordSet.Length);
            Console.WriteLine("Total Swaps: {0}", swaps);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("HeapSort Took: {0}", stopwatch.Elapsed);
            #endregion
        }

        // Max Heaify method of the Heap sort algorithm
        private static void MaxHeapify(Record[] recordSet, int heapSize, int index, int order, ref int recurCallHeapSort, ref int comparisons, ref int swaps)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            if (order == 0)
            {
                comparisons++;
                // Calls the compare to method of the record class
                if (left < heapSize && Record.CompareStrings(recordSet[left].Region, recordSet[index].Region) < 0)
                    largest = left;
                else
                    largest = index;

                comparisons++;
                // Calls the compare to method of the record class
                if (right < heapSize && Record.CompareStrings(recordSet[right].Region, recordSet[largest].Region) < 0)
                    largest = right;

                if (largest != index)
                {
                    swaps++;
                    Swap(recordSet, index, largest);
                    MaxHeapify(recordSet, heapSize, largest, order, ref recurCallHeapSort, ref comparisons, ref swaps);
                }
            }

            else if (order == 1)
            {
                comparisons++;
                // Calls the compare to method of the record class
                if (right < heapSize && Record.CompareStrings(recordSet[left].Region, recordSet[index].Region) > 0)
                    largest = left;
                else
                    largest = index;

                comparisons++;
                // Calls the compare to method of the record class
                if (right < heapSize && Record.CompareStrings(recordSet[right].Region, recordSet[largest].Region) > 0)
                    largest = right;
                if (largest != index)
                {
                    swaps++;
                    Swap(recordSet, index, largest);
                    MaxHeapify(recordSet, heapSize, largest, order, ref recurCallHeapSort, ref comparisons, ref swaps);
                }
            }
        }


        // Method used to swap two elements of a Record array
        // Parameters passed to the method: Record array to have elements swapped and index of the elements to be swapped
        public static void Swap(Record[] recordSet, int indexOne, int indexTwo)
        {
            Record temp = recordSet[indexOne];
            recordSet[indexOne] = recordSet[indexTwo];
            recordSet[indexTwo] = temp;
        }


        // Method to display each Record of the Record array after being sorted
        // Parameters passed to the method: Record array to display and name of the property that the Record array has been sorted by
        public static void ViewRecords(Record[] recordSet, string propName)
        {
            bool isPropOrRecord = false;

            #region Select To View The Full Record Or Property
            // While loop for the exception handling of the user input for selecting to view either the full record or only the property which the array was sorted by
            while (!isPropOrRecord)
            {
                Console.WriteLine("\nWould you like to view the full record or only the property which was sorted?" +
                "\n1: List of Full Record" +
                "\n2: Only Property of the Records Sorted");

                // Exception handling for the user input for selecting either to view the full record or only the property which the array was sorted by
                try
                {
                    // User input
                    int recordOrProp = Convert.ToInt32(Console.ReadLine());

                    // User input conditional statement
                    if (!(recordOrProp > 0 && recordOrProp <= 2))
                    {
                        throw new Exception(recordOrProp.ToString() +
                               " is not a valid option, please select a valid option");
                    }
                    else
                    {
                        // Display the full records or the searched property selected by the user
                        switch (recordOrProp)
                        {
                            case 1:
                                Console.WriteLine("You have selected to view the full list of records. Press enter to continue ...");
                                Console.WriteLine("*******************************************************************************************************");
                                for (int index = 0; index < recordSet.Length; index++)
                                    Console.WriteLine(recordSet[index].RecordDetails());
                                Console.WriteLine("*******************************************************************************************************");
                                isPropOrRecord = true;
                                break;
                            case 2:
                                Console.WriteLine("You have selected to view the list of the property which the records were sorted by. Press enter to continue ...");
                                Console.WriteLine("*******************************************************************************************************");
                                for (int index = 0; index < recordSet.Length; index++)
                                    Console.WriteLine(recordSet[index].PropertyDetails(propName));
                                Console.WriteLine("*******************************************************************************************************");
                                isPropOrRecord = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception recordOrPropertySelected)
                {
                    Console.WriteLine(recordOrPropertySelected.Message +
                        "\nPress Enter to Continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            #endregion
        }
        #endregion
    }

    // Searching class for the Record array
    public class Search
    {
        #region Searching Algorithms
        // Implementation of the linear search Algorithm which searchs the a Record array by property
        // Parameters passed to the method: Record array (to be searched), string of the property name, and the search key
        public static void LinearSearch(Record[] recordSet, string prop, dynamic key)
        {
            bool arrayContainsItem = false;
            int loopCounter = 0, comparisons = 0;
            string indexs = "";
            int size = recordSet.Length, srcIndex;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (srcIndex = 0; srcIndex < size; srcIndex++)
            {
                dynamic record = recordSet[srcIndex].GetPropertyValue(prop, recordSet[srcIndex]);

                comparisons++;
                if (record == key)
                {
                    arrayContainsItem = true;
                    indexs += srcIndex + " ";
                }
                loopCounter++;
            }

            stopwatch.Stop();
            if (arrayContainsItem)
            {
                Console.WriteLine("*******************************************************************************************************");
                if (prop == "DateOnly")
                    Console.WriteLine("Data Item ({0}) Found\n", key.Date.ToShortDateString());
                else
                    Console.WriteLine("Data Item ({0}) Found\n", key);
                ViewIndexsOfSearch(indexs, recordSet, prop);
                Console.WriteLine("*******************************************************************************************************");
            }
            else
            {
                Console.WriteLine("*******************************************************************************************************");
                if (prop == "DateOnly")
                    Console.WriteLine("Data Item ({0}) Not Found\n", key.Date.ToShortDateString());
                else
                    Console.WriteLine("Data Item ({0}) Not Found\n", key);
                Console.WriteLine("*******************************************************************************************************");
            }

            #region Display
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Search Loop Counter: {0}", loopCounter);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("Linear Search Took: {0}", stopwatch.Elapsed);
            #endregion
        }

        // Implementation of the linear search Algorithm which searchs the a Record array by region property
        // Parameters passed to the method: Record array (to be searched), string of the property name, and the search key
        public static void LinearSearch(Record[] recordSet, string key)
        {
            bool arrayContainsItem = false;
            int loopCounter = 0, comparisons = 0;
            string indexs = "";
            int size = recordSet.Length, srcIndex;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (srcIndex = 0; srcIndex < size; srcIndex++)
            {
                string record = recordSet[srcIndex].GetPropertyValue("Region", recordSet[srcIndex]).ToString();

                comparisons++;
                if (record == key.ToUpper())
                {
                    arrayContainsItem = true;
                    indexs += srcIndex + " ";
                }
                loopCounter++;
            }

            stopwatch.Stop();
            if (arrayContainsItem)
            {
                Console.WriteLine("*******************************************************************************************************");
                Console.WriteLine("Data Item ({0}) Found\n", key);
                ViewIndexsOfSearch(indexs, recordSet, "Region");
                Console.WriteLine("*******************************************************************************************************");
            }
            else
            {
                Console.WriteLine("*******************************************************************************************************");
                Console.WriteLine("Data Item ({0}) not found.", key);
                Console.WriteLine("*******************************************************************************************************");
            }

            #region Display
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Search Loop Counter: {0}", loopCounter);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("Linear Search Took: {0}", stopwatch.Elapsed);
            #endregion
        }

        // Implementation of the binary search Algorithm which searchs the a Record array by property
        // Parameters passed to the method: Record array (to be searched), string of the property name, and the search key
        public static void BinarySearch(Record[] recordSet, string propName, dynamic key)
        {
            int size = recordSet.Length;
            int recurCallBinSrc = 0;
            int comparisons = 0;

            Sort.MergeSort(recordSet, record => record.GetPropertyValue(propName, record), 0, propName, true);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            recurCallBinSrc++;
            BinarySearchRecursive(recordSet, key, propName, 0, size - 1, ref recurCallBinSrc, ref comparisons, ref stopwatch);

            #region Display
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Recursive Call Count: {0}", recurCallBinSrc);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("Binary Search Took: {0}", stopwatch.Elapsed);
            #endregion
        }

        // Recursive binary search method
        private static void BinarySearchRecursive(Record[] recordSet, dynamic key, string propName, int start, int end,
            ref int recurCallBinSrc, ref int comparisons, ref Stopwatch stopwatch)
        {
            int size = recordSet.Length;
            string indexs = "";

            comparisons++;
            if (start <= end)
            {
                int middle = (start + end) / 2;

                dynamic record = recordSet[middle].GetPropertyValue(propName, recordSet[middle]);

                if (key == record)
                {
                    stopwatch.Stop();
                    indexs += middle + " ";
                    #region Check For More Results
                    for (int index = middle + 1; index < recordSet.Length; index++)
                    {
                        dynamic recordNext = recordSet[index].GetPropertyValue(propName, recordSet[index]);
                        if (key == recordNext)
                            indexs += index + " ";
                        else
                            break;
                    }
                    for (int index = middle - 1; index > 0; --index)
                    {
                        dynamic recordPrevious = recordSet[index].GetPropertyValue(propName, recordSet[index]);
                        if (key == recordPrevious)
                            indexs += index + " ";
                        else
                            break;
                    }
                    #endregion
                    Console.WriteLine("*******************************************************************************************************");
                    if (propName == "DateOnly")
                        Console.WriteLine("Data Item ({0}) Found\n", key.Date.ToShortDateString());
                    else
                        Console.WriteLine("Data Item ({0}) Found\n", key);
                    ViewIndexsOfSearch(indexs, recordSet, propName);
                    Console.WriteLine("*******************************************************************************************************");
                }
                else if (key < record)
                {
                    recurCallBinSrc++;
                    BinarySearchRecursive(recordSet, key, propName, start, middle - 1, ref recurCallBinSrc, ref comparisons, ref stopwatch);
                }
                else
                {
                    recurCallBinSrc++;
                    BinarySearchRecursive(recordSet, key, propName, middle + 1, end, ref recurCallBinSrc, ref comparisons, ref stopwatch);
                }
            }
            else if (start > end)
            {
                stopwatch.Stop();
                Console.WriteLine("*******************************************************************************************************");
                if (propName == "DateOnly")
                    Console.WriteLine("Data Item ({0}) Not Found\n", key.Date.ToShortDateString());
                else
                    Console.WriteLine("Data Item ({0}) Not Found\n", key);
                Console.WriteLine("*******************************************************************************************************");
            }
        }

        // Implementation of the binary search Algorithm which searchs the a Record array by region property
        // Parameters passed to the method: Record array (to be searched), string of the property name, and the search key
        public static void BinarySearch(Record[] recordSet, string key)
        {
            int size = recordSet.Length;
            int recurCallBinSrc = 0;
            int comparisons = 0;

            Sort.MergeSort(recordSet, 0, true);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            recurCallBinSrc++;
            BinarySearchRecursive(recordSet, key, 0, size - 1, ref recurCallBinSrc, ref comparisons, ref stopwatch);

            #region Display
            Console.WriteLine("Array Size: {0}", size);
            Console.WriteLine("Recursive Call Count: {0}", recurCallBinSrc);
            Console.WriteLine("Total Comaprisons: {0}", comparisons);
            Console.WriteLine("Binary Search Took: {0}", stopwatch.Elapsed);
            #endregion
        }

        // Recursive binary search method
        private static void BinarySearchRecursive(Record[] recordSet, string key, int start, int end,
            ref int recurCallBinSrc, ref int comparisons, ref Stopwatch stopwatch)
        {
            int size = recordSet.Length;
            string indexs = "";

            comparisons++;
            if (start <= end)
            {
                int middle = (start + end) / 2;

                string record = recordSet[middle].GetPropertyValue("Region", recordSet[middle]).ToString();

                if (key.ToUpper() == record)
                {
                    stopwatch.Stop();
                    indexs += middle + " ";
                    #region Check For More Results
                    for (int index = middle + 1; index < recordSet.Length; index++)
                    {
                        string recordNext = recordSet[index].GetPropertyValue("Region", recordSet[index]).ToString();
                        if (Record.CompareStrings(record, recordNext) == 0)
                            indexs += index + " ";
                        else
                            break;
                    }
                    for (int index = middle - 1; index > 0; --index)
                    {
                        string recordPrevious = recordSet[index].GetPropertyValue("Region", recordSet[index]).ToString();
                        if (Record.CompareStrings(record, recordPrevious.ToUpper()) == 0)
                            indexs += index + " ";
                        else
                            break;
                    }
                    #endregion
                    Console.WriteLine("*******************************************************************************************************");
                    Console.WriteLine("Data Item ({0}) Found\n", key);
                    ViewIndexsOfSearch(indexs, recordSet, "Region");
                    Console.WriteLine("*******************************************************************************************************");
                }

                else if (Record.CompareStrings(recordSet[middle].GetPropertyValue("Region", recordSet[middle]).ToString(), key.ToUpper()) < 0)
                {
                    recurCallBinSrc++;
                    BinarySearchRecursive(recordSet, key, start, middle - 1, ref recurCallBinSrc, ref comparisons, ref stopwatch);
                }
                else
                {
                    recurCallBinSrc++;
                    BinarySearchRecursive(recordSet, key, middle + 1, end, ref recurCallBinSrc, ref comparisons, ref stopwatch);
                }
            }
            else if (start > end)
            {
                stopwatch.Stop();
                Console.WriteLine("*******************************************************************************************************");
                Console.WriteLine("Data Item ({0}) not found.", key);
                Console.WriteLine("*******************************************************************************************************");
            }
        }

        // Method which allows the user to when searching for records to view all the records that match thier search key either as a complete record or the property searched
        // Pramaters passed: string containing the indexs of found itmes, the Record array and a string with name of the property that was searched 
        public static void ViewIndexsOfSearch(string indexs, Record[] recordSet, string propName)
        {
            // Splits the string of indexs into an array
            string[] indexStrings = indexs.Split(' ');
            int[] numOfIndexs = new int[indexStrings.Length];
            int count = 0;
            bool isPropOrRecord = false;

            // Converts the indexs of the string array to an int array
            for (int index = 0; index < indexStrings.Length - 1; index++)
            {
                int number = Convert.ToInt32(indexStrings[index]);
                numOfIndexs[count] = number;
                count++;
            }

            #region Slecet To View Full Record Or Property
            // While loop for the exception handling of the user input for selecting either to view the full record or the property that was searched
            while (!isPropOrRecord)
            {
                Console.WriteLine("Would you like to view the full record or only the property which was searched?" +
                "\n1: List of Full Records" +
                "\n2: Only Property of the Records which was Searched");

                // Exception handling of the user input for selecting either the full record or only the property that was searched
                try
                {
                    // User input
                    int recordOrProp = Convert.ToInt32(Console.ReadLine());

                    // User option input conditional statement
                    if (!(recordOrProp > 0 && recordOrProp <= 2))
                    {
                        throw new Exception(recordOrProp.ToString() +
                                " is not a valid option, please select a valid option");
                    }
                    else
                    {
                        // Display the full records or the searched property selected by the user
                        switch (recordOrProp)
                        {
                            case 1:
                                Console.WriteLine("You have selected to view the list of full records found. Press enter to continue ...");
                                Console.WriteLine("*******************************************************************************************************");
                                for (int index = 0; index < numOfIndexs.Length - 1; index++)
                                    Console.WriteLine(recordSet[numOfIndexs[index]].RecordDetails());
                                Console.WriteLine("*******************************************************************************************************");
                                isPropOrRecord = true;
                                break;
                            case 2:
                                Console.WriteLine("You have selected to view the list of the property of the  found records. Press enter to continue ...");
                                Console.WriteLine("*******************************************************************************************************");
                                for (int index = 0; index < numOfIndexs.Length - 1; index++)
                                    Console.WriteLine(recordSet[numOfIndexs[index]].PropertyDetails(propName));
                                Console.WriteLine("*******************************************************************************************************");
                                isPropOrRecord = true;
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("Number of indexs found: {0}", numOfIndexs.Length - 1);
                        Console.WriteLine("*******************************************************************************************************");
                    }
                }
                catch (Exception recordOrPropertySelected)
                {
                    Console.WriteLine(recordOrPropertySelected.Message +
                        "\nPress Enter to Continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            #endregion
        }
        #endregion
    }

    // Find Min/Max values class for the Record array
    public class MinMaxValues
    {
        #region Min/Max Algorithms
        // Method which finds the smallest element of a property of a Record array
        // Paramaters passed: Record array, Func Method - passed with an input parameter Type of Record and TResult of type dynamic, a dynamic has been used as the TResult dataType
        // will be different datatypes depending on the property of the record used in the algorthim and a string of the property name to find minimum element
        public static void SmallestElement(Record[] recordSet, string prop)
        {
            // Calls the propertySelectHelper method which is assigned to the string tempProp
            string tempProp = PropertySelectHelper(prop);

            // Set the first element of the property of the record array as the current minimum (uses the GetPropertyValue method of the record class to get the property value of the record)
            dynamic min = recordSet[0].GetPropertyValue(tempProp, recordSet[0]);

            // Loop through every element of the array and compare to the current minimum (property) element
            for (int index = 0; index < recordSet.Length; index++)
            {
                // If the minimum (property) value is less than the (property) value at the index (uses the GetPropertyValue method of the record class to get the property value of the record)
                if (min > (dynamic)recordSet[index].GetPropertyValue(tempProp, recordSet[index]))

                    // Set the minimum as the current record property (uses the GetPropertyValue method of the record class to get the property value of the record)
                    min = recordSet[index].GetPropertyValue(tempProp, recordSet[index]);
            }
            // Display the maximum value and the property name (propertyValueDisplayHelper method called)
            Console.WriteLine("The minimum value for {0} is {1}", prop, PropertyValueDisplayHelper(tempProp, min));

            // Once the minimum (propperty) element has been found perform a linear search for that element and display all elements which match the minimum (property) element found
            Search.LinearSearch(recordSet, tempProp, min);
        }

        // Method which finds the smallest element of the Region property of a Record array
        // Paramaters passed: Record array
        public static void SmallestElement(Record[] recordSet)
        {
            // Set the first element of the region of the record array as the current minimum (uses the GetPropertyValue method of the record class to get the property value of the record)
            string min = recordSet[0].GetPropertyValue("Region", recordSet[0]).ToString();

            // Loop through every element of the array and compare to the current minimum (region) element
            for (int index = 0; index < recordSet.Length; index++)
            {
                // If the minimum (region) value is less than the (region) value at the index (uses the GetPropertyValue method of the record class to get the property value of the record
                // and also the sompareStrings method of the record class)
                if (Record.CompareStrings(min, recordSet[index].GetPropertyValue("Region", recordSet[index]).ToString()) > 0)

                    // Set the minimum as the current record region (uses the GetPropertyValue method of the record class to get the property value of the record)
                    min = recordSet[index + 1].GetPropertyValue("Region", recordSet[index + 1]).ToString();
            }
            Console.WriteLine("The minimum value for Region is {0}", min);

            // Once the minimum (region) element has been found perform a linear search for that element and display all elements which match the minimum (region) element found
            Search.LinearSearch(recordSet, min);
        }

        // Method which finds the largest element of a property of a Record array
        // Paramaters passed: Record array, Func Method - passed with an input parameter Type of Record and TResult of type dynamic, a dynamic has been used as the TResult dataType
        // will be different datatypes depending on the property of the record used in the algorthim and a string of the property name to find maximum element
        public static void LargestElement(Record[] recordSet, string prop)
        {
            // Calls the propertySelectHelper method which is assigned to the string tempProp
            string tempProp = PropertySelectHelper(prop);

            // Set the first element of the property of the record array as the current maximum (uses the GetPropertyValue method of the record class to get the property value of the record)
            dynamic max = recordSet[0].GetPropertyValue(tempProp, recordSet[0]);

            // Loop through every element of the array and compare to the current maximum (property) element
            for (int index = 0; index < recordSet.Length; index++)
            {
                // If the maximum (property) value is greater than the (property) value at the index (uses the GetPropertyValue method of the record class to get the property value of the record)
                if (max < (dynamic)recordSet[index].GetPropertyValue(tempProp, recordSet[index]))

                    // Set the maximum as the current record property (uses the GetPropertyValue method of the record class to get the property value of the record)
                    max = recordSet[index].GetPropertyValue(tempProp, recordSet[index]);
            }
            // Display the maximum value and the property name (propertyValueDisplayHelper method called)
            Console.WriteLine("The maximum value for {0} is {1}", prop, PropertyValueDisplayHelper(tempProp, max));

            // Once the maximum (propperty) element has been found perform a linear search for that element and display all elements which match the maximum (property) element found
            Search.LinearSearch(recordSet, tempProp, max);
        }

        // Method which finds the smallest element of the Region property of a Record array
        // Paramaters passed: Record array
        public static void LargestElement(Record[] recordSet)
        {
            // Set the first element of the region of the record array as the current minimum (uses the GetPropertyValue method of the record class to get the property value of the record)
            string max = recordSet[0].GetPropertyValue("Region", recordSet[0]).ToString();

            // Loop through every element of the array and compare to the current minimum (region) element
            for (int index = 0; index < recordSet.Length; index++)
            {
                // If the maximum (region) value is less than the (region) value at the index (uses the GetPropertyValue method of the record class to get the property value of the record
                // and also the sompareStrings method of the record class)
                if (Record.CompareStrings(max, recordSet[index].GetPropertyValue("Region", recordSet[index]).ToString()) < 0)

                    // Set the maximum as the current record region (uses the GetPropertyValue method of the record class to get the property value of the record)
                    max = recordSet[index + 1].GetPropertyValue("Region", recordSet[index + 1]).ToString();
            }
            Console.WriteLine("The minimum value for Region is {0}", max);

            // Once the minimum (region) element has been found perform a linear search for that element and display all elements which match the minimum (region) element found
            Search.LinearSearch(recordSet, max);
        }

        // Helper method that allows the user to select if want to find the max/min element for the date inculding the time component
        // or the date component only of the property of a record. Parameter passed: the string of the property name
        private static string PropertySelectHelper(string prop)
        {
            string tempProp = "";
            bool isDateComponentSelected = false;

            if (prop == "Date")
            {
                // While loop for the exception handling of the user input for selecting either the  date inculding the time component
                // or the date component only of the property of a record
                while (!isDateComponentSelected)
                {
                    Console.WriteLine("Would like to find values for the date inculding the time component or the date component only?" +
                        "\n1: Date and Time components" +
                        "\n2: Date component only");

                    // Exception handling of the user input for selecting either the  date inculding the time component
                    // or the date component only of the property of a record
                    try
                    {
                        // User input
                        int dateComponent = Convert.ToInt32(Console.ReadLine());

                        // User option input conditional statement
                        if (!(dateComponent > 0 && dateComponent <= 2))
                        {
                            throw new Exception(dateComponent.ToString() +
                                   " is not a valid option, please select a valid option");
                        }
                        else
                        {
                            // Set the property to use as selected by the user
                            switch (dateComponent)
                            {
                                case 1:
                                    tempProp = prop;
                                    isDateComponentSelected = true;
                                    break;
                                case 2:
                                    tempProp = "DateOnly";
                                    isDateComponentSelected = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception dateHelper)
                    {
                        Console.WriteLine(dateHelper.Message +
                            "\nPress Enter to Continue ...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
            else
                tempProp = prop;
            return tempProp;
        }

        // Helper method to return the maxMinValue of the max/min functions as a string. Method is used to only return the date component of a dateTime datatype
        // when finding the max/min values of the date property of thr record
        private static string PropertyValueDisplayHelper(string tempProp, dynamic maxMinValue)
        {
            if (tempProp == "DateOnly")
                return maxMinValue.Date.ToShortDateString();
            else
                return maxMinValue.ToString();
        }
        #endregion
    }
}