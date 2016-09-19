// Author: Fred
// Start Date: 2016.09.19 22:00
// Location: ShangHai
// Last Udpate Date:

using System.Collections;
using System.Collections.Generic;
using TreaslandLib.Utils;

namespace TreaslandLib.Algorithms
{

    public delegate void Listener ();
    public delegate bool BListener ();

    public class Point
    {
        // relative position
        public int x;
        public int y;

        // total cost , f = g + h normaly, but you can calculate in other ways,
        // according your special game map
        public int f;

        // cost from start point to current point
        public int g;

        // cost from current point to goal point
        public int h;

        // the parent point
        public Point parent;


        public bool isObstacle = false;


        /*
         * There is something i need to say
         * the cost function, is not const
         * you should according your unique game to design how to calculate g h f
         * 
         */

        public void CalculateG (Point startPoint)
        {
            
        }

        public void CalculateH (Point goalPoint)
        {
            
        }

        public void CalculateF ()
        {
            
        }
    }



    public class AStar
    {
        private List<Point> openList = new List<Point> ();
        private List<Point> closeList = new List<Point> ();

        // point to game world map grids
        private Dictionary<int, Dictionary<int, Point>> map = null;
        private Point startPoint = null;
        private Point goalPoint = null;

        public AStar (Dictionary<int, Dictionary<int, Point>> _map, Point _startPoint, Point _goalPoint)
        {
            this.map = _map;
            this.startPoint = GetPointFromMap (_startPoint);
            this.goalPoint = GetPointFromMap (_goalPoint);
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



        }


        private Point GetPointFromMap (Point p)
        {
            if (!this.map.ContainsKey (p.x) || !this.map [p.x].ContainsKey (p.y))
            {
                return null;
            }

            return this.map [p.x].Values [p.y];
        }


        // always keep open list is sorted, so when you add a new item, resort the open list
        private void AddPointToOpenList (Point p)
        {
            this.openList.Add (p);
            // resort open list
        }




    }
}

