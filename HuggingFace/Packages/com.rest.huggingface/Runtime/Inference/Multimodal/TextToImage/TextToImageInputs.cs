using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.TextToImage
{
    public sealed class TextToImageInputs
    {
        public TextToImageInputs(string prompt)
        {
            Prompt = prompt;
        }

        /// <summary>
        /// The prompt to generate an image from.
        /// </summary>
        [JsonProperty("inputs")]
        public string Prompt { get; set; }

        /// <summary>
        /// An optional negative prompt for the image generation.
        /// </summary>
        [JsonProperty("negative_prompt")]
        public string NegativePrompt { get; set; }

        /// <summary>
        /// The height in pixels of the image to generate.
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; set; }

        /// <summary>
        /// The width in pixels of the image to generate.
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }

        /// <summary>
        /// The number of denoising steps. More denoising steps usually lead to a higher quality image at the expense of slower inference.
        /// </summary>
        [JsonProperty("num_inference_steps")]
        public int? NumberOfSteps { get; set; }

        /// <summary>
        /// Higher guidance scale encourages to generate images that are closely linked to the text <see cref="Prompt"/>,
        /// usually at the expense of lower image quality.
        /// </summary>
        [JsonProperty("guidance_scale")]
        public float? GuidanceScale { get; set; }
    }
}