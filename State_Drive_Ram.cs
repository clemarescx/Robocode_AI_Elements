using System;
using Santom;

namespace PG4500_2017_Exam1{
	public class State_Drive_Ram : State{
		public State_Drive_Ram(Marcle15 robot) : base(robot) { }

		public override State Execute(){
			if(!_Robot.HasEnemy()){
				Console.WriteLine("RAM to IDLE triggered");
				return new State_Drive_Idle(_Robot);
			}

			if(FuzzyLogic.PlayerIsHealthy()){
				return new State_Drive_Chase(_Robot);
			}

			Ram();

			return new State_Drive_Ram(_Robot);
		}

		private void Ram(){
			var ramVector = new Vector2D(_Robot.Position, _Robot.Enemy.Position);
			Steering.Steer(_Robot, ramVector);
		}
	}
}