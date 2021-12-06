using System;
using System.Collections.Generic;
using System.Linq;

namespace AocHelpers
{
    public class Point
    {
        private struct _vector
        {
            public long lengthX {get; set;}
            public long lengthY {get; set;}

            public _vector(long[] lengthsIn) {lengthX = lengthsIn[0]; lengthY = lengthsIn[1];}

        }
        private _vector _vect = new _vector();

        public long positionX {get; set;} = 0;
        public long positionY {get; set;} = 0;
        public Point()
        {

        }
        public Point(long[] positionsIn)
        {
            this.positionX = positionsIn[0];
            this.positionY = positionsIn[1];
        }

        public void GenerateVector(long[] vectIn)
        {
            this._vect = new _vector(vectIn);
        }

        public long[] GetVector()
        {
            long[] result = {this._vect.lengthX, this._vect.lengthY};
            return result;
        }
        public long GetVectorLength(bool vertical = false)
        {
            if (vertical)
            {
                return this._vect.lengthX;
            }
            return this._vect.lengthY;
        }
        public long[] GetTargetPositions()
        {
            long[] result = new long[] {this.positionX + this._vect.lengthX, this.positionY + this._vect.lengthY};
            return result;
        }
    }
}