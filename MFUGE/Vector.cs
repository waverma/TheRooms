﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheRooms.MFUGE
{
    public struct Vector
    {
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

        public static IEnumerable<Vector> GetVectorsArray(int width, int height)
        { // TODO No tests
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    yield return new Vector(x, y);
        }

        public bool IsNeighboringVector(Vector other)
        { // TODO Test me
            return Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }

        public static Vector operator *(Vector left, int right)
        {
            return new Vector(left.X * right, left.Y * right);
        }

        public static Vector operator *(int left, Vector right)
        {
            return right * left;
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return left.ToPoint() == right.ToPoint();
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return left.ToPoint() != right.ToPoint();
        }
    }

    public static class PointExt
    {
        public static bool IsBetween(this Point source, Point f, Point s)
        {
            return f.X <= source.X && source.X <= s.X
                                   && f.Y <= source.Y && source.Y <= s.Y;
        }
    }
}
