using System;

namespace EfCosmos.Services.Api.Entities
{
    public class Template
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PartitionKey { get; set; }
    }
}