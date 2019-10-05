using System;
using System.Runtime.InteropServices;
using System.Text;

using ClassicUO.Utility.Platforms;

using SDL2;

namespace ClassicUO.Utility
{
    internal static class SDL2EX
    {
        public delegate void OnGlClear(uint bit);
        public delegate void OnGlColor4f(float r, float g, float b, float a);
        public delegate void OnGlClearColor(float r, float g, float b, float a);
        public delegate void OnGlColor4fub(byte r, byte g, byte b, byte a);

        public delegate IntPtr OnLoadFunction(IntPtr module, StringBuilder sb);

        public delegate IntPtr OnSDLLoadObject(StringBuilder sb);


        private static readonly OnSDLLoadObject _loadObject;
        private static readonly OnLoadFunction _loadFunction;

        private static readonly OnGlClear _glClear;
        private static readonly OnGlColor4f _glColor4F;
        private static readonly OnGlClearColor _glClearColor;
        private static readonly OnGlColor4fub _glColor4Fub;

        static SDL2EX()
        {
            IntPtr sdl;

            if (Environment.OSVersion.Platform == PlatformID.MacOSX)
                sdl = Native.LoadLibrary("libSDL2-2.0.0.dylib");
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
                sdl = Native.LoadLibrary("libSDL2-2.0.so.0");
            else
                sdl = Native.LoadLibrary("SDL2.dll");

            IntPtr loadLib = Native.GetProcessAddress(sdl, "SDL_LoadObject");
            _loadObject = Marshal.GetDelegateForFunctionPointer<OnSDLLoadObject>(loadLib);

            IntPtr loadFunc = Native.GetProcessAddress(sdl, "SDL_LoadFunction");
            _loadFunction = Marshal.GetDelegateForFunctionPointer<OnLoadFunction>(loadFunc);

            _glClear = Marshal.GetDelegateForFunctionPointer<OnGlClear>(SDL.SDL_GL_GetProcAddress("glClear"));
            _glColor4F = Marshal.GetDelegateForFunctionPointer<OnGlColor4f>(SDL.SDL_GL_GetProcAddress("glColor4f"));
            _glColor4Fub = Marshal.GetDelegateForFunctionPointer<OnGlColor4fub>(SDL.SDL_GL_GetProcAddress("glColor4ub"));
            _glClearColor = Marshal.GetDelegateForFunctionPointer<OnGlClearColor>(SDL.SDL_GL_GetProcAddress("glClearColor"));
        }

        public static void glClear(uint bit)
            => _glClear(bit);

        public static void glColor4f(float r, float g, float b, float a)
        {
            _glColor4F(r, g, b, a);
        }

        public static void glColor4ub(byte r, byte g, byte b, byte a)
        {
            _glColor4Fub(r, g, b, a);
        }

        public static void glClearColor(float r, float g, float b, float a)
        {
            _glClearColor(r, g, b, a);
        }

        public static IntPtr SDL_LoadObject(string name)
        {
            return _loadObject(new StringBuilder(name));
        }

        public static IntPtr SDL_LoadFunction(IntPtr module, string name)
        {
            return _loadFunction(module, new StringBuilder(name));
        }
    }
}