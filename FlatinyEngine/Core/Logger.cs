using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine
{
    public static class Logger
    {
        private static List<Message> messages { get; set; } = new List<Message>();
        
        public static int Limit = 20;

        private static void Log<T>(T message, Message.TYPE type)
        {
            if (message == null)
                return;
            var messageValue = message.ToString();

            var match = messages
           .FirstOrDefault(stringToCheck => stringToCheck.Value == messageValue);

            if (match != null && match.Type == type)
            {
                match.Count++;
            }
            else
            {
                if (messages.Count >= Limit)
                {
                    messages.RemoveAt(0);
                }

                messages.Add(new Message() { Value = messageValue, Count = 1, Type = type });
            }
            UpdateDisplay();
        }

        public static void EditLastMessage<T>(T newMessage)
        {
            if (messages.Count == 0)
                return;

            messages.Last().Value = newMessage.ToString();
            UpdateDisplay();
        }

        public static void LogError<T>(T message)
        {
            Log(message, Message.TYPE.ErrorMessage);
        }

        public static void LogWarning<T>(T message)
        {
            Log(message, Message.TYPE.WarningMessage);
        }

        public static void Log<T>(T message)
        {
            Log(message, Message.TYPE.InfoMessage);
        }

        private static void UpdateDisplay()
        {
            Console.Clear();

            for (int i = 0; i < messages.Count; i++)
            {
                switch (messages[i].Type)
                {
                    case Message.TYPE.WarningMessage:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case Message.TYPE.ErrorMessage:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                if(messages[i].Count > 1)
                    Console.WriteLine(messages[i].Value + "(" + messages[i].Count + ")");
                else
                    Console.WriteLine(messages[i].Value);
            }

            Console.ResetColor();
        }

        public class Message
        {
            public string Value;
            public int Count;
            public TYPE Type = TYPE.InfoMessage;

            public bool Equals(string obj)
            {
                return obj == Value;
            }

            public enum TYPE
            {
                InfoMessage,
                WarningMessage,
                ErrorMessage
            }
        }



    }
}
