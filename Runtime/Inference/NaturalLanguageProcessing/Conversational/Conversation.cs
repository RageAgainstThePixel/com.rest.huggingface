using System;
using System.Collections.Generic;
using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Conversational
{
    public sealed class Conversation
    {
        /// <summary>
        /// Use when starting a new conversation.
        /// </summary>
        /// <param name="userInput"></param>
        public Conversation(string userInput)
        {
            UserInput = userInput;
            PastResponses = new List<string>();
            PastUserInputs = new List<string>();
        }

        /// <summary>
        /// Use when continuing a previous conversation.
        /// </summary>
        /// <param name="generatedResponses"></param>
        /// <param name="pastUserInput"></param>
        public Conversation(
            OneOrMoreOf<string> pastUserInput,
            OneOrMoreOf<string> generatedResponses)
        {
            if (pastUserInput?.Values != null &&
                generatedResponses?.Values != null &&
                generatedResponses.Values.Count != pastUserInput.Values.Count)
            {
                throw new InvalidOperationException("The generated responses and past user input should be the same length!");
            }

            PastUserInputs = pastUserInput?.Values;
            PastResponses = generatedResponses?.Values;
        }

        [JsonConstructor]
        public Conversation(
            [JsonProperty("generated_text")] string response,
            [JsonProperty("past_user_inputs")] IReadOnlyList<string> pastUserInputs,
            [JsonProperty("generated_responses")] IReadOnlyList<string> pastResponses)
        {
            Response = response;
            PastUserInputs = pastUserInputs;
            PastResponses = pastResponses;
        }

        /// <summary>
        /// The last input from the user in the conversation.
        /// </summary>
        [JsonProperty("text")]
        public string UserInput { get; set; }

        [JsonProperty("generated_text")]
        public string Response { get; }

        /// <summary>
        /// A list of strings corresponding to the earlier replies from the user.
        /// Should be of the same length of generated_responses.
        /// </summary>
        [JsonProperty("past_user_inputs")]
        public IReadOnlyList<string> PastUserInputs { get; }

        /// <summary>
        /// A list of strings corresponding to the earlier replies from the model.
        /// </summary>
        [JsonProperty("generated_responses")]
        public IReadOnlyList<string> PastResponses { get; }
    }
}
