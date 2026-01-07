namespace ChatSharp.Classes
{
    public class Users{
            public int User_Id { get; private set; }
            public string Username { get; private set; } = string.Empty;


            public string password_hash {get; private set; } = string.Empty;

            public DateTime created_at {get; set;}

        
    }

}   