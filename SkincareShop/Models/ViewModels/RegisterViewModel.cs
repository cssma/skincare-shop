using System.ComponentModel.DataAnnotations;
namespace KoreanSkincareShop.Models.ViewModels;

public class RegisterViewModel
{
    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required, DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}