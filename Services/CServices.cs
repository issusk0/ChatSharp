using ChatSharp.Classes;

namespace ChatSharp.Services 

{   
    

    public class CServices{

        //to set one globally service instance
        public static CServices Instance { get; private set; } = new CServices();
        //To send the message to a chat
        public bool SendMessageChat(Chat chat, Message message)
        {
           return true;

        }


        
        
        
    }


}