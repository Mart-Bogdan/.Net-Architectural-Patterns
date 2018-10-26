using System.ComponentModel.DataAnnotations;

namespace WebAppCore.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}