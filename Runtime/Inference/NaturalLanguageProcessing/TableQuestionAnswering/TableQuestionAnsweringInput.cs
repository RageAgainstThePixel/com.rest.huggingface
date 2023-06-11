using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public class TableQuestionAnsweringInput<TTableData>
        where TTableData : class
    {
        public TableQuestionAnsweringInput(string query, TTableData table)
        {
            Query = query;
            Table = table;
        }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("table")]
        public TTableData Table { get; set; }
    }
}
