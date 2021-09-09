
//ovaj dto saljemo na view da bi mogli da se logujemo
using System.ComponentModel.DataAnnotations;

namespace LoginAppSessionBased.DTOs
{
    /// <summary>
    /// Kalsa koja nam sluzi za logovanje na sistem
    /// Nju saljemo na loginView i u nju popunjavamo podatke iz forme
    /// </summary>
    public class LoginDTO
    {
        #region Properties
        [Required]
        [StringLength(50,ErrorMessage ="Maximalan broj karaktera je 50")]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximalan broj karaktera je 50")]
        public string Password { get; set; }

        #endregion
    }
}