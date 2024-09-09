using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

public interface ICardReader

//                     Use of Interface
// We use interface when we want that implementing class should contain this functionality.
// We don't know the implementation, we are only concern about its existence. 
// The intent of interface is not to inherit behaviour. It is not inheritance. It is a kind of contract.

{
    // Interfaces methods are public.
    // The reason is that the interface is a contract between the class and the interface.
    // This contract is public so that anyone can implement it.
    // To restrict it, we can use have helper private methods inside the concrete class

    static abstract Card ReadCard();
    // Reason of putting abstract keyword here:
    // The intend of static keyword is not to inherit the method in the child class.
    // Static methods as attached to class.
    // Hence if the intend is not to inherit the method,
    // then it is expected that we will declare the body in the same class.
    // And if we are not declaring the body in the same class,
    // then we have to mentiond it that the method is abstract.
}