using System;

namespace Dawnx.AspNetCore
{
    public class SimpleResponse
    {
        /// <summary>
        /// A simple code describing the state of the service invocation
        /// </summary>
        public string state;

        /// <summary>
        /// The detailed information
        /// </summary>
        public string status;

        /// <summary>
        /// A friendly message that tells client that the status
        /// </summary>
        public string message;

        /// <summary>
        /// If necessary, carry a data model 
        /// </summary>
        public object model;

        public const string SuccessState = "success";
        public static SimpleResponse SuccessResponse => new SimpleResponse { state = SuccessState };        

    }
}
