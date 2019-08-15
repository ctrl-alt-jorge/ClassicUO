// Copyright (C) 2019 ClassicUO Development Community on Github.
//
// This project is an alternative client for the game Ultima Online.
// The goal of this is to develop a lightweight client considering
//  new technologies.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

namespace ClassicUO.Input
{
    using Microsoft.Xna.Framework;
    using SDL2;

    internal class InputMouseEvent : InputEvent
    {
        private readonly int clicks;
        private readonly int data;

        public InputMouseEvent(MouseEvent type, MouseButton button, int clicks, int x, int y, int data, SDL.SDL_Keymod mod)
            : base(mod)
        {
            EventType = type;
            Button = button;
            this.clicks = clicks;
            X = x;
            Y = y;
            this.data = data;
        }

        public InputMouseEvent(MouseEvent type, InputMouseEvent parent)
            : base(parent)
        {
            EventType = type;
            Button = parent.Button;
            clicks = parent.clicks;
            X = parent.X;
            Y = parent.Y;
            data = parent.data;
        }

        public int X { get; }

        public int Y { get; }

        public MouseEvent EventType { get; }

        public Point Position => new Point(X, Y);

        public MouseButton Button { get; }
    }
}