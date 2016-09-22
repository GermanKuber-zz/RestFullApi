﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Community.Constants;
using Newtonsoft.Json.Linq;

namespace Community.Mvc.Helpers
{
    //TODO: Paso 23 - 2 - Se crea Helper para mostrar

    public static class EndpointAndTokenHelper
    {
        public static async Task<JObject> CallUserInfoEndpoint(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(CommunityConstants.IdSrvUserInfo);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JObject.Parse(json); //.ToString();
 
            }
            else
            {
                return null;
            }
        }


        public static void DecodeAndWrite(string token)
        {
            try
            {

           
            var parts = token.Split('.');

            string partToConvert = parts[1];
            partToConvert = partToConvert.Replace('-', '+'); 
            partToConvert = partToConvert.Replace('_', '/');  
            switch (partToConvert.Length % 4)  
            {
                case 0:
                    break;  
                case 2: 
                    partToConvert += "==";
                    break;  
                case 3: 
                    partToConvert += "=";
                    break;  
                default:
                    break;
            }

            var partAsBytes = Convert.FromBase64String(partToConvert);             
            var partAsUTF8String = Encoding.UTF8.GetString(partAsBytes, 0, partAsBytes.Count());

            // Json .NET
            var jwt = JObject.Parse(partAsUTF8String);

            // Write to output
            Debug.Write(jwt.ToString());

            }
            catch (Exception ex)
            {
                // something went wrong
                Debug.Write(ex.Message);

            }
        }

     
    }
}