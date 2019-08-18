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

namespace ClassicUO.EngineNew.Graphics.OpenGL
{
    using System.Runtime.InteropServices;

    internal static class GL
    {
        public const uint GL_COLOR_BUFFER_BIT = 0x00004000;

        [DllImport("opengl32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "glClear")]
        internal static extern void Clear(uint mask);

        [DllImport("opengl32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "glClearColor")]
        internal static extern void ClearColor(float red, float green, float blue, float alpha);
    }
}
