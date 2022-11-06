namespace Beta_comparing
{
    [System.Serializable]
    public class Node
    {
        public int value;
        public int depth;
        public int childCount;
        public List<Node> children;
    }
    internal class Program
    {
        static List<Node> leafs;
        static int MAX = 5000;
        static int MIN = -5000;
        static List<int> numbers;
        static Random rng = new Random();
        static int visitCount = 0;
        static int ModifVisitCount = 0;
        static int x, y, pp;
        static int DiffirencePlusCount = 0;
        static int totalNodeCount = 0;
        static int treeChildCount, treeDepth;

        static void Main(string[] args)
        {
            Console.WriteLine("input treeChildCount");
            treeChildCount = Convert.ToInt32(Console.ReadLine());
            for (int j = 2; j < 11; j++)
            {

                // Console.WriteLine("input treeDepth");
                treeDepth = j;// Convert.ToInt32(Console.ReadLine());

                decimal ModifVisitCountAve = 0;
                decimal VisitDifAve = 0;
                decimal VisitCountAve = 0;
                leafs = new List<Node>();
                var mainNode = CreateMainBranch(treeChildCount, treeDepth);
                CreateNumbers();
                //Console.WriteLine($"{"ResultOfAlphaBeta"} {"ResultOfModifiedAlphaBeta",30} {"x",3} {"y",3} {"VisitCount",13} {"ModifiedVisitCount",13} {"VisitDifference",17}\n");
                for (int i = 0; i < 100; i++)
                {
                    CompareAlgorithms(mainNode);
                    ModifVisitCountAve += ModifVisitCount;
                    VisitCountAve += visitCount;
                    VisitDifAve += visitCount - ModifVisitCount;
                }
                ModifVisitCountAve /= 100;
                VisitDifAve /= 100;
                VisitCountAve /= 100;
                Console.WriteLine("depth = {0} \n ", treeDepth);
                Console.WriteLine("totalNodeCount = {0} \n ", totalNodeCount);
                Console.WriteLine("Number of times when Modified faster = {0} \n ", DiffirencePlusCount);
                Console.WriteLine("Modified Node Visit Count Average = {0} \n ", ModifVisitCountAve);
                Console.WriteLine("Node Visit Count Average = {0} \n ", VisitCountAve);
                //Console.WriteLine("Visit Difference between AlphaBeta & Modified (if + Modif better) = {0} \n ", VisitDifAve);
                Console.WriteLine("Modified Node Visit Count Average/totalNodeCount = {0} \n ", ModifVisitCountAve/totalNodeCount);
                Console.WriteLine("Visit Count Average/totalNodeCount = {0} \n ", VisitCountAve / totalNodeCount);
                ModifVisitCount = 0;
                visitCount=0;
                DiffirencePlusCount = 0;
                totalNodeCount = 0;
                ModifVisitCountAve=0;
                VisitDifAve=0;
                VisitCountAve = 0;
            }
        }

        private static void CompareAlgorithms(Node mainNode)
        {
            ShuffleIntervalAndNodes();
            int resultOfAlphaBeta = AlphaBeta(0, true, mainNode, MIN, MAX);

            int resultOfModAlphaBeta = ModifiedAlphaBeta(0, true, mainNode, MIN, MAX);
            DiffirencePlusCount = visitCount - ModifVisitCount > 0 ? DiffirencePlusCount + 1 : DiffirencePlusCount;
            //Console.WriteLine($"{resultOfAlphaBeta,-25} {resultOfModAlphaBeta,-24} {x,-3} {y,-7} {visitCount,-10} {ModifVisitCount,-20} {visitCount - ModifVisitCount,-10}\n");
        }

        private static void ShuffleIntervalAndNodes()
        {
            Shuffle(numbers);
            SetLeafs();
            SetInterval();
            visitCount = 0;
            ModifVisitCount = 0;
        }

        static void SetInterval()
        {
            Random rng = new Random();
            pp = rng.Next(leafs.Count);
            int interval = rng.Next(100);
            x = pp - interval;
            y = pp + interval;
        }
        static void CreateNumbers()
        {
            numbers = new List<int>() /*{7,15,9,3,11,2,8,4,1,0,5,12,10,6,13,14 }*/;
            for (int i = 0; i < leafs.Count; i++)
            {
                numbers.Add(i);
            }
        }
        static void SetLeafs()
        {
            for (int i = 0; i < leafs.Count; i++)
            {
                leafs[i].value = numbers[i];
            }
        }
        static Node CreateMainBranch(int childCount, int depth)
        {
            Node node = new Node();
            node.depth = 0;
            node.childCount = childCount;
            node.children = CreateBranches(childCount, 1, depth);
            totalNodeCount = (int)Math.Pow(childCount, depth + 1) - 1;
            return node;
        }
        static List<Node> CreateBranches(int childCount, int depth, int maxDepth)
        {
            if (maxDepth == 0)
                return null;
            List<Node> branches = new List<Node>();
            for (int i = 0; i < childCount; i++)
            {
                Node node = new Node();
                node.depth = depth;
                node.childCount = childCount;
                node.children = CreateBranches(childCount, depth++, maxDepth - 1);
                branches.Add(node);
                if (maxDepth == 1)
                {
                    leafs.Add(node);
                }
            }
            return branches;
        }


        // Returns optimal value for
        // current player (Initially called
        // for root and maximizer)
        static int AlphaBeta(int depth, Boolean maximizingPlayer, Node node, int alpha, int beta)
        {
            // Terminating condition. i.e
            // leaf node is reached
            visitCount++;
            if (depth == treeDepth)
                return node.value;

            if (maximizingPlayer)
            {
                int best = MIN;

                // Recur for left and
                // right children
                for (int i = 0; i < node.childCount; i++)
                {
                    int val = AlphaBeta(depth + 1, false, node.children[i], alpha, beta);
                    best = Math.Max(best, val);
                    alpha = Math.Max(alpha, best);

                    // Alpha Beta Pruning
                    if (beta <= alpha)
                        break;
                }
                return best;
            }
            else
            {
                int best = MAX;

                // Recur for left and
                // right children
                for (int i = 0; i < node.childCount; i++)
                {

                    int val = AlphaBeta(depth + 1, true, node.children[i], alpha, beta);
                    best = Math.Min(best, val);
                    beta = Math.Min(beta, best);

                    // Alpha Beta Pruning
                    if (beta <= alpha)
                        break;
                }
                return best;
            }
        }
        static int ModifiedAlphaBeta(int depth, Boolean maximizingPlayer, Node node, int alpha, int beta)
        {
            // Terminating condition. i.e
            // leaf node is reached
            ModifVisitCount++;
            if (depth == treeDepth)
                return node.value;

            if (maximizingPlayer)
            {
                int best = MIN;

                // Recur for left and
                // right children
                for (int i = 0; i < node.childCount; i++)
                {
                    int val = ModifiedAlphaBeta(depth + 1, false, node.children[i], alpha, beta);
                    if (val < x || val > y)
                        continue;
                    best = Math.Max(best, val);
                    alpha = Math.Max(alpha, best);

                    // Alpha Beta Pruning
                    if (beta <= alpha)
                        break;
                }
                return best;
            }
            else
            {
                int best = MAX;

                // Recur for left and
                // right children
                for (int i = 0; i < node.childCount; i++)
                {

                    int val = ModifiedAlphaBeta(depth + 1, true, node.children[i], alpha, beta);
                    if (val < x || val > y)
                        continue;
                    best = Math.Min(best, val);
                    beta = Math.Min(beta, best);

                    // Alpha Beta Pruning
                    if (beta <= alpha)
                        break;

                }
                return best;
            }
        }
        public static void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}