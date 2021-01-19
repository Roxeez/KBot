﻿/*! 
@file JumpPointFinder.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief Jump Point Search Algorithm Interface
@version 2.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

@section DESCRIPTION

An Interface for the Jump Point Search Algorithm Class.

*/
using C5;
using System;
using System.Collections.Generic;
using System.Collections;
using Spark.Pathfinder.Grid;


 namespace Spark.Pathfinder
{
    public enum IterationType
    {
        Loop,
        Recursive,
    };

    public enum EndNodeUnWalkableTreatment
    {
        Allow,
        Disallow
    };


    public class JumpPointParam : ParamBase
    {
        [System.Obsolete("This constructor is deprecated, please use the Constructor with EndNodeUnWalkableTreatment and DiagonalMovement instead.")]
        public JumpPointParam(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, bool iAllowEndNodeUnWalkable = true, bool iCrossCorner = true, bool iCrossAdjacentPoint = true, HeuristicMode iMode = HeuristicMode.Euclidean)
            :base(iGrid, iStartPos, iEndPos, Util.GetDiagonalMovement(iCrossCorner, iCrossAdjacentPoint), iMode)
        {
            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable ? EndNodeUnWalkableTreatment.Allow : EndNodeUnWalkableTreatment.Disallow;
            OpenList = new IntervalHeap<Node>();

            CurIterationType = IterationType.Loop;
        }

        [System.Obsolete("This constructor is deprecated, please use the Constructor with EndNodeUnWalkableTreatment and DiagonalMovement instead.")]
        public JumpPointParam(BaseGrid iGrid, bool iAllowEndNodeUnWalkable = true, bool iCrossCorner = true, bool iCrossAdjacentPoint = true, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, Util.GetDiagonalMovement(iCrossCorner, iCrossAdjacentPoint), iMode)
        {
            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable ? EndNodeUnWalkableTreatment.Allow : EndNodeUnWalkableTreatment.Disallow;

            OpenList = new IntervalHeap<Node>();
            CurIterationType = IterationType.Loop;
        }

        [System.Obsolete("This constructor is deprecated, please use the Constructor with EndNodeUnWalkableTreatment and DiagonalMovement instead.")]
        public JumpPointParam(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, bool iAllowEndNodeUnWalkable = true, DiagonalMovement iDiagonalMovement= DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid,iStartPos,iEndPos, iDiagonalMovement, iMode)
        {

            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable ? EndNodeUnWalkableTreatment.Allow : EndNodeUnWalkableTreatment.Disallow;
            OpenList = new IntervalHeap<Node>();

            CurIterationType = IterationType.Loop;
        }

        [System.Obsolete("This constructor is deprecated, please use the Constructor with EndNodeUnWalkableTreatment and DiagonalMovement instead.")]
        public JumpPointParam(BaseGrid iGrid, bool iAllowEndNodeUnWalkable = true, DiagonalMovement iDiagonalMovement= DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iDiagonalMovement, iMode)
        {
            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable ? EndNodeUnWalkableTreatment.Allow : EndNodeUnWalkableTreatment.Disallow;
            
            OpenList = new IntervalHeap<Node>();
            CurIterationType = IterationType.Loop;
        }


        public JumpPointParam(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, EndNodeUnWalkableTreatment iAllowEndNodeUnWalkable = EndNodeUnWalkableTreatment.Allow, DiagonalMovement iDiagonalMovement = DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iStartPos, iEndPos, iDiagonalMovement, iMode)
        {

            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable;
            OpenList = new IntervalHeap<Node>();

            CurIterationType = IterationType.Loop;
        }

        public JumpPointParam(BaseGrid iGrid, EndNodeUnWalkableTreatment iAllowEndNodeUnWalkable = EndNodeUnWalkableTreatment.Allow, DiagonalMovement iDiagonalMovement = DiagonalMovement.Always, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iDiagonalMovement, iMode)
        {
            CurEndNodeUnWalkableTreatment = iAllowEndNodeUnWalkable;

            OpenList = new IntervalHeap<Node>();
            CurIterationType = IterationType.Loop;
        }


        public JumpPointParam(JumpPointParam b):base(b)
        {
            MHeuristic = b.MHeuristic;
            CurEndNodeUnWalkableTreatment = b.CurEndNodeUnWalkableTreatment;

            OpenList = new IntervalHeap<Node>();
            OpenList.AddAll(b.OpenList);

            CurIterationType = b.CurIterationType;
        }


        internal override void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
        {
            OpenList = new IntervalHeap<Node>();
            //openList.Clear();
        }

