﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Tradgardsgolf.ApiClient.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class AuthenticationResponse
    {
        /// <summary>
        /// Initializes a new instance of the AuthenticationResponse class.
        /// </summary>
        public AuthenticationResponse() { }

        /// <summary>
        /// Initializes a new instance of the AuthenticationResponse class.
        /// </summary>
        public AuthenticationResponse(string email = default(string), string name = default(string), string token = default(string))
        {
            Email = email;
            Name = name;
            Token = token;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; private set; }

    }
}
