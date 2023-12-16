// EmailModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class EmailModel
{
    [Key]
    public int EmailId { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public DateTime SendDate { get; set; }
    [Required]
    public string Recipient { get; set; }
}