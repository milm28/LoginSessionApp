
using System.ComponentModel.DataAnnotations;

namespace LoginAppSeesionBased.DTOs
{
    /// <summary>
    /// Klasa koja nam sluzi za logovanje na sistem.
    /// Nju saljemo na LoginView i u nju popunjavamo pdoatke sa forme.
    /// </summary>
    public class LoginDTO
    {
        #region Properties
        [Required]
        [StringLength(50, ErrorMessage = "Duzina polja je max 50 karaktera.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Duzina polja je max 50 karaktera.")]
        public string Password { get; set; }

        #endregion
    }
}