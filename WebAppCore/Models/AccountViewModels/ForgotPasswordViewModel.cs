using System.ComponentModel.DataAnnotations;

namespace WebAppCore.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}