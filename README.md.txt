Cybersecurity Awareness Assistant — Part 2

Table of Contents

Overview
Features
Technologies
Project Structure
How to Run
Testing
Author
Overview

The Cybersecurity Awareness Assistant is a C# application that helps users learn about online safety. Part 2 introduces a graphical interface using Windows Presentation Foundation (WPF), building on the original console application.

Features

Graphical chat interface
Password, phishing, scam and privacy guidance
Randomised cybersecurity tips
Follow-up questions and explanations
Session-based memory for the user’s name and favourite topic
Basic sentiment detection and supportive responses
Default responses for unrecognised questions
WAV voice greeting
ASCII artwork
Enter-to-send and Clear Chat functionality
Technologies

C#
.NET 8
WPF and XAML
Visual Studio 2022
Project Structure

CybersecurityAwarenessBot.GUI/ — graphical application
Audio/ — WAV greeting
Assets/ — ASCII artwork

How to Run
Clone or download this repository.
Open the solution in Visual Studio 2022.
Set CybersecurityAwarenessBot.GUI as the startup project.
Build the solution.
Run the application.
Testing

Test password, phishing, scam and privacy questions, follow-up prompts, sentiment-related messages, memory, empty input and unknown questions. Confirm that the voice greeting and artwork load correctly.

Author

ST10498503

Project Status

Part 2 — GUI and dynamic cybersecurity responses.