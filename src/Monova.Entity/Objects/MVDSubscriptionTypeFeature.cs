using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monova.Entity
{
    [Table("SubscriptionTypeFeature")]
    public class MVDSubscriptionTypeFeature
    {
        [Key]
        public Guid SubscriptionTypeFeatureId { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsFeature { get; set; }
    }
}