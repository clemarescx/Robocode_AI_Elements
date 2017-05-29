using System;

namespace PG4500_2017_Exam1{
	public class State_Drive_Idle : State{
		public State_Drive_Idle(Marcle15 robot) : base(robot) { }

		public override State Execute(){

			if(_Robot.HasEnemy()){
				Console.WriteLine("IDLE to CHASE triggered!");
				return new State_Drive_Chase(_Robot);
			}

			return new State_Drive_Idle(_Robot);
		}
	}
}