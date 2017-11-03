using System;
using System.Text;
// The System.IO namespace allow reading from files provide basic file and directory support.
using System.IO;
using static DataBase.Record;

namespace DataBase
{
    // DataReadWrite class - Used to read, write and convert data from the text files and constructs Records to hold the data
    public static class DataReadWrite
    {
        #region DataReadWrite Class Properties
        // Property for the number of 'rows' in the two dimensional string array
        private static int dataRows { get; set; }

        // Property for the number of 'columns' in the two dimensional string array
        private static int dataColumns { get; set; }
        #endregion


        #region DataReadWrite Class Static Methods
        // Assigns the value of the dataRows
        public static void SetDataRows(int rows)
        { dataRows = rows; }


        // Assigns the value of the dataColumns
        public static void SetDataColumns(int columns)
        { dataColumns = columns; }


        // Loops through and assigns the filepath for each text file to be read - in both datasets - and passes this filepath name to a method to read all the lines of the text files whcih are added to a
        // two dimensional string array. This array is used to hold all the data untill all files are read into it. The string array is then passed to a method to convert all of the data to the correct data
        // type and assign the data to the relevant properties of the records in the recordSet Record array. The complete Record array is returned.
        public static Record[] ReadData(int dataSet, Record[] recordSet)
        {
            // String to hold the filepath
            string filePath = "";

            // Two dimensional string array to temporarily hold the data from the files
            string[,] dataArray = new string[dataRows, dataColumns];

            #region For Loop/Switch For Dataset And Array Column
            // Loop through each 'column' of the 2D string array
            for (int column = 0; column < dataColumns; column++)
            {
                switch (column)
                {
                    // Assign the filepath for each text file (and in the specififed dataset)
                    case 0:
                        filePath = "Year_" + dataSet + ".txt";
                        break;
                    case 1:
                        filePath = "Month_" + dataSet + ".txt";
                        break;
                    case 2:
                        filePath = "Day_" + dataSet + ".txt";
                        break;
                    case 3:
                        filePath = "Magnitude_" + dataSet + ".txt";
                        break;
                    case 4:
                        filePath = "Latitude_" + dataSet + ".txt";
                        break;
                    case 5:
                        filePath = "Longitude_" + dataSet + ".txt";
                        break;
                    case 6:
                        filePath = "Depth_" + dataSet + ".txt";
                        break;
                    case 7:
                        filePath = "Region_" + dataSet + ".txt";
                        break;
                    case 8:
                        filePath = "IRIS_ID_" + dataSet + ".txt";
                        break;
                    case 9:
                        filePath = "Time_" + dataSet + ".txt";
                        break;
                    case 10:
                        filePath = "Timestamp_" + dataSet + ".txt";
                        break;
                    default:
                        break;
                }
                // Calls the AddData method and passes the index of the column to add data to, the filepath as a string and the 2D string array
                AddData(filePath, column, dataArray);
            }
            #endregion
            // Calls the WriteData method which creates the rocords and converts and passes the relevant data to their properties
            WriteData(recordSet, dataArray, dataSet);

            // Returns the populated Record array
            return recordSet;
        }


        // Using the system.IO namespace the addData method reads and writes all of the text for each text file (defined by the pathfile string pass to it) to a two dimensional array
        private static void AddData(string filePath, int column, string[,] dataArray)
        {
            int count = 0;

            // Read all the text from the file
            foreach (var item in File.ReadLines(Path.Combine(Environment.CurrentDirectory, filePath), Encoding.UTF8))
            {
                // Assign the string array at the specified index with the text which has been read in
                dataArray[count, column] = item;
                count++;
            }
        }


        // Method that writes all the data from each row of the two dimensional string array to the properties of a constructed Record object
        private static void WriteData(Record[] recordSet, string[,] dataArray, int dataSet)
        {
            #region Convert And Write Data To Record Array
            for (int index = 0; index < dataRows; index++)
            {
                // Creates a new rocrd object
                Record record = new Record();

                // Converts and assigns all the data from each 'column' and 'row' to the specific property of the constructed record
                record.Year = Convert.ToInt32(dataArray[index, 0]);
                record.Month = (Months)Enum.Parse(typeof(Months), dataArray[index, 1].ToUpper().TrimEnd());
                record.Day = Convert.ToInt32(dataArray[index, 2]);
                record.Magnitude = Convert.ToDouble(dataArray[index, 3]);
                record.Latitude = Convert.ToDouble(dataArray[index, 4]);
                record.Longitude = Convert.ToDouble(dataArray[index, 5]);
                record.Depth = Convert.ToDouble(dataArray[index, 6]);
                record.Region = dataArray[index, 7];
                record.IRISID = Convert.ToInt32(dataArray[index, 8]);
                record.Time = ConvertToTimeSpan(dataArray[index, 9]);
                record.TimeStamp = Convert.ToInt64(dataArray[index, 10]);
                record.Date = ConvertToDateTime(dataArray[index, 2], dataArray[index, 1], dataArray[index, 0], dataArray[index, 9]);
                record.DateOnly = ConvertToDate(dataArray[index, 2], dataArray[index, 1], dataArray[index, 0]);
                record.DataSet = dataSet.ToString();

                // Populates the Record array
                recordSet[index] = record;
            }
            #endregion
        }


        // Method which is passed a string and converts it to a timespan (used for converting to the time.txt files data to the data type of timespan)
        // *****MSDN recommended the use of data type timespan when using times*****
        public static TimeSpan ConvertToTimeSpan(string timeString)
        {
            // Char for the dividing the timeString
            char divider = ':';

            // String array to hold each element of the timeString when divide by the char divider
            string[] timeSplit = timeString.Split(divider);

            // Convert the string array to a timeSpan (using the indexes which have been assigned for hour, minute and seconds)
            TimeSpan time = new TimeSpan(Convert.ToInt32(timeSplit[0]), Convert.ToInt32(timeSplit[1]), Convert.ToInt32(timeSplit[2]));

            // Return the timeSpan
            return time;
        }


        // Method which is passed strings and converts them to a DateTime (used for converting to the day/month/year/time.txt files data to the data type of DateTime)
        public static DateTime ConvertToDateTime(string day, string month, string year, string time)
        {
            // String for the concatenating the passed strings
            string dateString = day + " " + month + "" + year + " " + time;

            // Declares a new dateTime
            DateTime date = new DateTime();

            // Parse the dateString as a dateTime to the date DateTime variable
            date = DateTime.Parse(dateString);

            // Return the date
            return date;
        }


        // Method which is passed strings and converts them to a DateTime (used for converting to the day/month/year.txt files data to the data type of DateTime)
        public static DateTime ConvertToDate(string day, string month, string year)
        {
            // String for the concatenating the passed strings
            string dateString = day + " " + month + "" + year;

            // Declares a new dateTime
            DateTime date = new DateTime();

            // Declares a new dateTime
            DateTime dateOnly = new DateTime();

            // Parse the dateString as a dateTime to the date DateTime variable
            date = DateTime.Parse(dateString);

            // Assign the dateOnly variable with the date.Date
            dateOnly = date.Date;

            // Return the date
            return dateOnly;
        }
        #endregion
    }
}
