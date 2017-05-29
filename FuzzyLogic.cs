namespace PG4500_2017_Exam1{
	public static class FuzzyLogic{
		private static Marcle15 _r;
		public static void Init(Marcle15 robot) => _r = robot;
		public static bool PlayerIsHealthy(){ return _r.Energy > 50; }
		public static bool PlayerIsDying(){ return _r.Energy < 30; }
	}
}