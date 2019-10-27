using OpenTK;
using OpenTK.Input;

namespace MaximovInk.FlatinyEngine
{
    public static class Input
    {
        private static GameWindow window;

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;

        public static int MouseX { get; private set; }
        public static int MouseY { get; private set; }

        public static int MouseXDelta { get; private set; }
        public static int MouseYDelta { get; private set; }

        public static float MouseScrollDelta => mouseState.WheelPrecise - lastMouseState.WheelPrecise;

        public static void Init(GameWindow window)
        {
            Input.window = window;
            window.MouseMove += MouseMove;
            window.UpdateFrame += Update;
            window.FocusedChanged += FocusedChanged;
        }

        private static void MouseMove(object sender, MouseMoveEventArgs e)
        {
            MouseX = e.Position.X;
            MouseY = e.Position.Y;
            MouseXDelta = e.XDelta;
            MouseYDelta = e.YDelta;
        }

        private static void Update(object sender, FrameEventArgs e)
        {
            if (!window.Focused)
                return;

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        private static void FocusedChanged(object sender, System.EventArgs e)
        {
            if (window.Focused)
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
