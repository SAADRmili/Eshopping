﻿using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entites;

public class ProductBrand : BaseEntity
{
    [BsonElement("Name")]
    public string Name { get; set; }
}