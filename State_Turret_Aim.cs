using System;
using Robocode.Util;
using Santom;

namespace PG4500_2017_Exam1{
	internal class State_Turret_Aim : State{
		public State_Turret_Aim(Marcle15 robot) : base(robot){ }

		public override State Execute(){
			if(!_Robot.HasEnemy()){
				Console.WriteLine("AIM to SCAN triggered!");
				return new State_Turret_Scan(_Robot);
			}

			var enemy = _Robot.Enemy;
			var firepower = AdjustedFirepower(_Robot.Enemy);
			var predictedTarget = GetPredictedHitPosition(enemy, firepower);
			predictedTarget = ClampWithinBounds(predictedTarget);
			var aimAngle = AimAngleForGun(predictedTarget);
			_Robot.SetTurnGunLeftRadians(aimAngle);


			if(GunFacesTarget()){
				return new State_Turret_Fire(_Robot, firepower);
			}

			return new State_Turret_Aim(_Robot);
		}

		private Point2D ClampWithinBounds(Point2D target){
			var delta = _Robot.Width / 2;
			var max_X = _Robot.BattleFieldWidth - delta;
			var max_Y = _Robot.BattleFieldHeight - delta;

			var x = target.X.Clamp(delta, max_X);
			var y = target.Y.Clamp(delta, max_Y);
			return new Point2D(x, y);
		}

		private bool GunFacesTarget(){ return _Robot.GunTurnRemainingRadians.IsCloseToZero(Math.PI / 18); }

		private double AdjustedFirepower(EnemyData enemy){ return Math.Min(500 / enemy.Distance, 3); }

		private Point2D GetPredictedHitPosition(EnemyData enemy, double firepower){
			var enemyPos = enemy.Position;
			var enemyVel = enemy.VelocityVector;


			var bulletSpeed = 20 - (firepower * 3);
			var time = enemy.Distance / bulletSpeed;

			return enemyPos + (enemyVel * time);
		}

		private double AimAngleForGun(Point2D target){
			var turnAngle = Math.Atan2(-(target.X - _Robot.X), target.Y - _Robot.Y) + _Robot.GunHeadingRadians;
			return Utils.NormalRelativeAngle(turnAngle);
		}
	}
}