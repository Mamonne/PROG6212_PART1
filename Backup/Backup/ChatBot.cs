using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace CybersecurityChatbot
{
   
    /// Main chatbot class that handles the whole program
   
    public class ChatBot
    {
        // Dictionary storing cybersecurity topics (user keyword → bot response)
        private Dictionary<string, string> responses;
        private string userName;

       
        /// Constructor: initialises the keyword-response dictionary.
       
        public ChatBot()
        {
            responses = new Dictionary<string, string>()
            {
                {"about", "I'm your friendly cybersecurity sidekick  here to keep you safe online!"},
                {"purpose", "My job is to teach you how to avoid phishing, create strong passwords, and browse safely."},
                {"cybersecurity", "Cybersecurity is like locking your digital doors and windows  to keep hackers out!"},
                {"virus", "A virus is like a digital flu  – it spreads and can damage your computer!"},
                {"password", "Use strong passwords! Try something like 'Lion#2026!'  – never '123456' or 'password'."},
                {"encryption", "Encryption scrambles your data into secret code  so only the right person can read it."},
                {"phishing", "Phishing is a scam  where attackers pretend to be someone you trust to steal your info."},
                {"malware", "Malware is evil software  (viruses, ransomware) that tries to harm your data or spy on you."},
                {"firewall", "A firewall acts like a security guard , blocking unwanted access to your network."},
                {"hacker", "Hackers try to break into systems  – stay protected with updates and strong passwords!"},
                {"2fa", "Two-factor authentication (2FA) is like double‑locking your door  – even if they steal your password, they still need your phone."},
                {"safe browsing", "Always check for 'https://' and avoid suspicious links  – if it looks too good to be true, it probably is!"},
                {"privacy", "Protect your privacy  – never share your ID number, bank details, or passwords online."},
                {"update", "Software updates fix security holes  – turn on automatic updates!"},
                {"wifi", "Public WiFi is risky  – avoid online banking or shopping on open networks."},
                {"ransomware", "Ransomware locks your files and demands money  – always back up your data!"}
            };
        }

       
        /// Plays the voice greeting before the program stats
       
        private void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(@"C:\\Users\\PC\\source\\repos\\Backup\\Greeting.wav");
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[Voice greeting not found – continuing without sound]");
                Console.ResetColor();
            }
        }

        
        /// Displays an ASCII art logo as the header.
       
        private void ShowAsciiArt()
        {
         
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  ██████╗██╗   ██╗██████╗ ███████╗██████╗     ██████╗  ██████╗ ████████╗
 ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗    ██╔══██╗██╔═══██╗╚══██╔══╝
 ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝    ██████╔╝██║   ██║   ██║   
 ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗    ██╔══██╗██║   ██║   ██║   
 ╚██████╗   ██║   ██████╔╝███████╗██║  ██║    ██████╔╝╚██████╔╝   ██║   
  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚═════╝  ╚═════╝    ╚═╝  
    ");
            Console.ResetColor();
            Console.WriteLine();
        }
        /// Simulates a typing effect by printing one character at a time with a small delay.
        
        private void TypeText(string message, int delayMs = 35)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        
        /// Prints a bot message with the "ChatBot:" prefix, using the typing effect.
       
        private void WriteBotMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot: ");
            Console.ResetColor();
            TypeText(message);
        }

       
        /// Prints an error message in red.
       
        private void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠️  " + message);
            Console.ResetColor();
        }

        
        /// Prints a decorative separator line.
        
        private void PrintSeparator()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
        }

       
        /// Displays a "thinking" animation with a progress bar from 0% to 100%.
        
        private void Think()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Thinking: [");
            Console.ResetColor();

            int totalSteps = 20; // 20 blocks = 100%
            for (int i = 0; i <= totalSteps; i++)
            {
                int percent = (i * 100) / totalSteps;
                // Draw progress bar
                Console.CursorLeft = 12 + i; // start after " Thinking: ["
                Console.Write("█");
                // Show percentage at the end of the line
                Console.CursorLeft = 35;
                Console.Write($"{percent}%");
                Thread.Sleep(25); // smooth filling
            }

            // Clear the whole "Thinking" line so the bot's answer appears cleanly
            Console.CursorLeft = 0;
            Console.Write(new string(' ', 45)); // overwrite with spaces
            Console.CursorLeft = 0;
            Console.ResetColor();
        }

        
        /// Asks the user for their name and validates it , it does not accept numbers and symbols
       
        private void AskUserName()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\nEnter your name: ");
                Console.ResetColor();
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Name cannot be empty. Please enter a valid name.");
                    continue;
                }

                bool hasInvalidChars = false;
                foreach (char c in input)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        hasInvalidChars = true;
                        break;
                    }
                }

                if (hasInvalidChars)
                {
                    WriteError("Name cannot contain numbers or symbols. Please use only letters and spaces.");
                }
                else
                {
                    userName = input.Trim();
                    break;
                }
            }
        }

        
        /// Handles emotional keywords (sad, happy, worried, etc.) and returns an appropriate empathetic response.
        
        /*private bool TryEmotionalResponse(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("sad") || lower.Contains("unhappy") || lower.Contains("depressed"))
            {
                Think();
                WriteBotMessage("I'm sorry you're feeling down.  Remember, staying safe online can give you peace of mind. Would you like to talk about a security topic?");
                return true;
            }
            if (lower.Contains("worried") || lower.Contains("scared") || lower.Contains("anxious"))
            {
                Think();
                WriteBotMessage("It's normal to be worried about cyber threats. I'm here to help you learn how to protect yourself. 💪 Ask me about 'phishing' or 'password'.");
                return true;
            }
            if (lower.Contains("angry") || lower.Contains("frustrated"))
            {
                Think();
                WriteBotMessage("I hear you. Sometimes tech can be frustrating! Let's take a breath. Want a quick tip on staying safe? 😊");
                return true;
            }
            if (lower.Contains("happy") || lower.Contains("great") || lower.Contains("awesome"))
            {
                Think();
                WriteBotMessage("I'm glad you're feeling happy! 😊 Let's make your online life even safer. What would you like to learn?");
                return true;
            }
            return false;
        }
        */
       
        /// Main conversation loop. Welcomes the user, asks for name, and repeatedly handles user input.
       
        public void StartChat()
        {
            PlayVoiceGreeting();
            ShowAsciiArt();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================");
            Console.WriteLine("    WELCOME TO THE CYBERSECURITY ASSISTANT  ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            AskUserName();

            WriteBotMessage($"Hello {userName}! Ready to stay safe online? 😊");
            PrintSeparator();
            WriteBotMessage("You can ask me about: password, phishing, malware, wifi, 2fa, encryption, ransomware, or just type 'exit' to quit.");
            PrintSeparator();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n{userName}: ");
                Console.ResetColor();
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("You didn't type anything. Please ask a question or type 'exit'.");
                    continue;
                }

                input = input.ToLower().Trim();

                if (input == "exit" || input == "quit" || input == "bye")
                {
                    Think();  // even goodbye gets a short think for consistency
                    WriteBotMessage($"Goodbye {userName}! Stay cyber‑safe, and remember: think before you click! ");
                    break;
                }

                // Emotional response (already includes Think inside)
                /*if (TryEmotionalResponse(input))
                    continue;
                */
                if (input.Contains("how are you"))
                {
                    Think();
                    WriteBotMessage("I'm just a bunch of code, but I'm functioning perfectly!  More importantly, how are YOU doing today?");
                    continue;
                }
                if (input.Contains("what can i ask") || input.Contains("help") || input == "help")
                {
                    Think();
                    WriteBotMessage("You can ask me about: password, phishing, malware, virus, encryption, firewall, 2fa, wifi, ransomware, privacy, safe browsing, update, or just type 'exit'.");
                    continue;
                }

                // Keyword-based cybersecurity responses
                bool found = false;
                foreach (var key in responses.Keys)
                {
                    if (input.Contains(key))
                    {
                        Think();  // show thinking before answering
                        WriteBotMessage($"{userName}, {responses[key]}");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Think();  // still think before saying "I didn't understand"
                    WriteError("I didn't quite understand that.  Could you rephrase? Try words like: password, phishing, malware, wifi, 2fa, or type 'help'.");
                }
            }
        }
    }
}