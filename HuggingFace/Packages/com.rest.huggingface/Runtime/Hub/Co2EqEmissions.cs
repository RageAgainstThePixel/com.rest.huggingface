using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Co2EqEmissions
    {
        [JsonConstructor]
        public Co2EqEmissions(
            [JsonProperty("emissions")] string emissions,
            [JsonProperty("source")] string source,
            [JsonProperty("training_type")] string trainingType,
            [JsonProperty("geographical_location")] string geographicalLocation,
            [JsonProperty("hardware_used")] string hardwareUsed)
        {
            Emissions = emissions;
            Source = source;
            TrainingType = trainingType;
            GeographicalLocation = geographicalLocation;
            HardwareUsed = hardwareUsed;
        }

        [JsonProperty("emissions")]
        public string Emissions { get; }

        [JsonProperty("source")]
        public string Source { get; }

        [JsonProperty("training_type")]
        public string TrainingType { get; }

        [JsonProperty("geographical_location")]
        public string GeographicalLocation { get; }

        [JsonProperty("hardware_used")]
        public string HardwareUsed { get; }
    }
}