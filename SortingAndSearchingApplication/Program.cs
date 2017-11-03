using System;
using DataBase;
using Algorithms;
// System.Globalization used for defining the formatting options that customize string parsing for the TimeSpan.ParseExact, TimeSpan.TryParseExact methods and also
// the formatting options that are used to customise string parsing for date and time parsing methods.
using System.Globalization;
using static DataBase.Record;
// System.Runtime.InteropServices is used for setting the console screen to maximse on start up
using System.Runtime.InteropServices;

/* Sorting and Searching application. This application will sort/search sets of data (Records) and return results based on the data which has been analyzed.

There are three options for the application: Sorting the default files, Searching the files and returning the Max/Min values from the files.

The application features:
* Five sorting algorithms
* Two searching algorithms
* View all sorted/searched data or individual sorting/searching data
* Both datasets provided and also the datasets merged into a complete data set (duplicate entries have been merged/removed)
* Find the maximum and minimum values of the data
* Algorithm analysis
* Ascending and descending sorting
* Numerical data sorting/searching and text data sorting/searching

Created by Ben Hide for the CMP1127M assessment item 2 of the Algorithms and Complexity Module for the Games Computing BSc (1st Year).*/

namespace Program
{
    class Program
    {
        // Used to maximise the console window at startup.
        #region Maximize Window
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        #endregion

