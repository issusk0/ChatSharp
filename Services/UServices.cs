using System.ComponentModel;



//service for manage users
namespace ChatSharp.Services
{
    
    public class UServices
    {
        public static UServices Instance {get; private set;} = new UServices();

        public string get_username(int user_id)
        {
            //an a fictionary db connection
            
            string query_reponse = "User_1";

            return query_reponse;
        }
        
  
        public bool validate_function(string username, string password_hash)
        {
            //an a fictionary db connection and a query response

            bool query_reponse = false; //here will be called a funtion that takes those two parameters and does the query to the database
            if (!query_reponse)
            {
                return false;
            }

            return true;
        }
    }

}