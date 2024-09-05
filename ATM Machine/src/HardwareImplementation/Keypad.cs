using ATM_Machine.HardwareInterface;

namespace ATM_Machine.HardwareImplementation;

public class Keypad: IKeyPad
{
    public string ReadKeyPad()
    {
        var input = Console.ReadLine();
        return input;
    }
}