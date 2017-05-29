namespace PG4500_2017_Exam1{
	/// <summary>
	/// This state machine follows the Active State Generates model,
	/// which means the FSM_Drive class contains all available states 
	/// and call the current state's Execute function. 
	/// 
	/// The responsibility to switch states is delegated to each state,
	/// excepted for the Flee state, which is higher in hierarchy 
	/// over the other states.
	/// </summary>
	public class FSM_Drive : FSM{
		public State CurrentState{ get; set; }

		public FSM_Drive(Marcle15 robot) : base(robot){ CurrentState = new State_Drive_Idle(_Robot); }

		public override void Update(){
			if(FuzzyLogic.PlayerIsDying())
				CurrentState = new State_Drive_Ram(_Robot);

			State nextState = CurrentState.Execute();
			CurrentState = nextState;
		}
	}
}