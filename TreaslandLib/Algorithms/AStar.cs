// Author: Fred
// Start Date: 2016.09.19 22:00
// Location: ShangHai
// Last Udpate Date: 2016.09.20 13:16

using System.Collections;
using System.Collections.Generic;
using TreaslandLib.Utils;
using System;
using TreaslandLib.Core;

namespace TreaslandLib.Algorithms.AStar
{

    public enum enNeighborDirection
    {
        Left,
        Right,
        Up,
        Down,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }

    public enum enWayStyle
    {
        BaseDirection,
        AllDirection,
    }

    public class Box
    {
        public int borderLeft;
        public int borderRight;
        public int borderUp;
        public int borderDown;

        public Box(int _bLeft, int _bRight, int _bUp, int _bDown)
        {
            this.borderLeft = _bLeft;
            this.borderRight = _bRight;
            this.borderUp = _bUp;
            this.borderDown = _bDown;
        }
    }


    public class Point
    {
        // relative position
        public int x = 0;
        public int y = 0;

        // total cost , f = g + h normaly, but you can calculate in other ways,
        // according your special game map
        public int f = 0;

        // cost from start point to current point
        public int g = 0;

        // cost from current point to goal point
        public int h = 0;

        // the parent point
        public Point parent = null;

        public Point(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }

        /*
         * There is something i need to say
         * the cost function, is not const
         * you should according your unique game to design how to calculate g h f
         * 
         */

        public void CalculateG (int parentG, int increase)
        {
            this.g = parentG + increase;
        }

        public void SetNewG(int newG)
        {
            this.g = newG;
        }

        public void CalculateH (Point goalPoint,int baseValue)
        {
            this.h = (Math.Abs(goalPoint.x - this.x) + Math.Abs(goalPoint.y - this.y)) * baseValue;
        }

        public void CalculateF ()
        {
            this.f = this.g + this.h;
        }
    }



    public class AStar
    {
        private List<Point> openList = new List<Point> ();
        private Dictionary<int, Dictionary<int, Point>> openDict = new Dictionary<int, Dictionary<int, Point>>();
        private List<Point> closeList = new List<Point> ();
        private Dictionary<int, Dictionary<int, Point>> closeDict = new Dictionary<int, Dictionary<int, Point>>();

        private List<Point> finalWays = new List<Point>();

        // point to game world map grids
        private Dictionary<int, Dictionary<int, Point>> map = null;
        private Point startPoint = null;
        private Point goalPoint = null;

        private Box border = null;

        private enWayStyle wayStyle = enWayStyle.BaseDirection;

        private int basePointCostValue = 10;
        private int slopePointCostValue = 14;

        public bool isFoundWay = false;

        private Listener<List<Point>> onWayFound = null;

        public void Init (Dictionary<int, Dictionary<int, Point>> _map, 
            Point _startPoint, 
            Point _goalPoint,
            Box _border,
            enWayStyle _wayStyle,
            Listener<List<Point>> _onWayFound)
        {
            this.map = _map;
            this.border = _border;
            this.wayStyle = _wayStyle;
            this.onWayFound = _onWayFound;

            if (IsAvaliableWayPoint(_startPoint.x, _startPoint.y) == false)
            {
                this.startPoint = null;
            }
            else
            {
                this.startPoint = new Point(_startPoint.x, _startPoint.y);
            }

            if (IsAvaliableWayPoint(_goalPoint.x, _goalPoint.y) == false)
            {
                this.goalPoint = null;
            }
            else
            {
                this.goalPoint = new Point(_goalPoint.x, _goalPoint.y);
            }
        }

        IEnumerator CalculatePath()
        {
            if (this.startPoint == null && this.goalPoint == null)
            {
                Log.Error(this, "startPoint and goalPoint can not be null");
                yield return null;
            }
            else if (this.startPoint == null)
            {
                Log.Error(this, "startPoint can not be null");
                yield return null;
            }
            else if (this.goalPoint == null)
            {
                Log.Error(this, "goalPoint can not be null");
                yield return null;
            }
            else
            {
                this.isFoundWay = false;
                this.openList.Clear();
                this.closeList.Clear();
                this.openDict.Clear();
                this.closeDict.Clear();

                this.startPoint.CalculateG(0, 0);
                this.startPoint.CalculateH(this.goalPoint, this.basePointCostValue);
                this.startPoint.CalculateF();

                // step 1 , add start point to the open list
                AddPointToOpenList(this.startPoint);

                // loop
                // 1. find min cost point A from open list and remove it from open list
                //    if A is the goal , then finished
                //    else add A to close list
                // 2. find all neighbors Na , 
                // 3. if the neighbor Na is not in open list, then calculate the g h f of Na, and set Na's parent as A, then add Na to open list
                //    else if the neighbor is already in open list, calculate g' from A to Na, compare origin g of Na and new g', if g' is less than g, 
                //    then set g as g', set Na's parent as A, recalculate f of Na; if g' is not less than g, then do thing 
                // 4. back loop
                // 
                // now is 2016.09.20 00:01:46, i think i need to sleep, because i want get up early and go to KFC eat breakfast,
                // so , tomorrow i'll finish this A* algorithm module

                while (this.openList.Count > 0)
                {
                    Point currentPoint = GetAndRemoveMinFPointFromOpenList();
                    this.closeList.Add(currentPoint);
                    // ok , the way found
                    if (currentPoint.x == this.goalPoint.x && currentPoint.y == this.goalPoint.y)
                    {
                        this.isFoundWay = true;
                        SetTheFianlWays(currentPoint);
                        break;
                    }
                    ProcessNeighbors(currentPoint);
                    yield return null;
                }
            }
        }


