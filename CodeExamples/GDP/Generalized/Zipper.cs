namespace GdpZip
{
    using GDP;
    using Nat;
    
    public class Length<Xs> {
        internal Length() {}
    }
    
    public class Zipper
    {
        public static Named<int, Length<Xs>> Len<Xs>(Named<List<int>, Xs> xs)
        {
            return Named<int, Length<Xs>>.Defn(xs.GetVal().Count, new Length<Xs>());
        }
        
        public static Proof<Equals<Length<Xs>, Succ<Succ<Zero>>>> LengthIsTwo<Xs>(Named<int, Length<Xs>> len)
        {
            if (len.GetVal() == 2)
            {
                return Proof<Equals<Length<Xs>, Succ<Succ<Zero>>>>.Axiom();
            }
            return null;
        }
        
        public static List<Tuple<int, int>> Zip<Xs, Ys, N>(
            SuchThat<Named<List<int>, Xs>, Equals<Length<Xs>, N>> xs,
            SuchThat<Named<List<int>, Ys>, Equals<Length<Ys>, N>> ys)
        {
            return xs.GetVal().GetVal().Zip(ys.GetVal().GetVal(), (x, y) => new Tuple<int, int>(x, y)).ToList();
        }
    }
}