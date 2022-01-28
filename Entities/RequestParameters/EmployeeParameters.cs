﻿using System.Text.Json.Serialization;

namespace Entities.RequestParameters;

public class EmployeeParameters : RequestParameters
{
    public uint MinAge { get; set; }
    public uint MaxAge { get; set; } = int.MaxValue;
  [JsonIgnore]  public bool ValidAgeRange => MaxAge > MinAge;
}