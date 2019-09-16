using System;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Text;
using GLFWDotNet;
using OpenGL;
using PurpleStyrofoam.Utility;

namespace PurpleStyrofoam
{
    class Program
    {
        static void Main(string[] args)
        {
            const string windowTitle = "Purple Styrofoam"; //The title of the program
            const int windowWidth = 1200; //The width in pixels of the program
            const int windowHeight = 900; //The Height in pixels of the program
            if (GLFW.glfwInit() == 0) //Checks to see if the program successfully initializes GLFW (1 is succ, 0 is fail)
            { //If not, print an error and kill the program.
                Console.Error.WriteLine("Failed to create a GLFW window.");
                Environment.Exit(1);
            }
            
            //Creates the window (width, height, title, monitor # [set it to primary for fullscreen], idk)
            var window = GLFW.glfwCreateWindow(windowWidth, windowHeight, windowTitle, IntPtr.Zero, IntPtr.Zero);

            float[] vertices =
            {
                -0.5f, -0.25f, 0.0f,
                0.5f, -0.25f, 0.0f,
                0.0f, 0.75f, 0.0f
            };
            
            var vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, FileReader.ReadShaderFile("C:\\Users\\alexa\\Programming Projects\\PurpleStyrofoam\\Shaders\\vertex_shader.glsl"));
            Gl.CompileShader(vertexShader);
            checkStatus(vertexShader);

            var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, FileReader.ReadShaderFile("C:\\Users\\alexa\\Programming Projects\\PurpleStyrofoam\\Shaders\\fragment_shader.glsl"));
            Gl.CompileShader(fragmentShader);
            checkStatus(fragmentShader);

            var shaderProgram = Gl.CreateProgram();
            Gl.AttachShader(shaderProgram, vertexShader);
            Gl.AttachShader(shaderProgram, fragmentShader);
            Gl.LinkProgram(shaderProgram);
            //Gl.DeleteShader(vertexShader);
            //Gl.DeleteShader(fragmentShader);
            
            var vbo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) vertices.Length, vertices, BufferUsage.StaticDraw);

            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 3*sizeof(float), 0);
            Gl.EnableVertexAttribArray(0);

            var vao = Gl.GenVertexArray();

            while (GLFW.glfwWindowShouldClose(window) == 0) // Checks to see if the program should close. (1 yes, 0 no)
            {
                // OpenGL rendering
                // Implement any timing for flow control, etc (see Glfw.GetTime())
        
                if (GLFW.glfwGetKey(window, GLFW.GLFW_KEY_ESCAPE) == GLFW.GLFW_PRESS) GLFW.glfwSetWindowShouldClose(window, 1);
                
                // Clears screen
                Gl.Clear(ClearBufferMask.ColorBufferBit);
                
                // Code
                
                Gl.UseProgram(shaderProgram);
                Gl.BindVertexArray(vao);
                Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);

                // Swap the front/back buffers
                GLFW.glfwSwapBuffers(window);

                // Poll native operating system events (must be called or OS will think application is hanging)
                GLFW.glfwPollEvents();
            }
            
            GLFW.glfwTerminate(); // Kills the program / window
        }

        static void checkStatus(uint shader)
        {
            int success;
            Gl.GetShader(shader, ShaderParameterName.CompileStatus, out success);
            if (success == 0)
            {
                const int logMax = 1024;
                StringBuilder infoLog = new StringBuilder(logMax);
                int infoLogLength;
                Gl.GetShaderInfoLog(shader, logMax, out infoLogLength, infoLog);
                Gl.DeleteShader(shader);
                throw new InvalidOperationException($"Unable to compile shader: {infoLog}");
            }
        }
    }
}