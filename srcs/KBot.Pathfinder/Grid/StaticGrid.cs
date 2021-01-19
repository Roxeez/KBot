﻿/*! 
@file StaticGrid.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief StaticGrid Interface
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

An Interface for the StaticGrid Class.

*/
using System;
using System.Collections.Generic;
using System.Collections;

namespace Spark.Pathfinder.Grid
{
    public class StaticGrid : BaseGrid
    {
        public override int Width { get; protected set; }

        public override int Height { get; protected set; }

        private Node[][] mNodes;

        public StaticGrid(int iWidth, int iHeight, bool[][] iMatrix = null):base()
        {
            Width = iWidth;
            Height = iHeight;
            MGridRect.MinX = 0;
            MGridRect.MinY = 0;
            MGridRect.MaxX = iWidth-1;
            MGridRect.MaxY = iHeight - 1;
            this.mNodes = BuildNodes(iWidth, iHeight, iMatrix);
        }

        public StaticGrid(StaticGrid b)
            : base(b)
        {
            var tMatrix = new bool[b.Width][];
            for (int widthTrav = 0; widthTrav < b.Width; widthTrav++)
            {
                tMatrix[widthTrav] = new bool[b.Height];
                for (int heightTrav = 0; heightTrav < b.Height; heightTrav++)
                {
                    if(b.IsWalkableAt(widthTrav,heightTrav))
                        tMatrix[widthTrav][heightTrav] = true;
                    else
                        tMatrix[widthTrav][heightTrav] = false;
                }
            }
            this.mNodes = BuildNodes(b.Width, b.Height, tMatrix);
        }
       
        private Node[][] BuildNodes(int iWidth, int iHeight, bool[][] iMatrix)
        {

            var tNodes = new Node[iWidth][];
            for (int widthTrav = 0; widthTrav < iWidth; widthTrav++)
            {
                tNodes[widthTrav] = new Node[iHeight];
                for (int heightTrav = 0; heightTrav < iHeight; heightTrav++)
                {
                    tNodes[widthTrav][heightTrav] = new Node(widthTrav, heightTrav, null);
                }
            }

            if (iMatrix == null)
            {
                return tNodes;
            }

            if (iMatrix.Length != iWidth || iMatrix[0].Length != iHeight)
            {
                throw new System.Exception("Matrix size does not fit");
            }


            for (int widthTrav = 0; widthTrav < iWidth; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < iHeight; heightTrav++)
                {
                    if (iMatrix[widthTrav][heightTrav])
                    {
                        tNodes[widthTrav][heightTrav].Walkable = true;
                    }
                    else
                    {
                        tNodes[widthTrav][heightTrav].Walkable = false;
                    }
                }
            }
            return tNodes;
        }

        public override Node GetNodeAt(int iX, int iY)
        {
            return this.mNodes[iX][iY];
        }

        public override bool IsWalkableAt(int iX, int iY)
        {
            return IsInside(iX, iY) && this.mNodes[iX][iY].Walkable;
        }

        protected bool IsInside(int iX, int iY)
        {
            return (iX >= 0 && iX < Width) && (iY >= 0 && iY < Height);
        }

        public override bool SetWalkableAt(int iX, int iY, bool iWalkable)
        {
            this.mNodes[iX][iY].Walkable = iWalkable;
            return true;
        }

        protected bool IsInside(GridPos iPos)
        {
            return IsInside(iPos.X, iPos.Y);
        }

        public override Node GetNodeAt(GridPos iPos)
        {
            return GetNodeAt(iPos.X, iPos.Y);
        }

        public override bool IsWalkableAt(GridPos iPos)
        {
            return IsWalkableAt(iPos.X, iPos.Y);
        }

        public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
        {
            return SetWalkableAt(iPos.X, iPos.Y, iWalkable);
        }

        public override void Reset()
        {
            Reset(null);
        }

        public void Reset(bool[][] iMatrix)
        {
            for (int widthTrav = 0; widthTrav < Width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < Height; heightTrav++)
                {
                    mNodes[widthTrav][heightTrav].Reset();
                }
            }

            if (iMatrix == null)
            {
                return;
            }
            if (iMatrix.Length != Width || iMatrix[0].Length != Height)
            {
                throw new System.Exception("Matrix size does not fit");
            }

            for (int widthTrav = 0; widthTrav < Width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < Height; heightTrav++)
                {
                    if (iMatrix[widthTrav][heightTrav])
                    {
                        mNodes[widthTrav][heightTrav].Walkable = true;
                    }
                    else
                    {
                        mNodes[widthTrav][heightTrav].Walkable = false;
                    }
                }
            }
        }

        public override BaseGrid Clone()
        {
            int tWidth = Width;
            int tHeight = Height;
            Node[][] tNodes = this.mNodes;

            var tNewGrid = new StaticGrid(tWidth, tHeight, null);

            var tNewNodes = new Node[tWidth][];
            for (int widthTrav = 0; widthTrav < tWidth; widthTrav++)
            {
                tNewNodes[widthTrav] = new Node[tHeight];
                for (int heightTrav = 0; heightTrav < tHeight; heightTrav++)
                {
                    tNewNodes[widthTrav][heightTrav] = new Node(widthTrav, heightTrav, tNodes[widthTrav][heightTrav].Walkable);
                }
            }
            tNewGrid.mNodes = tNewNodes;

            return tNewGrid;
        }
    }


}
