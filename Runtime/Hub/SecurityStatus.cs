// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class SecurityStatus
    {
        [JsonConstructor]
        public SecurityStatus(
            [JsonProperty("repositoryId")] string repositoryId,
            [JsonProperty("revision")] string revision,
            [JsonProperty("hasUnsafeFile")] bool? hasUnsafeFile,
            [JsonProperty("clamAVInfectedFiles")] List<string> clamAVInfectedFiles,
            [JsonProperty("dangerousPickles")] List<string> dangerousPickles,
            [JsonProperty("scansDone")] bool? scansDone)
        {
            RepositoryId = repositoryId;
            Revision = revision;
            HasUnsafeFile = hasUnsafeFile;
            ClamAVInfectedFiles = clamAVInfectedFiles;
            DangerousPickles = dangerousPickles;
            ScansDone = scansDone;
        }

        [JsonProperty("repositoryId")]
        public string RepositoryId { get; }

        [JsonProperty("revision")]
        public string Revision { get; }

        [JsonProperty("hasUnsafeFile")]
        public bool? HasUnsafeFile { get; }

        [JsonProperty("clamAVInfectedFiles")]
        public IReadOnlyList<string> ClamAVInfectedFiles { get; }

        [JsonProperty("dangerousPickles")]
        public IReadOnlyList<string> DangerousPickles { get; }

        [JsonProperty("scansDone")]
        public bool? ScansDone { get; }
    }
}
