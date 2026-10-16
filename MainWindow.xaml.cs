using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Media;
using System.Windows.Input;

namespace CybersecurityAwarenessBot.GUI
{
    public partial class MainWindow : Window
    {
        private string userName = "";
        private string favouriteTopic = "";
        private string currentTopic = "password";
        private readonly Random random = new Random();

        private readonly Dictionary<string, List<string>> tips =
            new Dictionary<string, List<string>>
            {
                {
                    "password",
                    new List<string>
                    {
                        "Use a long, unique password for every account.",
                        "Never share your passwords, even with someone you trust.",
                        "Use a password manager to store passwords securely.",
                        "Enable multi-factor authentication wherever possible."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        "Check the sender's email address before clicking links.",
                        "Never enter your password through a suspicious email link.",
                        "Unexpected messages demanding urgent action may be phishing.",
                        "Verify suspicious messages through the organisation's official website."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                        "Never pay unexpected fees to claim a prize you did not enter for.",
                        "Do not share banking PINs or one-time passwords with callers.",
                        "Be suspicious of offers that promise guaranteed money.",
                        "Verify payment notifications in your banking app before releasing goods."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Avoid sharing your home address and personal details publicly.",
                        "Review the privacy settings on your social media accounts.",
                        "Only grant apps the permissions they genuinely need.",
                        "Avoid accessing sensitive accounts on public Wi-Fi without protection."
                    }
                }
            };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message first.");
                return;
            }

            AddChatBubble(message, true);
            MessageInput.Clear();

