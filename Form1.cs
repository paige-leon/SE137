using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Use these namespaces to include DB capabilities for generic components and SQL Server
using System.Data.SqlClient;
//Added to use Ebook & Validation items
using Week_6_Sample1_DataValidation;    

namespace Week7_Sample3_WindowsVersion
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Defaut constructor (Add)
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// NEW - Constructor that Receives an EBook ID....this means we need to look up the data and populate fields (View/Edit/Del)
        /// </summary>
        /// <param name="intEBook_ID"></param>
        public Form1(int intEBook_ID)
        {
            InitializeComponent();  //Creates and init's all form objects

            //Gather info about this one person and store it in a datareader
            EBook temp = new EBook();
            SqlDataReader dr = temp.FindOneEBook(intEBook_ID);

            //Use that info to fill out the form
            //Loop thru the records stored in the reader 1 record at a time
            // Note that since this is based on one person's ID, then we
            //  should only have one record
            while (dr.Read())
            {
                //Take the Name(s) from the datareader and copy them
                // into the appropriate text fields
                txtTitle.Text = dr["Title"].ToString();
                txtAuthorFirst.Text = dr["AuthorFirst"].ToString();
                txtAuthorLast.Text = dr["AuthorLast"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtPages.Text = dr["Pages"].ToString();
                txtBookmarkPage.Text = dr["BookmarkPage"].ToString();
                lblEBook_ID.Text = dr["EBook_ID"].ToString();

                dtpDatePublished.Value = DateTime.Parse(dr["DatePublished"].ToString());
                dtpDateRentalExpires.Value = DateTime.Parse(dr["DateRentalExpires"].ToString());

                //We added this one to store the ID in a new label
                lblEBook_ID.Text = dr["EBook_ID"].ToString();
            }


        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            EBook temp = new EBook();

            //Getting the strings from the form and setting them in object
            temp.Title = txtTitle.Text;
            temp.AuthorFirst = txtAuthorFirst.Text;
            temp.AuthorLast = txtAuthorLast.Text;
            temp.Email = txtEmail.Text;

            //Getting te dates from the datetime pickers
            temp.DatePublished = dtpDatePublished.Value;
            temp.DateRentalExpires = dtpDateRentalExpires.Value;

            //**************************************************************************
            //get the string from page # textboxes,convert to ints, set their values
            //**************************************************************************
            int intTempPages;
            bool blnResult = Int32.TryParse(Console.ReadLine(), out intTempPages);

            if (blnResult == false)
            {
                lblFeedback.Text += "Sorry incorrect page #.  Please try again. (Ex: 214) ";
            }
            else
            {
                temp.Pages = intTempPages;
            }
            //**************************************************************************


            //**************************************************************************
            //get the string from Bookmark page # textboxes,convert to ints, set their values
            //**************************************************************************
            int intBMPage;
            blnResult = Int32.TryParse(Console.ReadLine(), out intBMPage);

            if (blnResult == false)
            {
                lblFeedback.Text += "Sorry incorrect Bookmark page #.  Please try again. (Ex: 214) ";
            }
            else
            {
                temp.BookmarkPage = intBMPage;
            }
            //**************************************************************************


            if (!temp.Feedback.Contains("ERROR:"))
            {
                lblFeedback.Text = temp.AddARecord();   //if no errors weh setting values, then perform the insertion into db
            }
            else
            {
                lblFeedback.Text = temp.Feedback;       //else...dispay the error msg
            }

        }
    }
}
