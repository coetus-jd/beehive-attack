namespace Bee.Enums
{
    public static class Tags
    {
        public static string Player = "Player";
        public static string GameController = "GameController";
        public static string Defense = "Defense";
        public static string Base = "Base";
        public static string Enemy = "Enemy";
        public static string SwarmOfBeesController = "SwarmOfBeesController";

        /// <summary>
        /// Indicates a Transform gameobject used as a point to the enemy walk
        /// </summary>
        public static string EnemyPathPoint = "EnemyPathPoint";

        /// <summary>
        /// Indicates a gameobject that holds reference to various EnemyPathPoints
        /// </summary>
        public static string EnemyPath = "EnemyPath";


        public static string DefenseController = "DefenseController";
    }
}