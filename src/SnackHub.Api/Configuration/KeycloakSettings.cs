namespace SnackHub.Configuration
{
    public class KeycloakSetting
    {
        public KeycloakSetting()
        {
        }

        public string Endpoint { get; set; }

        public string Realm { get; set; }

        public List<RealmSetting> Realms { get; set; } = new List<RealmSetting>();

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public sealed class RealmSetting
    {
        public string Name { get; set; }

        public string Issuer { get; set; }

        public string Authority { get; set; }
    }
}
