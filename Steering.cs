using System;
using Robocode;
using Robocode.Util;
using Santom;

namespace PG4500_2017_Exam1{
	public static class Steering{


		/// <summary>
		/// Steers the robot according to a desired velocity vector
		/// </summary>
		/// <param name="robot"></param>
		/// <param name="course"></param>
		public static void Steer(Marcle15 robot, Vector2D course){
			var correctedCourse = VectorCorrection(robot, course);
			Vector2D coords = new Vector2D(robot.Position, robot.Position + correctedCourse);
			double distance = coords.Length();
			double steerBearing = Utils.NormalRelativeAngle(Math.Atan2(coords.X, coords.Y) - robot.HeadingRadians);
			robot.SetTurnRightRadians(steerBearing);

			if(Math.Abs(robot.TurnRemainingRadians) < Math.PI / 6)
				robot.SetAhead(distance);
		}


		/// <summary>
		/// return the argument vector with corrected components 
		/// for the tank not to run into walls
		/// </summary>
		/// <param name="robot"></param>
		/// <param name="course"></param>
		/// <returns></returns>
		private static Vector2D VectorCorrection(Marcle15 robot, Vector2D course){
			var safetyMarginX = robot.Width + 10;
			var safetyMarginY = robot.Height + 10; 
			var futurePos = robot.Position + course;

			var minPoint = new Point2D{
				X = safetyMarginX,
				Y = safetyMarginY
			};
			var maxPoint = new Point2D{
				X = robot.BattleFieldWidth - safetyMarginX,
				Y = robot.BattleFieldHeight - safetyMarginY
			};

			futurePos = futurePos.PositionClamp(minPoint, maxPoint);

			var correctedVector = new Vector2D(robot.Position, futurePos);

			correctedVector.Normalize();
			correctedVector *= course.Length();

			return correctedVector;
		}

		private static Point2D PositionClamp(this Point2D v, Point2D pMin, Point2D pMax){
			var clampedPoint = new Point2D{
				X = v.X.Clamp(pMin.X, pMax.X),
				Y = v.Y.Clamp(pMin.Y, pMax.Y)
			};
			return clampedPoint;
		}
	}
}