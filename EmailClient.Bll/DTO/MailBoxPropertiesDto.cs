namespace EmailClient.Models
{
  public class MailBoxPropertiesDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string IncomingServer { get; set; }
        public int IncomingServerPort { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }
        public string HashedPassword { get; set; }


    }

}
