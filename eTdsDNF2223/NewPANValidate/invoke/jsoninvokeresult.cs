using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.invoke
{
    public class jsoninvokeresult
    {
        public string allOk { get; set; }
        public dataresult data = null;
    }
    public class dataresult
    {
        public string entityNumber { get; set; }
        public string entityFirstName { get; set; }
        public string entityAddrLine1Txt { get; set; }
        public string entityAddrLine2Txt { get; set; }
        public string entityPinCd { get; set; }
        public string entityLocalityCd { get; set; }
        public string entityLocalityDesc { get; set; }
        public string entityStateCd { get; set; }
        public string entityStateDesc { get; set; }
        public string allentityCountryCdOk { get; set; }
        public string entityCountryName { get; set; }
        public string entityDistrictCd { get; set; }
        public string entityDistrictDesc { get; set; }
        public string entityPostOfficeCd { get; set; }
        public string entityPostofficeDesc { get; set; }
        public string entityTaxPayerCatgCd { get; set; }
        public string entityTaxPayerCatgDesc { get; set; }
        public string entityPrimaryEmail { get; set; }
        public string entityPrimaryMobile { get; set; }
        public string pcFirstName { get; set; }
        public string pcLastName { get; set; }
        public string pcDesig { get; set; }
        public string citId { get; set; }
        public string pcPan { get; set; }
        public string formVersion { get; set; }
        public string schemaVersion { get; set; }
        public string userId { get; set; }
        public string userFirstName { get; set; }
        public string userRoleCd { get; set; }
        public string userPan { get; set; }
        public string userEmail { get; set; }
        public string userMobile { get; set; }
        public string fvuVersion { get; set; }
    }
}
