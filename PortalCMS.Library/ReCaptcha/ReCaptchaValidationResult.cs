using Newtonsoft.Json;
using System.Collections.Generic;

namespace PortalCMS.Library.ReCaptcha
{
	public class ReCaptchaValidationResult
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("challenge_ts")]
		public string TimeStamp { get; set; }

		[JsonProperty("hostname")]
		public string HostName { get; set; }

		[JsonProperty("error-codes")]
		public List<string> ErrorCodes { get; set; }
	}
}
