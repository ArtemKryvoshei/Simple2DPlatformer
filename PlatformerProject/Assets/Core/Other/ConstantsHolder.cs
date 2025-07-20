namespace Core.Other
{
    public static class ConstantsHolder
    {
        //Scene names
        public const string LOADER_SCENE_NAME = "BootstrapScene";
        public const string GAMEPLAY_SCENE_NAME = "GameplayScene";  
        
        
        //Input
        public const float SWIPE_THRESHOLD = 0.01f;
        public const string TAP_ACTION_NAME = "Tap";
        public const string TOUCH_START_ACTION_NAME = "TouchStart";
        public const string TOUCH_END_ACTION_NAME = "TouchEnd";
        
        //Other
        public const float ROAD_TILE_SPACING = 29.75f;
        public const float ROAD_END_POINT_OFFSET = 5f;
        public const string ROAD_POOL_INIT_NAME = "RoadPoolInitializer";
        public const string ENEMIES_POOL_INIT_NAME = "EnemiesPoolInitializer";
        public const string BULLETS_POOL_INIT_NAME = "BulletsPoolInitializer";
        public const int LOAD_SCREEN_ADDITIONAL_WAIT = 3000;
    }
}