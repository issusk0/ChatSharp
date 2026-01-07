namespace ChatSharp.Classes
{
    public class Sessions
    {
       public int session_id {get; private set;}
       public int user_id{get; private set;}

       public string token {get; private set;} = string.Empty;

       public DateTime expires_at{get; private set;}

        public DateTime created_at {get; private set;} = DateTime.UtcNow;

        public DateTime last_used_at {get; private set;}


        public Sessions(int user_Id, string Token, int hourValids = 24)
        {
            user_id=user_Id;
            token = Token;
            created_at =DateTime.UtcNow;
            last_used_at = DateTime.UtcNow;
            expires_at = DateTime.UtcNow.AddHours(hourValids);

        }



        public void UpdateLastUsed()
        {
            last_used_at = DateTime.UtcNow;
        }



        public bool IsExpired() => DateTime.UtcNow > expires_at;
    }



}