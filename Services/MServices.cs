using ChatSharp.Classes;

namespace ChatSharp.Services
{
    public class MServices
    {
        //to create an instance to use these services
        public static MServices Instance { get; private set; } = new MServices();

        //function to send the message and save the data
        public bool SendMessage(Message message) {
            return true;
        }
        //function to recieve messages (returns an array of message object type)
        public List<Message> RecieveMessages() {
            return new List<Message>();
        
        }

        //function to validate the data (like user_id or message_id) before sending it
        public bool ValidateMessage(Message message) {
            return true;
        }
        
        
    }
}