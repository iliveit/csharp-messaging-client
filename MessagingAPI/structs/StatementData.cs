using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Structs
{
    public class StatementData
    {
        [JsonProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("Address")]
        public List<string> Address {get; set;}

        [JsonProperty("AmountDue")]
        public decimal AmountDue {get; set;}

        [JsonProperty("BankAccountNumber")]
        public string BankAccountNumber {get; set;}

        [JsonProperty("BankName")]
        public string BankName {get; set;}

        [JsonProperty("BranchCode")]
        public string BranchCode {get; set;}

        [JsonProperty("ClosingBalance")]
        public decimal ClosingBalance {get; set;}

        [JsonProperty("CurrentBalance")]
        public decimal CurrentBalance {get; set;}

        [JsonProperty("MSISDN")]
        public string MSISDN {get; set;}

        [JsonProperty("Name")]
        public string Name {get; set;}

        [JsonProperty("OpeningBalance")]
        public decimal OpeningBalance {get; set;}

        [JsonProperty("PaymentDue")]
        public string PaymentDue {get; set;}

        [JsonProperty("PaymentType")]
        public string PaymentType {get; set;}

        [JsonProperty("PostalCode")]
        public string PostalCode {get; set;}

        [JsonProperty("TotalOutstandingBalance")]
        public decimal TotalOutstandingBalance {get; set;}

        [JsonProperty("Transactions")]
        public List<DataTransaction> Transactions {get; set;}

        [JsonProperty("VatNo")]
        public string VatNo {get; set;}

        [JsonProperty("ThirtyDaysOverdue")]
        public decimal ThirtyDaysOverdue {get; set;}

        [JsonProperty("ThirtyDaysOverdueText")]
        public string ThirtyDaysOverdueText {get; set;}

        [JsonProperty("SixtyDaysOverdue")]
        public decimal SixtyDaysOverdue {get; set;}

        [JsonProperty("SixtyDaysOverdueText")]
        public string SixtyDaysOverdueText {get; set;}

        [JsonProperty("NinetyDaysOverdue")]
        public decimal NinetyDaysOverdue {get; set;}

        [JsonProperty("NinetyDaysOverdueText")]
        public string NinetyDaysOverdueText { get; set; }

        [JsonProperty("Distribution")]
        public string Distribution { get; set; }

        [JsonProperty("OutboundLink")]
        public string OutboundLink { get; set; }
    }
}
