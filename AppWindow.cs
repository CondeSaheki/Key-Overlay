using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace KeyOverlay
{
    public class AppWindow
    {
        // configs        
        private readonly uint _maxFPS = 250;
        private static readonly uint _sizeX = 302;
        private static readonly uint _sizeY = 166;
        private static readonly uint _fontSize = 150;
        private static readonly uint _margin = 10;
        private readonly Key _key = new Key("X");
        private readonly int _delay = 200;
        private readonly Color _backgroundColor = new Color(0, 0, 0, 255);
        private readonly Color _fontColor1 = new Color(255, 255, 255, 255);
        private readonly Color _fontColor2 = new Color(0, 255, 0, 255);
        private readonly Color _fontColor3 = new Color(255, 255, 0, 255);
        private readonly Color _fontColor4 = new Color(255, 0, 0, 255);

        // data
        private readonly RenderWindow _window = new RenderWindow(new VideoMode(_sizeX, _sizeY), "Key Counter Gamer", Styles.Default);
        public static readonly Font _font = new Font(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "VarelaRound-Regular.ttf")));
        
        private void OnClose(object sender, EventArgs e)
        {
            _window.Close();
        }

        public void Run()
        {
            _window.Closed += OnClose;
            _window.SetFramerateLimit(_maxFPS);
            Stopwatch clock = new Stopwatch();
            clock.Start();

            while (_window.IsOpen)
            {
                _window.Clear(_backgroundColor);
                _window.DispatchEvents();

                if (_key.isKey && Keyboard.IsKeyPressed(_key.KeyboardKey) || !_key.isKey && Mouse.IsButtonPressed(_key.MouseButton))
                {
                    _key.Hold++;
                }
                else
                {
                    _key.Hold = 0;
                    if (clock.ElapsedMilliseconds >= _delay)
                    {
                        _key.Counter = 15;
                    }
                }
                if (_key.Hold == 1)
                {
                    clock.Reset();
                    clock.Start();
                    _key.Counter--;
                }
                if (_key.Counter != 0)
                {
                    var text = new Text(Convert.ToString(_key.Counter), _font);
                    text.CharacterSize = (uint)(_fontSize);
                    text.Style = Text.Styles.Bold;
                    switch(_key.Counter)
                    {
                        case 2:
                            text.FillColor = _fontColor2;
                        break;
                        case 1:
                            text.FillColor = _fontColor3;
                        break;
                        case 0:
                            text.FillColor = _fontColor4;
                        break;
                        default:
                            text.FillColor = _fontColor1;
                    }
                    text.Origin = new Vector2f(((Convert.ToString(_key.Counter)).Length - 1) * 2 * _fontSize * 0.32f + _fontSize * 0.32f, _fontSize * 0.64f);
                    text.Position = new Vector2f(_sizeX - _fontSize * 0.32f - _margin, _sizeY / 2f);
                    _window.Draw(text);
                }
                _window.Display();
            }
        }
    }
}