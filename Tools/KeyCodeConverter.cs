using UnityEngine;
using UnityEngine.InputSystem;

// Simple tool for mapping any old player saved key-binding files from the old input system to the new system
public static class KeyCodeConverter
{
    private static readonly Dictionary<KeyCode, Key> keyCodeToNewKeyMap = new Dictionary<KeyCode, Key>
    {
        { KeyCode.None, Key.None },
        { KeyCode.Backspace, Key.Backspace },
        { KeyCode.Delete, Key.Delete },
        { KeyCode.Tab, Key.Tab },
        { KeyCode.Clear, Key.Clear },
        { KeyCode.Return, Key.Enter },
        { KeyCode.Pause, Key.Pause },
        { KeyCode.Escape, Key.Escape },
        { KeyCode.Space, Key.Space },
        { KeyCode.Keypad0, Key.Numpad0 },
        { KeyCode.Keypad1, Key.Numpad1 },
        { KeyCode.Keypad2, Key.Numpad2 },
        { KeyCode.Keypad3, Key.Numpad3 },
        { KeyCode.Keypad4, Key.Numpad4 },
        { KeyCode.Keypad5, Key.Numpad5 },
        { KeyCode.Keypad6, Key.Numpad6 },
        { KeyCode.Keypad7, Key.Numpad7 },
        { KeyCode.Keypad8, Key.Numpad8 },
        { KeyCode.Keypad9, Key.Numpad9 },
        { KeyCode.KeypadPeriod, Key.NumpadPeriod },
        { KeyCode.KeypadDivide, Key.NumpadDivide },
        { KeyCode.KeypadMultiply, Key.NumpadMultiply },
        { KeyCode.KeypadMinus, Key.NumpadMinus },
        { KeyCode.KeypadPlus, Key.NumpadPlus },
        { KeyCode.KeypadEnter, Key.NumpadEnter },
        { KeyCode.KeypadEquals, Key.NumpadEquals },
        { KeyCode.UpArrow, Key.UpArrow },
        { KeyCode.DownArrow, Key.DownArrow },
        { KeyCode.RightArrow, Key.RightArrow },
        { KeyCode.LeftArrow, Key.LeftArrow },
        { KeyCode.Insert, Key.Insert },
        { KeyCode.Home, Key.Home },
        { KeyCode.End, Key.End },
        { KeyCode.PageUp, Key.PageUp },
        { KeyCode.PageDown, Key.PageDown },
        { KeyCode.F1, Key.F1 },
        { KeyCode.F2, Key.F2 },
        { KeyCode.F3, Key.F3 },
        { KeyCode.F4, Key.F4 },
        { KeyCode.F5, Key.F5 },
        { KeyCode.F6, Key.F6 },
        { KeyCode.F7, Key.F7 },
        { KeyCode.F8, Key.F8 },
        { KeyCode.F9, Key.F9 },
        { KeyCode.F10, Key.F10 },
        { KeyCode.F11, Key.F11 },
        { KeyCode.F12, Key.F12 },
        { KeyCode.F13, Key.F13 },
        { KeyCode.F14, Key.F14 },
        { KeyCode.F15, Key.F15 },
        { KeyCode.Alpha0, Key.Digit0 },
        { KeyCode.Alpha1, Key.Digit1 },
        { KeyCode.Alpha2, Key.Digit2 },
        { KeyCode.Alpha3, Key.Digit3 },
        { KeyCode.Alpha4, Key.Digit4 },
        { KeyCode.Alpha5, Key.Digit5 },
        { KeyCode.Alpha6, Key.Digit6 },
        { KeyCode.Alpha7, Key.Digit7 },
        { KeyCode.Alpha8, Key.Digit8 },
        { KeyCode.Alpha9, Key.Digit9 },
        { KeyCode.Exclaim, Key.None }, // No direct equivalent
        { KeyCode.DoubleQuote, Key.None }, // No direct equivalent
        { KeyCode.Hash, Key.None }, // No direct equivalent
        { KeyCode.Dollar, Key.None }, // No direct equivalent
        { KeyCode.Percent, Key.None }, // No direct equivalent
        { KeyCode.Ampersand, Key.None }, // No direct equivalent
        { KeyCode.Quote, Key.None }, // No direct equivalent
        { KeyCode.LeftParen, Key.None }, // No direct equivalent
        { KeyCode.RightParen, Key.None }, // No direct equivalent
        { KeyCode.Asterisk, Key.None }, // No direct equivalent
        { KeyCode.Plus, Key.None }, // No direct equivalent
        { KeyCode.Comma, Key.Comma },
        { KeyCode.Minus, Key.Minus },
        { KeyCode.Period, Key.Period },
        { KeyCode.Slash, Key.Slash },
        { KeyCode.Colon, Key.None }, // No direct equivalent
        { KeyCode.Semicolon, Key.Semicolon },
        { KeyCode.Less, Key.None }, // No direct equivalent
        { KeyCode.Equals, Key.Equals },
        { KeyCode.Greater, Key.None }, // No direct equivalent
        { KeyCode.Question, Key.None }, // No direct equivalent
        { KeyCode.At, Key.None }, // No direct equivalent
        { KeyCode.LeftBracket, Key.LeftBracket },
        { KeyCode.Backslash, Key.Backslash },
        { KeyCode.RightBracket, Key.RightBracket },
        { KeyCode.Caret, Key.None }, // No direct equivalent
        { KeyCode.Underscore, Key.None }, // No direct equivalent
        { KeyCode.BackQuote, Key.None }, // No direct equivalent
        { KeyCode.A, Key.A },
        { KeyCode.B, Key.B },
        { KeyCode.C, Key.C },
        { KeyCode.D, Key.D },
        { KeyCode.E, Key.E },
        { KeyCode.F, Key.F },
        { KeyCode.G, Key.G },
        { KeyCode.H, Key.H },
        { KeyCode.I, Key.I },
        { KeyCode.J, Key.J },
        { KeyCode.K, Key.K },
        { KeyCode.L, Key.L },
        { KeyCode.M, Key.M },
        { KeyCode.N, Key.N },
        { KeyCode.O, Key.O },
        { KeyCode.P, Key.P },
        { KeyCode.Q, Key.Q },
        { KeyCode.R, Key.R },
        { KeyCode.S, Key.S },
        { KeyCode.T, Key.T },
        { KeyCode.U, Key.U },
        { KeyCode.V, Key.V },
        { KeyCode.W, Key.W },
        { KeyCode.X, Key.X },
        { KeyCode.Y, Key.Y },
        { KeyCode.Z, Key.Z },
        { KeyCode.Numlock, Key.NumLock },
        { KeyCode.CapsLock, Key.CapsLock },
        { KeyCode.ScrollLock, Key.ScrollLock },
        { KeyCode.RightShift, Key.RightShift },
        { KeyCode.LeftShift, Key.LeftShift },
        { KeyCode.RightControl, Key.RightCtrl },
        { KeyCode.LeftControl, Key.LeftCtrl },
        { KeyCode.RightAlt, Key.RightAlt },
        { KeyCode.LeftAlt, Key.LeftAlt },
        { KeyCode.LeftCommand, Key.LeftMeta },
        { KeyCode.LeftApple, Key.LeftMeta },
        { KeyCode.LeftWindows, Key.LeftMeta },
        { KeyCode.RightCommand, Key.RightMeta },
        { KeyCode.RightApple, Key.RightMeta },
        { KeyCode.RightWindows, Key.RightMeta },
        { KeyCode.AltGr, Key.AltGr },
        { KeyCode.Help, Key.Help },
        { KeyCode.Print, Key.PrintScreen },
        { KeyCode.SysReq, Key.SysReq },
        { KeyCode.Break, Key.Pause },
        { KeyCode.Menu, Key.ContextMenu },
        // Add other keys you need
    };

    public static Key Convert(KeyCode keyCode)
    {
        if (keyCodeToNewKeyMap.TryGetValue(keyCode, out Key newKey))
        {
            return newKey;
        }
        else
        {
            Debug.LogWarning($"No mapping found for KeyCode: {keyCode}");
            return Key.None; // Or however you want to handle unmapped keys
        }
    }
}
