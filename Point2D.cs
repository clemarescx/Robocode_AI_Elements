using System;
using Santom;

/// <summary>
/// Class for storing a 2D point of type {double, double}. 
/// (Really just a stripped down version of the Vector2D class.)
/// version: 1.0
/// author: Tomas Sandnes - santom@westerdals.no
/// </summary>
public class Point2D{
	public double X{ get; set; }
	public double Y{ get; set; }

	/// <summary>
	/// Default constructor.
	/// </summary>
	public Point2D(){
		// Intentionally left blank.
	}

	/// <summary>
	/// Copy constructor.
	/// </summary>
	public Point2D(Point2D cloneMe){
		X = cloneMe.X;
		Y = cloneMe.Y;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public Point2D(double xVal, double yVal){
		X = xVal;
		Y = yVal;
	}

	/// <summary>
	/// Sets X and Y to zero.
	/// </summary>
	public void Zero(){
		X = 0.0;
		Y = 0.0;
	}

	/// <summary>
	/// Returns true if both X and Y are zero.
	/// </summary>
	public bool IsZero(){
		return (((X * X) + (Y * Y)).Equals(0.0));
	}

	public bool Equals(Point2D point){
		// If parameter is null return false:
		if((Object)point == null){
			return false;
		}

		// Return true if the fields match:
		return (X.Equals(point.X)) && (Y.Equals(point.Y));
	}

	public override bool Equals(Object obj){
		// If parameter is null return false.
		if(obj == null){
			return false;
		}

		// If parameter cannot be cast to Point2D return false.
		Point2D point = obj as Point2D;
		if((Object)point == null){
			return false;
		}

		// Return true if the fields match:
		return (X.Equals(point.X)) && (Y.Equals(point.Y));
	}

	public override int GetHashCode() { return X.GetHashCode() ^ Y.GetHashCode(); }

	// We want some overloaded operators:
	// ==================================

	// Added by Clement - translation of a point according to vector
	public static Point2D operator +(Point2D point, Vector2D vector){
		return new Point2D(point.X + vector.X, point.Y + vector.Y);
	}

	public static Point2D operator +(Point2D lhs, Point2D rhs) { return new Point2D(lhs.X + rhs.X, lhs.Y + rhs.Y); }

	public static Point2D operator -(Point2D lhs, Point2D rhs) { return new Point2D(lhs.X - rhs.X, lhs.Y - rhs.Y); }

	public static Point2D operator *(Point2D lhs, double rhs) { return new Point2D(lhs.X * rhs, lhs.Y * rhs); }

	public static Point2D operator *(double lhs, Point2D rhs) { return new Point2D(lhs * rhs.X, lhs * rhs.Y); }

	public static Point2D operator /(Point2D lhs, double rhs) { return new Point2D(lhs.X / rhs, lhs.Y / rhs); }

	public static bool operator ==(Point2D lhs, Point2D rhs){
		// If both are null, or both are same instance, return true.
		if(ReferenceEquals(lhs, rhs)){
			return true;
		}
		// (Else) If lhs is null, return false.
		if((object)lhs == null){
			return false;
		}
		// (Else) Pass it on to the Equals method.
		return lhs.Equals(rhs);
	}

	public static bool operator !=(Point2D lhs, Point2D rhs) { return !(lhs == rhs); }
}