using System.Collections.Generic;
using UnityEngine;

namespace HuggingFace.Hub
{
    public class ModelFilter
    {
        public ModelFilter(
            string author = null,
            string modelName = null,
            OneOrMoreOf<string> task = null,
            OneOrMoreOf<string> tags = null,
            OneOrMoreOf<string> library = null,
            OneOrMoreOf<string> language = null,
            OneOrMoreOf<string> trainedDataset = null,
            Vector2? emissionsThresholds = null)
        {
            Author = author;
            ModelName = modelName;
            Tags = tags?.Values ?? new List<string>();
            Tasks = task?.Values ?? new List<string>();
            Library = library?.Values ?? new List<string>();
            Language = language?.Values ?? new List<string>();
            TrainedDataset = trainedDataset?.Values ?? new List<string>();
            EmissionsThresholds = emissionsThresholds;
        }

        /// <summary>
        /// A string that can be used to identify models on the Hub by the original uploader (author or organization),
        /// such as 'facebook' or 'huggingface'.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// A string or list of strings of foundational libraries models were originally trained from,
        /// such as pytorch, tensorflow, or allennlp.
        /// </summary>
        public IReadOnlyList<string> Library { get; }

        /// <summary>
        /// A string or list of strings of languages, both by name and country code, such as "en" or "English".
        /// </summary>
        public IReadOnlyList<string> Language { get; }

        /// <summary>
        /// A string that contains complete or partial names for models on the Hub, such as "bert" or "bert-base-cased".
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// A string or list of strings of tasks models were designed for, such as "fill-mask" or "automatic-speech-recognition".
        /// </summary>
        public IReadOnlyList<string> Tasks { get; }

        /// <summary>
        /// A string or list of string tags to filter models on the Hub by, such as 'text-generation' or 'spacy'.
        /// </summary>
        public IReadOnlyList<string> Tags { get; }

        /// <summary>
        /// A string or list of string tags of the trained dataset for a model on the Hub.
        /// </summary>
        public IReadOnlyList<string> TrainedDataset { get; }

        /// <summary>
        /// A minimum and maximum carbon footprint to filter the resulting models with in grams.
        /// </summary>
        public Vector2? EmissionsThresholds { get; }
    }
}
