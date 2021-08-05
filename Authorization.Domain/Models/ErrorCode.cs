using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.Models
{
    public enum ErrorCode
    {
        [Display(Name = "error_client_authorization_failed")]
        ClientAuthorizationFailed,
        [Display(Name = "error_role_not_found")]
        RoleNotFound,
        [Display(Name = "error_duplicated_email")]
        DuplicateMail,
        [Display(Name = "error_validation_failed")]
        ValidationFailed,
    }
}
