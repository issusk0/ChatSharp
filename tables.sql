/*
Table users summary:
    This table is for saving the user data such as username and password hash, i decided to use hash since is more secure
    than only plain text
Table sessions summary:
    This table contains all that reference to tokens to give to the frontend, i decided to did this
    because i wanted to implement a simple query to validate only the hash instead of asking for credentials again
    it is just cookie system for my backend, thats why the table has a constraint to assing each hash to a one user
    using foreign keys that references user id from the user table
Table chats summary:
    This table IS NOT A CHAT is a sort of ID for a chat, this means that this table will be the connector
    to search messages for a specific chat, why?, beacause it is simple and scalable, how it works?, the table creates a 
    chat id that will be used for a groupchat(bool is_group) or for a 1p1 chat, this table wont save any message data
    between the users

Table chat_participants summary:
    This tables connects chat and users (PK CHAT_ID AND USER_ID) and saves it in a big table that contains every user that belongs a chat(either is group or not)
    i made this because its simply to search and filter when the backend asks for the messages of a specific chat, i used
    foreign keys to connect each user to a unique chat so a user cannot join again to the same chat(unless he either exit or got kick)


Table messages summary:
    This tables saves all the messages and who sent it and where it belongs(CHAT_ID, USER_ID), 
    in this case an specific chat id(do not confuse chat participants that is only for manage user-chat)

Indexs summary:
    idx_user_username: I decided to create an index per username to make faster queries over among all the usernames that exists
    idx_chat_sharp_sessions_token: I used indexs here to make a faster access to the token so it makes it quicker to get the token and response data
    idx_chat_sharp_sessions_user_id: This is core for the other index, we look up based on the query like Select token from chat_sharp_sesisons where token = x
                            and by that, using the indexes will it make these queries faster and making the service way better by not searching among millons of users
    idx_chat_participants_user_id: This is due to the compose of how the primary key (x,y)works, the x has an index by itself beacuse is the first one, but "y" doesn´t,
                                    so that will make queries like this "select (data) from chat_participants where user_id = something" slower that it should be
                                    and by indexing that column it will make it faster due to the connections of the inner data structure
    idx_message_chat_id: This is the same as the others, due to the quantity of queries that will be needing the chat id, i decided to make an index so it will find the data faster
    idx_message_sender_id: This index is for searching faster and specific user between the messages


    Example of the queries:
    idx_users_username              → Login: WHERE username = X
    idx_chat_sharp_sessions_token   → Auth: WHERE token = X  
    idx_chat_sharp_sessions_user_id → Session mgmt: WHERE user_id = X
    idx_chat_participants_user_id   → User's chats: WHERE user_id = X (PK doesn't cover this)
    idx_messages_chat_id            → Chat messages: WHERE chat_id = X (most frequent query)
    idx_messages_sender_id          → User messages: WHERE sender_id = X
*/





CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL, 
    password_hash TEXT NOT NULL,         
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

--Index for username lookups (login, user search)
CREATE INDEX idx_user_username ON users(username);

CREATE TABLE chat_sharp_sessions(
    session_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    token VARCHAR(255) UNIQUE NOT NULL,   
    expires_at TIMESTAMP NOT NULL,        
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_used_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    

    CONSTRAINT fk_user 
        FOREIGN KEY(user_id) 
        REFERENCES users(user_id) 
        ON DELETE CASCADE
);
--Index for tokens and user_id
CREATE INDEX idx_chat_sharp_sessions_token ON chat_sharp_sessions(token);
CREATE INDEX idx_chat_sharp_sessions_user_id ON chat_sharp_sessions(user_id);

CREATE TABLE chats (
    chat_id SERIAL PRIMARY KEY,
    is_group BOOLEAN DEFAULT FALSE,       
    name VARCHAR(100),                   
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE chat_participants (
    chat_id INT NOT NULL,
    user_id INT NOT NULL,
    joined_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (chat_id, user_id),       
    FOREIGN KEY (chat_id) REFERENCES chats(chat_id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);
--Index for searching user_id
CREATE INDEX idx_chat_participants_user_id ON chat_participants(user_id);

CREATE TABLE messages (
    message_id SERIAL PRIMARY KEY,
    chat_id INT NOT NULL,                 
    sender_id INT NOT NULL,               
    content TEXT NOT NULL,                
    sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (chat_id) REFERENCES chats(chat_id) ON DELETE CASCADE,
    FOREIGN KEY (sender_id) REFERENCES users(user_id) ON DELETE SET NULL
);

--Index for chat_id and sender_id
CREATE INDEX idx_messages_chat_id ON messages(chat_id);
CREATE INDEX idx_messages_sender_id ON messages(sender_id);