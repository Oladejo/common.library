using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class TransactionRequestData
    {
        [JsonProperty("request_ref")]
        [JsonPropertyName("request_ref")]
        public string? RequestRef { get; set; }

        [JsonProperty("request_type")]
        [JsonPropertyName("request_type")]
        public string? RequestType { get; set; }

        [JsonProperty("request_mode")]
        [JsonPropertyName("request_mode")]
        public string? RequestMode { get; set; }

        public Auth? Auth { get; set; }

        public TransactionAdvanced? Transaction {get; set; }

        public Tuple<bool, string> ValidateBasic()
        {
            if (string.IsNullOrEmpty(this.Transaction?.Transactionref))
            {
                return new Tuple<bool, string>(false, "Transaction Reference is required");
            }
            if (string.IsNullOrEmpty(this.Transaction?.Payer?.Customerref))
            {
                return new Tuple<bool, string>(false, "Customer Reference is required");
            }
            if (this.Transaction?.AppInfo?.Extras == null)
            {
                return new Tuple<bool, string>(false, "Application Info Extras is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("consumer_key") || string.IsNullOrEmpty(Transaction.AppInfo.Extras["consumer_key"]))
            {
                return new Tuple<bool, string>(false, "Consumer Key is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("consumer_secret") || string.IsNullOrEmpty(Transaction.AppInfo.Extras["consumer_secret"]))
            {
                return new Tuple<bool, string>(false, "Consumer Secret is required");
            }
            return new Tuple<bool, string>(true, "");
        }

        public bool IsTransact()
        {
            return RequestMode?.ToLower() == "transact";
        }

        public bool IsQuery()
        {
            return RequestMode?.ToLower() == "query";
        }

        public bool IsValidate()
        {
            return RequestMode?.ToLower() == "validate";
        }

        public bool IsReverse()
        {
            return RequestMode?.ToLower() == "reverse";
        }

        public bool RequireOTPValidation()
        {
            // Check If otp_override exist in App extras and it's set as false;
            if (Transaction?.AppInfo?.Extras != null &&
                Transaction.AppInfo.Extras.ContainsKey("otp_override"))
            {
                return Transaction.AppInfo.Extras["otp_override"] != "true";
            }
            //Check if otp_override exist in transaction details and it's set to true'
            if (Transaction?.Details != null && Transaction.Details.ContainsKey("otp_override"))
            {
                return Transaction.Details["otp_override"].ToString() != "true";
            }
            return true;
        }

        public Tuple<bool, string> ValidateForLookupNuban()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;

            if (Transaction?.Details == null || !Transaction.Details.ContainsKey("account_number") || string.IsNullOrEmpty(Transaction.Details["account_number"].ToString()))
            {
                return new Tuple<bool, string>(false, "Account number is required");
            }
            return new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForGetBalance()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type?.ToLower() != "bank.account")
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            return new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForGetAccount()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            return string.IsNullOrEmpty(Transaction?.Payer?.Customerref) ? new Tuple<bool, string>(false, "Customer Ref is required") : new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForCollect()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type?.ToLower() != "bank.account")
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            if (Transaction?.AppInfo?.Extras == null || !Transaction.AppInfo.Extras.ContainsKey("bank_code"))
            {
                return new Tuple<bool, string>(false, "Bank code is required in app extras");
            }

            if (!Transaction.AppInfo.Extras.ContainsKey("destination_account"))
            {
                return new Tuple<bool, string>(false, "Destination Account is required in app extras");
            }

            return new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForTransferFunds()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type?.ToLower() != "bank.account")
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            if (Transaction?.Details == null)
            {
                return new Tuple<bool, string>(false, "Bank code is required in details");
            }
            if (!Transaction.Details.ContainsKey("destination_bank_code"))
            {
                return new Tuple<bool, string>(false, "Bank code is required in details");
            }
            if (!Transaction.Details.ContainsKey("destination_account"))
            {
                return new Tuple<bool, string>(false, "Destination Account is required in details");
            }
            return new Tuple<bool, string>(true, "");
        }

        public Tuple<bool, string> ValidateForDisburse()
        {

            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;

            if (Transaction?.Details == null)
            {
                return new Tuple<bool, string>(false, "Transaction details is expected");
            }
            if (!Transaction.Details.ContainsKey("destination_account"))
            {
                return new Tuple<bool, string>(false, "Destination Account is required in transaction details");
            }
            if (!Transaction.Details.ContainsKey("destination_bank_code"))
            {
                return new Tuple<bool, string>(false, "Destination bank code is required in transaction details");
            }
            if (Transaction.AppInfo?.Extras == null || !Transaction.AppInfo.Extras.ContainsKey("source_account_number"))
            {
                return new Tuple<bool, string>(false, "Source account number is required in application extras");
            }
            return new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForLookupBVN()
        {
            var validateBasicResponse = ValidateBasic();
            var validAuthTypes = new List<string> { "bvn", "id" };
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type == null || !validAuthTypes.Contains(Auth.Type.ToLower()))
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            return new Tuple<bool, string>(true, "");
        }
        public Tuple<bool, string> ValidateForGetStatement()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type?.ToLower() != "bank.account")
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            if (Transaction?.Details == null)
            {
                return new Tuple<bool, string>(false, "Transaction details is required");
            }
            if (!Transaction.Details.ContainsKey("start_date"))
            {
                return new Tuple<bool, string>(false, "Start date is required");
            }
            if (!Transaction.Details.ContainsKey("end_date"))
            {
                return new Tuple<bool, string>(false, "End date is required");
            }

            var startDate = Transaction.Details["start_date"].ToString();
            var isValidStartDate = DateTime.TryParse(startDate, out var parsedStartDate);
            if (!isValidStartDate)
            {
                return new Tuple<bool, string>(false, "Start date is invalid, expected format yyyy-mm-dd");
            }
            var endDate = Transaction.Details["end_date"].ToString();
            var isValidEndDate = DateTime.TryParse(endDate, out var parsedEndDate);
            if (!isValidEndDate)
            {
                return new Tuple<bool, string>(false, "End date is invalid, expected format yyyy-mm-dd");
            }
            if (parsedStartDate > parsedEndDate)
            {
                return new Tuple<bool, string>(false, "Start date cannot be greater than end date");
            }
            return new Tuple<bool, string>(true, "");
        }

        public Tuple<bool, string> ValidateForAccountCreation()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;
            if (Auth?.Type?.ToLower() != "bvn")
            {
                return new Tuple<bool, string>(false, "Auth type is invalid");
            }
            if (string.IsNullOrEmpty(Auth?.Secure))
            {
                return new Tuple<bool, string>(false, "Auth Secure is required");
            }
            if (Transaction?.Details == null)
            {
                return new Tuple<bool, string>(false, "Transaction details is required");
            }
            if (!Transaction.Details.ContainsKey("name_on_account"))
            {
                return new Tuple<bool, string>(false, "Account name is required");
            }
            if (!Transaction.Details.ContainsKey("middlename"))
            {
                return new Tuple<bool, string>(false, "Middle name is required");
            }
            if (!Transaction.Details.ContainsKey("dob"))
            {
                return new Tuple<bool, string>(false, "Date of birth is required");
            }
            if (!Transaction.Details.ContainsKey("gender"))
            {
                return new Tuple<bool, string>(false, "Gender is required");
            }
            if (!Transaction.Details.ContainsKey("title"))
            {
                return new Tuple<bool, string>(false, "Title is required");
            }
            if (!Transaction.Details.ContainsKey("address_line_1"))
            {
                return new Tuple<bool, string>(false, "Address Line 1 is required");
            }
            if (!Transaction.Details.ContainsKey("address_line_2"))
            {
                return new Tuple<bool, string>(false, "Address Line 2 is required");
            }
            if (!Transaction.Details.ContainsKey("address_line_2"))
            {
                return new Tuple<bool, string>(false, "Address Line 2 is required");
            }
            if (!Transaction.Details.ContainsKey("city"))
            {
                return new Tuple<bool, string>(false, "City is required");
            }
            if (!Transaction.Details.ContainsKey("state"))
            {
                return new Tuple<bool, string>(false, "state is required");
            }
            if (!Transaction.Details.ContainsKey("country"))
            {
                return new Tuple<bool, string>(false, "Country is required");
            }
            if (Transaction?.AppInfo?.Extras == null || !Transaction.AppInfo.Extras.ContainsKey("wallet_type"))
            {
                return new Tuple<bool, string>(false, "Wallet Type is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("wallet_short_name"))
            {
                return new Tuple<bool, string>(false, "Wallet Short Name is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("wallet_name"))
            {
                return new Tuple<bool, string>(false, "Wallet Name is required");
            }
            return new Tuple<bool, string>(true, "");
        }

        #region virtual account 

        public bool OpenVirtualAccount()
        {
            // Check If open_virtual exist in App extras and it's set as true;
            if (Transaction?.AppInfo?.Extras != null
                && Transaction.AppInfo.Extras.ContainsKey("open_virtual")
                && !string.IsNullOrWhiteSpace(Transaction.AppInfo.Extras["open_virtual"]))
            {
                return Transaction.AppInfo.Extras["open_virtual"].ToLower() == "true";
            }

            return false;
        }

        public Tuple<bool, string> ValidateForVirtualAccountCreation()
        {
            var validateBasicResponse = ValidateBasic();
            if (!validateBasicResponse.Item1)
                return validateBasicResponse;

            if (Transaction?.Details == null)
            {
                return new Tuple<bool, string>(false, "Transaction details is required");
            }
            if (!Transaction.Details.ContainsKey("name_on_account")
                || string.IsNullOrWhiteSpace(Transaction.Details["name_on_account"].ToString()))
            {
                return new Tuple<bool, string>(false, "Account name is required");
            }
            if (Transaction?.AppInfo?.Extras == null || !Transaction.AppInfo.Extras.ContainsKey("wallet_type"))
            {
                return new Tuple<bool, string>(false, "Wallet Type is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("wallet_short_name"))
            {
                return new Tuple<bool, string>(false, "Wallet Short Name is required");
            }
            if (!Transaction.AppInfo.Extras.ContainsKey("wallet_name"))
            {
                return new Tuple<bool, string>(false, "Wallet Name is required");
            }
            return new Tuple<bool, string>(true, "");
        }

        #endregion

        public string GetBassAuthToken()
        {
            if (Transaction?.AppInfo?.Extras != null
                && Transaction.AppInfo.Extras.ContainsKey("token")
                && !string.IsNullOrWhiteSpace(Transaction.AppInfo.Extras["token"]))
            {
                return Transaction.AppInfo.Extras["token"];
            }
            return string.Empty;
        }

    }

}
