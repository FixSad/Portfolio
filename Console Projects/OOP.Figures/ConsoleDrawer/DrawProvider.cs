namespace ConsoleDrawer
{
    public static class DrawProvider
    {
        private static IDrawer _drawer = new Drawer();

        public static IDrawer Drawer { get {  return _drawer; } }
    }
}
