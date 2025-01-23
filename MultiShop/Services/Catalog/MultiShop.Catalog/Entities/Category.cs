using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities;

public class Category
{
    // Bu atribut, MongoDB-də bu xüsusiyyətin sənədin (document) unikal ID-si olacağını bildirir.
    [BsonId]
    // Bu atribut, CategoryId dəyərinin MongoDB-də ObjectId formatında saxlanacağını bildirir.
    // Ancaq C#-da string kimi istifadə olunur:
    // MongoDB-də: ObjectId("64b1d59a6e12d512347a567b")

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string ImageUrl { get; set; }
}