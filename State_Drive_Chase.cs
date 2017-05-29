using System;
using Santom;

namespace PG4500_2017_Exam1{
	public class State_Drive_Chase : State{
		public State_Drive_Chase(Marcle15 robot) : base(robot){ }

		public override State Execute(){
			if(!_Robot.HasEnemy()){
				Console.WriteLine("CHASE to IDLE TRIGGERED");
				return new State_Drive_Idle(_Robot);
			}

			Chase();

			return new State_Drive_Chase(_Robot);
		}

		/// <summary>
		/// The robot follows an enemy but keeps a distance
		/// </summary>
		private void Chase(){
			const int distanceOffset = 100;

			var bearingVec = new Vector2D(_Robot.Enemy.Position, _Robot.Position);
			bearingVec.Normalize();
			bearingVec *= distanceOffset;
			var closestFollowPos = _Robot.Enemy.Position + bearingVec;
			var closestFollowTrajectory = new Vector2D(_Robot.Position, closestFollowPos);
			Steering.Steer(_Robot, closestFollowTrajectory);
		}
	}
}