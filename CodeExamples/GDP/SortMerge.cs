namespace SortMerge
{
    using GDP;
    
    public struct SortedBy<A, Comp> {
        private List<A> list;
  
        public SortedBy(List<A> list) {
            this.list = list;
        }
            
        public List<A> GetList() {
            return this.list;
        }
    }
    
    public class SortMerger {
        // If only we could "consume" the lists somehow... ;)
        // We can work around this with immutability at least
        public static SortedBy<int, Comp> GdpSortBy<Comp>(List<int> list, Named<Comparison<int>, Comp> comp)
        {
            List<int> listCopy = list.Select(item => item).ToList();
            listCopy.Sort(comp.GetVal());
            return new SortedBy<int, Comp>(listCopy);
        }

        public static List<int> GdpMergeBy<Comp>(SortedBy<int, Comp> l1, SortedBy<int, Comp> l2, Named<Comparison<int>, Comp> comp)
        {
            return MergeBy(l1.GetList(), l2.GetList(), comp.GetVal());
        }

        public static List<int> MergeBy(List<int> l1, List<int> l2, Comparison<int> comp)
        {
            List<int> list1Copy = l1.Select(item => item).ToList();
            list1Copy.AddRange(l2);
            list1Copy.Sort(comp);
            return list1Copy;
        }
    }
}

namespace Rextester
{
    using GDP;
    using SortMerge;
    
    public class Program
    {
        public struct MyFn : NameFn<Comparison<int>, List<int>> {
            public List<int> Call<Name>(Named<Comparison<int>, Name> named) {
                List<int> l1 = new List<int> {1, 5, 2};
                List<int> l2 = new List<int> {9, 3, 7};
                SortedBy<int, Name> sorted1 = SortMerge.SortMerger.GdpSortBy<Name>(l1, named);
                SortedBy<int, Name> sorted2 = SortMerge.SortMerger.GdpSortBy<Name>(l2, named);
                
                return SortMerge.SortMerger.GdpMergeBy(sorted1, sorted2, named);
            }
        }
        
        public static void Main(string[] args)
        {
            List<int> result = GDP.Namer.Name((int x, int y) => x < y ? 0 : 1, new MyFn());
            foreach (int i in result) {
                Console.WriteLine(i);
            }
        }
    }
}