        [System.Obsolete("This property is deprecated, please use the CurEndNodeUnWalkableTreatment instead.")]
        public bool AllowEndNodeUnWalkable
        {
            get
            {
                return CurEndNodeUnWalkableTreatment == EndNodeUnWalkableTreatment.Allow;
            }
            set
            {
                CurEndNodeUnWalkableTreatment = value ? EndNodeUnWalkableTreatment.Allow : EndNodeUnWalkableTreatment.Disallow;
            }
        }

        [System.Obsolete("This property is deprecated, please use the CurIterationType instead.")]
        public bool UseRecursive
        {
            get
            {
                return CurIterationType==IterationType.Recursive;
            }
            set
            {
                CurIterationType = value ? IterationType.Recursive : IterationType.Loop;
            }
        }

        public EndNodeUnWalkableTreatment CurEndNodeUnWalkableTreatment
        {
            get;
            set;
        }
        public IterationType CurIterationType
        {
            get;
            set;
        }

        //public List<Node> openList;
        public IntervalHeap<Node> OpenList;

    }
    public class JumpPointFinder
    {
        public static List<GridPos> GetFullPath(List<GridPos> routeFound)
        {
            if (routeFound == null)
                return null;
            var consecutiveGridList = new List<GridPos>();
            if (routeFound.Count > 1)
                consecutiveGridList.Add(new GridPos(routeFound[0]));
            for (int routeTrav = 0; routeTrav < routeFound.Count - 1; routeTrav++)
            {
                var fromGrid = new GridPos(routeFound[routeTrav]);
                GridPos toGrid = routeFound[routeTrav + 1];
                int dX = toGrid.X - fromGrid.X;
                int dY = toGrid.Y - fromGrid.Y;

                int nDx = 0;
                int nDy = 0;
                if (dX != 0)
                {
                    nDx = (dX / Math.Abs(dX));
                }
                if (dY != 0)
                {
                    nDy = (dY / Math.Abs(dY));
                }

                while (fromGrid != toGrid)
                {
                    fromGrid.X += nDx;
                    fromGrid.Y += nDy;
                    consecutiveGridList.Add(new GridPos(fromGrid));
                }

            }
            return consecutiveGridList;
        }
        public static List<GridPos> FindPath(JumpPointParam iParam)
        {

            IntervalHeap<Node> tOpenList = iParam.OpenList;
            Node tStartNode = iParam.StartNode;
            Node tEndNode = iParam.EndNode;
            Node tNode;
            bool revertEndNodeWalkable = false;

            // set the `g` and `f` value of the start node to be 0
            tStartNode.StartToCurNodeLen = 0;
            tStartNode.HeuristicStartToEndLen = 0;

            // push the start node into the open list
            tOpenList.Add(tStartNode);
            tStartNode.IsOpened = true;

            if (iParam.CurEndNodeUnWalkableTreatment == EndNodeUnWalkableTreatment.Allow && !iParam.SearchGrid.IsWalkableAt(tEndNode.X, tEndNode.Y))
            {
                iParam.SearchGrid.SetWalkableAt(tEndNode.X, tEndNode.Y, true);
                revertEndNodeWalkable = true;
            }

            // while the open list is not empty
            while (tOpenList.Count > 0)
            {
                // pop the position of node which has the minimum `f` value.
                tNode = tOpenList.DeleteMin();
                tNode.IsClosed = true;

                if (tNode.Equals(tEndNode))
                {
                    if (revertEndNodeWalkable)
                    {
                        iParam.SearchGrid.SetWalkableAt(tEndNode.X, tEndNode.Y, false);
                    }
                    return Node.Backtrace(tNode); // rebuilding path
                }

                IdentifySuccessors(iParam, tNode);
            }

            if (revertEndNodeWalkable)
            {
                iParam.SearchGrid.SetWalkableAt(tEndNode.X, tEndNode.Y, false);
            }

            // fail to find the path
            return new List<GridPos>();
        }

