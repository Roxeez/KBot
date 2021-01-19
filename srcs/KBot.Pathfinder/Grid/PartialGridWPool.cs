/*! 
@file PartialGridWPool.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief PartialGrid with Pool Interface
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

An Interface for the PartialGrid with Pool Class.

*/
using System;
using System.Collections.Generic;
using System.Collections;

namespace Spark.Pathfinder.Grid
{
    public class PartialGridWPool : BaseGrid
    {
        private NodePool mNodePool;

        public override int Width
        {
            get
            {
                return MGridRect.MaxX - MGridRect.MinX + 1;
            }
            protected set
            {

            }
        }

        public override int Height
        {
            get
            {
                return MGridRect.MaxY - MGridRect.MinY + 1;
            }
            protected set
            {

            }
        }


        public PartialGridWPool(NodePool iNodePool, GridRect iGridRect = null)
            : base()
        {
            if (iGridRect == null)
                MGridRect = new GridRect();
            else
                MGridRect = iGridRect;
            mNodePool = iNodePool;
        }

        public PartialGridWPool(PartialGridWPool b)
            : base(b)
        {
            mNodePool = b.mNodePool;
        }
       
        public void SetGridRect(GridRect iGridRect)
        {
            MGridRect = iGridRect;
        }


        public bool IsInside(int iX, int iY)
        {
            if (iX < MGridRect.MinX || iX > MGridRect.MaxX || iY < MGridRect.MinY || iY > MGridRect.MaxY)
                return false;
            return true;
        }

        public override Node GetNodeAt(int iX, int iY)
        {
            var pos = new GridPos(iX, iY);
            return GetNodeAt(pos);
        }

        public override bool IsWalkableAt(int iX, int iY)
        {
            var pos = new GridPos(iX, iY);
            return IsWalkableAt(pos);
        }

        public override bool SetWalkableAt(int iX, int iY, bool iWalkable)
        {
            if (!IsInside(iX,iY))
                return false;
            var pos = new GridPos(iX, iY);
            mNodePool.SetNode(pos, iWalkable);
            return true;
        }

        public bool IsInside(GridPos iPos)
        {
            return IsInside(iPos.X, iPos.Y);
        }

        public override Node GetNodeAt(GridPos iPos)
        {
            if (!IsInside(iPos))
                return null;
            return mNodePool.GetNode(iPos);
        }

        public override bool IsWalkableAt(GridPos iPos)
        {
            if (!IsInside(iPos))
                return false;
            return mNodePool.Nodes.ContainsKey(iPos);
        }

        public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
        {
            return SetWalkableAt(iPos.X, iPos.Y, iWalkable);
        }

        public override void Reset()
        {
            int rectCount=(MGridRect.MaxX-MGridRect.MinX) * (MGridRect.MaxY-MGridRect.MinY);
            if (mNodePool.Nodes.Count > rectCount)
            {
                var travPos = new GridPos(0, 0);
                for (int xTrav = MGridRect.MinX; xTrav <= MGridRect.MaxX; xTrav++)
                {
                    travPos.X = xTrav;
                    for (int yTrav = MGridRect.MinY; yTrav <= MGridRect.MaxY; yTrav++)
                    {
                        travPos.Y = yTrav;
                        Node curNode=mNodePool.GetNode(travPos);
                        if (curNode!=null)
                            curNode.Reset();
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<GridPos, Node> keyValue in mNodePool.Nodes)
                {
                    keyValue.Value.Reset();
                }
            }
        }


        public override BaseGrid Clone()
        {
            var tNewGrid = new PartialGridWPool(mNodePool,MGridRect);
            return tNewGrid;
        }
    }

}