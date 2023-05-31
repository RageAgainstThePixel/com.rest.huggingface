using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskResponse
    {
        [JsonConstructor]
        public FillMaskResponse(
            [JsonProperty("token")] int token,
            [JsonProperty("score")] double score,
            [JsonProperty("sequence")] string sequence,
            [JsonProperty("token_str")] string tokenString)
        {
            Score = score;
            Token = token;
            Sequence = sequence;
            TokenString = tokenString;
        }

        [JsonProperty("token")]
        public int Token { get; }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("sequence")]
        public string Sequence { get; }

        [JsonProperty("token_str")]
        public string TokenString { get; }
    }
}
