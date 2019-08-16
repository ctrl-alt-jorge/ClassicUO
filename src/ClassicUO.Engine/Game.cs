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
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Game : Microsoft.Xna.Framework.Game
    {
        private static Game instance = null;

        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public Game(string[] args)
        {
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

        public static void SetPreferredBackBufferSize(int width, int height)
        {
            instance.graphicsDeviceManager.PreferredBackBufferWidth = width;
            instance.graphicsDeviceManager.PreferredBackBufferHeight = height;
            instance.graphicsDeviceManager.ApplyChanges();
        }

        internal static Game Instance { get { return Game.instance; } }
    }
}
