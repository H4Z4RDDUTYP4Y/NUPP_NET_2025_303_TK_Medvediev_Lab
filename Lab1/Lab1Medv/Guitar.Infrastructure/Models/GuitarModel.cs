using Guitar.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Infrastructure.Models
{
    public class GuitarModel : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StringCount { get; set; }
        public int ScaleLength { get; set; }
        public float Price { get; set; }

        // Foreign key to Player
        public Guid PlayerId { get; set; }
        public PlayerModel Player { get; set; }
    }
}
