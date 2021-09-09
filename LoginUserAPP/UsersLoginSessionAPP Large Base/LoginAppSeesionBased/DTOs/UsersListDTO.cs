
namespace LoginAppSeesionBased.DTOs
{
    /// <summary>
    /// DTO koji se koristi za prikaz svih usera[za tabelrani prikaz]
    /// </summary>
    public class UsersListDTO
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string IsActive { get; set; }

        #endregion
    }
}