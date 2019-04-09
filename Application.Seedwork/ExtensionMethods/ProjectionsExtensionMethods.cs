using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Crosscutting.NetFramework.Adapter;

namespace LMS.Application.Seedwork
{
    public static class ProjectionsExtensionMethods
    {
        #region 扩展函数

        /// <summary>
        /// 将持久化类转为DTO
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TTarget ProjectedAs<TTarget>(this EntityBase item) where TTarget : class, new()
        {
            return TypeAdapterFactory.Create().Adapt<EntityBase, TTarget>(item);
        }

        /// <summary>
        /// 类与类之间转化
        /// </summary>
        /// <typeparam name="TSource">需要转化的类</typeparam>
        /// <typeparam name="TTarget">转化后的类</typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TTarget ProjectedAs<TSource,TTarget>(this TSource item) 
            where TSource : class 
            where TTarget : class, new()
        {
            return TypeAdapterFactory.Create().Adapt<TSource, TTarget>(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public static IList<TTarget> ProjectedAsList<TTarget>(this DataTable sourceTable) where TTarget : class, new()
        {
            return TypeAdapterFactory.Create().Adapt<TTarget>(sourceTable);
        }

        /// <summary>
        /// 转化了拼音简拼
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToConvertPyCode(this string text)
        {
            string pyCode = "";
            foreach (char c in text)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留
                    pyCode += c.ToString();
                }
                else
                {//累加拼音声母
                    pyCode += GetPYChar(c.ToString());
                }
            }
            return pyCode;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 取单个字符的拼音声母
        /// </summary>
        /// <param name="c">要转换的单个汉字</param>
        /// <returns>拼音声母</returns>
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "J";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }

        #endregion
    }
}
