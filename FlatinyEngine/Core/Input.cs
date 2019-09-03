using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using OpenTK;
using OpenTK.Input;

namespace MaximovInk.FlatinyEngine
{
    public static class Input
    {
        private static GameWindow gameWindow;

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;

        public static int MouseX => mouseState.X;
        public static int MouseY => mouseState.Y;

        public static float MouseScrollDelta => mouseState.WheelPrecise - lastMouseState.WheelPrecise;

        public static int MouseXDelta => mouseState.X - lastMouseState.X;
        public static int MouseYDelta => mouseState.Y - lastMouseState.Y;

        public static UpdateProcess Init(GameWindow window)
        {
            gameWindow = window;

            if (gameWindow != null)
            {
                gameWindow.KeyUp += KeyUp;
                gameWindow.KeyDown += KeyDown;
                gameWindow.MouseDown += MouseDown;
                gameWindow.MouseUp += MouseUp;
            }

            var handler = new UpdateProcess();
            handler.onUpdate += Update;
            return handler;
        }

        private static void Update(float deltaTime)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        private static void KeyDown(object sender, KeyboardKeyEventArgs e) =>
            keyboardState = e.Keyboard;

        private static void KeyUp(object sender, KeyboardKeyEventArgs e) =>
            keyboardState = e.Keyboard;

        private static void MouseDown(object sender, MouseButtonEventArgs args) =>
            mouseState = args.Mouse;

        private static void MouseUp(object sender, MouseButtonEventArgs args) =>
            mouseState = args.Mouse;

        public static bool GetKey(Key key) => keyboardState[key];

        public static bool GetKeyDown(Key key) =>
            keyboardState[key] && (keyboardState[key] != lastKeyboardState[key]);

        public static bool GetKeyUp(Key key) =>
            lastKeyboardState[key] && (keyboardState[key] != lastKeyboardState[key]);

        public static bool GetMouseButton(MouseButton button) =>
            mouseState[button];

        public static bool GetMouseButtonDown(MouseButton button) =>
           mouseState[button] && (mouseState[button] != lastMouseState[button]);

        public static bool GetMouseButtonUp(MouseButton button) =>
            lastMouseState[button] && (mouseState[button] != lastMouseState[button]);
    }
}
