// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ImageToImage
{
    public sealed class ImageToImageParameters
    {
        public ImageToImageParameters(
            [JsonProperty("prompt")] string prompt,
            [JsonProperty("negative_prompt")] string negativePrompt = null,
            [JsonProperty("height")] float? height = null,
            [JsonProperty("width")] float? width = null,
            [JsonProperty("num_inference_steps")] int? denoisingSteps = null,
            [JsonProperty("guidance_scale")] float? guidanceScale = null)
        {
            Prompt = prompt;
            NegativePrompt = negativePrompt;
            Height = height;
            Width = width;
            DenoisingSteps = denoisingSteps;
            GuidanceScale = guidanceScale;
        }

        /// <summary>
        /// The input image for translation encoded to b64 string.
        /// </summary>
        [JsonProperty("inputs")]
        public string Input { get; internal set; }

        /// <summary>
        /// The prompt to generate an image from.
        /// </summary>
        [JsonProperty("prompt")]
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
        public float? Height { get; set; }

        /// <summary>
        /// The width in pixels of the image to generate.
        /// </summary>
        [JsonProperty("width")]
        public float? Width { get; set; }

        /// <summary>
        /// The number of denoising steps.More denoising steps usually lead to a higher quality image at the
        /// expense of slower inference.
        /// </summary>
        [JsonProperty("num_inference_steps")]
        public int? DenoisingSteps { get; set; }

        /// <summary>
        /// Higher guidance scale encourages to generate images that are closely linked to the text `prompt`,
        /// usually at the expense of lower image quality.
        /// </summary>
        [JsonProperty("guidance_scale")]
        public float? GuidanceScale { get; set; }
    }
}
