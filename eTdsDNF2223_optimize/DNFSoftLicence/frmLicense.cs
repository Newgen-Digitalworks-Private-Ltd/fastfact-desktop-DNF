//using eTDSWizard.WinForm;
using SoftLicensingLib;
using SoftLicensingService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using eTdsDNF1516;
using System.Collections; //jk
using System.Xml;//jkll
using System.Configuration;




namespace DNFSoftLicence
{
    public partial class frmLicense : Form
    {
       // private int renewalDays = 15;





        private string strProductCode = Convert.ToString(DNFSoftLicence.Properties.Settings.Default["ProductCode"]);
        private int renewalDays = Convert.ToInt32(DNFSoftLicence.Properties.Settings.Default["RenewalPeriod"]);
    
        private string strRegistrationKey;
        string strMessageboxHeading = "DNF Soft Licencing";
        string strCustomerRegistrationLink = "http://hyd-hubd-s01:8080/slcustomer/register/index";
        string strLICDetailXMLFile = Application.StartupPath + "\\SLDetails.xml";

        public frmLicense()
        {
            InitializeComponent();
            bool isExpired = false;

          
            
            if (File.Exists(Application.StartupPath + "\\DNFSoftLicence.exe.config")==true )
            {
                string configPath = Application.StartupPath + "\\DNFSoftLicence.exe.config";
                getSettings(configPath);

            }
            else
            {
                MessageBox.Show("'DNFSoftLicence.exe.config' not found", "DNF Soft Licencing", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            //MessageBox.Show("Product code"+ strProductCode,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //MessageBox.Show("renewal period"+ renewalDays,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
          

            // Generate Registration Key
            AESEncryption aes = new AESEncryption();
            aes.AesIV = @"~X#M^)GZ_JB$Q(&B";
            aes.AesKey = @"`B*S<D>G+{I}B[T]";

            string filePath = Path.GetFullPath("activation.dll");
            if (ValidateRenewalTime(renewalDays, isExpired, aes, filePath))
            {
                AddLastUsedDate(filePath, aes);
               // Program.blnIsDemoVersion = false;  jk
                CommonModule.blnIsDemoVersion = false;
                CommonModule.blnDemoVersion = false;
                CommonModule.blnIsSoftLicenceActivated = true;
                this.Shown += new EventHandler(MyForm_CloseOnStart);                                            
            }
            else
            {
              
                txtRegKeyOnline.Text = Activation.GetRegistrationKey(strProductCode);
                txtRegKeyOffline.Text = txtRegKeyOnline.Text;
                txtActivationUrl.Text = Convert.ToString(DNFSoftLicence.Properties.Settings.Default["ActivationUrl"]);

                //Auto ReActivate
                if (File.Exists (filePath)==true )
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                    {
                        if (File.Exists(strLICDetailXMLFile) == true)
                        {
                            using (DataSet dsLIC = new DataSet())
                            {
                                dsLIC.ReadXml(strLICDetailXMLFile);
                                if (dsLIC.Tables[0].Rows.Count > 0)
                                {
                                    txtProductKey.Text = dsLIC.Tables[0].Rows[0]["ProductKey"].ToString();
                                    bool blnReActivated = AutoReActivate();
                                    if (blnReActivated)
                                    {
                                      //  Program.blnIsDemoVersion = false;  jk
                                        CommonModule.blnIsDemoVersion = false;
                                        CommonModule.blnDemoVersion = false;
                                        CommonModule.blnIsSoftLicenceActivated = true;
                                        this.Shown += new EventHandler(MyForm_CloseOnStart);
                                    }
                                }
                            }
                        }
                    }
                }

               
                   
                //if (DialogResult.No == MessageBox.Show("Your product is not activated.Do you want to activate?", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //{
                //    Program.blnIsDemoVersion = true;
                //    this.Shown += new EventHandler(MyForm_CloseOnStart);
                //}
            }
        }

     

        public frmLicense (string strProductKey)
        {
            strRegistrationKey = Activation.GetRegistrationKey(strProductKey);
        }
       
        private void AddLastUsedDate(string filePath, AESEncryption aes)
        {
            string decryptedActivationkey = string.Empty;
            DateTime currentDate = DateTime.Now;
            DateTime activatedOn = new DateTime();
            DateTime activatedTill = new DateTime();

            FileInfo activationFile = new FileInfo(filePath);
            if (activationFile.Exists)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string aKey = sr.ReadToEnd();

                    decryptedActivationkey = aes.Decrypt(aKey);
                    activatedOn = Convert.ToDateTime(decryptedActivationkey.Split('+')[1]);
                    activatedTill = Convert.ToDateTime(decryptedActivationkey.Split('+')[2]);
                    decryptedActivationkey = decryptedActivationkey.Replace(activatedOn.ToString(), currentDate.ToString());
                }
                try
                {
                    Activation.StoreActivationKey(decryptedActivationkey, aes, filePath, currentDate, activatedTill);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please try again", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MyForm_CloseOnStart(object sender, EventArgs e)
        {
            this.Hide();
            //Jitendra

            frmGeneralInfo GenObj = new frmGeneralInfo();
            GenObj.Show();
            //Jitendra
        }

        private bool ValidateRenewalTime(int renewalDays, bool isExpired, AESEncryption aes, string filePath)
        {
            string decryptedActivationkey = "";
            FileInfo activationFile = new FileInfo(filePath);
            if (activationFile.Exists)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string aKey = sr.ReadToEnd();
                    try
                    {
                        decryptedActivationkey = aes.Decrypt(aKey);

                        DateTime activatedOn = Convert.ToDateTime(decryptedActivationkey.Split('+')[1]);
                        DateTime activatedTill = Convert.ToDateTime(decryptedActivationkey.Split('+')[2]);
                        DateTime currentDate = DateTime.Now;

                        if (activatedOn <= currentDate && currentDate <= activatedTill)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (CryptographicException)
                    {
                        //throw new Exception("Activation file manipulated");
                        MessageBox.Show("Activation file manipulated", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return isExpired;
        }

        private void ProductActivation(bool isInsert)
        {
            string filePath = Path.GetFullPath("activation.dll");

            // AES for encryption and decryption
            AESEncryption aes = new AESEncryption();
            aes.AesIV = @"~X#M^)GZ_JB$Q(&B";
            aes.AesKey = @"`B*S<D>G+{I}B[T]";
            DateTime activatedOn = DateTime.Now.Date;
            DateTime activatedTill = activatedOn.AddDays(renewalDays);

            GenerateActivionfile(aes, filePath, activatedOn, activatedTill);


            //Store License Details Locally
            strRegistrationKey = txtRegKeyOnline.Text; 
            StoreLicenseDetails(strLICDetailXMLFile,txtProductKey.Text);

            if (isInsert)
                MessageBox.Show("Successfully Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Successfully Re-Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Jitendra

            CommonModule.blnIsDemoVersion = false;
            CommonModule.blnDemoVersion = false;
            CommonModule.blnIsSoftLicenceActivated = true;
            frmGeneralInfo GenObj = new frmGeneralInfo();
            GenObj.Show();
            

            //Jitendra
            this.Hide();
        }

        private void GenerateActivionfile(AESEncryption aes, string filePath, DateTime activatedOn, DateTime activatedTill, string ActivationKey = null)
        {
            if (ActivationKey == null)
            {
                ActivationKey = Activation.GetActivationKey(strProductCode, txtProductKey.Text, txtRegKeyOffline.Text);
            }
            if (ActivationKey != null)
            {
                ActivationKey = ActivationKey + "+" + activatedOn + "+" + activatedTill;
                Activation.StoreActivationKey(ActivationKey, aes, filePath, activatedOn, activatedTill);
            }
        }

        private void frmLicense_Load(object sender, EventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                optOnline.Checked = true; 
            }
            else
            {
                optOffline.Checked = true; 
            }
        }

        private void frmLicense_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (txtProductKey.Text =="" )
            {
                MessageBox.Show("Please enter valid Product key", strMessageboxHeading,MessageBoxButtons.OK ,MessageBoxIcon.Error   );
                txtProductKey.Focus(); 
                return;
            }
            btnValidate.Enabled = false;
            this.Cursor = Cursors.WaitCursor;  
            // Checking the product key status
            string strProductKeyStatus = Activation.GetProductKeyStatus(strProductCode, txtProductKey.Text, txtRegKeyOnline.Text);

            switch (strProductKeyStatus)
            {
                case "NeedRegistration":
                    MessageBox.Show("To proceed with activation product need to be registered.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(strCustomerRegistrationLink);
                    break;
                case "LicensesCompleted":
                    MessageBox.Show("Number of licenses for the product key exceeded.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "Undefined":
                    MessageBox.Show("Invalid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductKey.Focus(); 
                    break;
                case "ProductFoundInsert":
                    ProductActivation(true);
                    break;
                case "ProductFoundUpdate":
                    ProductActivation(false);
                    break;
                default:
                    MessageBox.Show("Some error has occurred", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            btnValidate.Enabled = true ;
            this.Cursor = Cursors.Default;  
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (txtProductKeyOffline.Text == "")
            {
                MessageBox.Show("Please enter valid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductKeyOffline.Focus();
                return;
            }
            else if (txtActivationKey.Text == "")
            {
                MessageBox.Show("Please enter valid Activation Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActivationKey.Focus();
                return;
            }
            else if (txtCustCodeOffline.Text == "")
            {
                MessageBox.Show("Please enter valid Customer Code", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustCodeOffline.Focus();
                return;
            }

            btnActivate.Enabled = false;
            this.Cursor = Cursors.WaitCursor;  


            string strProductKey = txtProductKeyOffline.Text;
            string browsedActivationKey = txtActivationKey.Text;
            string originalActivationKey = KeyGenerator.GenerateActivationKey(txtRegKeyOffline.Text  , strProductKey, DateTime.Now.Date, renewalDays);

            if (originalActivationKey.Trim() == browsedActivationKey.Trim())
            {
                string filePath = Path.GetFullPath("activation.dll");

                // AES for encryption and decryption
                AESEncryption aes = new AESEncryption();
                aes.AesIV = @"~X#M^)GZ_JB$Q(&B";
                aes.AesKey = @"`B*S<D>G+{I}B[T]";
                DateTime activatedOn = DateTime.Now.Date;


                // Successful activation
                Activation.StoreActivationKey(browsedActivationKey, aes, filePath, activatedOn, activatedOn.AddDays(renewalDays));

                //Store License Details Locally
                strRegistrationKey = txtRegKeyOffline.Text; 
                StoreLicenseDetails(strLICDetailXMLFile, txtProductKeyOffline.Text, txtCustCodeOffline.Text );

                MessageBox.Show("Successfully Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Jitendra
              
               CommonModule.blnIsDemoVersion = false;
               CommonModule.blnDemoVersion = false;
               CommonModule.blnIsSoftLicenceActivated = true;
                

                //Jitendra
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnValidate.Enabled = true;
            this.Cursor = Cursors.Default; 
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            string activationkey = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time

            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding()))
                {
                    activationkey = reader.ReadToEnd();
                }
            }

            txtActivationKey.Text = activationkey;

        }

        private void optOnline_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControl();
        }

        private void optOffline_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControl();
        }

        private void EnableDisableControl()
        {
            if (optOnline.Checked == true)
            {
                grpOnline.Visible  = true;
                grpOnline.Location = new System.Drawing.Point(529, 170);
                grpOnlineSteps.Location = new System.Drawing.Point(23, 170);
                grpOffline.Visible = false;
                grpOnlineSteps.Visible = true;
                grpOfflineSteps.Visible = false;
            }
            else
            {
                grpOnline.Visible = false;
                grpOffline.Visible = true;
                grpOffline.Location = new System.Drawing.Point(529, 170);
                grpOfflineSteps.Location = new System.Drawing.Point(23, 170);
                grpOnlineSteps.Visible = false;
                grpOfflineSteps.Visible = true;
            }
        }

        private void lblCustomerReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(strCustomerRegistrationLink);
        }

        public bool StoreLicenseDetails(string strFileName, string strProductKey, string strCustomerCode = null)
        {
          bool blnSuccess=false ;
            Dictionary <string,string > lstRetrunData = new Dictionary<string ,string >();
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                 lstRetrunData = Activation.GetCustomerProductDetails(strProductCode, strProductKey);
                 if (lstRetrunData == null)
                 {
                      blnSuccess = false;
                     return blnSuccess;
                 }
            }
            DataSet dsLicense = new DataSet();
            DataTable dtTemp = new DataTable("License");
            dtTemp.Columns.Add("CustomerCode");
            dtTemp.Columns.Add("DealerCode");
            dtTemp.Columns.Add("EmailId");
            dtTemp.Columns.Add("RegKey");
            dtTemp.Columns.Add("ProductKey");
            dtTemp.Columns.Add("ProductCode");
            dtTemp.Columns.Add("SerialNo");


            DataRow drRow = dtTemp.NewRow();
            if (lstRetrunData.Count > 0)
            {
                drRow["CustomerCode"] = lstRetrunData["CustomerCode"];
                drRow["DealerCode"] = lstRetrunData["DelearCode"];
                drRow["EmailId"] = lstRetrunData["UserEmail"];
                drRow["SerialNo"] = lstRetrunData["SrNo"];
            }
            else
            {
                if (strCustomerCode==null )
                  drRow["CustomerCode"] = "";
                else
                    drRow["CustomerCode"] = strCustomerCode ;

                drRow["DealerCode"] = "";
                drRow["EmailId"] = "";               
                drRow["SerialNo"] = "";
            }

            drRow["RegKey"] = strRegistrationKey ;
            drRow["ProductKey"] = strProductKey;
            drRow["ProductCode"] = strProductCode ;

            dtTemp.Rows.Add(drRow);
            dsLicense.Tables.Add(dtTemp);
            dsLicense.AcceptChanges();

            dsLicense.WriteXml(strFileName);
            blnSuccess = true;

            return blnSuccess;
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            //Jitendra

            CommonModule.blnIsDemoVersion = true;
            CommonModule.blnDemoVersion = true;
            CommonModule.blnIsSoftLicenceActivated = false;
            frmGeneralInfo GenObj = new frmGeneralInfo();
            GenObj.Show();
            //Jitendra
            this.Hide();
        }

        private bool AutoReActivate()
        {
            bool blnIsSuccess=false ;
            string strProductKeyStatus = Activation.GetProductKeyStatus(strProductCode, txtProductKey.Text, txtRegKeyOnline.Text);

            switch (strProductKeyStatus)
            {
                case "NeedRegistration":
                    MessageBox.Show("To proceed with activation product need to be registered.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(strCustomerRegistrationLink);
                    break;
                case "LicensesCompleted":
                    MessageBox.Show("Number of licenses for the product key exceeded.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "Undefined":
                    MessageBox.Show("Invalid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductKey.Focus();
                    break;
                case "ProductFoundInsert":
                    blnIsSuccess = true;
                    AutoProductActivation(true);
                    break;
                case "ProductFoundUpdate":
                    blnIsSuccess = true;
                    AutoProductActivation(false);
                    break;
                default:
                    MessageBox.Show("Some error has occurred", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return blnIsSuccess;
        }

        private void AutoProductActivation(bool isInsert)
        {
            string filePath = Path.GetFullPath("activation.dll");

            // AES for encryption and decryption
            AESEncryption aes = new AESEncryption();
            aes.AesIV = @"~X#M^)GZ_JB$Q(&B";
            aes.AesKey = @"`B*S<D>G+{I}B[T]";
            DateTime activatedOn = DateTime.Now.Date;
            DateTime activatedTill = activatedOn.AddDays(renewalDays);

            GenerateActivionfile(aes, filePath, activatedOn, activatedTill);


            //Store License Details Locally
            strRegistrationKey = txtRegKeyOnline.Text;
            StoreLicenseDetails(strLICDetailXMLFile, txtProductKey.Text);
           

        }

        //jitendra



       // private Hashtable getSettings(string path)
            private void  getSettings(string path)
        {   
            string productName;
            string renewalPeriod;
           // Hashtable returnProduct = new Hashtable();

            if (File.Exists(path))
                
            {
                StreamReader reader = new StreamReader
                (
                    new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read)
                );
                XmlDocument doc = new XmlDocument();
                string xmlIn = reader.ReadToEnd();
                reader.Close();
                doc.LoadXml(xmlIn);


              // XmlNodeList productList = doc.GetElementsByTagName("DNFSoftLicence.Properties.Settings");

                XmlNodeList productList = doc.GetElementsByTagName("applicationSettings");
                
                   foreach (XmlNode node in productList )
                   {
                       XmlElement productElement =(XmlElement)node ;

                       productName = productElement.GetElementsByTagName("setting")[0].InnerText;
                       renewalPeriod = productElement.GetElementsByTagName("setting")[1].InnerText;

                        strProductCode=productName;
                        renewalDays=  int.Parse (renewalPeriod);           

                   }


               }
           // return (returnProduct);
        }

        //jitendra
       


    }
}
