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

namespace ClassicUO.Development
{
    using System;
    using SDL2;

    public class GameWindow : IDisposable
    {
        internal GameWindow()
        {
            Valid = false;
            Handle = IntPtr.Zero;

            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"Could not init SDL: {SDL.SDL_GetError()}");
                return;
            }

            SDL.SDL_WindowFlags windowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
            Handle = SDL.SDL_CreateWindow("SDL Test", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 640, 480, windowFlags);

            if (Handle == IntPtr.Zero)
            {
                Console.WriteLine($"Could not create window: {SDL.SDL_GetError()}");
                return;
            }

            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 2);
            //SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, SDL.SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);

            if (SDL.SDL_GL_CreateContext(Handle) == IntPtr.Zero)
            {
                Console.WriteLine($"Could not create OpenGL context: {SDL.SDL_GetError()}");
                return;
            }

            Valid = true;
        }

        internal IntPtr Handle { get; }

        internal bool Valid { get; }

        public void Dispose()
        {
            SDL.SDL_DestroyWindow(Handle);
            SDL.SDL_Quit();
        }
    }
}
