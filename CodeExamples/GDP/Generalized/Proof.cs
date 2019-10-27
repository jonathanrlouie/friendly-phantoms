namespace GDP
{
    public class Proof<P>
    {
        internal Proof() {}
        
        public static Proof<P> Axiom()
        {
            return new Proof<P>();
        }
    }
    
    public struct SuchThat<A, P>
    {
        private A val;
        
        public SuchThat(A val, Proof<P> proof)
        {
            this.val = val;
        }
        
        public A GetVal()
        {
            return this.val;
        }
    }
    
    public class True
    {
        internal True() {}
    }
    
    public class False
    {
        internal False() {}
    }
    
    public class And<P, Q>
    {
        internal And() {}
    }
    
    public class Or<P, Q>
    {
        internal Or() {}
    }
    
    public class Implies<P, Q>
    {
        internal Implies() {}
    }
    
    public class Not<P>
    {
        internal Not() {}
    }
    
    public class Equals<P, Q>
    {
        internal Equals() {}
    }
    
    public class Combinators {
        public static Proof<And<P, Q>> AndIntro<P, Q>(Proof<P> p, Proof<Q> q)
        {
            return new Proof<And<P, Q>>();
        }
        
        public static Proof<P> AndElimL<P, Q>(Proof<And<P, Q>> pAndQ)
        {
            return new Proof<P>();
        }
        
        public static Proof<Q> AndElimR<P, Q>(Proof<And<P, Q>> pAndQ)
        {
            return new Proof<Q>();
        }
        
        // etc.
    }
}