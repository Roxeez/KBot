using System.Threading.Tasks;
using C5;
using System;
using System.Collections.Generic;
using Spark.Pathfinder.Grid;
 
 /*
  * ORIGINAL https://github.com/juhgiyo/EpPathFinding.cs
  * TODO : Remove all unity related stuff
  */
 namespace Spark.Pathfinder
{
    public class AStarParam : ParamBase
    {
        public delegate float HeuristicDelegate(int iDx, int iDy);


        public float Weight;

        public AStarParam(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, float iweight, DiagonalMovement iDiagonalMovement = DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid,iStartPos,iEndPos, iDiagonalMovement,iMode)
        {
            Weight = iweight;
        }

        public AStarParam(BaseGrid iGrid, float iweight, DiagonalMovement iDiagonalMovement = DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iDiagonalMovement, iMode)
        {
            Weight = iweight;
        }

        internal override void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
        {

        }
    }
    public static class AStarFinder
    {
        /*
        private class NodeComparer : IComparer<Node>
        {
            public int Compare(Node x, Node y)
            {
                var result = (x.heuristicStartToEndLen - y.heuristicStartToEndLen);
                if (result < 0) return -1;
                else
                if (result > 0) return 1;
                else
                {
                    return 0;
                }
            }
        }
        */
        public static List<GridPos> FindPath(AStarParam iParam)
        {
            var lo = new object();
            //var openList = new IntervalHeap<Node>(new NodeComparer());
            var openList = new IntervalHeap<Node>();
            Node startNode = iParam.StartNode;
            Node endNode = iParam.EndNode;
            HeuristicDelegate heuristic = iParam.HeuristicFunc;
            BaseGrid grid = iParam.SearchGrid;
            DiagonalMovement diagonalMovement = iParam.DiagonalMovement;
            float weight = iParam.Weight;


            startNode.StartToCurNodeLen = 0;
            startNode.HeuristicStartToEndLen = 0;

            openList.Add(startNode);
            startNode.IsOpened = true;

            while (openList.Count != 0)
            {
                Node node = openList.DeleteMin();
                node.IsClosed = true;

                if (node == endNode)
                {
                    return Node.Backtrace(endNode);
                }

                List<Node> neighbors = grid.GetNeighbors(node, diagonalMovement);

#if (UNITY)
                foreach(var neighbor in neighbors)
#else
                Parallel.ForEach(neighbors, neighbor =>
#endif
                {
#if (UNITY)
                    if (neighbor.isClosed) continue;
#else
                    if (neighbor.IsClosed) return;
#endif
                    int x = neighbor.X;
                    int y = neighbor.Y;
                    float ng = node.StartToCurNodeLen + (float)((x - node.X == 0 || y - node.Y == 0) ? 1 : Math.Sqrt(2));

                    if (!neighbor.IsOpened || ng < neighbor.StartToCurNodeLen)
                    {
                        neighbor.StartToCurNodeLen = ng;
                        if (neighbor.HeuristicCurNodeToEndLen == null) neighbor.HeuristicCurNodeToEndLen = weight * heuristic(Math.Abs(x - endNode.X), Math.Abs(y - endNode.Y));
                        neighbor.HeuristicStartToEndLen = neighbor.StartToCurNodeLen + neighbor.HeuristicCurNodeToEndLen.Value;
                        neighbor.Parent = node;
                        if (!neighbor.IsOpened)
                        {
                            lock (lo)
                            {
                                openList.Add(neighbor);
                            }
                            neighbor.IsOpened = true;
                        }
                        else
                        {

                        }
                    }
                }
#if (!UNITY)
                );
#endif
            }
            return new List<GridPos>();

        }
    }
}
