using AtmMachine.Hardware.Screen;
using AtmMachine.Models;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Hardware.CardReader.Implementation;

class CardReader : ICardReader
{
    #region PublicMethod
    public Card? ReadCard()
    {
        const int maxAttempts = 3;
        var attemptsRemaining = maxAttempts;

        Console.Clear();
        AtmScreen.DisplayHighlightedText("Card Reader");

        string? cardNumber;
        do
        {
            Console.WriteLine("Enter Card Number");
            cardNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(cardNumber))
            {
                AtmScreen.DisplayErrorMessage("CardNumber cannot be empty.");
                return null;
            }

            if ((cardNumber.Length != 16  && cardNumber.Length!=3)|| !cardNumber.All(char.IsDigit))
            {
                AtmScreen.DisplayErrorMessage("Invalid Card Number. Must be exactly 16 digits.");
                if (--attemptsRemaining == 0)
                {
                    return FailWithAttemptsReached();
                }

                if (!PromptRetry(attemptsRemaining))
                {
                    return null;
                }
            }
            else
            {
                break;
            }
        } while (attemptsRemaining > 0);

        attemptsRemaining = maxAttempts;
        do
        {
            Console.WriteLine("Enter Pin");
            var input = InputReader.ReadSensitiveData();
            if (!int.TryParse(input, out var pin) || pin < 1000 || pin > 9999)
            {
                AtmScreen.DisplayErrorMessage("Invalid PIN. Enter a 4-digit PIN.");
                if (--attemptsRemaining == 0)
                {
                    return FailWithAttemptsReached();
                }

                if (!PromptRetry(attemptsRemaining))
                {
                    return null;
                }
            }
            else
            {
                return new Card(Convert.ToInt32(cardNumber), pin);
            }
        } while (attemptsRemaining > 0);

        return null; 
    }
    #endregion

    #region PrivateMethods
    private static bool PromptRetry(int attemptsRemaining)
    {
        AtmScreen.DisplayHighlightedText($"Attempts Remaining: {attemptsRemaining}");
        var choice = MenuOptionSelector.GetRetryChoice();
        return choice == 1;
    }

    private static Card? FailWithAttemptsReached()
    {
        AtmScreen.DisplayWarningMessage("Attempts Limit Reached.");
        return null;
    }
    #endregion
}
