using System;
using Robocode;
using Robocode.Util;

namespace Santom{
	/// <summary>
	/// Helper class, storing data about scanned enemies (including the timestamp (turn) for that scan of data). 
	/// version: 1.0
	/// author: Tomas Sandnes - santom@westerdals.no
	/// </summary>
	public class EnemyData{
		// P R O P E R T I E S
		// -------------------

		// General stuff 
		public long Time{ get; set; } // Time (turn) of currently stored scan.

		// Enemy stuff
		public string Name{ get; set; } // Name of enemy.

		public double BearingRadians{ get; set; } // Bearing from us to enemy, in radians.

		public double BearingDegrees
		{
			get { return Utils.ToDegrees(BearingRadians); }
			set { BearingRadians = Utils.ToRadians(value); }
		} // Bearing from us to enemy, in degrees.

		public double Distance{ get; set; } // Distance from us to enemy.
		public double Energy{ get; set; } // Energy of enemy.
		public Point2D Position{ get; set; } // Position of enemy, in battlefield x y coordinates.
		public double Velocity{ get; set; } // Velocity of enemy.

		public double Acceleration{ get; set; }

		// How fast our enemy changes speed. (Calculated by comparing values over 2 scans.)
		public double HeadingRadians{ get; set; } // Heading of enemy, in radians.

		public double HeadingDegrees
		{
			get { return Utils.ToDegrees(HeadingRadians); }
			set { HeadingRadians = Utils.ToRadians(value); }
		} // Heading of enemy, in degrees.

		public double TurnRateRadians{ get; set; }

		// How fast our enemy turns, in radians (change of heading per turn). (Calculated by comparing values over 2 scans.)
		public double TurnRateDegrees
		{
			get { return Utils.ToDegrees(TurnRateRadians); }
			set { TurnRateRadians = Utils.ToRadians(value); }
		}

		// How fast our enemy turns, in degrees (change of heading per turn). (Calculated by comparing values over 2 scans.)

		//Added by Clement
		public Vector2D  VelocityVector{ get; set; }
		public Vector2D  HeadingVector{ get; set; }


		// P U B L I C   M E T H O D S 
		// ---------------------------

		/// <summary>
		/// Constructor.
		/// </summary>
		public EnemyData(){
			Time = 0;
			Name = null;
			BearingRadians = 0.0;
			Distance = 0.0;
			Energy = 0.0;
			Position = new Point2D();
			Velocity = 0.0;
			Acceleration = 0.0;
			HeadingRadians = 0.0;
			TurnRateRadians = 0.0;

			//Added by Clement
			VelocityVector = new Vector2D();
			HeadingVector = new Vector2D();
		}


		/// <summary>
		/// Copy constructor.
		/// </summary>
		public EnemyData(EnemyData cloneMe){
			Time = cloneMe.Time;
			Name = cloneMe.Name;
			BearingRadians = cloneMe.BearingRadians;
			Distance = cloneMe.Distance;
			Energy = cloneMe.Energy;
			Position = new Point2D(cloneMe.Position);
			Velocity = cloneMe.Velocity;
			Acceleration = cloneMe.Acceleration;
			HeadingRadians = cloneMe.HeadingRadians;
			TurnRateRadians = cloneMe.TurnRateRadians;

			//Added by Clement
			VelocityVector = new Vector2D(cloneMe.VelocityVector);
			HeadingVector = new Vector2D(cloneMe.HeadingVector);
		}


		/// <summary>
		/// Resets this EnemyData instance.
		/// </summary>
		public void Clear(){
			Time = 0;
			Name = null;
			BearingRadians = 0.0;
			Distance = 0.0;
			Energy = 0.0;
			Position.Zero();
			Velocity = 0.0;
			Acceleration = 0.0;
			HeadingRadians = 0.0;
			TurnRateRadians = 0.0;

			//Added by Clement
			VelocityVector.Zero();
			HeadingVector.Zero();
		}


		/// <summary>
		/// Sets all EnemyData.
		/// </summary>
		public void SetEnemyData(
			ScannedRobotEvent newEnemyData,
			Point2D newPosition){
			// First we set the stuff that depends on last updates' values:
			long deltaTime = newEnemyData.Time - Time;


			TurnRateRadians = Utils.NormalRelativeAngle(newEnemyData.HeadingRadians - HeadingRadians) / deltaTime;
			Acceleration = (newEnemyData.Velocity - Velocity) / deltaTime;

			// General data:
			Time = newEnemyData.Time;

			// Compared-to-us data:
			BearingRadians = newEnemyData.BearingRadians;
			Distance = newEnemyData.Distance;

			// Enemy specific data:
			Name = newEnemyData.Name;
			Energy = newEnemyData.Energy;
			Position = newPosition;
			Velocity = newEnemyData.Velocity;
			HeadingRadians = newEnemyData.HeadingRadians;

			//Added by Clement
			HeadingVector.X = Math.Sin(HeadingRadians);
			HeadingVector.Y = Math.Cos(HeadingRadians);
			VelocityVector = HeadingVector * Velocity; 
		}
		
	}
}