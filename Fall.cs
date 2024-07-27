namespace fall {

    public static class Fall {

        private static LevelVoid levelVoid;

        public static void PrepareLevel() {
            levelVoid = new();
            InitVoidTrigs();
            DisablePassTrigs();
            Plugin.Print("ready to fall!");
        }

        public static void InitVoidTrigs() {
            levelVoid.FallTrigs.Value.ForEach(LevelVoid.Disable);
            levelVoid.VoidObjs.Value.ForEach(LevelVoid.Add);
        }

        public static void DisablePassTrigs() {
            levelVoid.PassTrigs.Value.ForEach(LevelVoid.Disable);
        }

        public static void RestoreLevel() {
            levelVoid.VoidTrigs.Value.ForEach(LevelVoid.Destroy);
            levelVoid.FallTrigs.Value.ForEach(LevelVoid.Enable);
            levelVoid.PassTrigs.Value.ForEach(LevelVoid.Enable);
            levelVoid = new();
            Plugin.Print("Stopped falling.");
        }
    }
}