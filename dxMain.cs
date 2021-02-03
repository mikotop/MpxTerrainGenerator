using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TerrainGenerator
{
    public partial class dxMain : Form
    {
        private Device dev = null;
        private VertexBuffer verBuff = null;

        float angle = 0.0f;

        CustomVertex.PositionColored[] verts = null;

        public dxMain()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            InitializeComponent();
            InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            PresentParameters prep = new PresentParameters();
            prep.Windowed = true;
            prep.SwapEffect = SwapEffect.Discard;

            dev = new Device(0,DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, prep);
            verBuff = new VertexBuffer(typeof(CustomVertex.PositionColored), 36, dev, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            verBuff.Created += new EventHandler(this.onVertexBufferCreate);
            onVertexBufferCreate(verBuff, null);

            verBuff.SetData(verts, 0, LockFlags.None);
        }
        
        private void onVertexBufferCreate(object sender, EventArgs e)
        {
            VertexBuffer vertex = (VertexBuffer)sender;

            verts = new CustomVertex.PositionColored[36]; // points on the screen (sise of vertexs[0-...n + 1])

            // Cube
            // 6 vertex = 1 side (2 triangles)
            // Green side
            verts[0] = new CustomVertex.PositionColored(-1.0f, 1.0f, 1.0f, Color.Green.ToArgb());
            verts[1] = new CustomVertex.PositionColored(-1.0f, -1.0f, 1.0f, Color.Green.ToArgb());
            verts[2] = new CustomVertex.PositionColored(1.0f, 1.0f, 1.0f, Color.Green.ToArgb());

            verts[3] = new CustomVertex.PositionColored(-1.0f, -1.0f, 1.0f, Color.Green.ToArgb());
            verts[4] = new CustomVertex.PositionColored(1.0f, -1.0f, 1.0f, Color.Green.ToArgb());
            verts[5] = new CustomVertex.PositionColored(1.0f, 1.0f, 1.0f, Color.Green.ToArgb());

            // Blue side
            verts[6] = new CustomVertex.PositionColored(-1.0f, 1.0f, -1.0f, Color.Blue.ToArgb());
            verts[7] = new CustomVertex.PositionColored(1.0f, 1.0f, -1.0f, Color.Blue.ToArgb());
            verts[8] = new CustomVertex.PositionColored(-1.0f, -1.0f, -1.0f, Color.Blue.ToArgb());

            verts[9] = new CustomVertex.PositionColored(-1.0f, -1.0f, -1.0f, Color.Blue.ToArgb());
            verts[10] = new CustomVertex.PositionColored(1.0f, 1.0f, -1.0f, Color.Blue.ToArgb());
            verts[11] = new CustomVertex.PositionColored(1.0f, -1.0f, -1.0f, Color.Blue.ToArgb());
            
            // Red side
            verts[12] = new CustomVertex.PositionColored(-1.0f, 1.0f, 1.0f, Color.Red.ToArgb());
            verts[13] = new CustomVertex.PositionColored(1.0f, 1.0f, -1.0f, Color.Red.ToArgb());
            verts[14] = new CustomVertex.PositionColored(-1.0f, 1.0f, -1.0f, Color.Red.ToArgb());

            verts[15] = new CustomVertex.PositionColored(-1.0f, 1.0f, 1.0f, Color.Red.ToArgb());
            verts[16] = new CustomVertex.PositionColored(1.0f, 1.0f, 1.0f, Color.Red.ToArgb());
            verts[17] = new CustomVertex.PositionColored(1.0f, 1.0f, -1.0f, Color.Red.ToArgb());

            // Pink side
            verts[18] = new CustomVertex.PositionColored(-1.0f, -1.0f, 1.0f, Color.Pink.ToArgb());
            verts[19] = new CustomVertex.PositionColored(-1.0f, -1.0f, -1.0f, Color.Pink.ToArgb());
            verts[20] = new CustomVertex.PositionColored(1.0f, -1.0f, -1.0f, Color.Pink.ToArgb());

            verts[21] = new CustomVertex.PositionColored(-1.0f, -1.0f, 1.0f, Color.Pink.ToArgb());
            verts[22] = new CustomVertex.PositionColored(1.0f, -1.0f, -1.0f, Color.Pink.ToArgb());
            verts[23] = new CustomVertex.PositionColored(1.0f, -1.0f, 1.0f, Color.Pink.ToArgb());
            
            // Yellow side
            verts[24] = new CustomVertex.PositionColored(-1.0f, 1.0f, 1.0f, Color.Yellow.ToArgb());
            verts[25] = new CustomVertex.PositionColored(-1.0f, -1.0f, -1.0f, Color.Yellow.ToArgb());
            verts[26] = new CustomVertex.PositionColored(-1.0f, -1.0f, 1.0f, Color.Yellow.ToArgb());

            verts[27] = new CustomVertex.PositionColored(-1.0f, 1.0f, -1.0f, Color.Yellow.ToArgb());
            verts[28] = new CustomVertex.PositionColored(-1.0f, -1.0f, -1.0f, Color.Yellow.ToArgb());
            verts[29] = new CustomVertex.PositionColored(-1.0f, 1.0f, 1.0f, Color.Yellow.ToArgb());

            // Black side
            verts[30] = new CustomVertex.PositionColored(1.0f, 1.0f, 1.0f, Color.Black.ToArgb());
            verts[31] = new CustomVertex.PositionColored(1.0f, -1.0f, 1.0f, Color.Black.ToArgb());
            verts[32] = new CustomVertex.PositionColored(1.0f, -1.0f, -1.0f, Color.Black.ToArgb());

            verts[33] = new CustomVertex.PositionColored(1.0f, 1.0f, -1.0f, Color.Black.ToArgb());
            verts[34] = new CustomVertex.PositionColored(1.0f, 1.0f, 1.0f, Color.Black.ToArgb());
            verts[35] = new CustomVertex.PositionColored(1.0f, -1.0f, -1.0f, Color.Black.ToArgb());

            /*-------------------------------------------------------------------------------------------
            // Triangle
            // [0]
            verts[0].Position = new Vector3(0, 1, 1); // X, Y, Z
            verts[0].Color = Color.Green.ToArgb(); // set color for this pos to ARGB

            // [1]
            verts[1].Position = new Vector3(-1, -1, 0); // X, Y, Z
            verts[1].Color = Color.Blue.ToArgb(); // set color for this pos to ARGB

            // [2]
            verts[2].Position = new Vector3(1, -1, 0); // X, Y, Z 
            verts[2].Color = Color.Red.ToArgb(); // set color for this pos to ARGB 
                               ^                                  |
                               |  choose what you want to use it  |
                               |                                  v
            verts[0] = new CustomVertex.PositionColored(0.0f, 1.0f, 1.0f, Color.Green.ToArgb());
            verts[1] = new CustomVertex.PositionColored(-1.0f, -1.0f, 0.0f, Color.Blue.ToArgb());
            verts[2] = new CustomVertex.PositionColored(1.0f, -1.0f, 0.0f, Color.Red.ToArgb());

            ------------------------------------------------------------------------------------------*/

            vertex.SetData(verts, 0, LockFlags.None);
        }

        private void setCamera()
        {
            dev.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 100.0f);
            dev.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -25.0f), new Vector3(), new Vector3(0.0f, 1.0f, 0.0f));
            dev.Transform.World = Matrix.RotationYawPitchRoll(angle / (float)Math.PI / 2, angle / (float)Math.PI * 6, angle / (float)Math.PI);
            //dev.Transform.World = Matrix.RotationY(angle);
            angle += 0.05f;

            dev.RenderState.Lighting = false;
            dev.RenderState.CullMode = Cull.CounterClockwise;
        }

        private void dxMain_Paint(object sender, PaintEventArgs e)
        {
            dev.Clear(ClearFlags.Target, Color.LightBlue, 1.0f, 0);

            setCamera();

            dev.BeginScene();
            dev.VertexFormat = CustomVertex.PositionColored.Format;
            dev.SetStreamSource(0, verBuff, 0);
            dev.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            dev.Transform.World = Matrix.RotationYawPitchRoll(angle / (float)Math.PI / 2, angle / (float)Math.PI * 4, angle / (float)Math.PI) * (Matrix.Translation(5.0f, 0.0f, 0.0f));
            dev.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            dev.Transform.World = Matrix.RotationYawPitchRoll(angle / (float)Math.PI / 2, angle / (float)Math.PI * 4, angle / (float)Math.PI) * (Matrix.Translation(-5.0f, 0.0f, 0.0f));
            dev.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            dev.EndScene();
            dev.Present();
            this.Invalidate();
        }
    }
}
