//This is the sessions services (transfer data between controller-service-database and viceversa)
using System.ComponentModel;
using ChatSharp.Classes;

namespace ChatSharp.Services
{
    public class SService
    {
        public static SService Instance{get; private set;} = new SService();

        //a prototipe function to create a session
        public ChatSharp.Classes.Sessions create_session(int user_id, string token, int hours)
        {   
            ChatSharp.Classes.Sessions session = new Sessions(user_id, token, hours);

            return session;

        }


        //inserts the session into the database and then return the result to the Session Controller login

        public bool save_session(ChatSharp.Classes.Sessions session)
        {
            //we simulate a query to make an insert to the db
            
            return true;


        }
    } 


}

