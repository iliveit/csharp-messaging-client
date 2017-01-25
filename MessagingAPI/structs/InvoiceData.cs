using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Structs
{
    public class InvoiceData
    {
        [JsonProperty("Account")]
        public string Account { get; set; }

        [JsonProperty("Address")]
        public List<string> Address {get; set; }

        [JsonProperty("AmountDue")]
        public decimal AmountDue {get; set; }

        [JsonProperty("BankAccountNumber")]
        public string BankAccountNumber {get; set; }

        [JsonProperty("BankName")]
		public string BankName {get; set; }

        [JsonProperty("BranchCode")]
        public string BranchCode {get; set; }

        [JsonProperty("MSISDN")]
        public string MSISDN {get; set; }

        [JsonProperty("Name")]
        public string Name {get; set; }

        [JsonProperty("PaymentDue")]
        public string PaymentDue {get; set; }

        [JsonProperty("PaymentType")]
        public string PaymentType {get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode {get; set; }

        [JsonProperty("TotalExclVat")]
        public decimal TotalExclVat {get; set; }

        [JsonProperty("TotalInclVat")]
		public decimal TotalInclVat {get; set; }

        [JsonProperty("TotalVat")]
		public decimal TotalVat {get; set; }

        [JsonProperty("Transactions")]
        public List<DataTransaction> Transactions { get; set; }

        [JsonProperty("VatNo")]
        public string VatNo {get; set; }

        [JsonProperty("Distribution")]
        public string Distribution { get; set; }

        [JsonProperty("AdID")]
        public string AdID { get; set; }

        [JsonProperty("OutboundLink")]
        public string OutboundLink { get; set; }
    }
}
