namespace GDP
{
    public struct Named<A, N>
    {
        private A val;
  
        public Named(A val)
        {
            this.val = val;
        }
            
        public A GetVal()
        {
            return this.val;
        }
    }
    
    struct Unit {}
    
    public class Namer
    {
        public static R Name<A, R>(A a, NameFn<A, R> f)
        {
            return f.Call(new Named<A, Unit>(a));
        }
    }
    
    public interface NameFn<Input, Output>
    {
        Output Call<Name>(Named<Input, Name> named);
    }
}