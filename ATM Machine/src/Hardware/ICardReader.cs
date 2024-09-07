using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

public interface ICardReader
{
    static Card card { get; set; }
    static abstract Card ReadCard();
    // Reason of putting abstract keyword here:
    // The intend of static keyword is not to inherit the method in the child class.
    // Static methods as attached to class.
    // Hence if the intend is not to inherit the method,
    // then it is expected that we will declare the body in the same class.
    // And if we are not declaring the body in the same class,
    // then we have to mentiond it that the method is abstract.
}