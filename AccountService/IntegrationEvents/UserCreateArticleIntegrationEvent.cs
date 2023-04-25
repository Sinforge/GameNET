﻿using System.Runtime.Serialization;

namespace ArticleService.IntegrationEvents
{
    public class UserCreateArticleIntegrationEvent
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = null!;
        public string UserId { get; set; }

    }
}
