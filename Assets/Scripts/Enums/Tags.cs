namespace Bee.Enums
{
    public static class Tags
    {
        public static string Player = "Player";
        public static string GameController = "GameController";
        public static string Defense = "Defense";

        /// <summary>
        /// Indicates a GameObject that holds reference to various defenses
        /// </summary>
        public static string Defenses = "Defenses";
        public static string Base = "Base";
        public static string Enemy = "Enemy";
        public static string Flower = "Flower";

        /// <summary>
        /// Indicates a Transform GameObject used as a point to the enemy walk
        /// </summary>
        public static string EnemyPathPoint = "EnemyPathPoint";

        /// <summary>
        /// Indicates a GameObject that holds reference to various EnemyPathPoints
        /// </summary>
        public static string EnemyPath = "EnemyPath";

        public static string DefenseController = "DefenseController";
        public static string Honeycomb = "Honeycomb";
        public static string Hive = "Hive";

        public static string EnemiesController { get; set; } = nameof(EnemiesController);
        public static string PunctuationController { get; set; } = nameof(PunctuationController);
        public static string EnemyFakePath { get; set; } = nameof(EnemyFakePath);
        public static string BeeQueen { get; set; } = nameof(BeeQueen);
        public static string BaseCanvas { get; set; } = nameof(BaseCanvas);
        public static string Enemies { get; set; } = nameof(Enemies);
    }
}
