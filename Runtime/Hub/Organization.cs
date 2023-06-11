// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class Organization
    {
        [JsonConstructor]
        public Organization(
            [JsonProperty("type")] string type,
            [JsonProperty("id")] string id,
            [JsonProperty("name")] string name,
            [JsonProperty("fullname")] string fullname,
            [JsonProperty("email")] string email,
            [JsonProperty("apiToken")] string apiToken,
            [JsonProperty("periodEnd")] object periodEnd,
            [JsonProperty("plan")] string plan,
            [JsonProperty("canPay")] bool canPay,
            [JsonProperty("avatarUrl")] string avatarUrl,
            [JsonProperty("roleInOrg")] string roleInOrg)
        {
            Type = type;
            Id = id;
            Name = name;
            Fullname = fullname;
            Email = email;
            ApiToken = apiToken;
            PeriodEnd = periodEnd;
            Plan = plan;
            CanPay = canPay;
            AvatarUrl = avatarUrl;
            RoleInOrg = roleInOrg;
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("fullname")]
        public string Fullname { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("apiToken")]
        public string ApiToken { get; }

        [JsonProperty("periodEnd")]
        public object PeriodEnd { get; }

        [JsonProperty("plan")]
        public string Plan { get; }

        [JsonProperty("canPay")]
        public bool CanPay { get; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; }

        [JsonProperty("roleInOrg")]
        public string RoleInOrg { get; }
    }
}
