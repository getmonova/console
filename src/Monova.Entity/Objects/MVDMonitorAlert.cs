using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monova.Entity
{
    [Table("MonitorAlert")]
    public class MVDMonitorAlert
    {
        [Key]
        public Guid MonitorAlertId { get; set; }
        public Guid MonitorId { get; set; }
        public string Title { get; set; }
        public MVDMonitorAlertChannelTypes ChannelType { get; set; }
        public string Settings { get; set; }
    }

    public enum MVDMonitorAlertChannelTypes : short
    {
        Unknown = 0,
        Email = 1,
        SMS = 2,
        Webhook = 3,
        Slack = 4,
        Telegram = 5
    }
}