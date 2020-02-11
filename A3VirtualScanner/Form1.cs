using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A3VirtualScanner
{

    public partial class Form1 : Form
	{
		bool On;
		Point Pos;

		public Form1()
		{
			InitializeComponent();

			MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
			MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
			MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
		}
        /*
		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] 파일경로들 = (string[])e.Data.GetData(DataFormats.FileDrop);

                string 파일경로0 = "", 파일경로1 = "";

                if(String.Compare(파일경로들[0], 파일경로들[1]) < 0)
                {
                    파일경로0 = 파일경로들[0];
                    파일경로1 = 파일경로들[1];
                }
                else
                {
                    파일경로0 = 파일경로들[1];
                    파일경로1 = 파일경로들[0];
                }

                이미지90도꺾기(파일경로0);

				if (파일경로들.Length > 1)
				{
					//MessageBox.Show(파일경로들[0].Replace(".jpg", "_stitch.jpg"));

					이미지270도꺾어서저장하기(파일경로1);

                    int 가로 = 1508;
                    int 세로 = 2110;

					Bitmap 비트맵 = new Bitmap(가로, 세로);
					비트맵.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
					Graphics 그래픽 = Graphics.FromImage(비트맵);


                    Bitmap 네배넓이의투명한비트맵 = new Bitmap(가로, 세로);
                    네배넓이의투명한비트맵.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                    Graphics 네배넓이의투명한그래픽 = Graphics.FromImage(네배넓이의투명한비트맵);
                    네배넓이의투명한그래픽.Clear(Color.Transparent);
                    네배넓이의투명한그래픽.DrawImage(_원본이미지90도꺾은이미지, -242, 0);
                    //MessageBox.Show(파일경로들[0].Replace(".jpg", "_stitch1.png"));
                    네배넓이의투명한비트맵.Save(파일경로들[0].Replace(".jpg", "_stitch1.png"), System.Drawing.Imaging.ImageFormat.Png);

                    //1750-1508
                    //1240, 1508


                    Bitmap 네배넓이의투명한비트맵2 = new Bitmap(가로, 세로);
                    네배넓이의투명한비트맵2.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                    Graphics 네배넓이의투명한그래픽2 = Graphics.FromImage(네배넓이의투명한비트맵2);
                    네배넓이의투명한그래픽2.Clear(Color.Transparent);
                    네배넓이의투명한그래픽2.DrawImage(_원본이미지270도꺾은이미지, 0, 870);
                    네배넓이의투명한비트맵2.Save(파일경로들[0].Replace(".jpg", "_stitch2.png"), System.Drawing.Imaging.ImageFormat.Png);


//                    Bitmap 네배넓이의투명한비트맵2 = new Bitmap(가로, 세로);
//                    네배넓이의투명한비트맵2.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
//                    Graphics 네배넓이의투명한그래픽2 = Graphics.FromImage(네배넓이의투명한비트맵2);

                    int 최저치i = 0;
                    int 최저치j = 0;

                    long 상관관계최저치 = 10000000000;

                    long 현재상관관계값 = 0;


                    for(int i = -20; i < 20; i++)
                    {
                        for(int j = -35; j < 35; j++)
                        {
                            // 여기서 +-로 돌려서, 더 긁어본다.

                            네배넓이의투명한그래픽2.Clear(Color.Transparent);
                            네배넓이의투명한그래픽2.DrawImage(_원본이미지270도꺾은이미지, 0 + i, 870 + j);

                            //현재상관관계값 = 상관관계측정(ref 네배넓이의투명한비트맵, ref 네배넓이의투명한비트맵2);
                            현재상관관계값 = 빠른상관관계측정(ref 네배넓이의투명한비트맵, ref 네배넓이의투명한비트맵2);
                            if (상관관계최저치 > 현재상관관계값 && 현재상관관계값 != 0)
                            {
                                상관관계최저치 = 현재상관관계값;
                                최저치i = i;
                                최저치j = j;
                            }
                        }
                    }

                    //네배넓이의투명한그래픽2.Clear(Color.Transparent);
                    //네배넓이의투명한그래픽2.DrawImage(_원본이미지270도꺾은이미지, 최저치i, 870 + 최저치j);

                    // 네배넓이의투명한비트맵2.Save(파일경로들[0].Replace(".jpg", "_stitch2.png"), System.Drawing.Imaging.ImageFormat.Png);

                    그래픽.DrawImage(_원본이미지90도꺾은이미지, -242, 0);

                    그래픽.DrawImage(_원본이미지270도꺾은이미지, 최저치i, 870 + 최저치j);

                    MessageBox.Show(최저치i.ToString() + "_" + 최저치j.ToString());

                    MessageBox.Show(파일경로0.Replace(".jpg", "_stitch.png"));
                    비트맵.Save( 파일경로0.Replace(".jpg", "_stitch.png"), System.Drawing.Imaging.ImageFormat.Png);

                }
            }
		}
        */

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] 파일경로들 = (string[])e.Data.GetData(DataFormats.FileDrop);

                string 파일경로0 = "", 파일경로1 = "";

                if (String.Compare(파일경로들[0], 파일경로들[1]) < 0)
                {
                    파일경로0 = 파일경로들[0];
                    파일경로1 = 파일경로들[1];
                }
                else
                {
                    파일경로0 = 파일경로들[1];
                    파일경로1 = 파일경로들[0];
                }

                이미지90도꺾기(파일경로0);

                if (파일경로들.Length > 1)
                {



                    //MessageBox.Show(파일경로들[0].Replace(".jpg", "_stitch.jpg"));

                    이미지270도꺾어서저장하기(파일경로1);

                    int 가로 = 1508;
                    int 세로 = 2110;

                    Bitmap 비트맵 = new Bitmap(가로, 세로);
                    비트맵.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                    Graphics 그래픽 = Graphics.FromImage(비트맵);

                    
                    Bitmap 비트맵상단 = new Bitmap(가로, 세로);
                    비트맵상단.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                    Graphics 그래픽상단 = Graphics.FromImage(비트맵상단);
                    그래픽상단.Clear(Color.Transparent);
                    그래픽상단.DrawImage(_원본이미지90도꺾은이미지, -242, 0);
                    //MessageBox.Show(파일경로들[0].Replace(".jpg", "_stitch1.png"));
                    //비트맵상단.Save(파일경로들[0].Replace(".jpg", "_stitch1.png"), System.Drawing.Imaging.ImageFormat.Png);

                    //1750-1508
                    //1240, 1508

                    long 회전상관관계최저치 = 10000000000;
                    long 현재회전상관관계;
                    float 최저회전치= 0.0f;

                    int 현재회전값에서의x축이동최저치 = 0;
                    int 현재회전값에서의y축이동최저치 = 0;

                    for (float f = -1.0f; f < 1.0f; f += 0.1f)
                    {
                        Bitmap 비트맵하단 = new Bitmap(가로, 세로);
                        비트맵하단.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                        Graphics 그래픽하단 = Graphics.FromImage(비트맵하단);
                        그래픽하단.Clear(Color.Transparent);
                        그래픽하단.DrawImage(_원본이미지270도꺾은이미지, 0, 870);

                        비트맵하단 = (Bitmap)RotateImage((Image)비트맵하단, f);
                        //비트맵하단.Save(파일경로들[0].Replace(".jpg", "_stitch2.png"), System.Drawing.Imaging.ImageFormat.Png);

                        int 최저치i = 0;
                        int 최저치j = 0;

                        빠른상관관계측정초기화(ref 비트맵상단, ref 비트맵하단);
                        현재회전상관관계 = 빠른상관관계측정회전없음(ref 최저치i, ref 최저치j);
                        빠른상관관계측정마무으리(ref 비트맵상단, ref 비트맵하단);

                        if(회전상관관계최저치 > 현재회전상관관계)
                        {
                            회전상관관계최저치 = 현재회전상관관계;
                            최저회전치 = f;
                            
                            현재회전값에서의x축이동최저치 = 최저치i;
                            현재회전값에서의y축이동최저치 = 최저치j;
                        }
                    }


                    Bitmap 비트맵하단최종 = new Bitmap(가로, 세로);
                    비트맵하단최종.SetResolution(_원본이미지90도꺾은이미지.HorizontalResolution, _원본이미지90도꺾은이미지.VerticalResolution);
                    Graphics 그래픽하단최종 = Graphics.FromImage(비트맵하단최종);
                    그래픽하단최종.Clear(Color.Transparent);
                    그래픽하단최종.DrawImage(_원본이미지270도꺾은이미지, 0, 870);

                    비트맵하단최종 = (Bitmap)RotateImage((Image)비트맵하단최종, 최저회전치);
                    //비트맵하단최종.Save(파일경로들[0].Replace(".jpg", "_stitch2.png"), System.Drawing.Imaging.ImageFormat.Png);

                    그래픽.Clear(Color.White);
                    그래픽.DrawImage(_원본이미지90도꺾은이미지, -242, 0);


                    그래픽.DrawImage(비트맵하단최종, -현재회전값에서의x축이동최저치, -현재회전값에서의y축이동최저치);

                    MessageBox.Show(현재회전값에서의x축이동최저치.ToString() + "_" + 현재회전값에서의y축이동최저치.ToString() + "_" + 최저회전치.ToString());

                    MessageBox.Show(파일경로0.Replace(".jpg", "_stitch.png"));
                    비트맵.Save(파일경로0.Replace(".jpg", "_stitch.png"), System.Drawing.Imaging.ImageFormat.Png);

                }
            }
        }

        private int _bytes;
        private int _width1;
        private int _height1;
        private int _width2;
        private int _height2;

        private byte[] _b1bytes;
        private byte[] _b2bytes;
        private BitmapData _bitmapData1;
        private BitmapData _bitmapData2;

        private void 빠른상관관계측정초기화(ref Bitmap bmp1, ref Bitmap bmp2)
        {
            _bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);
            _width1 = bmp1.Width;
            _height1 = bmp1.Height;

            _width2 = bmp2.Width;
            _height2 = bmp2.Height;

            _b1bytes = new byte[_bytes];
            _b2bytes = new byte[_bytes];

            _bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            _bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(_bitmapData1.Scan0, _b1bytes, 0, _bytes);
            Marshal.Copy(_bitmapData2.Scan0, _b2bytes, 0, _bytes);
        }


        private void 빠른상관관계측정(ref int 최저치i, ref int 최저치j, ref float 최저치t)
        {
            long 상관관계최저치 = 10000000000;

            long 현재상관관계값;
            long 전체추출갯수 = 0;

            // 회전변환
            // x = cosΘx - sinΘy
            // y = sinΘx + cosΘy

            for(float t= -1.0f; t < 1.0f; t+=0.1f) // t for theta
            //for (float t = 0; t < 0.2f; t += 0.2f) // t for theta
            { 
                for (int x = -25; x < 25; x++)
                {
                    for (int y = -35; y < 35; y++)
                    {
                        전체추출갯수 = 0;
                        현재상관관계값 = 0;

                        for (int i = 50; i < _width1 - 50; i += 16)
                        {
                            for (int j = 1000; j < 1100; j += 16)
                            {
                                //if (알파값추출1(i, j) == 0 || 알파값추출2(i + x, j + y) == 0)
                                //if (알파값추출1(i, j) == 0 || 알파값추출2((int)(Math.Cos(t)*(i + x) - Math.Sin(t) * (j + y)) , (int)(Math.Sin(t)*(i+x) + Math.Cos(t) *(j + y))) == 0)
                                if (알파값추출1(i, j) == 0 || 알파값추출2((int)(Math.Cos(t) * i - Math.Sin(t) * j) + x, (int)(Math.Sin(t) * i + Math.Cos(t) * j) + y) == 0)
                                {
                                    ;
                                }
                                else
                                {
                                    현재상관관계값 += Math.Abs(회색값추출1(i,j) - 회색값추출2((int)(Math.Cos(t) * i - Math.Sin(t) * j) + x, (int)(Math.Sin(t) * i + Math.Cos(t) * j) + y));
                                    전체추출갯수++;
                                }
                            }
                        }

                        if (전체추출갯수 != 0)
                            현재상관관계값 /= 전체추출갯수; // 추출된 것의 숫자만 비교하는 전략이다.
                        else
                            현재상관관계값 = 0;

                        if (상관관계최저치 > 현재상관관계값 && 현재상관관계값 != 0)
                        {
                            상관관계최저치 = 현재상관관계값;
                            최저치i = x;
                            최저치j = y;
                            최저치t = t;
                        }
                    }
                }
            }
        }


        private long 빠른상관관계측정회전없음(ref int 최저치i, ref int 최저치j)
        {
            long 상관관계최저치 = 10000000000;

            long 현재상관관계값;
            long 전체추출갯수 = 0;

            // 회전변환
            // x = cosΘx - sinΘy
            // y = sinΘx + cosΘy

            for (int x = -35; x < 35; x++)
            {
                for (int y = -35; y < 35; y++)
                {
                    전체추출갯수 = 0;
                    현재상관관계값 = 0;

                    for (int i = 50; i < _width1 - 50; i += 2) // 16이 적절하고, 8은 조금 느리다.
                    {
                        for (int j = 1000; j < 1100; j += 16) // 16이 적절하고, 8은 조금 느리다.
                        {
                            if (알파값추출1(i, j) == 0 || 알파값추출2(i + x, j + y) == 0)
                            {
                                ;
                            }
                            else
                            {
                                현재상관관계값 += Math.Abs(회색값추출1(i, j) - 회색값추출2(i + x, j + y));
                                전체추출갯수++;
                            }
                        }
                    }

                    if (전체추출갯수 != 0)
                        현재상관관계값 /= 전체추출갯수; // 추출된 것의 숫자만 비교하는 전략이다.
                    else
                        현재상관관계값 = 0;

                    if (상관관계최저치 > 현재상관관계값 && 현재상관관계값 != 0)
                    {
                        상관관계최저치 = 현재상관관계값;
                        최저치i = x;
                        최저치j = y;
                    }
                }
            }

            return 상관관계최저치;
        }

        private int 회색값추출1(int x, int y)
        {
            if (x < 0) return 0;
            if (y < 0) return 0;
            if (x >= _width1) return 0;
            if (y >= _height1) return 0;

            return (_b1bytes[y * _width1 * 4 + x * 4] + _b1bytes[y * _width1 * 4 + x * 4 + 1] + _b1bytes[y * _width1 * 4 + x * 4 + 2]) / 3;
        }

        private int 회색값추출2(int x, int y)
        {
            if (x < 0) return 0;
            if (y < 0) return 0;

            if (x >= _width2) return 0;
            if (y >= _height2) return 0;

            return (_b2bytes[y * _width2 * 4 + x * 4] + _b2bytes[y * _width2 * 4 + x * 4 + 1] + _b2bytes[y * _width2 * 4 + x * 4 + 2]) / 3;
        }

        private int 알파값추출1(int x, int y)
        {
            if (x < 0) return 0;
            if (y < 0) return 0;

            if (x >= _width1) return 0;
            if (y >= _height1) return 0;

            return _b1bytes[y * _width1 * 4 + x * 4 + 3];
        }

        private int 알파값추출2(int x, int y)
        {
            if (x < 0) return 0;
            if (y < 0) return 0;

            if (x >= _width2) return 0;
            if (y >= _height2) return 0;

            return _b2bytes[y * _width2 * 4 + x * 4 + 3];
        }

        private void 빠른상관관계측정마무으리(ref Bitmap bmp1, ref Bitmap bmp2)
        {
            bmp1.UnlockBits(_bitmapData1);
            bmp2.UnlockBits(_bitmapData2);
        }

        private long 상관관계측정(ref Bitmap 비트맵1, ref Bitmap 비트맵2)
        {
            Color color1 = new Color();
            Color color2 = new Color();

            long 상관관계편차 = 0;

            // GetPixel을 한번 안써보자!!!


            for (int i = 0; i < 비트맵1.Width ; i+=16)
            {
                for(int j = 1000; j < 1100; j+=16)
                {
                    color1 = 비트맵1.GetPixel(i, j);
                    color2 = 비트맵2.GetPixel(i, j);

                    if (color1.A == 0 || color2.A == 0)
                    {
                        ;
                    }
                    else
                    {
                        int grey1 = (color1.R + color1.G + color1.B) / 3;
                        int grey2 = (color2.R + color2.G + color2.B) / 3;

                        상관관계편차 += Math.Abs(grey1 - grey2);
                    }
                }
            }


            return 상관관계편차;
        }

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
			{
				String[] strGetFormats = e.Data.GetFormats();
				e.Effect = DragDropEffects.None;
			}
		}

		Image _원본이미지90도꺾은이미지;

		private void 이미지90도꺾기(String 원본이미지)
		{
			_원본이미지90도꺾은이미지 = Image.FromFile(원본이미지);
			_원본이미지90도꺾은이미지.RotateFlip(RotateFlipType.Rotate90FlipNone);
		}

		Image _원본이미지270도꺾은이미지;

		private void 이미지270도꺾어서저장하기(String 원본이미지)
		{
			_원본이미지270도꺾은이미지 = Image.FromFile(원본이미지);
			_원본이미지270도꺾은이미지.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);


            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            //gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            //gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //now draw the new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of the Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        public string ShowFileOpenDialog()
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 오픈 예제창";
            ofd.FileName = "test";
            ofd.Filter = "그림 파일 (*.jpg, *.gif, *.bmp, *png) | *.jpg; *.gif; *.bmp; *.png; | 모든 파일 (*.*) | *.*";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string fileName = ofd.SafeFileName;
                //File경로와 File명을 모두 가지고 온다.
                string fileFullName = ofd.FileName;
                //File경로만 가지고 온다.
                string filePath = fileFullName.Replace(fileName, "");

                return fileFullName;
            }
            //취소버튼 클릭시 또는 ESC키로 파일창을 종료 했을경우
            else if (dr == DialogResult.Cancel)
            {
                return "";
            }

            return "";
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            
        }

    }

}
