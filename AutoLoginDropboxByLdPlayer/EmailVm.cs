using System;

namespace AutoLoginDropboxByLdPlayer
{
    public class EmailVm
    {
        public long EmailId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? RegistrationTime { get; set; }
        public DateTime? LoginTime { get; set; }
        public byte StateId { get; set; }
        public bool IsActive { get; set; }
        public string CreateByUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedByUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
