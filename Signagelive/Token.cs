namespace Signagelive
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public float expires_in { get; set; }
        public DateTime expires { get; set; }
        public string audience { get; set; }
    }
}
