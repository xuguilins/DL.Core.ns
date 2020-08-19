using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using DL.Core.utility.Logging;

namespace DL.Core.utility.Tools
{
    /// <summary>
    /// 验证码帮助类
    /// </summary>
    public static class ValidateCodeHelper
    {
        /// <summary>
        /// 获取纯数字的验证码
        /// </summary>
        /// <param name="num">验证码长度</param>
        /// <param name="lineMore">是否需要很多干扰线，默认4条</param>
        /// <returns></returns>
        public static byte[] CreateValidteCode(int num, bool lineMore = false)
        {
            byte[] arry = null;
            int width = 100;
            int height = 50;
            Bitmap bit = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bit);
            g.Clear(Color.White);
            var lineCount = lineMore ? 60 : 4;
            Random random = new Random();
            for (int i = 0; i < lineCount; i++)
            {
                int x1 = random.Next(width);
                int x2 = random.Next(width);
                int y1 = random.Next(height);
                int y2 = random.Next(height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //产生数字
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                random = new Random(Guid.NewGuid().GetHashCode());
                sb.Append(random.Next(1, 10) + " ");
            }
            g.DrawString(sb.ToString(), new Font(FontFamily.GenericSansSerif, 13, FontStyle.Italic), Brushes.Black, new Point(10, 15));
            //绘制干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                bit.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            MemoryStream ms = new MemoryStream();
            bit.Save(ms, ImageFormat.Jpeg);
            arry = ms.ToArray();
            ms.Dispose();
            ILogger logger = Logging.LogManager.GetLogger();
            logger.Debug($"长度:{arry.Length}");
            return arry;
        }
    }
}