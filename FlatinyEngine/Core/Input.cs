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

        public static int MouseX { get; private set; }
        public static int MouseY { get; private set; }

        public static int MouseXDelta { get; private set; }
        public static int MouseYDelta { get; private set; }

        public static float MouseScrollDelta => mouseState.WheelPrecise - lastMouseState.WheelPrecise;

        public static UpdateProcess Init(GameWindow window)
        {
            gameWindow = window;

            if (gameWindow != null)
            {
                gameWindow.FocusedChanged += GameWindow_FocusedChanged;
                gameWindow.MouseMove += (sender, e)=> {
                    MouseX = e.X;
                    MouseY = e.Y;
                    MouseXDelta = e.XDelta;
                    MouseYDelta = e.YDelta;
                };
            }

            var handler = new UpdateProcess();
            handler.onUpdate += Update;
            return handler;
        }

        private static void GameWindow_FocusedChanged(object sender, System.EventArgs e)
        {
            if (gameWindow.Focused)
            {
                ResetInput();
            }
        }

        public static void ResetInput()
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
        }

        private static void Update(float deltaTime)
        {
            if (!gameWindow.Focused)
                return;

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        public static bool GetKey(Key key)
        {
            return keyboardState[key];
        }

        public static bool GetKeyDown(Key key)
        {
            return keyboardState[key] && (keyboardState[key] != lastKeyboardState[key]);
        }

        public static bool GetKeyUp(Key key)
        {
            return lastKeyboardState[key] && (keyboardState[key] != lastKeyboardState[key]);
        }

        public static bool GetMouseButton(MouseButton button)
        {
            return mouseState[button];
        }

        public static bool GetMouseButtonDown(MouseButton button)
        {
            return mouseState[button] && (mouseState[button] != lastMouseState[button]);
        }

        public static bool GetMouseButtonUp(MouseButton button)
        {
            return lastMouseState[button] && (mouseState[button] != lastMouseState[button]);
        }

    }
}
