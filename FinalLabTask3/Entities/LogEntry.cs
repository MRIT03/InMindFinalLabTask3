using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalLabTask3.Entities;
public class LogEntry
{
    public long Id { get; set; }
    public Guid RequestId { get; set; }
    public Dictionary<string, object> RequestObject { get; set; } = new();
    public string RouteURL { get; set; }
    public DateTime Timestamp { get; set; }
}