namespace ClassicUO.Graphics
{
    using System;
    using System.Diagnostics;
    using ClassicUO.EngineNew.Graphics.OpenGL;
    using ClassicUO.Graphics.OpenGL;

    internal class ShaderProgram
    {
        private const string TestVertexSource = "" +
            "#version 440 core\n" +
            "void main(void)\n" +
            "{\n" +
            "   gl_Position = vec4(0.25, -0.25, 0.5, 1.0);\n" +
            "}";

        private const string TestFragmentSource = "" +
            "#version 440 core\n" +
            "out vec4 colour\n" +
            "void main(void)\n" +
            "{\n" +
            "   colour = vec4(0.25, -0.25, 0.5, 1.0);\n" +
            "}";

        public ShaderProgram()
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            CheckGLError();

            GL.ShaderSource(vertexShader, TestVertexSource);
            CheckGLError();

            GL.CompileShader(vertexShader);
            CheckGLError();


            int program = GL.CreateProgram();
            CheckGLError();

            GL.AttachShader(program, vertexShader);
            CheckGLError();

            GL.LinkProgram(program);
            CheckGLError();
        }

        [Conditional("DEBUG")]
        private void CheckGLError()
        {
        }
    }


}