            string response = GetResponse(message);
            AddChatBubble(response, false);
        }

        private string GetResponse(string message)
        {
            string text = message.ToLowerInvariant();

            // Remember the user's name.
            if (text.StartsWith("my name is "))
            {
                userName = message.Substring(11).Trim().TrimEnd('.', '!', '?');

                if (userName.Length > 0)
                    return "Nice to meet you, " + userName +
                           "! I will remember your name during this session.";
            }

            // Remember the user's favourite topic.
            if (text.Contains("my favourite topic is ") ||
                text.Contains("my favorite topic is "))
            {
                int position = text.IndexOf("topic is ") + 9;
                string topic = message.Substring(position).Trim()
                    .TrimEnd('.', '!', '?');

                string foundTopic = FindTopic(topic);

                if (foundTopic != "")
                {
                    favouriteTopic = foundTopic;
                    currentTopic = foundTopic;

                    return "I'll remember that your favourite topic is " +
                           favouriteTopic + " during this session!";
                }

                return "I'll remember your favourite topic. Try choosing " +
                       "passwords, phishing, scams or privacy.";
            }

            // Identify the topic mentioned in the message.
            string detectedTopic = FindTopic(text);

            if (detectedTopic != "")
                currentTopic = detectedTopic;

            // Detect emotions and respond with a tip immediately.
            if (ContainsAny(text, "worried", "scared", "afraid",
                            "anxious", "nervous", "concerned"))
            {
                return "It's understandable to feel worried about online safety. " +
                       "Take things one step at a time. Here's a tip: " +
                       GetRandomTip(currentTopic);
            }

            if (ContainsAny(text, "frustrated", "annoyed", "confused",
                            "angry", "upset"))
            {
                return "I understand that this can be frustrating. Let's keep it " +
                       "simple. Here's one useful step: " + GetRandomTip(currentTopic);
            }

            if (ContainsAny(text, "curious", "interested", "wondering"))
            {
                return "Great question! Learning about cybersecurity helps you " +
                       "stay safe online. Here's a tip: " + GetRandomTip(currentTopic);
            }

            // Follow-up questions use the current topic.
            if (ContainsAny(text, "another tip", "more tips",
                            "tell me more", "explain more",
                            "more information", "go on"))
            {
                if (text.Contains("explain") || text.Contains("information"))
                {
                    return GetExplanation(currentTopic);
                }

                return GetRandomTip(currentTopic);
            }

            if (text.Contains("thank"))
                return "You're welcome" +
                       (userName != "" ? ", " + userName : "") +
                       "! Staying safe online starts with small habits.";

            if (ContainsAny(text, "hello", "hi ", "hey", "good morning",
                            "good afternoon"))
            {
                return "Hello" + (userName != "" ? ", " + userName : "") +
                       "! Ask me about passwords, phishing, scams or privacy.";
            }

            // Give a response based on the recognised topic.
            if (detectedTopic != "")
            {
                string response = GetRandomTip(detectedTopic);

                if (detectedTopic == favouriteTopic && favouriteTopic != "")
                    response = "Since " + favouriteTopic +
                               " is your favourite topic: " + response;

                return response;
            }

            // Default response for unknown messages.
            return "I'm still learning about that. You can ask me about " +
                   "passwords, phishing, scams or online privacy. " +
                   "You can also say 'another tip' or 'explain more'.";
        }

        private string FindTopic(string text)
        {
            if (ContainsAny(text, "password", "passcode", "login"))
                return "password";

            if (ContainsAny(text, "phishing", "suspicious email",
                            "fake email", "email link"))
                return "phishing";

            if (ContainsAny(text, "scam", "fraud", "fake prize",
                            "online fraud"))
                return "scam";

            if (ContainsAny(text, "privacy", "personal information",
                            "personal data", "private details"))
                return "privacy";

            return "";
        }

        private string GetRandomTip(string topic)
        {
            if (!tips.ContainsKey(topic))
                topic = "password";

            List<string> topicTips = tips[topic];
            return topicTips[random.Next(topicTips.Count)];
        }

        private string GetExplanation(string topic)
        {
            switch (topic)
            {
                case "password":
                    return "Strong passwords are long and difficult to guess. " +
                           "Use a different password for each account and enable " +
                           "multi-factor authentication for extra protection.";

                case "phishing":
                    return "Phishing is when criminals pretend to be trusted " +
                           "people or organisations to steal information. Check " +
                           "the sender, avoid suspicious links and verify requests " +
                           "using official contact details.";

                case "scam":
                    return "Online scams trick people into sending money or " +
                           "revealing information. Be cautious of urgent requests, " +
                           "unexpected prizes and offers that seem too good to be true.";

                case "privacy":
                    return "Online privacy means controlling who can access your " +
                           "personal information. Review app permissions, secure " +
                           "your accounts and think before sharing personal details.";

                default:
                    return "Cybersecurity means protecting your devices, accounts " +
                           "and personal information from online threats.";
            }
        }

        private bool ContainsAny(string text, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (text.Contains(keyword))
                    return true;
            }

            return false;
        }

        private void AddChatBubble(string message, bool isUser)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(
                    isUser
                    ? Color.FromRgb(101, 230, 209)
                    : Color.FromRgb(38, 56, 82)),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalAlignment = isUser
                    ? HorizontalAlignment.Right
                    : HorizontalAlignment.Left,
                MaxWidth = 600
            };

            TextBlock messageText = new TextBlock
            {
                Text = (isUser ? "You: " : "Assistant: ") + message,
                Foreground = new SolidColorBrush(
                    isUser
                    ? Color.FromRgb(16, 24, 39)
                    : Colors.White),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 15
            };

            bubble.Child = messageText;
            ChatPanel.Children.Add(bubble);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the ASCII logo from the Assets folder.
            string logoPath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                "ascii-logo.txt");

            if (File.Exists(logoPath))
            {
                LogoText.Text = File.ReadAllText(logoPath);
            }
            else
            {
                LogoText.Text = "CYBERSECURITY AWARENESS";
            }

            // Play the greeting audio when the window opens.
            string audioPath = Path.Combine(
                AppContext.BaseDirectory,
                "Audio",
                "greeting.wav");

            if (File.Exists(audioPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.Play();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "The voice greeting could not be played.",
                        "Audio Notice");
                }
            }
        }

        private void MessageInput_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                SendButton_Click(sender, new RoutedEventArgs());
            }
        }

        private void ClearChatButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ChatPanel.Children.Clear();

            AddChatBubble(
                "Chat cleared! Ask me about passwords, phishing, scams or privacy.",
                false);

            MessageInput.Clear();
        }
    }
}