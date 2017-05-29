using System;
using System.Drawing;
using Robocode;
using Robocode.Util;
using Santom;

namespace PG4500_2017_Exam1{
	public class Marcle15 : AdvancedRobot{
		public EnemyData Enemy{ get; }
		public Point2D Position => new Point2D(X, Y);

		public FSM_Drive SteerMachine{ get; set; }
		public FSM_Turret TurretMachine{ get; set; }


		public Marcle15(){
			Enemy = new EnemyData();
			SteerMachine = new FSM_Drive(this);
			TurretMachine = new FSM_Turret(this);
			FuzzyLogic.Init(this);
		}


		public override void Run(){}

		public override void OnStatus(StatusEvent statusEvent){
			SetAllColors(Color.HotPink); // I'm hot, baby.

			//Scan at all times for lock in OnScannedRobot to work
			if(Math.Abs(RadarTurnRemaining) < 0.0001){
				SetTurnRadarRightRadians(double.PositiveInfinity);
			}

			TurretMachine.Update();
			SteerMachine.Update();

			try{
				Execute();
			}
			catch(Exception e){
				Console.WriteLine(e.Message);
			}
		}


		public override void OnScannedRobot(ScannedRobotEvent e){
			if(!HasEnemy() || e.Name.Equals(Enemy.Name)){
				var enemyPos = MathHelpers.GetGlobalCoords(this, e.Distance, e.BearingRadians);
				Enemy.SetEnemyData(e, enemyPos);
			}

			// taken from
			// http://old.robowiki.net/robowiki?Radar
			//
			const double factor = 2.1;
			double absBearing = e.BearingRadians + HeadingRadians;
			SetTurnRadarRightRadians(factor * Utils.NormalRelativeAngle(absBearing - RadarHeadingRadians));
		}


		public override void OnRobotDeath(RobotDeathEvent evnt){
			if(HasEnemy() && evnt.Name == Enemy.Name)
				Enemy.Clear();
		}


		public override void OnCustomEvent(CustomEvent evnt){ }

		public bool HasEnemy(){ return Enemy.Name != null; }

		private void PaintChasePosition(){
			const int distanceOffset = 100;

			var bearingVec = new Vector2D(Enemy.Position, Position);
			bearingVec.Normalize();
			bearingVec *= distanceOffset;
			var closestFollowPos = Enemy.Position + bearingVec;
			DrawLineAndTarget(Color.Blue, Position, closestFollowPos); 
		}

		public override void OnPaint(IGraphics graphics){
			if(HasEnemy()){
				DrawLineAndTarget(Color.Red, new Point2D(X, Y), Enemy.Position);
				PaintChasePosition();
			}
		}


		/// <summary>
		/// Method to draw half-transparent robot indicator box (size somewhat bigger than a robot) covering enemy.
		/// </summary>
		public void DrawRobotIndicator(Color drawColor, Point2D target){
			// Set color to a semi-transparent one.
			Color halfTransparent = Color.FromArgb(128, drawColor);
			// Draw rectangle at target.
			Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)(target.X - 26.5), (int)(target.Y - 26.5), 54, 54);
		}

		/// <summary>
		/// Method to draw half-transparent targeting-line (from start to end) & targeting-box (the size of a robot) 
		/// on the battlefield. The idea is to use this for visual debugging: Set start point to own robot's position, 
		/// and end point to where you mean the bullet to go. Then see if this really is where the bullet is heading: 
		/// 1) If the targeting-box is off the spot you wanted it, you got a bug in your target prediction code.
		/// 2) If the targeting-box is on the spot, but the bullet is off the line (and center of the box), you 
		///    got a bug in your "gun turning and firing" code.
		/// </summary>
		public void DrawLineAndTarget(Color drawColor, Point2D start, Point2D end){
			// Set color to a semi-transparent one.
			Color halfTransparent = Color.FromArgb(128, drawColor);
			// Draw line and rectangle.
			Graphics.DrawLine(new Pen(halfTransparent), (int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
			Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)(end.X - 17.5), (int)(end.Y - 17.5), 36, 36);
		}
	}
}