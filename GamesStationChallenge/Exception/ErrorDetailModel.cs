using Newtonsoft.Json;

namespace GamesStationChallenge.Exception
{
    [JsonObject(Title = "detalle_error_model")]
    public class ErrorDetailModel
    {
        public ErrorDetailModel()
        {
            Errors = new List<Error>();
        }

        [JsonProperty(PropertyName = "codigo")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "estado")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "detalle")]
        public string Detail { get; set; }

        [JsonProperty(PropertyName = "errores")]
        public List<Error> Errors { get; }
    }
    [JsonObject(Title = "error")]
    public class Error
    {
        [JsonProperty(PropertyName = "codigo")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "titulo")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "origen")]
        public string Source { get; set; }
        [JsonProperty(PropertyName = "detalle")]
        public string Detail { get; set; }
        [JsonProperty(PropertyName = "spvtrack_id")]
        public string SpvTrackId { get; set; }
    }
}