        private static void IdentifySuccessors(JumpPointParam iParam, Node iNode)
        {
            HeuristicDelegate tHeuristic = iParam.HeuristicFunc;
            IntervalHeap<Node> tOpenList = iParam.OpenList;
            int tEndX = iParam.EndNode.X;
            int tEndY = iParam.EndNode.Y;
            GridPos tNeighbor;
            GridPos tJumpPoint;
            Node tJumpNode;

            List<GridPos> tNeighbors = FindNeighbors(iParam, iNode);
            for (int i = 0; i < tNeighbors.Count; i++)
            {
                tNeighbor = tNeighbors[i];
                if (iParam.CurIterationType==IterationType.Recursive)
                    tJumpPoint = Jump(iParam, tNeighbor.X, tNeighbor.Y, iNode.X, iNode.Y);
                else
                    tJumpPoint = JumpLoop(iParam, tNeighbor.X, tNeighbor.Y, iNode.X, iNode.Y);
                if (tJumpPoint != null)
                {
                    tJumpNode = iParam.SearchGrid.GetNodeAt(tJumpPoint.X, tJumpPoint.Y);
                    if (tJumpNode == null)
                    {
                        if (iParam.EndNode.X == tJumpPoint.X && iParam.EndNode.Y == tJumpPoint.Y)
                            tJumpNode = iParam.SearchGrid.GetNodeAt(tJumpPoint);
                    }
                    if (tJumpNode.IsClosed)
                    {
                        continue;
                    }
                    // include distance, as parent may not be immediately adjacent:
                    float tCurNodeToJumpNodeLen = tHeuristic(Math.Abs(tJumpPoint.X - iNode.X), Math.Abs(tJumpPoint.Y - iNode.Y));
                    float tStartToJumpNodeLen = iNode.StartToCurNodeLen + tCurNodeToJumpNodeLen; // next `startToCurNodeLen` value

                    if (!tJumpNode.IsOpened || tStartToJumpNodeLen < tJumpNode.StartToCurNodeLen)
                    {
                        tJumpNode.StartToCurNodeLen = tStartToJumpNodeLen;
                        tJumpNode.HeuristicCurNodeToEndLen = (tJumpNode.HeuristicCurNodeToEndLen == null ? tHeuristic(Math.Abs(tJumpPoint.X - tEndX), Math.Abs(tJumpPoint.Y - tEndY)) : tJumpNode.HeuristicCurNodeToEndLen);
                        tJumpNode.HeuristicStartToEndLen = tJumpNode.StartToCurNodeLen + tJumpNode.HeuristicCurNodeToEndLen.Value;
                        tJumpNode.Parent = iNode;

                        if (!tJumpNode.IsOpened)
                        {
                            tOpenList.Add(tJumpNode);
                            tJumpNode.IsOpened = true;
                        }
                    }
                }
            }
        }

        private class JumpSnapshot
        {
            public int IX;
            public int IY;
            public int IPx;
            public int IPy;
            public int TDx;
            public int TDy;
            public int Stage;
            public JumpSnapshot()
            {

                IX = 0;
                IY = 0;
                IPx = 0;
                IPy = 0;
                TDx = 0;
                TDy = 0;
                Stage = 0;
            }
        }

