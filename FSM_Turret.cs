using System;
using System.Collections.Generic;
using Robocode.Util;

namespace PG4500_2017_Exam1{
	/// <summary>
	/// This FSM controls the behaviour of the gun and the radar
	/// </summary>
	public class FSM_Turret : FSM{
		public State CurrentState{ get; set; }
		public FSM_Turret(Marcle15 robot) : base(robot){ CurrentState = new State_Turret_Scan(_Robot); }

		public override void Update(){
			State nextState = CurrentState.Execute();
			CurrentState = nextState;
		}
	}
}