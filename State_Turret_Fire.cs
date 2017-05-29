using System;

namespace PG4500_2017_Exam1{
	internal class State_Turret_Fire : State{
		private readonly double _firepower;
		public State_Turret_Fire(Marcle15 robot, double firepower) : base(robot){ _firepower = firepower; }

		public override State Execute(){
			_Robot.SetFire(_firepower);
			return new State_Turret_Aim(_Robot);
		}
	}
}