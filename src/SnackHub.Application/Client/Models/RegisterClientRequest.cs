using Flunt.Notifications;

namespace SnackHub.Application.Client.Models
{
    public class RegisterClientRequest : Notifiable<Notification>
    {
        public RegisterClientRequest(string name, string cpf)
        {
            Name = name;
            CPF = cpf;
        }

        public string Name { get; set; }
        public string CPF { get; set; }
    }
}