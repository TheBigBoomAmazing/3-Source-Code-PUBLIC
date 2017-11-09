using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace eContract.BusinessService.Helper
{
    public class ValidateCodeHelper
    {

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="CodeCount">长度</param>
        /// <param name="isnumber">是否只随机数字</param>
        /// <returns></returns>
        public static string GetRandomCode(int CodeCount,bool isnumber=false)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,M,N,P,Q,R,S,T,U,W,X,Y,Z";
            int randomRange = 33;
            if (isnumber)
            {
                allChar = "0,1,2,3,4,5,6,7,8,9";
                randomRange = 10;
            }
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(randomRange);

                while (temp == t)
                {
                    t = rand.Next(randomRange);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }

            return RandomCode;
        }

        

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="checkCode">验证码编码</param>
        /// <param name="CodeCount">验证码数量</param>
        /// <param name="isBlackPen">是否生成水平线</param>
        /// <returns></returns>
        public static byte[] CreateValidateCodeImage(ref string checkCode, int CodeCount = 4, bool isBlackPen=false)
        {
            checkCode = GetRandomCode(CodeCount);
           
            int iwidth = (int)(checkCode.Length * 14);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial ", 10);//, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.Black);
            Brush r = new System.Drawing.SolidBrush(Color.FromArgb(166, 8, 8));

            //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height);
            //			g.Clear(Color.AliceBlue);//背景色
            g.Clear(System.Drawing.ColorTranslator.FromHtml("#99C1CB"));//背景色

            char[] ch = checkCode.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] >= '0' && ch[i] <= '9')
                {
                    //数字用红色显示
                    g.DrawString(ch[i].ToString(), f, r, 3 + (i * 12), 3);
                }
                else
                {   //字母用黑色显示
                    g.DrawString(ch[i].ToString(), f, b, 3 + (i * 12), 3);
                }
            }

            //for循环用来生成一些随机的水平线
            if (isBlackPen)
            {
                Pen blackPen = new Pen(Color.Black, 0);
                Random rand = new Random();
                for (int i = 0; i < 2; i++)
                {
                    int y = rand.Next(image.Height);
                    g.DrawLine(blackPen, 0, y, image.Width, y);
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imgByte = ms.ToArray(); 
            ms.Flush();
            g.Dispose();
            image.Dispose();
            return imgByte;
        }
    }
}
