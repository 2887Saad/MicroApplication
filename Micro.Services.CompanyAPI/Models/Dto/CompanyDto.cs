using Micro.Services.CompanyAPI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Services.CompanyAPI.Models.Dto;

public class CompanyDto
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = ErrorMessages.RequiredField)]
    [StringLength(100, MinimumLength = 2, ErrorMessage=ErrorMessages.RequiredField)]
    public string Name { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredField)]
    public string Address { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredField)]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredField)]
    [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailFormat)]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredField)]
    public string Status { get; set; } = "Active";

    /*public ICollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }*/
}