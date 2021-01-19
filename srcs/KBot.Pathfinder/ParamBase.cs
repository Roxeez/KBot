﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 using Spark.Pathfinder.Grid;

 namespace Spark.Pathfinder
{
    public delegate float HeuristicDelegate(int iDx, int iDy);

    public abstract class ParamBase
    {
        public ParamBase(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, DiagonalMovement iDiagonalMovement, HeuristicMode iMode) : this(iGrid, iDiagonalMovement, iMode)
        {
            MStartNode = MSearchGrid.GetNodeAt(iStartPos.X, iStartPos.Y);
            MEndNode = MSearchGrid.GetNodeAt(iEndPos.X, iEndPos.Y);
            if (MStartNode == null)
                MStartNode = new Node(iStartPos.X, iStartPos.Y, true);
            if (MEndNode == null)
                MEndNode = new Node(iEndPos.X, iEndPos.Y, true);
        }

        public ParamBase(BaseGrid iGrid, DiagonalMovement iDiagonalMovement, HeuristicMode iMode)
        {
            SetHeuristic(iMode);

            MSearchGrid = iGrid;
            DiagonalMovement = iDiagonalMovement;
            MStartNode = null;
            MEndNode = null;
        }

        public ParamBase(ParamBase param)
        {
            MSearchGrid = param.MSearchGrid;
            DiagonalMovement = param.DiagonalMovement;
            MStartNode = param.MStartNode;
            MEndNode = param.MEndNode;
            
        }

        internal abstract void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null);

        public void Reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
        {
            _reset(iStartPos, iEndPos, iSearchGrid);
            MStartNode = null;
            MEndNode = null;

            if (iSearchGrid != null)
                MSearchGrid = iSearchGrid;
            MSearchGrid.Reset();
            MStartNode = MSearchGrid.GetNodeAt(iStartPos.X, iStartPos.Y);
            MEndNode = MSearchGrid.GetNodeAt(iEndPos.X, iEndPos.Y);
            if (MStartNode == null)
                MStartNode = new Node(iStartPos.X, iStartPos.Y, true);
            if (MEndNode == null)
                MEndNode = new Node(iEndPos.X, iEndPos.Y, true);
        }

        public DiagonalMovement DiagonalMovement;
        public HeuristicDelegate HeuristicFunc
        {
            get
            {
                return MHeuristic;
            }
        }

        public BaseGrid SearchGrid
        {
            get
            {
                return MSearchGrid;
            }
        }

        public Node StartNode
        {
            get
            {
                return MStartNode;
            }
        }
        public Node EndNode
        {
            get
            {
                return MEndNode;
            }
        }

        public void SetHeuristic(HeuristicMode iMode)
        {
            MHeuristic = null;
            switch (iMode)
            {
                case HeuristicMode.Manhattan:
                    MHeuristic = new HeuristicDelegate(Heuristic.Manhattan);
                    break;
                case HeuristicMode.Euclidean:
                    MHeuristic = new HeuristicDelegate(Heuristic.Euclidean);
                    break;
                case HeuristicMode.Chebyshev:
                    MHeuristic = new HeuristicDelegate(Heuristic.Chebyshev);
                    break;
                default:
                    MHeuristic = new HeuristicDelegate(Heuristic.Euclidean);
                    break;
            }
        }

        protected BaseGrid MSearchGrid;
        protected Node MStartNode;
        protected Node MEndNode;
        protected HeuristicDelegate MHeuristic;
    }
}
