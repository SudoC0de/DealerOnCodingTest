using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers
{
    class Plateau
    {
        private int _northBounds = 1;

        public int NorthBounds
        {
            get
            {
                return _northBounds;
            }
            set
            {
                _northBounds = value;
            }
        }

        private int _eastBounds = 1;

        public int EastBounds
        {
            get
            {
                return _eastBounds;
            }
            set
            {
                _eastBounds = value;
            }
        }

        public int SouthBounds
        {
            get
            {
                return 0;
            }
        }

        public int WestBounds
        {
            get
            {
                return 0;
            }
        }
    }
}