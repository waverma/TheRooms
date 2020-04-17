﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public struct Vector
    { // Тестов нет
        public readonly int X;
        public readonly int Y;

        public double Length => Math.Sqrt(X * X + Y * Y);
        public Vector Zero => new Vector(0, 0);

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public bool IsNeighboringVector(Vector other)
        {
            throw new NotImplementedException();
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }

        public static Vector operator *(Vector left, Vector right)
        {
            throw new NotImplementedException();
        }

        public static Vector operator *(Vector left, int right)
        {
            return new Vector(left.X * right, left.Y * right);
        }

        public static Vector operator *(int left, Vector right)
        {
            return right * left;
        }
    }
}