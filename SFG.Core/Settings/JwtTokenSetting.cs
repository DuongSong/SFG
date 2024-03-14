using System;
namespace SFG.Core.Settings
{
	public class JwtTokenSetting
	{
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int LongExpiryMinutes { get; set; }

        public int ShortExpiryMinutes { get; set; }
    }
}

