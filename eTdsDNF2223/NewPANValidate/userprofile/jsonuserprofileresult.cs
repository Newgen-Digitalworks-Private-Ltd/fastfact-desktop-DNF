using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.userprofile
{
    public class jsonuserprofileresult
    {
        public headerresult header = null;
        public List<object> messages { get; set; }
        public List<object> errors { get; set; }
        public string userId { get; set; }
		public string orgName { get; set; }
		public string priMobileNum { get; set; }
		public string priMobBelongsTo { get; set; }
		public string priEmailRelationId { get; set; }
		public string priEmailId { get; set; }
		public string secMobRelationId { get; set; }
		public string addrLine1Txt { get; set; }
		public string addrLine2Txt { get; set; }
		public string addrLine3Txt { get; set; }
		public string addrLine4Txt { get; set; }
		public string addrLine5Txt { get; set; }
		public string pinCd { get; set; }
		public string stateCd { get; set; }
		public string countryCd { get; set; }
		public string createdTmstmp { get; set; }
		public string lastUpdatedTmstmp { get; set; }
		public string createdBy { get; set; }
		public string lastUpdatedBy { get; set; }
		public string incorpDt { get; set; }
		public string pan { get; set; }
		public string panStatus { get; set; }
		public string tan { get; set; }
		public string roleCd { get; set; }
		public string status { get; set; }
		public string lastLoginTmstmp { get; set; }
		public string dscFlag { get; set; }
		public string faxNo { get; set; }
		public string faxStdCd { get; set; }
		public string stdCd { get; set; }
		public string landlineNo { get; set; }
		public string isMigrated { get; set; }
		public string oldTranId { get; set; }
		public string transactionNo { get; set; }
		public string securedLogin { get; set; }
		public string createdByUser { get; set; }
		public string updatedByUser { get; set; }
		public string userType { get; set; }
		public string contactDesig { get; set; }
		public string contactDob { get; set; }
		public string contactFirstName { get; set; }
		public string contactLastName { get; set; }
		public string contanctPanNo { get; set; }
		public string contactVerificationStatus { get; set; }
		public string tanAllotDt { get; set; }
		public string roleDesc { get; set; }
		public string lastLogoutTmstmp { get; set; }
		public string panOrdName { get; set; }
		public string panAvailabityFlag { get; set; }
		public string logoutCapturedFlg { get; set; }
    }
	public class headerresult
    {
        public string formName { get; set; }
    }

}
