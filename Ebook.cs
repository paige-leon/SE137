using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Added this workspace to allow us to use BasicTools and ValidationLibrary
using Week4_Class1;
//Use these namespaces to include DB capabilities for generic components and SQL Server
using System.Data;
using System.Data.SqlClient;


namespace Week_6_Sample1_DataValidation
{
    class EBook: Book   //Welcome to inheritance...Ebook has all that book has, but more!
    {
        private DateTime dateRentalExpires;
        private int bookmarkPage;

        public DateTime DateRentalExpires
        {
            get
            {
                return dateRentalExpires;
            }

            set
            {
                //If the date given IS a future date...
                if (ValidationLibrary.IsAFutureDate(value))
                {
                    dateRentalExpires = value;  //Past Date...store it
                }
                else
                {
                    //Future Date...Store error msg in feedback
                    //**** ONLY WORKS BECAUSE feedback was changed to PROTECTED
                    feedback += "\nERROR: You must enter future dates";
                }
            }
        }



        public int BookmarkPage
        {
            get
            {
                return bookmarkPage;
            }

            set
            {
                //if we have the miimum number of pages needed...
                if (value >= 0 && value <= Pages)
                {
                    bookmarkPage = value;  //store the # of pages
                }
                else
                {
                    //Store an error msg in Feedback
                    feedback += "\nERROR: Sorry you entered an invalid page # for a bookmark.";
                }
            }
        }


        //**************************************************************************************
        //  NEW - Ultimate goal is to add a record, BUT first we need to connect to the DB
        //    So that is how we will start this process.
        //**************************************************************************************
        public string AddARecord()
        {
            //Init string var
            string strResult = "";

            //Make a connection object
            SqlConnection Conn = new SqlConnection();

            //Initialize it's properties
            Conn.ConnectionString = @"Server=sql.neit.edu,4500;Database=Slambert;User Id=slambert;Password=myPassword;";     //Set the Who/What/Where of DB

            //attempt to connect to the server
            try
            {
                Conn.Open();                                        //Open connection to DB - Think of dialing a friend on phone
                strResult = "SUCCESS: Connected to Database";       //Report that we made the connection
                Conn.Close();                                       //Hanging up after phone call
            }
            catch (Exception err)                                   //If we got here, there was a problem connecting to DB
            {
                strResult = "ERROR: " + err.Message;                //Set feedback to state there was an error & error info
            }
            finally
            {

            }



            //Return resulting feedback string
            return strResult;
        }







        //Default Constructor
        public EBook(): base()  //Calls the parents constructor
        {
            BookmarkPage = 0;
            dateRentalExpires = DateTime.Now.AddDays(14);
        }

    }
}
