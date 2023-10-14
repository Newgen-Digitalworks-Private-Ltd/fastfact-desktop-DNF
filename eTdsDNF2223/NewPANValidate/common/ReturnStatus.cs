using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data ;

namespace NewPANValidate
{
    //[System.Diagnostics.DebuggerStepThrough()]
    public class ReturnStatus
    {
        private bool m_status = false;
        private string m_description = string.Empty;
        private object m_object = null;
        private DataTable m_dt = new DataTable();
        private DataSet m_ds = new DataSet();

        public bool pro_status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string pro_description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public object pro_object
        {
            get { return m_object; }
            set { m_object = value; }
        }

        public DataTable pro_dt
        {
            get { return m_dt; }
            set { m_dt = value; }
        }

        public DataSet pro_ds
        {
            get { return m_ds; }
            set { m_ds = value; }
        }

        public ReturnStatus()
        {
            m_status = false;
            m_description = string.Empty;
            m_object = null;
        }

        public ReturnStatus(bool status, string description, object obj)
        {
            m_status = status;
            m_description = description;
            m_object = obj;
        }

        public ReturnStatus(bool status, object obj)
        {
            m_status = status;
            m_object = obj;
        }

        public ReturnStatus(bool status, string description)
        {
            m_status = status;
            m_description = description;
        }

        public ReturnStatus(bool status, DataTable dt)
        {
            m_status = status;
            m_dt = dt;
        }

        public ReturnStatus(bool status, DataSet ds)
        {
            m_status = status;
            m_ds = ds;
        }

        public string Encrypt(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Decrypt(string password)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(password);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int paging (int size,int pageno)
        {
            try
            {
                int startcount = ((size * pageno) - size) + 1;
                return startcount;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}