        private void Calcuate ()
        {
            if (this.startPoint == null && this.goalPoint == null)
            {
                Log.Error (this, "startPoint and goalPoint can not be null");
                return;
            }
            else if (this.startPoint == null)
            {
                Log.Error (this, "startPoint can not be null");
                return;
            }
            else if (this.goalPoint == null)
            {
                Log.Error (this, "goalPoint can not be null");
                return;
            }

            this.isFoundWay = false;
            this.openList.Clear();
            this.closeList.Clear();
            this.openDict.Clear();
            this.closeDict.Clear();

            this.startPoint.CalculateG(0, 0);
            this.startPoint.CalculateH(this.goalPoint, this.basePointCostValue);
            this.startPoint.CalculateF();

            // step 1 , add start point to the open list
            AddPointToOpenList (this.startPoint);

            // loop
            // 1. find min cost point A from open list and remove it from open list
            //    if A is the goal , then finished
            //    else add A to close list
            // 2. find all neighbors Na , 
            // 3. if the neighbor Na is not in open list, then calculate the g h f of Na, and set Na's parent as A, then add Na to open list
            //    else if the neighbor is already in open list, calculate g' from A to Na, compare origin g of Na and new g', if g' is less than g, 
            //    then set g as g', set Na's parent as A, recalculate f of Na; if g' is not less than g, then do thing 
            // 4. back loop
            // 
            // now is 2016.09.20 00:01:46, i think i need to sleep, because i want get up early and go to KFC eat breakfast,
            // so , tomorrow i'll finish this A* algorithm module

            while (this.openList.Count > 0)
            {
                Point currentPoint = GetAndRemoveMinFPointFromOpenList();
                this.closeList.Add(currentPoint);
                // ok , the way found
                if(currentPoint.x == this.goalPoint.x && currentPoint.y == this.goalPoint.y)
                {
                    this.isFoundWay = true;
                    SetTheFianlWays(currentPoint);
                    break;
                }
                ProcessNeighbors(currentPoint);
            }
        }


        private void ProcessNeighbors(Point currPoint)
        {
            int nX = 0;
            int nY = 0;


            // left
            nX = currPoint.x - 1;
            nY = currPoint.y;
            ProcessANeighbor(enNeighborDirection.Left, nX, nY, currPoint);

            // up
            nX = currPoint.x;
            nY = currPoint.y + 1;
            ProcessANeighbor(enNeighborDirection.Up, nX, nY, currPoint);

            // right
            nX = currPoint.x + 1;
            nY = currPoint.y;
            ProcessANeighbor(enNeighborDirection.Right, nX, nY, currPoint);

            // down
            nX = currPoint.x;
            nY = currPoint.y - 1;
            ProcessANeighbor(enNeighborDirection.Down, nX, nY, currPoint);

            if (wayStyle == enWayStyle.AllDirection)
            {
                // leftUp
                nX = currPoint.x - 1;
                nY = currPoint.y + 1;
                ProcessANeighbor(enNeighborDirection.LeftUp, nX, nY, currPoint);

                // leftDown
                nX = currPoint.x - 1;
                nY = currPoint.y - 1;
                ProcessANeighbor(enNeighborDirection.LeftDown, nX, nY, currPoint);

                // rightUp
                nX = currPoint.x + 1;
                nY = currPoint.y + 1;
                ProcessANeighbor(enNeighborDirection.RightUp, nX, nY, currPoint);

                // rightDown
                nX = currPoint.x + 1;
                nY = currPoint.y - 1;
                ProcessANeighbor(enNeighborDirection.RightDown, nX, nY, currPoint);
            }
        }


