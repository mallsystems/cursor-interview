namespace API.UserService.Model
{
    public class UserDto
    {
        public string IDCARD { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public DateTime DATEOFBIRTH { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? COUNTRY { get; set; }
        public string? LOCALITY { get; set; }
    }
}