        static void Main()
        {
            // Used to maximise the console window at startup.
            #region Maximise Window
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            #endregion

            #region Main Variables - Arrays / Records Initialisation
            // Boolean variables to control the program flow of the application. These variables control the while loops used to loop the statements in the program which reqiure user input.
            // These while loops are used in conjuction with the exception handling statements when user input is required so that they will loop the statements until the correct input has been recieved.
            bool loopProgram = true;
            bool isSearchSortMaxMin = false;
            bool isRecordSetSelected = false;
            bool isSortCiteria = false;
            bool isSortMethod = false;
            bool isSortOrder = false;
            bool isSearchCiteria = false;
            bool isSearchMethod = false;
            bool isSearchKey = false;
            bool isMaxMinCriteria = false;
            bool isMaxMinMethod = false;
            bool isContinue = false;
            bool isBothDataSets = false;

            // String variables to store the descriptions of the various user selected options of the program.
            string sortSearchMinMaxOption = "";
            string recordSet = "";
            string sortProp = "";
            string sortMethod = "";
            string sortOrder = "";
            string searchProp = "";
            string searchMethod = "";
            string maxMinProp = "";
            string maxMinMethod = "";

            // Integer variables to store the values for the various user selected options of the program.
            // These variables are used in the main methods switch statements when selecting the option/functions available.
            int propToSort = 0;
            int sort = 0;
            int order = 0;
            int propToSearch = 0;
            int search = 0;
            int propMaxMin = 0;
            int maxMin = 0;
            int continueExit = 0;
            int bothDataSets = 0;

            // Arrays of type Record for each of the datasets and the two datasest merged together. A 'temporay' array which is passed the array that is to be used in the program.
            Record[] records1 = new Record[600];
            Record[] records2 = new Record[600];
            Record[] recordsComp = new Record[records1.Length + records2.Length];
            Record[] recordsTemp1 = records1;
            Record[] recordsTemp2 = records2;

            // Passes the arrays to a method which calls the methods needed in the DataReadWriteClass to read, convert and write all the data from the files provided.
            // Each set of data is written to the specific properties of the records in each record array.
            GenerateRecordSets(ref records1, ref records2, ref recordsComp);
            #endregion

            #region Main Program

            // Program loop
            while (loopProgram)
            {
                Console.WriteLine("This application will allow you to sort and/or search through sets of designated records," +
                "\nand also search for min/max values with the records.\n" +
                "These records relate to Seismic Data collected from around the world.\n");

                // Resets the boolean to control the user decision to continue the program to false
                // (when the user reaches the end of the program and decides to continue this will change to true to loop the program again)
                isContinue = false;

                #region Sort / Search / MinMax Records

                // While loop for the exception handling of the user option to select program function e.g. sort, search, find min max.
                while (!isSearchSortMaxMin)
                {
                    Console.WriteLine("Please select if you would like to sort the records (option 1), search the records (option 2) or find the Max/Min values the records (option 3):" +
                        "\n1: Sort Records" +
                        "\n2: Search Records" +
                        "\n3: Find Max/Min Values");

                    // Exception handling of the user option to select program function e.g. sort, search, find min max.
                    try
                    {
                        // User input
                        int sortSearchMinMax = Convert.ToInt32(Console.ReadLine());

                        // User option select conditional statement
                        if (!(sortSearchMinMax > 0 && sortSearchMinMax <= 3))
                        {
                            throw new Exception(sortSearchMinMax.ToString() +
                                " is not a valid option, please select a valid option.");
                        }
                        else
                        {
                            // Set the function type selected by the user
                            switch (sortSearchMinMax)
                            {
                                case 1:
                                    sortSearchMinMaxOption = "SORT";
                                    break;
                                case 2:
                                    sortSearchMinMaxOption = "SEARCH";
                                    break;
                                case 3:
                                    sortSearchMinMaxOption = "FIND MAX/MIN OF";
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine("You have selected to {0} the Records. Press enter to continue ...",
                                sortSearchMinMaxOption);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                    catch (Exception optionSelected)
                    {
                        Console.WriteLine(optionSelected.Message +
                            "\nPress Enter to Continue ...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                #endregion


                #region Select Records
                // While loop for the exception handling of the user option to select record array e.g. array of dataset one, dataset two or complete dataset.
                while (!isRecordSetSelected)
                {
                    Console.WriteLine("Please select the set of Records you would like to {0} from the list below:" +
                        "\n1: Record Set 1" +
                        "\n2: Record Set 2" +
                        "\n3: Complete Record Set (Record Set 1 and 2 combined)", sortSearchMinMaxOption);

                    // Exception handling of the user option to select record array e.g. array of dataset one, dataset two or complete dataset.
                    try
                    {
                        // User input
                        int selectedRecords = Convert.ToInt32(Console.ReadLine());

                        // User option select conditional statement
                        if (!(selectedRecords > 0 && selectedRecords <= 3))
                        {
                            throw new Exception(selectedRecords.ToString() +
                                " is not a valid option, please select a valid set of Records");
                        }
                        else
                        {
                            // Set temp array to the array selected by the user
                            switch (selectedRecords)
                            {
                                case 1:
                                    recordsTemp1 = records1;
                                    recordsTemp2 = records2;
                                    recordSet = "RECORD SET 1";
                                    break;
                                case 2:
                                    recordsTemp1 = records2;
                                    recordsTemp2 = records1;
                                    recordSet = "RECORD SET 2";
                                    break;
                                case 3:
                                    recordsTemp1 = recordsComp;
                                    recordSet = "RECORD SET COMPLETE";
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine("You have selected to {0} the {1}. Press enter to continue ...",
                                sortSearchMinMaxOption,
                                recordSet);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                    catch (Exception recordsSelected)
                    {
                        Console.WriteLine(recordsSelected.Message +
                            "\nPress Enter to Continue ...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                #endregion


                #region Sort Records
                // If the option for sorting has been selected run this code
                if (sortSearchMinMaxOption == "SORT")
                {
                    #region Sort Criteria
                    // While loop for the exception handling of the user option to select the sorting property for the array
                    while (!isSortCiteria)
                    {
                        Console.WriteLine("Please enter the property you would like to {0} the {1} by from the list below:" +
                        "\n1:  Year" +
                        "\n2:  Month" +
                        "\n3:  Day" +
                        "\n4:  Magnitude" +
                        "\n5:  Latitude" +
                        "\n6:  Longitude" +
                        "\n7:  Depth" +
                        "\n8:  Region" +
                        "\n9:  IRIS ID" +
                        "\n10: Time" +
                        "\n11: Timestamp" +
                        "\n12: Date",
                        sortSearchMinMaxOption, recordSet);

                        // Exception handling of the user option to select the sorting property for the array
                        try
                        {
                            // User input
                            propToSort = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(propToSort > 0 && propToSort <= 12))
                            {
                                throw new Exception(propToSort.ToString() +
                                    " is not a valid option, please select a valid sorting property.");
                            }
                            else
                            {
                                // Set the sorting property selected by the user
                                switch (propToSort)
                                {
                                    case 1:
                                        sortProp = "Year";
                                        break;
                                    case 2:
                                        sortProp = "Month";
                                        break;
                                    case 3:
                                        sortProp = "Day";
                                        break;
                                    case 4:
                                        sortProp = "Magnitude";
                                        break;
                                    case 5:
                                        sortProp = "Latitude";
                                        break;
                                    case 6:
                                        sortProp = "Longitude";
                                        break;
                                    case 7:
                                        sortProp = "Depth";
                                        break;
                                    case 8:
                                        sortProp = "Region";
                                        break;
                                    case 9:
                                        sortProp = "IRISID";
                                        break;
                                    case 10:
                                        sortProp = "Time";
                                        break;
                                    case 11:
                                        sortProp = "TimeStamp";
                                        break;
                                    case 12:
                                        sortProp = "Date";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} by the {2} property. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    sortProp);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception criteriaSelected)
                        {
                            Console.WriteLine(criteriaSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Sort Method
                    // While loop for the exception handling of the user option to select the sorting algorithm
                    while (!isSortMethod)
                    {
                        Console.WriteLine("Please enter the method you would like to {0} the {1} by the {2} property from the list below:" +
                            "\n1:  Bubble Sort" +
                            "\n2:  Insertion Sort" +
                            "\n3:  Quick Sort" +
                            "\n4:  Merge Sort" +
                            "\n5:  Heap Sort",
                            sortSearchMinMaxOption, recordSet, sortProp);

                        // Exception handling of the user option to select the sorting property algorithm
                        try
                        {
                            // User input
                            sort = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(sort > 0 && sort <= 5))
                            {
                                throw new Exception(sort.ToString() +
                                    " is not a valid option, please select a valid sorting method");
                            }
                            else
                            {
                                // Set the sorting algorithm type selected by the user
                                switch (sort)
                                {
                                    case 1:
                                        sortMethod = "Bubble Sort";
                                        break;
                                    case 2:
                                        sortMethod = "Insertion Sort";
                                        break;
                                    case 3:
                                        sortMethod = "QuickSort";
                                        break;
                                    case 4:
                                        sortMethod = "MergeSort";
                                        break;
                                    case 5:
                                        sortMethod = "HeapSort";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} by the {2} property using {3}. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    sortProp,
                                    sortMethod);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception methodSelected)
                        {
                            Console.WriteLine(methodSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Sort Order
                    // While loop for the exception handling of the user option to select the sorting order
                    while (!isSortOrder)
                    {
                        Console.WriteLine("Please select the order you would like to {0} the {1} by the {2} property using the {3} from the list below:" +
                        "\n1:  Ascending" +
                        "\n2:  Descending",
                        sortSearchMinMaxOption, recordSet, sortProp, sortMethod);

                        // Exception handling of the user option to select the sorting order
                        try
                        {
                            // User input
                            order = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(order > 0 && order <= 2))
                            {
                                throw new Exception(order.ToString() +
                                    " is not a valid option, please select a valid sort order.");
                            }
                            else
                            {
                                // Set the sorting order selected by the user
                                switch (order)
                                {
                                    case 1:
                                        sortOrder = "Ascending";
                                        order = 0;
                                        break;
                                    case 2:
                                        sortOrder = "Descending";
                                        order = 1;
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} by the {2} property using the {3} in {4} order. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    sortProp,
                                    sortMethod,
                                    sortOrder);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception orderSelected)
                        {
                            Console.WriteLine(orderSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Sort Records
                    // Call the relevant sorting algorithm
                    // While loop for the exception handling of the user option to select to view both datasets
                    if (recordSet == "RECORD SET 2" || recordSet == "RECORD SET 1")
                    {
                        while (!isBothDataSets)
                        {
                            Console.WriteLine("Would you like to sort and view the data from both data sets?" +
                                "\n1:  Yes" +
                                "\n2:  No");

                            // Exception handling of the user option to select to view both datasets
                            try
                            {
                                // User input
                                bothDataSets = Convert.ToInt32(Console.ReadLine());

                                // User option select conditional statement
                                if (!(bothDataSets > 0 && bothDataSets <= 2))
                                {
                                    throw new Exception(bothDataSets.ToString() +
                                        " is not a valid option, please select a valid option");
                                }
                                else
                                {
                                    Console.WriteLine("You have selected to view both datasets. Press enter to continue ...");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                            catch (Exception bothSelected)
                            {
                                Console.WriteLine(bothSelected.Message +
                                    "\nPress Enter to Continue ...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        #endregion
                    }

                    switch (sort)
                    {
                        // Switch case for the sorting algorithm selected by the user
                        case 1:
                            // If the user is sorting by Region using bubble sort call the bubble sort method, passed the array to sort an integer which determines the order to sort the array  
                            if (propToSort == 8)
                            {
                                Sort.BubbleSortString(recordsTemp1, order);
                                if (bothDataSets == 1)
                                { Sort.BubbleSortString(recordsTemp2, order); }
                            }

                            // Else called an overloaded version of bubble sort that is used to sort the array by any other property of the record except the region property
                            else
                            // Passes the Record array to be sorted, a lambda expression to the Func method of the algorithm (the Func method takes a Record and returns the
                            // value of the specified property of the Record inside the algorithm), an integer which determines the order to sort the array by and the name of the sortinig property.
                            // The record.GetPropertyValue() method in the lambda expression uses the system.reflection namespace of the record class to get the value of the property of
                            // the record
                            {
                                Sort.BubbleSort(recordsTemp1, record => record.GetPropertyValue(sortProp, record), order, sortProp);
                                if (bothDataSets == 1)
                                { Sort.BubbleSort(recordsTemp2, record => record.GetPropertyValue(sortProp, record), order, sortProp); }
                            }

                            break;
                        case 2:
                            // If the user is sorting by Region using bubble sort call the insertion sort method, passed the array to sort an integer which determines the order to sort the array  
                            if (propToSort == 8)
                            {
                                Sort.InsertionSortString(recordsTemp1, order);
                                if (bothDataSets == 1)
                                { Sort.InsertionSortString(recordsTemp2, order); }
                            }

                            // Else called an overloaded version of insertion sort that is used to sort the array by any other property of the record except the region property
                            else
                            // Passes the Record array to be sorted, a lambda expression to the Func method of the algorithm (the Func method takes a Record and returns the
                            // value of the specified property of the Record inside the algorithm), an integer which determines the order to sort the array by and the name of the sortinig property.
                            // The record.GetPropertyValue() method in the lambda expression uses the system.reflection namespace of the record class to get the value of the property of
                            // the record
                            {
                                Sort.InsertionSort(recordsTemp1, record => record.GetPropertyValue(sortProp, record), order, sortProp);
                                if (bothDataSets == 1)
                                { Sort.InsertionSort(recordsTemp2, record => record.GetPropertyValue(sortProp, record), order, sortProp); }
                            }

                            break;
                        case 3:
                            // If the user is sorting by Region using quick sort call the insertion sort method, passed the array to sort an integer which determines the order to sort the array
                            if (propToSort == 8)
                            {
                                Sort.QuickSortString(recordsTemp1, order);
                                if (bothDataSets == 1)
                                { Sort.QuickSortString(recordsTemp2, order); }
                            }


                            // Else called an overloaded version of quick sort that is used to sort the array by any other property of the record except the region property
                            else
                            // Passes the Record array to be sorted, a lambda expression to the Func method of the algorithm (the Func method takes a Record and returns the
                            // value of the specified property of the Record inside the algorithm), an integer which determines the order to sort the array by and the name of the sortinig property.
                            // The record.GetPropertyValue() method in the lambda expression uses the system.reflection namespace of the record class to get the value of the property of
                            // the record
                            {
                                Sort.QuickSort(recordsTemp1, record => record.GetPropertyValue(sortProp, record), order, sortProp);
                                if (bothDataSets == 1)
                                { Sort.QuickSort(recordsTemp2, record => record.GetPropertyValue(sortProp, record), order, sortProp); }
                            }

                            break;
                        case 4:
                            // If the user is sorting by Region using merge sort call the insertion sort method, passed the array to sort an integer which determines the order to sort the array
                            if (propToSort == 8)
                            {
                                Sort.MergeSort(recordsTemp1, order, false);
                                if (bothDataSets == 1)
                                { Sort.MergeSort(recordsTemp2, order, false); }
                            }


                            // Else called an overloaded version of merge sort that is used to sort the array by any other property of the record except the region property
                            else
                            // Passes the Record array to be sorted, a lambda expression to the Func method of the algorithm (the Func method takes a Record and returns the
                            // value of the specified property of the Record inside the algorithm), an integer which determines the order to sort the array by and the name of the sortinig property.
                            // The record.GetPropertyValue() method in the lambda expression uses the system.reflection namespace of the record class to get the value of the property of
                            // the record
                            // The Mergesort algorithm also has a boolean which indicates if the algorithm has been used for a Binary Search method call. If merge sort is used when performing a binary 
                            // search the records once sorted aren't displayed and vices versa. This stops the records being displayed when sorted before being searched
                            {
                                Sort.MergeSort(recordsTemp1, record => record.GetPropertyValue(sortProp, record), order, sortProp, false);
                                if (bothDataSets == 1)
                                { Sort.MergeSort(recordsTemp2, record => record.GetPropertyValue(sortProp, record), order, sortProp, false); }
                            }

                            break;
                        case 5:
                            // If the user is sorting by Region using merge sort call the insertion sort method, passed the array to sort an integer which determines the order to sort the array
                            if (propToSort == 8)
                            {
                                Sort.HeapSort(recordsTemp1, order);
                                if (bothDataSets == 1)
                                { Sort.HeapSort(recordsTemp2, order); }
                            }


                            // Else called an overloaded version of merge sort that is used to sort the array by any other property of the record except the region property
                            else
                            // Passes the Record array to be sorted, a lambda expression to the Func method of the algorithm (the Func method takes a Record and returns the
                            // value of the specified property of the Record inside the algorithm), an integer which determines the order to sort the array by and the name of the sortinig property.
                            // The record.GetPropertyValue() method in the lambda expression uses the system.reflection namespace of the record class to get the value of the property of
                            // the record
                            {
                                Sort.HeapSort(recordsTemp1, record => record.GetPropertyValue(sortProp, record), order, sortProp);
                                if (bothDataSets == 1)
                                { Sort.HeapSort(recordsTemp2, record => record.GetPropertyValue(sortProp, record), order, sortProp); }
                            }

                            break;
                        default:
                            break;
                    }
                }
                #endregion
                #endregion


                #region Search Records
                // If the option for searching has been selected run this code
                else if (sortSearchMinMaxOption == "SEARCH")
                {
                    #region Search Criteria
                    // While loop for the exception handling of the user option to select the searching property for the array
                    while (!isSearchCiteria)
                    {
                        Console.WriteLine("Please enter the property you would like to {0} the {1} by from the list below:" +
                        "\n1:  Year" +
                        "\n2:  Month" +
                        "\n3:  Day" +
                        "\n4:  Magnitude" +
                        "\n5:  Latitude" +
                        "\n6:  Longitude" +
                        "\n7:  Depth" +
                        "\n8:  Region" +
                        "\n9:  IRIS ID" +
                        "\n10: Time" +
                        "\n11: Timestamp" +
                        "\n12: Date",
                        sortSearchMinMaxOption, recordSet);

                        // Exception handling of the user option to select the sorting property for the array
                        try
                        {
                            // User input
                            propToSearch = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(propToSearch > 0 && propToSearch <= 12))
                            {
                                throw new Exception(propToSearch.ToString() +
                                    " is not a valid option, please select a valid sorting property.");
                            }
                            else
                            {
                                // Set the searching property selected by the user
                                switch (propToSearch)
                                {
                                    case 1:
                                        searchProp = "Year";
                                        break;
                                    case 2:
                                        searchProp = "Month";
                                        break;
                                    case 3:
                                        searchProp = "Day";
                                        break;
                                    case 4:
                                        searchProp = "Magnitude";
                                        break;
                                    case 5:
                                        searchProp = "Latitude";
                                        break;
                                    case 6:
                                        searchProp = "Longitude";
                                        break;
                                    case 7:
                                        searchProp = "Depth";
                                        break;
                                    case 8:
                                        searchProp = "Region";
                                        break;
                                    case 9:
                                        searchProp = "IRISID";
                                        break;
                                    case 10:
                                        searchProp = "Time";
                                        break;
                                    case 11:
                                        searchProp = "TimeStamp";
                                        break;
                                    case 12:
                                        searchProp = "Date";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} by the {2} property. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    searchProp);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception criteriaSelected)
                        {
                            Console.WriteLine(criteriaSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Search Method
                    // While loop for the exception handling of the user option to select the searching algorithm for the array
                    while (!isSearchMethod)
                    {
                        Console.WriteLine("Please enter the method you would like to {0} the {1} by the {2} property from the list below:" +
                            "\n1:  Linear Search" +
                            "\n2:  Binary Search",
                            sortSearchMinMaxOption, recordSet, searchProp);

                        // Exception handling of the user option to select the searching algorithm for the array
                        try
                        {
                            // User input
                            search = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(search > 0 && search <= 2))
                            {
                                throw new Exception(search.ToString() +
                                    " is not a valid option, please select a valid sorting method");
                            }
                            else
                            {
                                // Set the searching algorithm selected by the user
                                switch (search)
                                {
                                    case 1:
                                        searchMethod = "Linear Search";
                                        break;
                                    case 2:
                                        searchMethod = "Binary Search";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} by the {2} property using {3}. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    searchProp,
                                    searchMethod);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception methodSelected)
                        {
                            Console.WriteLine(methodSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Search Key
                    // While loop for the exception handling of the user option to select to view both datasets
                    if (recordSet == "RECORD SET 2" || recordSet == "RECORD SET 1")
                    {
                        while (!isBothDataSets)
                        {
                            Console.WriteLine("Would you like to search and view the data from both data sets?" +
                                "\n1:  Yes" +
                                "\n2:  No");

                            // Exception handling of the user option to select to view both datasets
                            try
                            {
                                // User input
                                bothDataSets = Convert.ToInt32(Console.ReadLine());

                                // User option select conditional statement
                                if (!(bothDataSets > 0 && bothDataSets <= 2))
                                {
                                    throw new Exception(bothDataSets.ToString() +
                                        " is not a valid option, please select a valid option");
                                }
                                else
                                {
                                    Console.WriteLine("You have selected to view both datasets. Press enter to continue ...");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                            catch (Exception bothSelected)
                            {
                                Console.WriteLine(bothSelected.Message +
                                    "\nPress Enter to Continue ...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                    }
                    // While loop for the exception handling of the user input to define the search key for the array
                    while (!isSearchKey)
                    {
                        Console.WriteLine("Please enter the search key you would like to {0} the {1} by the {2} property using the {3} now:",
                        sortSearchMinMaxOption, recordSet, searchProp, searchMethod);

                        // Exception handling of the user option to select the searching algorithm for the array
                        try
                        {
                            // Set the search key selected by the user input
                            switch (propToSearch)
                            {
                                case 1:
                                    // User input
                                    int yearKey = Convert.ToInt32(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the year search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, yearKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, yearKey); }
                                    }


                                    // Else if the user selected binary search method and pass the year search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, yearKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, yearKey); }

                                    }

                                    break;
                                case 2:
                                    // User input
                                    Months monthKey = (Months)Enum.Parse(typeof(Months), Console.ReadLine().ToUpper());

                                    // If the user selected linear search call the linear search method and pass the month search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, monthKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, monthKey); }
                                    }


                                    // Else if the user selected binary search method and pass the month search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, monthKey);
                                        { Search.BinarySearch(recordsTemp2, searchProp, monthKey); }
                                    }
                                    break;
                                case 3:
                                    // User input
                                    int dayKey = Convert.ToInt32(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the day search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, dayKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, dayKey); }
                                    }


                                    // Else if the user selected binary search method and pass the year day key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, dayKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, dayKey); }
                                    }
                                    break;
                                case 4:
                                    // User input
                                    double magKey = Convert.ToDouble(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the magnitude search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, magKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, magKey); }
                                    }


                                    // Else if the user selected binary search method and pass the magnitude search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, magKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, magKey); }
                                    }
                                    break;
                                case 5:
                                    // User input
                                    double latKey = Convert.ToDouble(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the latitude search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, latKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, latKey); }
                                    }

                                    // Else if the user selected binary search method and pass the latitude search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, latKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, latKey); }
                                    }

                                    break;
                                case 6:
                                    // User input
                                    double lonKey = Convert.ToDouble(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the longitude search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, lonKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, lonKey); }
                                    }


                                    // Else if the user selected binary search method and pass the longitude search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, lonKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, lonKey); }
                                    }

                                    break;
                                case 7:
                                    // User input
                                    double depKey = Convert.ToDouble(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the depth search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, depKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, depKey); }
                                    }


                                    // Else if the user selected binary search method and pass the depth search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, depKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, depKey); }
                                    }

                                    break;
                                case 8:
                                    // User input
                                    string regKey = Console.ReadLine().ToUpper();

                                    // If the user selected linear search call the linear search method and pass the region search key (linear search method used for searching for strings called)
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, regKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, regKey); }
                                    }


                                    // Else if the user selected binary search method and pass the region search key (binary search method used for searching for strings called)
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, regKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, regKey); }
                                    }

                                    break;
                                case 9:
                                    // User input
                                    long irisKey = Convert.ToInt64(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the irid ID search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, irisKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, irisKey); }
                                    }


                                    // Else if the user selected binary search method and pass the iris ID search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, irisKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, irisKey); }
                                    }

                                    break;
                                case 10:
                                    // User input - ConvertToTimeSpan method called which returns a timespan from user input
                                    TimeSpan timeKey = ConvertToTimeSpan();

                                    // If the user selected linear search call the linear search method and pass the time search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, timeKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, timeKey); }
                                    }

                                    // Else if the user selected binary search method and pass the time search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, timeKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, timeKey); }
                                    }

                                    break;
                                case 11:
                                    // User input
                                    long stampKey = Convert.ToInt64(Console.ReadLine());

                                    // If the user selected linear search call the linear search method and pass the timestamp search key
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, searchProp, stampKey);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, searchProp, stampKey); }
                                    }


                                    // Else if the user selected binary search method and pass the timestamp search key
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, searchProp, stampKey);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, searchProp, stampKey); }
                                    }

