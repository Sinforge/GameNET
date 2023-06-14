﻿namespace AccountService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? HtmlText { get; set; } = null!;
        public bool isChecked { get; set; }
        public User User { get; set; } = null!;

    }
}
