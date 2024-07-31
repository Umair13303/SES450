using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class HttpStatus
    {

        public enum HttpResponses
        {
            CODE_SUCCESS = 200,
            CODE_DATA_UPDATED = 300,
            CODE_DATA_ALREADY_EXIST = 107,
            CODE_BAD_REQUEST = 400,
            CODE_UNAUTHORIZED = 401,
            CODE_DATA_DOES_NOT_EXIST = 404,
            CODE_INTERNAL_SERVER_ERROR = 500,
            CODE_UN_KNOWN_ACTIVITY = 510,
        }
        public static int HttpResponseByReturnValue(int? Response)
        {
            switch (Response)
            {
                case 107:
                    return (int)HttpResponses.CODE_DATA_ALREADY_EXIST;
                case 200:
                    return (int)HttpResponses.CODE_SUCCESS;
                case 300:
                    return (int)HttpResponses.CODE_DATA_UPDATED;
                default:
                    return (int)HttpResponses.CODE_UN_KNOWN_ACTIVITY;
            }
        }

        public static string HTTPTransactionMessagByStatusCode(int? StatusCode)
        {
            switch (StatusCode)
            {
                case (int)HttpResponses.CODE_SUCCESS:
                    return "TRANSACTION HAS BEEN PERFORMED SUCCESSFULLY";
                
                case (int)HttpResponses.CODE_DATA_UPDATED:
                    return "TRANSACTION HAS BEEN UPDATED SUCCESSFULLY";

                case (int)HttpResponses.CODE_DATA_ALREADY_EXIST:
                    return "THE DATA ALREADY EXIST.";

                case (int)HttpResponses.CODE_BAD_REQUEST:
                    return "BAD REQUEST PLEASE CHECK YOUR TRANSACTION.";

                case (int)HttpResponses.CODE_UNAUTHORIZED:
                    return "ACCESS DENIED.";

                case (int)HttpResponses.CODE_DATA_DOES_NOT_EXIST:
                    return "THE DATA DOES NOT EXIST IN SERVER SIDE.";

                case (int)HttpResponses.CODE_INTERNAL_SERVER_ERROR:
                    return "TRANSACTION ABORTED DUE TO INTERNAL SERVER.";

                default:
                    return "An unknown status code was returned.";
            }
        }
    }
}