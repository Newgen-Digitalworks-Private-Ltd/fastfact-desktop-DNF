using ClosedXML.Excel;
//using eTDSWizard.InitialLoadConfigFile;
//using eTDSWizard.WinForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DNFSoftLicence
{
    public partial class frmTracesUtility : Form
    {
        DataTable destTable = new DataTable();
        //LoadConfigFiles loadConfigFiles = new LoadConfigFiles();
        DataSet dsConfig = new DataSet();
        DataTable dt = new DataTable();
        DataTable csvdt = new DataTable();
        DataSet objDs = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable csvdt1 = new DataTable();
        DataSet objDs1 = new DataSet();
        string chlnfromdt;
        string chlntodt;
        string strProductName = "eTdsWizard";
        string strMessageboxHeading = "eTdsWizard";
        private object dataGridView1;
        DataSet data_Structure = new DataSet();
        ////lblpathfile;
        public frmTracesUtility()
        {
            InitializeComponent();
        }

        private void frmTracesUtility_Load(object sender, EventArgs e)
        {


            txtpwd.UseSystemPasswordChar = true;
            //LoadTracesYear();
            loadtan();
            try
            {
                string fileName = Application.StartupPath + "\\traceslogin.txt";

                if (File.Exists(fileName))
                {
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "";
                        string itlogin = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            itlogin = itlogin + s + ',';
                        }
                        string[] it_login;
                        it_login = itlogin.Split(',');
                        txtuserid.Text = it_login[0];
                        txtpwd.Text = it_login[1];
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private void loadtan()
        {
            //string appfile = Application.StartupPath + "\\traces\\26qq2.xls";
            //string appfile = Application.StartupPath + "\\traces request for 26q q2.xls";
            string appfile = lblpath.Text;

            data_Structure = Import(appfile);
            gettan();
        }

        private void LoadTracesYear()
        {

            //cboQtr.Items.Add("Q1");
            //cboQtr.Items.Add("Q2");
            //cboQtr.Items.Add("Q3");
            //cboQtr.Items.Add("Q4");



            //Load Main config
            //using (DataTable dtMasterConfig = loadConfigFiles.LoadingConfigFiles("MasterConfig.xml"))
            //{
            //    dtMasterConfig.TableName = "MasterConfig";
            //    dsConfig.Tables.Add(dtMasterConfig);
            //}
            //calling function whcih loads ddl year
            IEnumerable<string> strYear = LaodFinYear(strProductName);
            //cboFinYr.DataSource = strYear;
            //cboFinYr.SelectedIndex = -1;


            DataTable dtTan = new DataTable();
            if (File.Exists(Application.StartupPath + "\\TANValuesFile.txt") == true)
            {
                using (FileStream fs = new FileStream(Application.StartupPath + "\\TANValuesFile.txt", FileMode.Open))
                {
                    dtTan.Columns.Add("Tan");
                    using (StreamReader str = new StreamReader(fs))
                    {
                        while (!str.EndOfStream)
                        {
                            dtTan.Rows.Add(str.ReadLine().ToString());
                        }

                        str.Dispose();
                        str.Close();
                    }
                    fs.Dispose();
                    fs.Close();
                }
                //cboFormNo.DataSource = dtTan;
                //cboFormNo.DisplayMember = "Tan";
                //cboFormNo.SelectedIndex = -1;


            }
        }
        public IEnumerable<string> LaodFinYear(string strProductName)
        {
            IEnumerable<string> year = null;
            try
            {
                // To get distint year from dataset
                //year = dsConfig.Tables["MasterConfig"].AsEnumerable()
                //    .Where(s => (s.Field<string>("Active").ToUpper() == "TRUE") && (s.Field<string>("ProductName") == strProductName))
                //       .OrderByDescending(s => s.Field<string>("FinYear")).Select(s => s.Field<string>("FinYear"))
                //        .Distinct().ToList();

                //year = dsConfig.Tables["MasterConfig"].AsEnumerable()
                //  .Where(s => (s.Field<string>("Active").ToUpper() == "TRUE") && (s.Field<string>("ProductName") == strProductName) && (Convert.ToInt32(s.Field<string>("FinYear").Replace("-", "").Trim()) <= Convert.ToInt32(Program.strFinancialYear.Replace("-20", ""))))
                //     .OrderByDescending(s => s.Field<string>("FinYear")).Select(s => s.Field<string>("FinYear"))
                //      .Distinct().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return year;
        }

        public IEnumerable<string> LoadFormType(string selectedYear, string strProductName)
        {
            IEnumerable<string> formType = null;
            if (selectedYear == "")
                return formType;

            try
            {
                //ds = loadConfigFiles.LoadingConfigFiles("MasterConfig.xml");
                formType = dsConfig.Tables["MasterConfig"].AsEnumerable()
                    .Where(s => ((s.Field<string>("Active") == "true") && (s.Field<string>("FinYear") == selectedYear) && (s.Field<string>("ProductName") == strProductName)))
                       .Select(s => s.Field<string>("FormNo"))
                       .Distinct().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return formType;
        }


        private void createpan()
        {

            create_txt();
            Pantraces();

        }

        private void create_pan()
        {
            try
            {
                string fileName = Application.StartupPath + "\\traces\\pan.txt";

                //check if the file already exist?
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                FileStream fs = File.Create(fileName);
                fs.Close();
                using (StreamWriter writer = new StreamWriter(fileName))
                {

                    writer.Write("Pan" + Environment.NewLine);
                    //string col = "PAN of the Deductee         (415)";
                    //for (int i = 0; i < data_Structure.Tables["Deductee$"].Rows.Count; i++)
                    //{
                    //    string s = "";
                    //    if (data_Structure.Tables["Deductee$"].Rows[i][18].ToString() != "")
                    //    {
                    //        s += data_Structure.Tables["Deductee$"].Rows[i][18].ToString();
                    //        writer.Write(s + Environment.NewLine);
                    //    }
                    //}
                    DataTable dtPanFiltered = data_Structure.Tables["Deductee$"].AsEnumerable().CopyToDataTable();

                    var distinctRows = (from DataRow dRow in dtPanFiltered.Rows
                                        select dRow[5]).Distinct().ToList();
                    for (int i = 0; i < distinctRows.Count - 1; i++)
                    {
                        string s = "";
                        s += distinctRows[i].ToString();
                        writer.Write(s + Environment.NewLine);
                    }



                    //MessageBox.Show("Data Saved Successfully", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void create_txt()

        {

            //foreach (DataColumn column in ds.Tables[0].Columns)
            //{
            //    string colname = column.ColumnName;
            //    if (colname == "Retailer" || colname == "Brand") ;
            //    else
            //    {
            //        ds.Tables[0].Columns[colname].ColumnName = "WK" + colname.split('-')[0];
            //    }
            //}

            try
            {
                string fileName = Application.StartupPath + "\\pan.txt";
                //string fileName = lblpath.Text;
                //check if the file already exist?
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                FileStream fs = File.Create(fileName);
                fs.Close();
                using (StreamWriter writer = new StreamWriter(fileName))
                {

                    writer.Write("Pan" + Environment.NewLine);
                    string col = "PAN of the Deductee         (415)";
                    for (int i = 0; i < data_Structure.Tables["Deductee$"].Rows.Count; i++)
                    {
                        string s = "";
                        if (data_Structure.Tables["Deductee$"].Rows[i][5].ToString() != "")
                        {
                            s += data_Structure.Tables["Deductee$"].Rows[i][5].ToString();
                            writer.Write(s + Environment.NewLine);
                        }
                    }

                    MessageBox.Show("Data Saved Successfully", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void createtxt()
        {
            try
            {

                string fileName = Application.StartupPath + "\\pan.txt";
                string sourcefileName = Application.StartupPath + "\\TANValuesFile.txt";

                //check if the file already exist?
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                FileStream fs = File.Create(fileName);
                fs.Close();
                using (StreamWriter writer = new StreamWriter(fileName))
                {

                    using (StreamReader sr = File.OpenText(sourcefileName))
                    {
                        string s = "";
                        writer.Write("Pan" + Environment.NewLine);
                        while ((s = sr.ReadLine()) != null)
                        {
                            //Console.WriteLine(s);
                            writer.Write(s + Environment.NewLine);

                        }
                    }


                    MessageBox.Show("Data Saved Successfully", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void Pantraces()
        {
            try
            {
                string result = null;
                string qtid;
                qtid = " PANVERIFY " + Application.StartupPath + "\\traces\\pan.txt" + " " + txtuserid.Text + " " + txtpwd.Text + " " + lbltan.Text;
                //qtid = " PANVERIFY " + Application.StartupPath + "\\traces\\pan.txt" + " Epage@123 Digital2017 CHEH00378A";
                //string epubCheckPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "epubcheck.jar");
                string epubCheckPath = Application.StartupPath + "\\traces\\traces.jar";
                //string arguments = "java -jar" + " \"" + epubCheckPath + "\"" + " \"" + qtid + "\"";
                //string arguments = "-jar" + " \"" + epubCheckPath + "\"" + " \"" + qtid + "\"";
                string arguments = "-jar" + " " + epubCheckPath + " " + qtid;

                //"javaw -jar C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\traces.jar PANVERIFY C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\pan.txt Epage@123 Digital2017 MUMA61614A"

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

                pProcess.StartInfo.FileName = @"java";


                //strCommandParameters are parameters to pass to program
                pProcess.StartInfo.Arguments = arguments;

                Debug.WriteLine("arguments: " + pProcess.StartInfo.Arguments);

                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                //Set output of program to be written to process output stream
                pProcess.StartInfo.RedirectStandardOutput = true;

                //Start the process
                pProcess.Start();

                //Get program output
                result = pProcess.StandardOutput.ReadToEnd();



                //Wait for process to finish
                pProcess.WaitForExit();

                //return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                throw ex;
            }
        }



        private void lowerratetraces()
        {
            try
            {
                string result = null;
                string qtid;
                qtid = " 197 2021-2022 " + Application.StartupPath + "\\traces\\pan.txt" + " " + txtuserid.Text + " " + txtpwd.Text + " " + lbltan.Text;
                //qtid = " PANVERIFY " + Application.StartupPath + "\\traces\\pan.txt" + " Epage@123 Digital2017 CHEH00378A";
                //string epubCheckPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "epubcheck.jar");
                string epubCheckPath = Application.StartupPath + "\\traces\\traces.jar";
                string arguments = "-jar" + " " + epubCheckPath + " " + qtid;

                //"javaw -jar C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\traces.jar PANVERIFY C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\pan.txt Epage@123 Digital2017 MUMA61614A"

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = @"java";

                //strCommandParameters are parameters to pass to program
                pProcess.StartInfo.Arguments = arguments;

                Debug.WriteLine("arguments: " + pProcess.StartInfo.Arguments);

                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                //Set output of program to be written to process output stream
                pProcess.StartInfo.RedirectStandardOutput = true;

                //Start the process
                pProcess.Start();

                //Get program output
                result = pProcess.StandardOutput.ReadToEnd();



                //Wait for process to finish
                pProcess.WaitForExit();

                //return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                throw ex;
            }
        }
        private void Consotraces()
        {
            try
            {
                string result = null;
                string qtid;
                qtid = " CONSO " + Application.StartupPath + "\\traces\\consoRequest.json" + " " + txtuserid.Text + " " + txtpwd.Text + " " + lbltan.Text;
                //string epubCheckPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "epubcheck.jar");
                string epubCheckPath = Application.StartupPath + "\\traces\\traces.jar";
                string arguments = "-jar" + " " + epubCheckPath + " " + qtid;

                //"javaw -jar C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\traces.jar PANVERIFY C:\NewGen_101122\TdsPac_102822\fastfact-desktop-tdspac-sql\TdsPacSQL\bin\traces\pan.txt Epage@123 Digital2017 MUMA61614A"

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

                pProcess.StartInfo.FileName = @"java";

                //strCommandParameters are parameters to pass to program
                pProcess.StartInfo.Arguments = arguments;

                Debug.WriteLine("arguments: " + pProcess.StartInfo.Arguments);

                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                //Set output of program to be written to process output stream
                pProcess.StartInfo.RedirectStandardOutput = true;

                //Start the process
                pProcess.Start();

                //Get program output
                result = pProcess.StandardOutput.ReadToEnd();

                //Wait for process to finish
                pProcess.WaitForExit();

                //return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                throw ex;
            }
        }



        private void cboFinYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string strProductName = "eTdsWizard";
            //cboFormNo.DataSource = LoadFormType(cboFinYr.Text.ToString(), strProductName);
            //cboFormNo.SelectedIndex = -1;

        }

        private void btnvalidate_Click(object sender, EventArgs e)
        {
            create_pan();
            Pantraces();
            readcsv();
            //validatepan();
            DataGridView1.DataSource = null;
            DataGridView1.DataSource = csvdt;
        }
        private void readcsv()
        {
            //string fileName = @"C:\eTdsWizard" + "\\traces\\report.csv";
            string fileName = Application.StartupPath + "\\traces\\report.csv";
            string contents = File.ReadAllText(fileName);
            File.WriteAllText(fileName, contents.Replace("\"", ""));


            //csvdt = new DataTable;

            //if (File.Exists(Application.StartupPath + @"\traces\report.csv") == true)
            //{
            //    string strPath = System.IO.Path.GetDirectoryName(Application.StartupPath + @"\traces\report.csv");
            //    string strFile = System.IO.Path.GetFileName(Application.StartupPath + @"\traces\report.csv");
            //    strPath = @"C:\eTdsWizard";
            csvdt = getcsvtable(fileName);
            //}

        }
        private void readcsv1()
        {
            //string fileName = @"C:\eTdsWizard" + "\\traces\\report.csv";
            string fileName = Application.StartupPath + "\\traces\\LowerRateCertDetails.csv";
            string contents = File.ReadAllText(fileName);
            File.WriteAllText(fileName, contents.Replace("\"", ""));

            //csvdt1 = new DataTable;


            //if (File.Exists(Application.StartupPath + @"\traces\LowerRateCertDetails.csv") == true)
            //{
            //    string strPath = System.IO.Path.GetDirectoryName(Application.StartupPath + @"\traces\LowerRateCertDetails.csv");
            //    string strFile = System.IO.Path.GetFileName(Application.StartupPath + @"\traces\LowerRateCertDetails.csv");
            //    strPath = @"C:\eTdsWizard";
            csvdt1 = getcsvtable1(fileName);
            //}

        }

        private DataTable getcsvtable(string csvfilepath)
        {
            csvdt.Clear();
            csvdt.Columns.Clear();
            //DataTable dt = new DataTable();
            string csvData;
            using (StreamReader sr = new StreamReader(csvfilepath))
            {
                csvData = sr.ReadToEnd().ToString();
                string[] row = csvData.Split('\n');
                int cntcsvcol = 0;
                for (int i = 0; i < row.Count() - 1; i++)
                {
                    string[] rowData = row[i].Split(',');
                    {
                        if (i == 0)
                        {
                            cntcsvcol = rowData.Count() - 1;
                            for (int j = 0; j < rowData.Count(); j++)
                            {
                                if (j <= cntcsvcol)
                                {
                                    csvdt.Columns.Add(rowData[j].Trim());

                                }
                            }
                        }
                        else
                        {
                            DataRow dr = csvdt.NewRow();
                            for (int k = 0; k < rowData.Count(); k++)
                            {
                                if (k <= cntcsvcol)
                                {
                                    dr[k] = rowData[k].ToString();
                                }
                            }
                            csvdt.Rows.Add(dr);
                        }
                    }
                }

                return csvdt;
            }
        }


        private DataTable getcsvtable1(string csvfilepath)
        {
            //DataTable dt = new DataTable();
            csvdt1.Clear();
            csvdt1.Columns.Clear();
            string csvData;
            using (StreamReader sr = new StreamReader(csvfilepath))
            {
                csvData = sr.ReadToEnd().ToString();
                string[] row = csvData.Split('\n');
                int cntcsvcol = 0;
                for (int i = 0; i < row.Count() - 1; i++)
                {
                    string[] rowData = row[i].Split(',');
                    {
                        if (i == 0)
                        {
                            cntcsvcol = rowData.Count() - 1;
                            for (int j = 0; j < rowData.Count(); j++)
                            {
                                if (j <= cntcsvcol)
                                {
                                    csvdt1.Columns.Add(rowData[j].Trim());
                                }
                            }
                        }
                        else
                        {
                            DataRow dr = csvdt1.NewRow();
                            for (int k = 0; k < rowData.Count(); k++)
                            {
                                if (k <= cntcsvcol)
                                {
                                    dr[k] = rowData[k].ToString();
                                }
                            }
                            csvdt1.Rows.Add(dr);
                        }
                    }
                }

                return csvdt1;
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            readcsv();
            //validatepan();
            DataGridView1.DataSource = null;
            DataGridView1.DataSource = csvdt;
            //create_pan();
            //Pantraces();
            //readcsv();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (txtuserid.Text != "" && txtpwd.Text != "")
            {
                try
                {
                    string fileName = Application.StartupPath + "\\traceslogin.txt";

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    FileStream fs = File.Create(fileName);
                    fs.Close();
                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        writer.Write(txtuserid.Text + Environment.NewLine);
                        writer.Write(txtpwd.Text);
                        MessageBox.Show("Data Saved Successfully", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        private void btnexport_Click(object sender, EventArgs e)
        {
            if (DataGridView1.Rows.Count > 0)
            {
                Panexporttoexcel();
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

        }

        private void btnvalidate1_Click(object sender, EventArgs e)
        {
            create_pan();
            lowerratetraces();
            readcsv1();
            //validate197();
            DataGridView2.DataSource = null;
            DataGridView2.DataSource = csvdt1;
        }

        private void btnrefresh1_Click(object sender, EventArgs e)
        {
            readcsv1();
            //validate197();
            DataGridView2.DataSource = null;
            DataGridView2.DataSource = csvdt1;
        }

        private void btnexport1_Click(object sender, EventArgs e)
        {
            if (DataGridView2.Rows.Count > 0)
            {
                exporttoexcel197();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == false)
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                txtpwd.UseSystemPasswordChar = false;
            }
            else
            {
                txtpwd.UseSystemPasswordChar = true;
            }
        }

        private void btnrequest_Click(object sender, EventArgs e)
        {

            if (cboFinYr.Text == "")
            {
                MessageBox.Show("Select Financial Year", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboQtr.Text == "")
            {
                MessageBox.Show("Select Quater", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboFormNo.Text == "")
            {
                MessageBox.Show("Select Form Number", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lblchallanno.Text == "")
            {
                MessageBox.Show("Select Valid Challan", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lblprnno.Text == "")
            {
                MessageBox.Show("Select Valid PRN Number", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            getchallandetails();
            Consotraces();

        }
        private void crtjson()
        {

            if (cboFinYr.Text != "" && cboFormNo.Text != "" && cboQtr.Text != "")
            {
                string[] fyr;
                string frmfy;
                string tofyr;
                fyr = cboFinYr.Text.Split('-');
                frmfy = fyr[0];
                tofyr = fyr[1];


                if (cboQtr.Text == "Q1")
                {
                    chlnfromdt = frmfy + "-04-01";
                    chlntodt = frmfy + "-06-30";
                }
                if (cboQtr.Text == "Q1")
                {
                    chlnfromdt = frmfy + "-04-01";
                    chlntodt = frmfy + "-06-30";
                }
                if (cboQtr.Text == "Q2")
                {
                    chlnfromdt = frmfy + "-07-01";
                    chlntodt = frmfy + "-09-30";
                }
                if (cboQtr.Text == "Q3")
                {
                    chlnfromdt = frmfy + "-10-01";
                    chlntodt = frmfy + "-12-31";
                }
                if (cboQtr.Text == "Q4")
                {
                    chlnfromdt = tofyr + "-01-01";
                    chlntodt = tofyr + "-03-31";
                }
                // lblprnno.Text = IIf(IsDBNull(drPRNNO.PRNNO) == true, "", drPRNNO.PRNNO);

                DataTable crt;
                crt = data_Structure.Tables["Deductor"];

                string prnno = data_Structure.Tables["Deductor$"].Rows[4].Field<string>(3);
                if (prnno == "")
                {
                    MessageBox.Show("Please update the PRN No:", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lblprnno.Text = prnno;

                getchallan3();
                if (lblchallanno.Text == "")
                {
                    getchallan2();
                }

                if (lblchallanno.Text == "")
                {
                    getchallan1();
                }
            }
        }

        private void getchallandetails()
        {
            data_Structure.Tables["Challan$"].Columns[0].ColumnName = "SNO";
            data_Structure.Tables["Challan$"].AcceptChanges();

            DataTable dtChallandetails = data_Structure.Tables["Challan$"].AsEnumerable()
           .Where(s => (s.Field<double>("SNO") == Convert.ToDouble(lblchallanno.Text)))
                  .CopyToDataTable();


            string fnyr;
            string frmtype;
            string qter;
            string prn;
            string chaln;
            string cdrno;

            string bsrcode = dtChallandetails.Rows[0].Field<string>(9);

            string chlnsrno = dtChallandetails.Rows[0].Field<string>(10);

            DateTime chlndate = dtChallandetails.Rows[0].Field<DateTime>(11);

            double chlnamt = dtChallandetails.Rows[0].Field<double>(7);

            ConsoDetails product = new ConsoDetails();
            product.fy = cboFinYr.Text;
            product.formType = cboFormNo.Text;
            product.quarter = cboQtr.Text;
            product.prn = lblprnno.Text;
            product.bsrCode = bsrcode;
            product.challanSerialNo = chlnsrno;
            product.challanTenderDate = chlndate.ToString("dd-MMM-yyyy");
            product.challanAmount = chlnamt.ToString();
            product.cdRecordNo = "1";
            product.panDetails = GetPanDetails();


            string json = JsonConvert.SerializeObject(product, Formatting.Indented);
            File.WriteAllText((Application.StartupPath + @"\traces\consoRequest.json"), json);

        }

        public List<PanDetails> GetPanDetails()
        {
            //5,13
            DataTable dtPanFillered = new DataTable();

            //dtPanFillered = data_Structure.Tables["Deductee$"].AsEnumerable()
            //.Where(s => (s.Field<double>("SNO") == Convert.ToDouble(lblchallanno.Text)))
            //            .CopyToDataTable();
            dtPanFillered = destTable.AsEnumerable()
            .Where(s => (s.Field<double>("SNO") == Convert.ToDouble(lblchallanno.Text)))
                        .CopyToDataTable();


            dtPanFillered.Columns[13].ColumnName = "taxdeduct";
            dtPanFillered.AcceptChanges();
            dtPanFillered.DefaultView.Sort = "taxdeduct DESC";
            dtPanFillered.AcceptChanges();

            DataView dv = dtPanFillered.DefaultView;
            dv.Sort = "taxdeduct DESC";
            DataTable sortedtable1 = dv.ToTable();

            List<PanDetails> pndtls = new List<PanDetails>();
            for (int i = 0; i <= 2; i++)
            {
                PanDetails pndtl = new PanDetails()
                {
                    //var pan = dtPanFillered.Rows[i][5];
                    pan = sortedtable1.Rows[i].Field<string>(5),
                    amount = Convert.ToString(sortedtable1.Rows[i].Field<double>(13))
                    //var amount = dtPanFillered.Rows[i][13]
                };
                pndtls.Add(pndtl);
            }

            return pndtls;

            //return data_Structure.Tables["Deductee$"];
            //var Bypandtl = from pandtl in data_Structure.Tables["Deductee$"]
            //                     select pandtl.Items.OrderByDescending(i => i.Field<DateTime>("UpdateDateTime")).Take(3);


        }
        private void getchallan3()
        {

            data_Structure.Tables["Deductee$"].Columns[1].ColumnName = "SNO";
            data_Structure.Tables["Deductee$"].AcceptChanges();

            for (int i = 0; i < data_Structure.Tables["Challan$"].Rows.Count; i++)
            {

                var challanno = data_Structure.Tables["Challan$"].Rows[i].Field<double>(0);


                try
                {

                    destTable = data_Structure.Tables["Deductee$"].Clone();

                    foreach (DataRow dr in data_Structure.Tables["Deductee$"].Rows)
                    {
                        if (dr["SNO"] != DBNull.Value)
                        {
                            destTable.ImportRow(dr);
                        }
                    }

                    DataTable dtChallanFillered = destTable.AsEnumerable().Where(s => (s.Field<double>("SNO") == Convert.ToDouble(challanno)))
                     .CopyToDataTable();

                    //DataTable dtChallanFillered = data_Structure.Tables["Deductee$"].AsEnumerable().Where(s => (s.Field<double>("SNO") ==  Convert.ToDouble(challanno)))
                    //   .CopyToDataTable();



                    var distinctRows = (from DataRow dRow in dtChallanFillered.Rows
                                        select dRow[5]).Distinct().ToList();

                    int lenpan = distinctRows.Count;

                    if (lenpan >= 3)
                    {
                        lblchallanno.Text = challanno.ToString();

                        return;
                    }
                }

                catch (Exception ex)
                {
                    //throw ex;
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }





            }

        }
        private void getchallan2()
        {


            data_Structure.Tables["Deductee$"].Columns[1].ColumnName = "SNO";
            data_Structure.Tables["Deductee$"].AcceptChanges();

            for (int i = 0; i < data_Structure.Tables["Challan$"].Rows.Count; i++)
            {

                var challanno = data_Structure.Tables["Challan$"].Rows[i].Field<double>(0);



                try
                {


                    DataTable dtChallanFillered = destTable.AsEnumerable().Where(s => (s.Field<double>("SNO") == Convert.ToDouble(challanno)))
                     .CopyToDataTable();

                    //     DataTable dtChallanFillered = data_Structure.Tables["Deductee$"].AsEnumerable()
                    //.Where(s => (s.Field<double>("SNO") == Convert.ToDouble(challanno)))
                    //            .CopyToDataTable();

                    var distinctRows = (from DataRow dRow in dtChallanFillered.Rows
                                        select dRow[5]).Distinct().ToList();

                    int lenpan = distinctRows.Count;

                    if (lenpan >= 2)
                    {
                        lblchallanno.Text = challanno.ToString();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    //throw ex;
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }

            }
        }
        private void getchallan1()
        {

            data_Structure.Tables["Deductee$"].Columns[1].ColumnName = "SNO";
            data_Structure.Tables["Deductee$"].AcceptChanges();

            for (int i = 0; i < data_Structure.Tables["Challan$"].Rows.Count; i++)
            {

                var challanno = data_Structure.Tables["Challan$"].Rows[i].Field<double>(0);
                try
                {


                    DataTable dtChallanFillered = destTable.AsEnumerable().Where(s => (s.Field<double>("SNO") == Convert.ToDouble(challanno)))
                     .CopyToDataTable();


                    //  DataTable dtChallanFillered = data_Structure.Tables["Deductee$"].AsEnumerable()
                    //.Where(s => (s.Field<double>("SNO") == Convert.ToDouble(challanno)))
                    //            .CopyToDataTable();

                    var distinctRows = (from DataRow dRow in dtChallanFillered.Rows
                                        select dRow[5]).Distinct().ToList();

                    int lenpan = distinctRows.Count;

                    if (lenpan >= 1)
                    {
                        lblchallanno.Text = challanno.ToString();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }

            }


        }

        private void Panexporttoexcel()
        {
            try
            {
                string folderPath = Application.StartupPath;

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(csvdt, "PanData");
                    wb.SaveAs(folderPath + "\\ValidatePan.xlsx");
                }
                Process IEProcess = new Process();
                //System.Diagnostics.Process.Start(Application.StartupPath + @"\DataGridViewExport.xlsx");
                if (File.Exists(Application.StartupPath + "\\ValidatePan.xlsx"))
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\ValidatePan.xlsx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void exporttoexcel197()
        {
            try
            {
                string folderPath = Application.StartupPath;

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(csvdt1, "LowerrateData");
                    wb.SaveAs(folderPath + "\\Validate197.xlsx");
                }
                Process IEProcess = new Process();
                //System.Diagnostics.Process.Start(Application.StartupPath + @"\DataGridViewExport.xlsx");
                if (File.Exists(Application.StartupPath + "\\Validate197.xlsx"))
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Validate197.xlsx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void gettan()
        {

            DataTable tan;
            tan = data_Structure.Tables["Deductor"];

            string tanno = data_Structure.Tables["Deductor$"].Rows[0].Field<string>(1);

            lbltan.Text = tanno;
        }


        public static DataSet Import(string path)
        {


            var dataStructure = new DataSet();
            // Create the connection string and connect to the excel table by OleDbConnection. 
            //Using objConn As New OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={strPath};Extended Properties=""Text;HDR={If(pHasHeader, "Yes", "Yes")};""")
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties=\"Excel 12.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text\"";
            using (var conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Cannot connect to the OLEDB (Excel) driver with the connection string \"{connectionString}\".\n{e}");

                    return null;
                }

                DataTable sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    foreach (DataRow row in sheets.Rows)
                    {
                        var tableName = row["TABLE_NAME"].ToString();
                        string sql = $"SELECT * FROM [{tableName}]";
                        var oleDbDataAdapter = new OleDbDataAdapter(sql, conn);
                        oleDbDataAdapter.Fill(dataStructure, tableName);
                    }
                }

                conn.Close();
            }

            return dataStructure;


        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (TabControl1.SelectedIndex != 0)
            {
                if (txtuserid.Text == "" || lbltan.Text == "" || txtpwd.Text == "")
                {
                    MessageBox.Show("Please fill the Login Credentials", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TabControl1.SelectedIndex = 0;
                }
            }
            if (TabControl1.SelectedIndex == 3)
            {
                crtjson();
            }

        }

    }

}
public class ConsoDetails
{
    public string fy { get; set; }
    public string formType { get; set; }
    public string quarter { get; set; }
    public string prn { get; set; }
    public string bsrCode { get; set; }
    public string challanSerialNo { get; set; }
    public string challanTenderDate { get; set; }
    public string challanAmount { get; set; }
    public string cdRecordNo { get; set; }
    public List<PanDetails> panDetails { get; set; }
}
public class PanDetails
{
    public string pan { get; set; } = "";
    public string amount { get; set; } = "";
}