        private static GridPos JumpLoop(JumpPointParam iParam, int iX, int iY, int iPx, int iPy)
        {
            GridPos retVal = null;
            var stack = new Stack<JumpSnapshot>();

            var currentSnapshot = new JumpSnapshot();
            JumpSnapshot newSnapshot = null;
            currentSnapshot.IX = iX;
            currentSnapshot.IY = iY;
            currentSnapshot.IPx = iPx;
            currentSnapshot.IPy = iPy;
            currentSnapshot.Stage = 0;

            stack.Push(currentSnapshot);
            while (stack.Count != 0)
            {
                currentSnapshot = stack.Pop();
                switch (currentSnapshot.Stage)
                {
                    case 0:
                        if (!iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY))
                        {
                            retVal = null;
                            continue;
                        }
                        else if (iParam.SearchGrid.GetNodeAt(currentSnapshot.IX, currentSnapshot.IY).Equals(iParam.EndNode))
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }

                        currentSnapshot.TDx = currentSnapshot.IX - currentSnapshot.IPx;
                        currentSnapshot.TDy = currentSnapshot.IY - currentSnapshot.IPy;
                        if (iParam.DiagonalMovement == DiagonalMovement.Always || iParam.DiagonalMovement == DiagonalMovement.IfAtLeastOneWalkable)
                        {
                            // check for forced neighbors
                            // along the diagonal
                            if (currentSnapshot.TDx != 0 && currentSnapshot.TDy != 0)
                            {
                                if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - currentSnapshot.TDx, currentSnapshot.IY + currentSnapshot.TDy) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - currentSnapshot.TDx, currentSnapshot.IY)) ||
                                    (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY - currentSnapshot.TDy) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY - currentSnapshot.TDy)))
                                {
                                    retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                    continue;
                                }
                            }
                            // horizontally/vertically
                            else
                            {
                                if (currentSnapshot.TDx != 0)
                                {
                                    // moving along x
                                    if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY + 1) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + 1)) ||
                                        (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY - 1) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY - 1)))
                                    {
                                        retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                        continue;
                                    }
                                }
                                else
                                {
                                    if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + 1, currentSnapshot.IY + currentSnapshot.TDy) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + 1, currentSnapshot.IY)) ||
                                        (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - 1, currentSnapshot.IY + currentSnapshot.TDy) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - 1, currentSnapshot.IY)))
                                    {
                                        retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                        continue;
                                    }
                                }
                            }
                            // when moving diagonally, must check for vertical/horizontal jump points
                            if (currentSnapshot.TDx != 0 && currentSnapshot.TDy != 0)
                            {
                                currentSnapshot.Stage = 1;
                                stack.Push(currentSnapshot);

                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                                newSnapshot.IY = currentSnapshot.IY;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }

                            // moving diagonally, must make sure one of the vertical/horizontal
                            // neighbors is open to allow the path

                            // moving diagonally, must make sure one of the vertical/horizontal
                            // neighbors is open to allow the path
                            if (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) || iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                            {
                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                                newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }
                            else if (iParam.DiagonalMovement==DiagonalMovement.Always)
                            {
                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                                newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }
                        }
                        else if (iParam.DiagonalMovement == DiagonalMovement.OnlyWhenNoObstacles)
                        {
                            // check for forced neighbors
                            // along the diagonal
                            if (currentSnapshot.TDx != 0 && currentSnapshot.TDy != 0)
                            {
                                if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY + currentSnapshot.TDy) && iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY)) ||
                                    (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY + currentSnapshot.TDy) && iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy)))
                                {
                                    retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                    continue;
                                }
                            }
                            // horizontally/vertically
                            else
                            {
                                if (currentSnapshot.TDx != 0)
                                {
                                    // moving along x
                                    if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + 1) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - currentSnapshot.TDx, currentSnapshot.IY + 1)) ||
                                        (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY - 1) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - currentSnapshot.TDx, currentSnapshot.IY - 1)))
                                    {
                                        retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                        continue;
                                    }
                                }
                                else
                                {
                                    if ((iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + 1, currentSnapshot.IY) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + 1, currentSnapshot.IY - currentSnapshot.TDy)) ||
                                        (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - 1, currentSnapshot.IY) && !iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX - 1, currentSnapshot.IY - currentSnapshot.TDy)))
                                    {
                                        retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                                        continue;
                                    }
                                }
                            }


                            // when moving diagonally, must check for vertical/horizontal jump points
                            if (currentSnapshot.TDx != 0 && currentSnapshot.TDy != 0)
                            {
                                currentSnapshot.Stage = 3;
                                stack.Push(currentSnapshot);

                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                                newSnapshot.IY = currentSnapshot.IY;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }

                            // moving diagonally, must make sure both of the vertical/horizontal
                            // neighbors is open to allow the path
                            if (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) && iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                            {
                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                                newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }
                        }
                        else // if(iParam.DiagonalMovement == DiagonalMovement.Never)
                        {
                            if (currentSnapshot.TDx != 0)
                            {
                                // moving along x
                                if (!iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY))
                                {
                                    retVal= new GridPos(iX, iY);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                                {
                                    retVal = new GridPos(iX, iY);
                                    continue;
                                }
                            }

                            //  must check for perpendicular jump points
                            if (currentSnapshot.TDx != 0)
                            {
                                currentSnapshot.Stage = 5;
                                stack.Push(currentSnapshot);

                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX;
                                newSnapshot.IY = currentSnapshot.IY + 1;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;
                            }
                            else // tDy != 0
                            {
                                currentSnapshot.Stage = 6;
                                stack.Push(currentSnapshot);

                                newSnapshot = new JumpSnapshot();
                                newSnapshot.IX = currentSnapshot.IX + 1;
                                newSnapshot.IY = currentSnapshot.IY;
                                newSnapshot.IPx = currentSnapshot.IX;
                                newSnapshot.IPy = currentSnapshot.IY;
                                newSnapshot.Stage = 0;
                                stack.Push(newSnapshot);
                                continue;

                            }
                        }
                        retVal = null;
                        break;
                    case 1:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }

                        currentSnapshot.Stage = 2;
                        stack.Push(currentSnapshot);

                        newSnapshot = new JumpSnapshot();
                        newSnapshot.IX = currentSnapshot.IX;
                        newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                        newSnapshot.IPx = currentSnapshot.IX;
                        newSnapshot.IPy = currentSnapshot.IY;
                        newSnapshot.Stage = 0;
                        stack.Push(newSnapshot);
                        break;
                    case 2:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }

                        // moving diagonally, must make sure one of the vertical/horizontal
                        // neighbors is open to allow the path
                        if (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) || iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                        {
                            newSnapshot = new JumpSnapshot();
                            newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                            newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                            newSnapshot.IPx = currentSnapshot.IX;
                            newSnapshot.IPy = currentSnapshot.IY;
                            newSnapshot.Stage = 0;
                            stack.Push(newSnapshot);
                            continue;
                        }
                        else if (iParam.DiagonalMovement==DiagonalMovement.Always)
                        {
                            newSnapshot = new JumpSnapshot();
                            newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                            newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                            newSnapshot.IPx = currentSnapshot.IX;
                            newSnapshot.IPy = currentSnapshot.IY;
                            newSnapshot.Stage = 0;
                            stack.Push(newSnapshot);
                            continue;
                        }
                        retVal = null;
                        break;
                    case 3:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }

                        currentSnapshot.Stage = 4;
                        stack.Push(currentSnapshot);

                        newSnapshot = new JumpSnapshot();
                        newSnapshot.IX = currentSnapshot.IX;
                        newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                        newSnapshot.IPx = currentSnapshot.IX;
                        newSnapshot.IPy = currentSnapshot.IY;
                        newSnapshot.Stage = 0;
                        stack.Push(newSnapshot);
                        break;
                    case 4:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }

                        // moving diagonally, must make sure both of the vertical/horizontal
                        // neighbors is open to allow the path
                        if (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) && iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                        {
                            newSnapshot = new JumpSnapshot();
                            newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                            newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                            newSnapshot.IPx = currentSnapshot.IX;
                            newSnapshot.IPy = currentSnapshot.IY;
                            newSnapshot.Stage = 0;
                            stack.Push(newSnapshot);
                            continue;
                        }
                        retVal = null;
                        break;
                    case 5:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }
                        currentSnapshot.Stage = 7;
                        stack.Push(currentSnapshot);

                        newSnapshot = new JumpSnapshot();
                        newSnapshot.IX = currentSnapshot.IX;
                        newSnapshot.IY = currentSnapshot.IY - 1;
                        newSnapshot.IPx = currentSnapshot.IX;
                        newSnapshot.IPy = currentSnapshot.IY;
                        newSnapshot.Stage = 0;
                        stack.Push(newSnapshot);
                        break;
                    case 6:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }
                        currentSnapshot.Stage = 7;
                        stack.Push(currentSnapshot);

                        newSnapshot = new JumpSnapshot();
                        newSnapshot.IX = currentSnapshot.IX - 1;
                        newSnapshot.IY = currentSnapshot.IY;
                        newSnapshot.IPx = currentSnapshot.IX;
                        newSnapshot.IPy = currentSnapshot.IY;
                        newSnapshot.Stage = 0;
                        stack.Push(newSnapshot);
                        break;
                    case 7:
                        if (retVal != null)
                        {
                            retVal = new GridPos(currentSnapshot.IX, currentSnapshot.IY);
                            continue;
                        }
                        // keep going
                        if (iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX + currentSnapshot.TDx, currentSnapshot.IY) && iParam.SearchGrid.IsWalkableAt(currentSnapshot.IX, currentSnapshot.IY + currentSnapshot.TDy))
                        {
                            newSnapshot = new JumpSnapshot();
                            newSnapshot.IX = currentSnapshot.IX + currentSnapshot.TDx;
                            newSnapshot.IY = currentSnapshot.IY + currentSnapshot.TDy;
                            newSnapshot.IPx = currentSnapshot.IX;
                            newSnapshot.IPy = currentSnapshot.IY;
                            newSnapshot.Stage = 0;
                            stack.Push(newSnapshot);
                            continue;
                        }
                        retVal = null;
                        break;
                }

            }

            return retVal;

        }
        private static GridPos Jump(JumpPointParam iParam, int iX, int iY, int iPx, int iPy)
        {
            if (!iParam.SearchGrid.IsWalkableAt(iX, iY))
            {
                return null;
            }
            else if (iParam.SearchGrid.GetNodeAt(iX, iY).Equals(iParam.EndNode))
            {
                return new GridPos(iX, iY);
            }

            int tDx = iX - iPx;
            int tDy = iY - iPy;
            if (iParam.DiagonalMovement == DiagonalMovement.Always || iParam.DiagonalMovement == DiagonalMovement.IfAtLeastOneWalkable)
            {
                // check for forced neighbors
                // along the diagonal
                if (tDx != 0 && tDy != 0)
                {
                    if ((iParam.SearchGrid.IsWalkableAt(iX - tDx, iY + tDy) && !iParam.SearchGrid.IsWalkableAt(iX - tDx, iY)) ||
                        (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY - tDy) && !iParam.SearchGrid.IsWalkableAt(iX, iY - tDy)))
                    {
                        return new GridPos(iX, iY);
                    }
                }
                // horizontally/vertically
                else
                {
                    if (tDx != 0)
                    {
                        // moving along x
                        if ((iParam.SearchGrid.IsWalkableAt(iX + tDx, iY + 1) && !iParam.SearchGrid.IsWalkableAt(iX, iY + 1)) ||
                            (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY - 1) && !iParam.SearchGrid.IsWalkableAt(iX, iY - 1)))
                        {
                            return new GridPos(iX, iY);
                        }
                    }
                    else
                    {
                        if ((iParam.SearchGrid.IsWalkableAt(iX + 1, iY + tDy) && !iParam.SearchGrid.IsWalkableAt(iX + 1, iY)) ||
                            (iParam.SearchGrid.IsWalkableAt(iX - 1, iY + tDy) && !iParam.SearchGrid.IsWalkableAt(iX - 1, iY)))
                        {
                            return new GridPos(iX, iY);
                        }
                    }
                }
                // when moving diagonally, must check for vertical/horizontal jump points
                if (tDx != 0 && tDy != 0)
                {
                    if (Jump(iParam, iX + tDx, iY, iX, iY) != null)
                    {
                        return new GridPos(iX, iY);
                    }
                    if (Jump(iParam, iX, iY + tDy, iX, iY) != null)
                    {
                        return new GridPos(iX, iY);
                    }
                }

                // moving diagonally, must make sure one of the vertical/horizontal
                // neighbors is open to allow the path
                if (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY) || iParam.SearchGrid.IsWalkableAt(iX, iY + tDy))
                {
                    return Jump(iParam, iX + tDx, iY + tDy, iX, iY);
                }
                else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                {
                    return Jump(iParam, iX + tDx, iY + tDy, iX, iY);
                }
                else
                {
                    return null;
                }
            }
            else if (iParam.DiagonalMovement == DiagonalMovement.OnlyWhenNoObstacles)
            {
                // check for forced neighbors
                // along the diagonal
                if (tDx != 0 && tDy != 0)
                {
                    if (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY + tDy) && ( !iParam.SearchGrid.IsWalkableAt(iX, iY + tDy) || !iParam.SearchGrid.IsWalkableAt(iX + tDx, iY)))
                    {
                        return new GridPos(iX, iY);
                    }
                }
                // horizontally/vertically
                else
                {
                    if (tDx != 0)
                    {
                        // moving along x
                        if ((iParam.SearchGrid.IsWalkableAt(iX, iY + 1) && !iParam.SearchGrid.IsWalkableAt(iX - tDx, iY + 1)) ||
                            (iParam.SearchGrid.IsWalkableAt(iX, iY - 1) && !iParam.SearchGrid.IsWalkableAt(iX - tDx, iY - 1)))
                        {
                            return new GridPos(iX, iY);
                        }
                    }
                    else
                    {
                        if ((iParam.SearchGrid.IsWalkableAt(iX + 1, iY) && !iParam.SearchGrid.IsWalkableAt(iX + 1, iY - tDy)) ||
                            (iParam.SearchGrid.IsWalkableAt(iX - 1, iY) && !iParam.SearchGrid.IsWalkableAt(iX - 1, iY - tDy)))
                        {
                            return new GridPos(iX, iY);
                        }
                    }
                }


                // when moving diagonally, must check for vertical/horizontal jump points
                if (tDx != 0 && tDy != 0)
                {
                    if (Jump(iParam, iX + tDx, iY, iX, iY) != null) return new GridPos(iX, iY);
                    if (Jump(iParam, iX, iY + tDy, iX, iY) != null) return new GridPos(iX, iY);
                }

                // moving diagonally, must make sure both of the vertical/horizontal
                // neighbors is open to allow the path
                if (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY) && iParam.SearchGrid.IsWalkableAt(iX, iY + tDy))
                {
                    return Jump(iParam, iX + tDx, iY + tDy, iX, iY);
                }
                else
                {
                    return null;
                }
            }
            else // if(iParam.DiagonalMovement == DiagonalMovement.Never)
            {
                if (tDx != 0)
                {
                    // moving along x
                    if (!iParam.SearchGrid.IsWalkableAt(iX + tDx, iY))
                    {
                        return new GridPos(iX, iY);
                    }
                }
                else
                {
                    if (!iParam.SearchGrid.IsWalkableAt(iX, iY + tDy))
                    {
                        return new GridPos(iX, iY);
                    }
                }

                //  must check for perpendicular jump points
                if (tDx != 0 )
                {
                    if (Jump(iParam, iX, iY + 1, iX, iY) != null) return new GridPos(iX, iY);
                    if (Jump(iParam, iX, iY - 1, iX, iY) != null) return new GridPos(iX, iY);
                }
                else // tDy != 0
                {
                    if (Jump(iParam, iX + 1, iY, iX, iY) != null) return new GridPos(iX, iY);
                    if (Jump(iParam, iX - 1, iY, iX, iY) != null) return new GridPos(iX, iY);
                }

                // keep going
                if (iParam.SearchGrid.IsWalkableAt(iX + tDx, iY) && iParam.SearchGrid.IsWalkableAt(iX, iY + tDy))
                {
                    return Jump(iParam, iX + tDx, iY + tDy, iX, iY);
                }
                else
                {
                    return null;
                }
            }
        }

        private static List<GridPos> FindNeighbors(JumpPointParam iParam, Node iNode)
        {
            var tParent = (Node)iNode.Parent;
            //var diagonalMovement = Util.GetDiagonalMovement(iParam.CrossCorner, iParam.CrossAdjacentPoint);
            int tX = iNode.X;
            int tY = iNode.Y;
            int tPx, tPy, tDx, tDy;
            var tNeighbors = new List<GridPos>();
            List<Node> tNeighborNodes;
            Node tNeighborNode;

            // directed pruning: can ignore most neighbors, unless forced.
            if (tParent != null)
            {
                tPx = tParent.X;
                tPy = tParent.Y;
                // get the normalized direction of travel
                tDx = (tX - tPx) / Math.Max(Math.Abs(tX - tPx), 1);
                tDy = (tY - tPy) / Math.Max(Math.Abs(tY - tPy), 1);

                if (iParam.DiagonalMovement == DiagonalMovement.Always || iParam.DiagonalMovement == DiagonalMovement.IfAtLeastOneWalkable)
                {
                    // search diagonally
                    if (tDx != 0 && tDy != 0)
                    {
                        if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy))
                        {
                            tNeighbors.Add(new GridPos(tX, tY + tDy));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                        {
                            tNeighbors.Add(new GridPos(tX + tDx, tY));
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY + tDy))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy) || iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                            {
                                tNeighbors.Add(new GridPos(tX + tDx, tY + tDy));
                            }
                            else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                            {
                                tNeighbors.Add(new GridPos(tX + tDx, tY + tDy));
                            }
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX - tDx, tY + tDy) && !iParam.SearchGrid.IsWalkableAt(tX - tDx, tY))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy))
                            {
                                tNeighbors.Add(new GridPos(tX - tDx, tY + tDy));
                            }
                            else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                            {
                                tNeighbors.Add(new GridPos(tX - tDx, tY + tDy));
                            }
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY - tDy) && !iParam.SearchGrid.IsWalkableAt(tX, tY - tDy))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                            {
                                tNeighbors.Add(new GridPos(tX + tDx, tY - tDy));
                            }
                            else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                            {
                                tNeighbors.Add(new GridPos(tX + tDx, tY - tDy));
                            }
                        }


                    }
                    // search horizontally/vertically
                    else
                    {
                        if (tDx != 0)
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                            {

                                tNeighbors.Add(new GridPos(tX + tDx, tY));

                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY + 1) && !iParam.SearchGrid.IsWalkableAt(tX, tY + 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY + 1));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY - 1) && !iParam.SearchGrid.IsWalkableAt(tX, tY - 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY - 1));
                                }
                            }
                            else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                            {
                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY + 1) && !iParam.SearchGrid.IsWalkableAt(tX, tY + 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY + 1));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY - 1) && !iParam.SearchGrid.IsWalkableAt(tX, tY - 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY - 1));
                                }
                            }
                        }
                        else
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy))
                            {
                                tNeighbors.Add(new GridPos(tX, tY + tDy));

                                if (iParam.SearchGrid.IsWalkableAt(tX + 1, tY + tDy) && !iParam.SearchGrid.IsWalkableAt(tX + 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX + 1, tY + tDy));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX - 1, tY + tDy) && !iParam.SearchGrid.IsWalkableAt(tX - 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX - 1, tY + tDy));
                                }
                            }
                            else if (iParam.DiagonalMovement == DiagonalMovement.Always)
                            {
                                if (iParam.SearchGrid.IsWalkableAt(tX + 1, tY + tDy) && !iParam.SearchGrid.IsWalkableAt(tX + 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX + 1, tY + tDy));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX - 1, tY + tDy) && !iParam.SearchGrid.IsWalkableAt(tX - 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX - 1, tY + tDy));
                                }
                            }
                        }
                    }
                }
                else if (iParam.DiagonalMovement == DiagonalMovement.OnlyWhenNoObstacles)
                {
                    // search diagonally
                    if (tDx != 0 && tDy != 0)
                    {
                        if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy))
                        {
                            tNeighbors.Add(new GridPos(tX, tY + tDy));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                        {
                            tNeighbors.Add(new GridPos(tX + tDx, tY));
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY + tDy))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy) && iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                                tNeighbors.Add(new GridPos(tX + tDx, tY + tDy));
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX - tDx, tY + tDy))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy) && iParam.SearchGrid.IsWalkableAt(tX - tDx, tY))
                                tNeighbors.Add(new GridPos(tX - tDx, tY + tDy));
                        }

                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY - tDy))
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY - tDy) && iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                                tNeighbors.Add(new GridPos(tX + tDx, tY - tDy));
                        }


                    }
                    // search horizontally/vertically
                    else
                    {
                        if (tDx != 0)
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                            {

                                tNeighbors.Add(new GridPos(tX + tDx, tY));

                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY + 1) && iParam.SearchGrid.IsWalkableAt(tX, tY + 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY + 1));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY - 1) && iParam.SearchGrid.IsWalkableAt(tX, tY - 1))
                                {
                                    tNeighbors.Add(new GridPos(tX + tDx, tY - 1));
                                }
                            }
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + 1))
                                tNeighbors.Add(new GridPos(tX, tY + 1));
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY - 1))
                                tNeighbors.Add(new GridPos(tX, tY - 1));
                        }
                        else
                        {
                            if (iParam.SearchGrid.IsWalkableAt(tX, tY + tDy))
                            {
                                tNeighbors.Add(new GridPos(tX, tY + tDy));

                                if (iParam.SearchGrid.IsWalkableAt(tX + 1, tY + tDy) && iParam.SearchGrid.IsWalkableAt(tX + 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX + 1, tY + tDy));
                                }
                                if (iParam.SearchGrid.IsWalkableAt(tX - 1, tY + tDy) && iParam.SearchGrid.IsWalkableAt(tX - 1, tY))
                                {
                                    tNeighbors.Add(new GridPos(tX - 1, tY + tDy));
                                }
                            }
                            if (iParam.SearchGrid.IsWalkableAt(tX + 1, tY))
                                tNeighbors.Add(new GridPos(tX + 1, tY));
                            if (iParam.SearchGrid.IsWalkableAt(tX - 1, tY))
                                tNeighbors.Add(new GridPos(tX - 1, tY));
                        }
                    }
                }
                else // if(iParam.DiagonalMovement == DiagonalMovement.Never)
                {
                    if (tDx != 0)
                    {
                        if (iParam.SearchGrid.IsWalkableAt(tX + tDx, tY))
                        {
                            tNeighbors.Add(new GridPos(tX + tDx, tY));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX, tY + 1))
                        {
                            tNeighbors.Add(new GridPos(tX, tY +1));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX, tY - 1))
                        {
                            tNeighbors.Add(new GridPos(tX, tY - 1));
                        }
                    }
                    else // if (tDy != 0)
                    {
                        if (iParam.SearchGrid.IsWalkableAt(tX, tY +tDy))
                        {
                            tNeighbors.Add(new GridPos(tX, tY + tDy));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX + 1, tY))
                        {
                            tNeighbors.Add(new GridPos(tX + 1, tY));
                        }
                        if (iParam.SearchGrid.IsWalkableAt(tX - 1, tY))
                        {
                            tNeighbors.Add(new GridPos(tX - 1, tY));
                        }
                    }
                }

            }
            // return all neighbors
            else
            {
                tNeighborNodes = iParam.SearchGrid.GetNeighbors(iNode, iParam.DiagonalMovement);
                for (int i = 0; i < tNeighborNodes.Count; i++)
                {
                    tNeighborNode = tNeighborNodes[i];
                    tNeighbors.Add(new GridPos(tNeighborNode.X, tNeighborNode.Y));
                }
            }

            return tNeighbors;
        }
    }
}
