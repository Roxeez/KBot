﻿/*! 
@file DynamicGridWPool.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief DynamicGrid with Pool Interface
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

An Interface for the DynamicGrid with Pool Class.

*/
using System;
using System.Collections.Generic;
using System.Collections;


namespace Spark.Pathfinder.Grid
{
    public class DynamicGridWPool : BaseGrid
    {
         private bool mNotSet;
         private NodePool mNodePool;

        public override int Width
        {
            get
            {
                if (mNotSet)
                    SetBoundingBox();
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
                if (mNotSet)
                    SetBoundingBox();
                return MGridRect.MaxY - MGridRect.MinY + 1;
            }
            protected set
            {

            }
        }

        public DynamicGridWPool(NodePool iNodePool)
            : base()
        {
            MGridRect = new GridRect();
            MGridRect.MinX = 0;
            MGridRect.MinY = 0;
            MGridRect.MaxX = 0;
            MGridRect.MaxY = 0;
            mNotSet = true;
            mNodePool = iNodePool;
        }

        public DynamicGridWPool(DynamicGridWPool b)
            : base(b)
        {
            mNotSet = b.mNotSet;
            mNodePool = b.mNodePool;
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

        private void SetBoundingBox()
        {
            mNotSet = true;
            foreach (KeyValuePair<GridPos, Node> pair in mNodePool.Nodes)
            {
                if (pair.Key.X < MGridRect.MinX || mNotSet)
                    MGridRect.MinX = pair.Key.X;
                if (pair.Key.X > MGridRect.MaxX || mNotSet)
                    MGridRect.MaxX = pair.Key.X;
                if (pair.Key.Y < MGridRect.MinY || mNotSet)
                    MGridRect.MinY = pair.Key.Y;
                if (pair.Key.Y > MGridRect.MaxY || mNotSet)
                    MGridRect.MaxY = pair.Key.Y;
                mNotSet = false;
            }
            mNotSet = false;
        }

        public override bool SetWalkableAt(int iX, int iY, bool iWalkable)
        {
            var pos = new GridPos(iX, iY);
            mNodePool.SetNode(pos, iWalkable);
            if (iWalkable)
            {
                if (iX < MGridRect.MinX || mNotSet)
                    MGridRect.MinX = iX;
                if (iX > MGridRect.MaxX || mNotSet)
                    MGridRect.MaxX = iX;
                if (iY < MGridRect.MinY || mNotSet)
                    MGridRect.MinY = iY;
                if (iY > MGridRect.MaxY || mNotSet)
                    MGridRect.MaxY = iY;
                //m_notSet = false;
            }
            else
            {
                if (iX == MGridRect.MinX || iX == MGridRect.MaxX || iY == MGridRect.MinY || iY == MGridRect.MaxY)
                    mNotSet = true;
                
            }
            return true;
        }

        public override Node GetNodeAt(GridPos iPos)
        {
            return mNodePool.GetNode(iPos);
        }

        public override bool IsWalkableAt(GridPos iPos)
        {
            return  mNodePool.Nodes.ContainsKey(iPos);
        }

        public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
        {
            return SetWalkableAt(iPos.X, iPos.Y, iWalkable);
        }


        public override void Reset()
        {
            foreach (KeyValuePair<GridPos, Node> keyValue in mNodePool.Nodes)
            {
                keyValue.Value.Reset();
            }
        }

        public override BaseGrid Clone()
        {
            var tNewGrid = new DynamicGridWPool(mNodePool);
            return tNewGrid;
        }
    }

}