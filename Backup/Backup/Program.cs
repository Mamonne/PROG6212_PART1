using System;
using System.Media;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Entry point of the application.
    /// Creates an instance of the ChatBot and starts the conversation.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ChatBot bot = new ChatBot();
            bot.StartChat();
        }
    }
}