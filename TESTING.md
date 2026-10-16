# Cybersecurity Awareness Assistant GUI — Testing Checklist

## Functional Tests

| Test | Expected Result |
|---|---|
| Enter a password-related question | The chatbot provides a password security tip. |
| Enter a phishing-related question | The chatbot provides phishing guidance. |
| Enter a privacy-related question | The chatbot provides a privacy tip. |
| Ask for another tip | The chatbot provides a relevant follow-up. |
| Share a name | The chatbot remembers the name during the session. |
| Express worry or frustration | The chatbot responds empathetically. |
| Submit an unknown question | The chatbot provides a fallback response. |
| Submit blank input | The application handles empty input safely. |
| Launch the application | The greeting and logo load as intended. |

## Testing Notes

Run each test in the WPF application and record the actual result before marking it as passed. Results should reflect the behaviour observed during testing.