        /// <summary>
        /// Process a neighbor
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="nX"></param>
        /// <param name="nY"></param>
        /// <param name="currPoint"></param>
        private void ProcessANeighbor(enNeighborDirection dir,int nX,int nY,Point currPoint)
        {
            // calculate the g cost from current point the specific neighbor
            int neighborCostValue = GetNeighborGBaseCostValue(dir);
            Point neighbor = null;
            if (IsAvaliableWayPoint(nX, nY))
            {
                neighbor = TryPeekPointFromOpenList(nX, nY);
                if (neighbor == null)
                {
                    // the neighbor is not in the open list, so create it and calculate g h f and add it to open list
                    neighbor = new Point(nX, nY);
                    neighbor.CalculateG(currPoint.g, neighborCostValue);
                    neighbor.CalculateH(this.goalPoint, this.basePointCostValue);
                    neighbor.CalculateF();
                    neighbor.parent = currPoint;
                    AddPointToOpenList(neighbor);
                }
                else
                {
                    int newG = currPoint.g + neighborCostValue;
                    if (newG < neighbor.g)
                    {
                        neighbor.SetNewG(newG);
                        neighbor.CalculateF();
                        neighbor.parent = currPoint;
                    }
                }
            }
        }



        /// <summary>
        /// according neighbor dirction calculate cost from current point to neighbor point
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public int GetNeighborGBaseCostValue(enNeighborDirection dir)
        {
            int costValue = -1;
            switch (dir)
            {
                case enNeighborDirection.Left:
                case enNeighborDirection.Right:
                case enNeighborDirection.Up:
                case enNeighborDirection.Down:
                    {
                        costValue = this.basePointCostValue;
                    }
                    break;
                case enNeighborDirection.LeftUp:
                case enNeighborDirection.LeftDown:
                case enNeighborDirection.RightUp:
                case enNeighborDirection.RightDown:
                    {
                        costValue = this.slopePointCostValue;
                    }
                    break;
            }
            return costValue;
        }



        /// <summary>
        /// the point should in border, not include border point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsOutOfMap(int x, int y)
        {
            bool result = false;
            // out left
            if(x <= this.border.borderLeft)
            {
                result = true;
            }
            // out right
            if(x >= this.border.borderRight)
            {
                result = true;
            }
            // out down
            if(y <= this.border.borderDown)
            {
                result = true;
            }
            // out up
            if(y >= this.border.borderUp)
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// the map contains all obstacle of point 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsPointAreObstacle(int x, int y)
        {
            if(this.map.ContainsKey(x) && this.map[x].ContainsKey(y))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// this point can walk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsAvaliableWayPoint(int x, int y)
        {
            if(IsOutOfMap(x,y) || IsPointAreObstacle(x, y))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// always keep open list is sorted, so when you add a new item, resort the open list 
        /// </summary>
        /// <param name="p"></param>
        private void AddPointToOpenList(Point p)
        {
            this.openList.Add(p);

            // also save the point to dict is for performance
            if(this.openDict.ContainsKey(p.x) == false)
            {
                this.openDict.Add(p.x, new Dictionary<int, Point>());
            }
            this.openDict[p.x].Add(p.y, p);
            
            // resort open list
            // when p1.f > p2.f , small f on start, large f on end, sort from small to big
            // like  1 2 3 4 5 6 7 8 9 10
            this.openList.Sort((p1, p2) =>
            {
                if (p1.f > p2.f)
                {
                    return 1;
                }
                else if (p1.f == p2.f)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            });
        }


        /// <summary>
        /// is a open already in open list?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsPointInOpenList(int x, int y)
        {
            if(this.openDict.ContainsKey(x) && this.openDict[x].ContainsKey(y))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// return the min f item from open list, and remove it from open list
        /// </summary>
        /// <returns></returns>
        private Point GetAndRemoveMinFPointFromOpenList()
        {
            int index = this.openList.Count - 1;
            if(index >= 0)
            {
                Point p = this.openList[index];
                this.openList.RemoveAt(index);

                this.openDict[p.x].Remove(p.y);

                return p;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// just get a reference point from open list
        /// </summary>
        /// <returns></returns>
        private Point TryPeekPointFromOpenList(int x, int y)
        {
            if(this.openDict.ContainsKey(x) && this.openDict[x].ContainsKey(y))
            {
                return this.openDict[x][y];
            }
            return null;

        }


        /// <summary>
        /// Save the final ways
        /// </summary>
        /// <param name="currentPoint"></param>
        private void SetTheFianlWays(Point currentPoint)
        {
            Point tmpP = currentPoint.parent;
            this.finalWays.Clear();
            this.finalWays.Add(currentPoint);

            while(tmpP != null)
            {
                this.finalWays.Add(tmpP);
                tmpP = tmpP.parent;
            }

            this.finalWays.Reverse();
            
            if(this.onWayFound != null)
            {
                this.onWayFound(this.finalWays);
            }
        }


        /// <summary>
        /// if the A* find the ways, then get it and return from clost list
        /// </summary>
        /// <returns></returns>
        public List<Point> GetTheFinalWays()
        {
            return this.finalWays;
        }

    }
}

