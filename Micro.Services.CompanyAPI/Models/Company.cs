using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Services.CompanyAPI.Models;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Status { get; set; } = "Active";

    /*public ICollection<PurchaseOrder> PurchaseOrders { get; set; }*/
}