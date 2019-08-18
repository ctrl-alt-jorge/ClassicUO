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

namespace ClassicUO.NewEngine
{
    using System;
    using System.Reflection;
    using ClassicUO.EngineNew.Graphics.OpenGL;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SDL2;

    public class Game : Microsoft.Xna.Framework.Game
    {
        protected readonly bool isHighDPI = Environment.GetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI") == "1";

        private static Game instance = null;
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private readonly Development.GameWindow gameWindow;

        public Game(string[] args)
        {
#if FALSE    //  Set to false for original behaviour.
            gameWindow = new Development.GameWindow();

            if (gameWindow.Valid)
            {
               TickNew();
               return;
            }
#endif
            instance = this;

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreparingDeviceSettings += (sender, e) => e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;

            if (graphicsDeviceManager.GraphicsDevice.Adapter.IsProfileSupported(GraphicsProfile.HiDef))
            {
                graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            }

            graphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            graphicsDeviceManager.ApplyChanges();
         }

        public static Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        public static bool AllowWindowResizing
        {
            get => instance.Window.AllowUserResizing;
            set => instance.Window.AllowUserResizing = value;
        }

        public static bool IsMaximized
        {
            get
            {
                IntPtr wnd = SDL.SDL_GL_GetCurrentWindow();
                uint flags = SDL.SDL_GetWindowFlags(wnd);

                return (flags & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED) != 0;
            }

            set
            {
                if (IsMaximized == value)
                {
                    return;
                }

                IntPtr wnd = SDL.SDL_GL_GetCurrentWindow();

                if (value)
                {
                    SDL.SDL_MaximizeWindow(wnd);
                }
                else
                {
                    SDL.SDL_RestoreWindow(wnd);
                }
            }
        }

        public static int WindowWidth
        {
            get => instance.graphicsDeviceManager.PreferredBackBufferWidth;
            set
            {
                instance.graphicsDeviceManager.PreferredBackBufferWidth = value;
                instance.graphicsDeviceManager.ApplyChanges();
            }
        }

        public static int WindowHeight
        {
            get => instance.graphicsDeviceManager.PreferredBackBufferHeight;
            set
            {
                instance.graphicsDeviceManager.PreferredBackBufferHeight = value;
                instance.graphicsDeviceManager.ApplyChanges();
            }
        }

        internal static Game Instance
        {
            get { return Game.instance; }
        }

        public static void SetPreferredBackBufferSize(int width, int height)
        {
            instance.graphicsDeviceManager.PreferredBackBufferWidth = width;
            instance.graphicsDeviceManager.PreferredBackBufferHeight = height;
            instance.graphicsDeviceManager.ApplyChanges();
        }

        public void TickNew()
        {
            while (true)
            {
                SDL.SDL_PollEvent(out SDL.SDL_Event sdlEvent);

                if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {
                    SDL.SDL_Quit();
                    break;
                }

                GL.Clear(GL.GL_COLOR_BUFFER_BIT);
                GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);

                SDL.SDL_GL_SwapWindow(gameWindow.Handle);
            }
        }
    }
}
