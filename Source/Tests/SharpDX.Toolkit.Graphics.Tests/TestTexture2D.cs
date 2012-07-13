﻿// Copyright (c) 2010-2012 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace SharpDX.Toolkit.Graphics.Tests
{
    /// <summary>
    /// Tests for <see cref="Texture2DBase"/>
    /// </summary>
    [TestFixture]
    [Description("Tests SharpDX.Toolkit.Graphics.Texture2D")]
    public unsafe class TestTexture2D
    {
        [Test]
        public void TestConstructors()
        {
            var defaultAdapter = GraphicsAdapter.Default;



            var device = GraphicsDevice.New(DriverType.Hardware, DeviceCreationFlags.Debug);


            var bytecode = ShaderBytecode.Compile(@"
struct Toto {
    float4 Position : SV_POSITION;
    float4 Test : TEST;
    float4 Test1 : TEST1;
    float4 Test2 : TEST2;
};

float4 main(Toto toto) : SV_POSITION
{
  return toto.Position + toto.Test + toto.Test1 + toto.Test2;
}
", "main", "vs_4_0");

            var vertexSignature = ShaderSignature.GetInputSignature(bytecode);

            var layout = VertexLayout.New(vertexSignature, new[]
                                           {
                                               new VertexElement("SV_POSITION", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST1", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST2", Format.R32G32B32A32_Float),
                                           });

            var layout2 = VertexLayout.New(vertexSignature, new[]
                                           {
                                               new VertexElement("SV_POSITION", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST1", Format.R32G32B32A32_Float),
                                               new VertexElement("TEST2", Format.R32G32B32A32_Float),
                                           });


            var r1d0 = RenderTarget1D.New(512, PixelFormat.R8G8B8A8.UNorm);
            
            
            
            var r1d1 = RenderTarget1D.New(512, true, PixelFormat.R8G8B8A8.UNorm);

            for (int i = 0; i < r1d1.Description.MipLevels; i++)
                device.Clear(r1d1.RenderTargetView[ViewType.Single, 0, i], new Color4((float)(i+1) / r1d1.Description.MipLevels));

            for (int i = 0; i < r1d1.Description.MipLevels; i++)
            {
                var testData = device.GetContent<PixelData.R8G8B8A8>(r1d1, 0, i);
                Console.WriteLine(testData[0]);
            }

            var r2d0 = RenderTarget2D.New(512, 256, PixelFormat.R8G8B8A8.UNorm);
            var r2d1 = RenderTarget2D.New(512, 256, true, PixelFormat.R8G8B8A8.UNorm);
            var r2d2 = RenderTarget2D.New(512, 256, MSAALevel.X8, PixelFormat.R8G8B8A8.UNorm);

            var r3d0 = RenderTarget3D.New(512, 256, 128, PixelFormat.R8G8B8A8.UNorm);
            var r3d1 = RenderTarget3D.New(512, 256, 128, true, PixelFormat.R8G8B8A8.UNorm);

            





            for(int i = 0; i < r3d1.Description.Depth/2; i++)
                device.Clear(r3d1.RenderTargetView[ViewType.Single, i, 1], new Color4((float)i / r3d1.Description.Depth));

            var textureData0 = device.GetContent<PixelData.R8G8B8A8>(r3d1, 0, 1);


            var rcu0 = RenderTargetCube.New(512, PixelFormat.R8G8B8A8.UNorm);
            var rcu1 = RenderTargetCube.New(512, true, PixelFormat.R8G8B8A8.UNorm);

        }
    }
}
