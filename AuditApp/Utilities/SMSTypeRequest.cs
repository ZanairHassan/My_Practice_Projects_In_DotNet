using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Exceptions;

namespace Utilities
{
    public static class SMSTypeRequest
    {
        public static async void SendSmS(string phoneNo, string password, string body)
        {
            try
            {
                var accountSid = "AC3e4361be8dc8e02d60102508af64b8e6";
                var authToken = "26e6055238249082028dc23330b38cde";

                TwilioClient.Init(accountSid, authToken);

                var message = await MessageResource.CreateAsync(
                    body: body,
                    from: new Twilio.Types.PhoneNumber("+1 218 332 7737"), // Your Twilio phone umair number
                    to: new Twilio.Types.PhoneNumber(phoneNo) // Recipient's phone number
                );

                Console.WriteLine($"SMS sent successfully! SID: {message.Sid}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Twilio API Error: {ex.Message}");
                // Handle Twilio API exceptions (e.g., invalid phone number)
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // Handle network-related exceptions (e.g., connection timeout)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
               
            }
        }
    }
}
