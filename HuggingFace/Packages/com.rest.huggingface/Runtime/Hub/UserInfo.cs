// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class UserInfo
    {
        [JsonConstructor]
        public UserInfo(
            [JsonProperty("type")] string type,
            [JsonProperty("id")] string id,
            [JsonProperty("name")] string name,
            [JsonProperty("fullname")] string fullname,
            [JsonProperty("email")] string email,
            [JsonProperty("emailVerified")] bool emailVerified,
            [JsonProperty("plan")] string plan,
            [JsonProperty("canPay")] bool canPay,
            [JsonProperty("isPro")] bool isPro,
            [JsonProperty("periodEnd")] object periodEnd,
            [JsonProperty("avatarUrl")] string avatarUrl,
            [JsonProperty("orgs")] IReadOnlyList<string> organizations,
            [JsonProperty("auth")] HubAuth hubAuth)
        {
            Type = type;
            Id = id;
            Name = name;
            Fullname = fullname;
            Email = email;
            EmailVerified = emailVerified;
            Plan = plan;
            CanPay = canPay;
            IsPro = isPro;
            PeriodEnd = periodEnd;
            AvatarUrl = avatarUrl;
            Organizations = organizations;
            HubAuth = hubAuth;
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

        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; }

        [JsonProperty("plan")]
        public string Plan { get; }

        [JsonProperty("canPay")]
        public bool CanPay { get; }

        [JsonProperty("isPro")]
        public bool IsPro { get; }

        [JsonProperty("periodEnd")]
        public object PeriodEnd { get; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; }

        [JsonProperty("orgs")]
        public IReadOnlyList<object> Organizations { get; }

        [JsonProperty("auth")]
        public HubAuth HubAuth { get; }
    }
}
