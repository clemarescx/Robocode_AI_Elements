namespace PG4500_2017_Exam1{
	public abstract class State{
		protected Marcle15 _Robot;

		protected State(Marcle15 robot) { _Robot = robot; }
//		public abstract EStates Execute();
		public abstract State Execute();
	}
}