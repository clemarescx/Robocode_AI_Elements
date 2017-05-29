using System;

namespace PG4500_2017_Exam1{
	internal class State_Turret_Scan : State{
		public State_Turret_Scan(Marcle15 robot) : base(robot) { }

		public override State Execute(){
			if(_Robot.HasEnemy()){
				Console.WriteLine("SCAN to AIM triggered!");
				return new State_Turret_Aim(_Robot);
			}

			return new State_Turret_Scan(_Robot);
		}
	}
}