                                    break;
                                case 12:
                                    // User input - ConvertToTDateTimemethod called which returns a dateTime from user input
                                    DateTime dateKey = ConvertToDateTime();
                                    dateKey = dateKey.Date;

                                    // If the user selected linear search call the linear search method and pass the date search key (searches for the date elements of the dateTime dataType)
                                    if (search == 1)
                                    {
                                        Search.LinearSearch(recordsTemp1, "DateOnly", dateKey.Date);
                                        if (bothDataSets == 1)
                                        { Search.LinearSearch(recordsTemp2, "DateOnly", dateKey.Date); }
                                    }


                                    // Else if the user selected binary search method and pass the date search key (searches for the date elements of the dateTime dataType)
                                    else if (search == 2)
                                    {
                                        Search.BinarySearch(recordsTemp1, "DateOnly", dateKey.Date);
                                        if (bothDataSets == 1)
                                        { Search.BinarySearch(recordsTemp2, "DateOnly", dateKey.Date); }
                                    }

                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        catch (Exception keySelected)
                        {
                            Console.WriteLine(keySelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion
                }
                #endregion


                #region MaxMin Records
                // If the option for finding max min has been selected run this code
                else if (sortSearchMinMaxOption == "FIND MAX/MIN OF")
                {
                    #region MinMax Criteria
                    // While loop for the exception handling of the user option to select the max/min property to locate in the array
                    while (!isMaxMinCriteria)
                    {
                        Console.WriteLine("Please enter the property you would like to {0} the {1} from the list below:" +
                        "\n1:  Year" +
                        "\n2:  Month" +
                        "\n3:  Day" +
                        "\n4:  Magnitude" +
                        "\n5:  Latitude" +
                        "\n6:  Longitude" +
                        "\n7:  Depth" +
                        "\n8:  Region" +
                        "\n9:  IRIS ID" +
                        "\n10: Time" +
                        "\n11: Timestamp" +
                        "\n12: Date",
                        sortSearchMinMaxOption, recordSet);

                        // Exception handling of the user option to select the max/min property to locate in the array
                        try
                        {
                            // User input
                            propMaxMin = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(propMaxMin > 0 && propMaxMin <= 12))
                            {
                                throw new Exception(propMaxMin.ToString() +
                                    " is not a valid option, please select a valid Max/Min property.");
                            }
                            else
                            {
                                // Set the max/min property selected by the user
                                switch (propMaxMin)
                                {
                                    case 1:
                                        maxMinProp = "Year";
                                        break;
                                    case 2:
                                        maxMinProp = "Month";
                                        break;
                                    case 3:
                                        maxMinProp = "Day";
                                        break;
                                    case 4:
                                        maxMinProp = "Magnitude";
                                        break;
                                    case 5:
                                        maxMinProp = "Latitude";
                                        break;
                                    case 6:
                                        maxMinProp = "Longitude";
                                        break;
                                    case 7:
                                        maxMinProp = "Depth";
                                        break;
                                    case 8:
                                        maxMinProp = "Region";
                                        break;
                                    case 9:
                                        maxMinProp = "IRISID";
                                        break;
                                    case 10:
                                        maxMinProp = "Time";
                                        break;
                                    case 11:
                                        maxMinProp = "TimeStamp";
                                        break;
                                    case 12:
                                        maxMinProp = "Date";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} using the {2} property. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    maxMinProp);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception criteriaSelected)
                        {
                            Console.WriteLine(criteriaSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region MaxMin Method
                    // While loop for the exception handling of the user option to select the max/min function for the array
                    while (!isMaxMinMethod)
                    {
                        Console.WriteLine("Please enter the method you would like to {0} the {1} by the {2} property from the list below:" +
                            "\n1:  Find Max Value" +
                            "\n2:  Find Min Value",
                            sortSearchMinMaxOption, recordSet, maxMinProp);

                        // Exception handling of the user option to select the max/min function for the array
                        try
                        {
                            // User input
                            maxMin = Convert.ToInt32(Console.ReadLine());

                            // User option select conditional statement
                            if (!(maxMin > 0 && maxMin <= 2))
                            {
                                throw new Exception(maxMin.ToString() +
                                    " is not a valid option, please select a valid Max/Min method");
                            }
                            else
                            {
                                // Set the max/min function selected by the user
                                switch (maxMin)
                                {
                                    case 1:
                                        maxMinMethod = "Find Max Value method";
                                        break;
                                    case 2:
                                        maxMinMethod = "Find Min Value method";
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("You have selected to {0} the {1} using the {2} property using {3}. Press enter to continue ...",
                                    sortSearchMinMaxOption,
                                    recordSet,
                                    maxMinProp,
                                    maxMinMethod);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        }
                        catch (Exception optionSelected)
                        {
                            Console.WriteLine(optionSelected.Message +
                                "\nPress Enter to Continue ...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    #endregion


                    #region Find Max/Min of Records
                    // While loop for the exception handling of the user option to select to view both datasets
                    if (recordSet == "RECORD SET 2" || recordSet == "RECORD SET 1")
                    {
                        while (!isBothDataSets)
                        {
                            Console.WriteLine("Would you like to view the data from both data sets?" +
                                "\n1:  Yes" +
                                "\n2:  No");

                            // Exception handling of the user option to select to view both datasets
                            try
                            {
                                // User input
                                bothDataSets = Convert.ToInt32(Console.ReadLine());

                                // User option select conditional statement
                                if (!(bothDataSets > 0 && bothDataSets <= 2))
                                {
                                    throw new Exception(bothDataSets.ToString() +
                                        " is not a valid option, please select a valid option");
                                }
                                else
                                {
                                    Console.WriteLine("You have selected to view both datasets. Press enter to continue ...");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                            catch (Exception bothSelected)
                            {
                                Console.WriteLine(bothSelected.Message +
                                    "\nPress Enter to Continue ...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                    }
                    // Call the relevant max min algorithm
                    switch (maxMin)
                    {
                        case 1:
                            // If the user is finding the max vlaue for the Region call the largestElement algorithm and pass in the record array  
                            if (propMaxMin == 8)
                            {
                                MinMaxValues.LargestElement(recordsTemp1);
                                if (bothDataSets == 1)
                                { MinMaxValues.LargestElement(recordsTemp2); }
                            }

                            // Else if the user is finding the max value for any other Record property call the overloaded largestElement algorithm and pass in the record array and max/min property to locate
                            else
                            {
                                MinMaxValues.LargestElement(recordsTemp1, maxMinProp);
                                if (bothDataSets == 1)
                                { MinMaxValues.LargestElement(recordsTemp2, maxMinProp); }
                            }

                            break;
                        case 2:
                            // If the user is finding the max vlaue for the Region call the smallestElement algorithm and pass in the record array  
                            if (propMaxMin == 8)
                            {
                                MinMaxValues.SmallestElement(recordsTemp1);
                                if (bothDataSets == 1)
                                { MinMaxValues.SmallestElement(recordsTemp2); }
                            }

                            // Else if the user is finding the min value for any other Record property call the overloaded smallestElement algorithm and pass in the record array and max/min property to locate
                            else
                            {
                                MinMaxValues.SmallestElement(recordsTemp1, maxMinProp);
                                if (bothDataSets == 1)
                                { MinMaxValues.SmallestElement(recordsTemp2, maxMinProp); }
                            }

                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                #endregion


                #region Continue or Exit Application
                Console.WriteLine("Press enter to continue ...");
                Console.ReadLine();
                Console.Clear();

                // While loop for the exception handling of the user option to select to continue or exit the program
                while (!isContinue)
                {
                    Console.WriteLine("Would you like to continue using the application or exit?" +
                    "\n1: Continue" +
                    "\n2: Exit");

                    // Exception handling of the user option to select to continue or exit the program
                    try
                    {
                        // User input
                        continueExit = Convert.ToInt32(Console.ReadLine());

                        // User option select conditional statement
                        if (!(continueExit > 0 && continueExit <= 2))
                        {
                            throw new Exception(continueExit.ToString() +
                                   " is not a valid option, please select a valid option");
                        }
                        else
                        {
                            // Continue or exit selected by the user
                            switch (continueExit)
                            {
                                case 1:
                                    Console.WriteLine("You have selected to Continue the application. Press enter to continue ...");
                                    Console.ReadLine();
                                    Console.Clear();

                                    // Lopp the program 
                                    isContinue = true;
                                    bothDataSets = 0;
                                    break;
                                case 2:
                                    Console.WriteLine("You have selected to Exit the application. Press enter to Exit ...");
                                    Console.ReadLine();

                                    // Exit the program
                                    Environment.Exit(0);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception continueAppSelected)
                    {
                        Console.WriteLine(continueAppSelected.Message +
                            "\nPress Enter to Continue ...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                #endregion
            }
        }


        #region Static Program Methods
        // Method to generate the record arrays for each of teh datasets. All record arrays are passed to the method by reference
        private static void GenerateRecordSets(ref Record[] records1, ref Record[] records2, ref Record[] recordsComp)
        {
            // Set the size of the two dimensional string array that holds the text from all the text files in a dataset
            DataReadWrite.SetDataRows(600);
            DataReadWrite.SetDataColumns(14);

            // Populate the record arrays with the data from the text files for both datasets
            records1 = DataReadWrite.ReadData(1, records1);
            records2 = DataReadWrite.ReadData(2, records2);

            // Copy the record arrays for both datasets into the record array that holds the combined datasets
            Array.Copy(records1, recordsComp, records1.Length);
            Array.Copy(records2, 0, recordsComp, records1.Length, records2.Length);
            Sort.MergeSort(recordsComp, record => record.GetPropertyValue("Date", record), 0, "Date", true);

            // Removes duplicates from the recordsComp array
            int duplicateCount = 0, prevIndex = 0;

            // Loop through the array
            for (int index = 0; index < recordsComp.Length; index++)
            {
                bool duplicate = false;
                for (int srcIndex = 0; srcIndex < index; srcIndex++)
                {
                    // If the date, region, latidtude, longitude, timestamp and IRISID match in both
                    // indexes then the data has been repeated
                    if (recordsComp[index].Date == recordsComp[srcIndex].Date &&
                        recordsComp[index].Region == recordsComp[srcIndex].Region &&
                        recordsComp[index].Latitude == recordsComp[srcIndex].Latitude &&
                        recordsComp[index].Longitude == recordsComp[srcIndex].Longitude &&
                        recordsComp[index].TimeStamp == recordsComp[srcIndex].TimeStamp &&
                        recordsComp[index].IRISID == recordsComp[srcIndex].IRISID)
                    {
                        // Duplicate found
                        duplicate = true;
                        // Count for duplicate found in array.
                        duplicateCount++;
                        break;
                    }
                }

                if (duplicate == false)
                {
                    recordsComp[prevIndex] = recordsComp[index];
                    prevIndex++;
                }
            }

            for (int index = 1; index <= duplicateCount; index++)
                recordsComp[recordsComp.Length - index] = null;

            // Resize and sort the array after duplicates are found
            Array.Resize(ref recordsComp, recordsComp.GetUpperBound(0) - duplicateCount + 1);
            Sort.MergeSort(recordsComp, record => record.GetPropertyValue("Date", record), 0, "Date", true);
        }


        // Helper method for the user to select a valid time key when using the searching algorithms.
        // Uses the user input in the method to return a valid timeSpan dataType to use in the searching methods
        private static TimeSpan ConvertToTimeSpan()
        {
            bool isTimeSpan = false;
            string pattern = "c";
            TimeSpan time = new TimeSpan();

            // While loop for the exception handling of the user input for enetring a valid timeSpan when searching the arrays for a time key
            while (!isTimeSpan)
            {
                // Exception handling of the user input for enetring a valid timeSpan when searching the arrays for a time key
                try
                {
                    Console.Write("Please enter the hour (use the format 'hh'): ");

                    // User input for the hour component of the timeSpan
                    string hour = Console.ReadLine();
                    int hourNum = Convert.ToInt32(hour);

                    // User option input conditional statement
                    if (hourNum > 23 || hourNum < 00)
                    {
                        throw new Exception(hour +
                               " is not a valid hours value, please try again.");
                    }

                    Console.Write("Please enter the minutes (use the format 'mm'): ");

                    // User input for the minute component of the timeSpan
                    string minutes = Console.ReadLine();
                    int minNum = Convert.ToInt32(minutes);

                    // User option input conditional statement
                    if (minNum > 59 || minNum < 00)
                    {
                        throw new Exception(minutes +
                               " is not a valid minutes value, please try again.");
                    }

                    Console.Write("Please enter the number of the Year now (use the format 'ss'): ");

                    // User input for the second component of the timeSpan
                    string seconds = Console.ReadLine();
                    int secsNum = Convert.ToInt32(seconds);

                    // User option input conditional statement
                    if (secsNum > 59 || secsNum < 00)
                    {
                        throw new Exception(seconds +
                               " is not a valid seconds value, please try again.");
                    }

                    // String to hold the timeSpan components
                    string timeString = hour + ":" + minutes + ":" + seconds;

                    // Try to parse the string to a timeSpan datatype
                    if (TimeSpan.TryParseExact(timeString, pattern, null, TimeSpanStyles.None, out time))
                    {
                        // If tryParse successful parse the string to a timeSpan datatype
                        time = TimeSpan.ParseExact(timeString, pattern, null);
                        break;
                    }
                    else
                    {
                        throw new Exception(timeString +
                                " is not a valid date, please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message +
                                "\nPress Enter to Continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            // Return the time key as entered by the user
            return time;
        }

        // Helper method for the user to select a valid date key when using the searching algorithms.
        // Uses the user input in the method to return a valid dateTime dataType to use in the searching methods
        private static DateTime ConvertToDateTime()
        {
            bool isDateTime = false;
            string pattern = "dd-MM-yyyy";
            DateTime date = new DateTime();
            DateTime dateOnly = new DateTime();

            // While loop for the exception handling of the user input for enetring a valid dateTime when searching the arrays for a date key
            while (!isDateTime)
            {
                // Exception handling of the user input for enetring a valid dateTime when searching the arrays for a date key
                try
                {
                    Console.Write("Please enter the number of the Day of the month now (use the format 'dd'): ");

                    // User input for the day component of the dateTime
                    string day = Console.ReadLine();
                    int dayNum = Convert.ToInt32(day);
                    if (dayNum > 31 || dayNum < 1)
                    {
                        throw new Exception(day +
                               " is not a valid day, please try again.");
                    }

                    Console.Write("Please enter the number of the Month of the year now (use the format 'mm', alternatively you can write the Month of the year): ");

                    // User input for the month component of the timeSpan
                    string month = Console.ReadLine();
                    int monthNum = -1;
                    #region Month Convert

                    // If the user enters a month using letters 
                    if (char.IsLetter(month[0]))
                    {
                        // Exception handling of the user input for enetring a valid month in letters
                        try
                        {
                            // Convert the user input to a Month enum
                            Months monthEnum = (Months)Enum.Parse(typeof(Months), month.ToUpper());
                            monthNum = (int)monthEnum + 1;

                            if (monthNum.ToString().Length < 2)
                                month = "0" + monthNum.ToString();
                            else
                                month = monthNum.ToString();
                        }
                        catch (Exception enumConvert)
                        {
                            Console.WriteLine(enumConvert.Message);
                        }
                    }

                    // If the user enters a month using digits 
                    else if (char.IsDigit(month[0]))
                    {
                        // Convert the user input to an integer value
                        try
                        {
                            monthNum = Convert.ToInt32(month);
                        }
                        catch (Exception numConvert)
                        {
                            Console.WriteLine(numConvert.Message);
                        }
                    }
                    #endregion

                    // User option input conditional statement
                    if (monthNum > 12 || monthNum < 1)
                    {
                        throw new Exception(month +
                               " is not a valid month, please try again.");
                    }

                    Console.Write("Please enter the number of the Year now (use the format 'yyyy'): ");

                    // User input for the year component of the dateTime
                    string year = Console.ReadLine();
                    int yearNum = Convert.ToInt32(year);

                    // User option input conditional statement
                    if (yearNum > DateTime.Now.Year || yearNum < 2000)
                    {
                        throw new Exception(year +
                               " is not a valid year (in the context of this application), please try again.");
                    }

                    // String to hold the dateTime components
                    string dateString = day + "-" + month + "-" + year;

                    // Try to parse the string to a dateTime datatype
                    if (DateTime.TryParseExact(dateString, pattern, null, DateTimeStyles.None, out date))
                    {
                        // If tryParse successful parse the string to a dateTime datatype
                        date = DateTime.ParseExact(dateString, pattern, null);
                        dateOnly = date.Date;
                        break;
                    }
                    else
                    {
                        throw new Exception(dateString +
                                " is not a valid date, please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message +
                                "\nPress Enter to Continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            // Return the date key as entered by the user
            return dateOnly;
        }
        #endregion
    }
}
