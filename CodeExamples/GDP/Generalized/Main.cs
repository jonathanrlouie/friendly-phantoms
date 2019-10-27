namespace Main
{
    using GDP;
    using Nat;
    using GdpZip;
    
    public class Program
    {
        public struct List1Fn : NameFn<List<int>, List<Tuple<int, int>>> {
            public List<Tuple<int, int>> Call<Name>(Named<List<int>, Name> named) {
                return GDP.Namer.Name(new List<int>() { 4, 5 }, new ZipFn<Name>(named));
            }
        }
        
        public struct ZipFn<Xs> : NameFn<List<int>, List<Tuple<int, int>>> {
            private Named<List<int>, Xs> xs;
            
            public ZipFn(Named<List<int>, Xs> xs)
            {
                this.xs = xs;
            }
            
            public List<Tuple<int, int>> Call<Name>(Named<List<int>, Name> named) {
                var len1 = Zipper.Len(this.xs);
                var len2 = Zipper.Len(named);
                
                var proof1 = Zipper.LengthIsTwo(len1);
                var proof2 = Zipper.LengthIsTwo(len2);
                
                if (proof1 != null && proof2 != null)
                {
                    return Zipper.Zip(
                        new SuchThat<Named<List<int>, Xs>, Equals<Length<Xs>, Succ<Succ<Zero>>>>(this.xs, proof1), 
                        new SuchThat<Named<List<int>, Name>, Equals<Length<Name>, Succ<Succ<Zero>>>>(named, proof2));
                }
                throw new Exception("Lists were not of length 2.");
            }
        }
        
        public static void Main(string[] args)
        {
            List<Tuple<int, int>> result = GDP.Namer.Name(new List<int>() { 1, 2 }, new List1Fn());
            
            foreach (Tuple<int, int> tuple in result)
            {
                Console.WriteLine(tuple.Item1.ToString() + ", " + tuple.Item2.ToString());
            }
        }
    }
}