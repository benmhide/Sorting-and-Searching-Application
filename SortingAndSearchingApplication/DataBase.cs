using System;
//The System.Reflection namespace is used to retrieve information about the constructed Record objects properties by examining their metadata.
using System.Reflection;

namespace DataBase
{
    // Record class - Used to store the data from the text files within the designated properties of a constructed Record
    public class Record
    {
        #region Record Class Properties
        // Properties which hold the values for each item of data - for the each set of data - from the files provided
        private int year;
        public int Year
        { get { return year; } set { year = value; } }

        private Months month;
        public Months Month
        { get { return month; } set { month = value; } }

        private int day;
        public int Day
        { get { return day; } set { day = value; } }

        private double magnitude;
        public double Magnitude
        { get { return magnitude; } set { magnitude = value; } }

        private double latitude;
        public double Latitude
        { get { return latitude; } set { latitude = value; } }

        private double longitude;
        public double Longitude
        { get { return longitude; } set { longitude = value; } }

        private double depth;
        public double Depth
        { get { return depth; } set { depth = value; } }

        private string region;
        public string Region
        { get { return region; } set { region = value; } }

        private long irisID;
        public long IRISID
        { get { return irisID; } set { irisID = value; } }

        private TimeSpan time;
        public TimeSpan Time
        { get { return time; } set { time = value; } }

        private long timeStamp;
        public long TimeStamp
        { get { return timeStamp; } set { timeStamp = value; } }

        private DateTime date;
        public DateTime Date
        { get { return date; } set { date = value; } }

        private DateTime dateOnly;
        public DateTime DateOnly
        { get { return dateOnly; } set { dateOnly = value; } }

        private string dataSet;
        public string DataSet
        { get { return dataSet; } set { dataSet = value; } }


        // Enumeration for the Months Property
        public enum Months : int
        { JANUARY, FEBRUARY, MARCH, APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER }
        #endregion


        #region Record Class Constructors
        // Default constructor for the record class
        public Record() { }

        // Constructor for the record class - Sets the property values of the constructed record object from the passed parameters
        public Record(int year, Months month, int day, double magnitude, double latitude, double longitude,
            double depth, string region, long irisID, TimeSpan time, long timeStamp, DateTime date, DateTime dateOnly, string dataSet)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.magnitude = magnitude;
            this.latitude = latitude;
            this.longitude = longitude;
            this.depth = depth;
            this.region = region;
            this.irisID = irisID;
            this.time = time;
            this.timeStamp = timeStamp;
            this.date = date;
            this.dateOnly = dateOnly;
            this.dataSet = dataSet;
        }
        #endregion


        #region Record Class Methods
        // Compare string characters from two strings and return an integer value which represents in which order they should be arranged (-1, 0, 1)
        public static int CompareStrings(string one, string two)
        {
            int compare = 0;
            int range;

            // Check the length of the strings and set the 'range' of indexes to compare (range defines the loop iterations)
            if (one.Length > two.Length)
                range = two.Length;
            else if (one.Length < two.Length)
                range = one.Length;
            else
                range = one.Length;

            // For each character within the checkable 'range' compare the characters at the same index
            for (int index = 0; index < range; index++)
            {
                // If string one character (ACSII code of char) at index is less than string two character (ACSII code of char) at index compare = 1
                if (one[index] < two[index])
                { compare = 1; break; }

                // If string one character (ACSII code of char) at index is greater than string two character (ACSII code of char) at index compare = -1
                else if (one[index] > two[index])
                { compare = -1; break; }

                // If string one character (ACSII code of char) at index is equalto string two character (ACSII code of char) at index
                else if (one[index] == two[index])
                {
                    // If the length of the string one is greater than the length of string two compare = -1
                    if (one.Length > two.Length)
                        compare = -1;

                    // If the length of the string one is less than the length of string two compare = 1
                    else if (one.Length < two.Length)
                        compare = 1;

                    // Else if the string lengths are equal compare = 0
                    else
                        compare = 0;
                }
            }
            return compare;
        }


        // Uses the system.reflection assembly to return the value of the property of the record defined by the string 'prop' (holds the variable name)
        public object GetPropertyValue(string prop, Record record)
        {
            // Defines the type of object
            Type type = typeof(Record);

            // Searches for the public property of the object type with the specified property name.
            PropertyInfo pi = type.GetProperty(prop);

            // Returns the property value of a specified object.
            return pi.GetValue(record);
        }


        // Returns the record details, this returns a string with the property names and values of a record 
        public string RecordDetails()
        {
            return string.Format("Region: {0}\nDay: {1}\nMonth: {2}\nYear: {3}\nTime: {4}\nLatitude: {5}\nLongitude: {6}\nMagnitude: {7}\nDepth: {8}(kM)\nIRISID: {9}\nTimeStamp: {10}\nDate: ({11})\nDate Only: ({12}) \nDataSet: {13}\n", region, day, month, year, time, latitude, longitude, magnitude, depth, irisID, timeStamp, date, dateOnly.Date.ToShortDateString(), dataSet);
        }


        // Returns the record details, this returns a string with the indivdual property name and value of a record 
        public string PropertyDetails(string prop)
        {
            // String to hold the property details
            string propertyDetails = "";

            #region Switch For Property Details
            switch (prop)
            {
                // Assigns the property details string with the relevant property data (inculdes the source of data e.g. the dataset)
                case "Year":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + year;
                    break;
                case "Month":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + month;
                    break;
                case "Day":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + day;
                    break;
                case "Magnitude":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + magnitude;
                    break;
                case "Latitude":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + latitude;
                    break;
                case "Longitude":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + longitude;
                    break;
                case "Depth":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + depth;
                    break;
                case "Region":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + region;
                    break;
                case "IRISID":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + irisID;
                    break;
                case "Time":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + time;
                    break;
                case "TimeStamp":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + timeStamp;
                    break;
                case "Date":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + date;
                    break;
                case "DateOnly":
                    propertyDetails = "DataSet: " + dataSet + " - " + prop + ": " + dateOnly.Date.ToShortDateString();
                    break;
                default:
                    break;
            }
            #endregion

            // Returns the property deatils as a string
            return propertyDetails;
        }
        #endregion
    }
}
