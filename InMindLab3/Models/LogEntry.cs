using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace InMindLab3.Models;
public class LogEntry
{
    public long Id { get; set; }
    public Guid RequestId { get; set; }
    [Column(TypeName = "jsonb")]
    public string RequestObject { get; set; }
    public string RouteURL { get; set; }
    public DateTime Timestamp { get; set; }
    
    [NotMapped]
    public Dictionary<string, object> RequestData
    {
        get => JsonConvert.DeserializeObject<Dictionary<string, object>>(RequestObject);
        set => RequestObject = JsonConvert.SerializeObject(value);
    }
}