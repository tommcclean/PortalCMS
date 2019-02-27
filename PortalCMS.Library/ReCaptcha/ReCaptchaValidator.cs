using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PortalCMS.Library.ReCaptcha
{
	public class ReCaptchaValidator
	{
		public static ReCaptchaValidationResult IsValidAsync(string captchaResponse, string secretKey)
		{
			if (string.IsNullOrWhiteSpace(captchaResponse) || string.IsNullOrWhiteSpace(secretKey))
			{
				return new ReCaptchaValidationResult{ Success = false };
			}

			HttpClient httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri("https://www.google.com");

			var values = new List<KeyValuePair<string, string>>();
			values.Add(new KeyValuePair<string, string>("secret", secretKey));
			values.Add(new KeyValuePair<string, string> ("response", captchaResponse));

			FormUrlEncodedContent content =	new FormUrlEncodedContent(values);
			HttpResponseMessage response = httpClient.PostAsync("/recaptcha/api/siteverify", content).Result;

			string verificationResponse = response.Content.ReadAsStringAsync().Result;
			var verificationResult = JsonConvert.DeserializeObject<ReCaptchaValidationResult>(verificationResponse);

			return verificationResult;
			}
	}
}
