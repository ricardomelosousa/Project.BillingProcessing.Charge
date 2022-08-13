using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Project.BillingProcessing.Charge.Domain.ChargeEntity
{
    public class Charge
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DueDate { get; set; }       
        public decimal ChargeValue { get; set; }
        public long Identification { get; set; }
       
    }
}
