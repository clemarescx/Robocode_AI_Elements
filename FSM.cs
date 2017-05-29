namespace PG4500_2017_Exam1{
	public abstract class FSM{
		protected Marcle15 _Robot{ get; }
		protected FSM(Marcle15 robot) { _Robot = robot; }

		public abstract void Update();
	}
}