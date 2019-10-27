namespace Nat
{
    // There are better encodings, but we'll just use classic Peano numbers
    public interface Nat {}
    public class Succ<T> : Nat where T: Nat {}
    public class Zero : Nat {}
}