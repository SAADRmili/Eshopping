﻿using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entites;
public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
}
