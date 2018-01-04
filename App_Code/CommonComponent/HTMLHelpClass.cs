using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace OnLineExam.CommonComponent
{

    /// <summary>
    ///HTMLHelpClass 的摘要说明
    ///生成HTML临时文件的辅助类
    /// </summary>
    public class HTMLHelpClass
    {
        /// <summary>
        /// 服务器根目录  所在目录
        /// </summary>
        protected string strHtmlFilePath = "";
        /// <summary>
        /// 服务器根目录  HTML 文件所在物理目录
        /// </summary>
        protected string strHtmlPhysicalPath = "";

        /// <summary>
        /// 服务器根目录  教师上传文件所在目录
        /// </summary>
        public string strTeacherUpfilesPath = "";

        public HTMLHelpClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            strHtmlFilePath = "~/UpLoadFile/HtmlTemp/";
            strHtmlPhysicalPath = HttpContext.Current.Server.MapPath(strHtmlFilePath);
            strTeacherUpfilesPath = "~/UpLoadFile/TeacherUpfiles/";
        }

        /// <summary>
        /// 根据用户提交数据生成静态网页（时间）
        /// 生成成功返回URL地址(地址+文件名)
        /// 否则返回null
        /// </summary>
        /// <param name="list">用户提交数据</param>
        /// <returns></returns>
        public string CreateHtml(string usercode)
        {

            string dataHtml = usercode;//存放数据 html
            #region 给网页添加语法检测代码
            string strHead = @"<script type=""text/javascript"" src=""http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js""></script><script src=""../JS/tecate.js"" type=""text/javascript""></script>";
            int iInserIndex = dataHtml.IndexOf("</head>");
            dataHtml = dataHtml.Insert(iInserIndex, strHead);
            #endregion
            //获取模板物理路径 ~/HTMTemp
            //string path = HttpContext.Current.Server.MapPath(@"~/HTMTemp/");
            string htmlfilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".html";  //年月日时分秒毫秒

            StreamWriter writer = null;
            try
            {

                writer = new StreamWriter(strHtmlPhysicalPath + htmlfilename, false, System.Text.Encoding.Unicode);
                writer.Write(dataHtml);//写入
                writer.Flush();
            }
            catch (Exception ex)
            {
                //计入日志
                return null;
            }
            finally
            {
                writer.Close();
            }
            return strHtmlFilePath + htmlfilename;//返回相对根目录路径

        }

        /// <summary>
        /// 根据用户提交数据生成静态网页（时间）
        /// 生成成功返回URL地址(地址+文件名)
        /// 否则返回null
        /// </summary>
        /// <param name="usercode">用户提交数据</param>
        /// <param name="userid">传递过来的用户id 文件名称：用户id+日期</param>
        /// <returns></returns>
        public string CreateHtml(string usercode , string userid)
        {

            string dataHtml = usercode;//存放数据 html
            //#region 给网页添加语法检测代码
            //string strHead = @"<script type=""text/javascript"" src=""http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js""></script><script src=""../JS/tecate.js"" type=""text/javascript""></script>";
            //int iInserIndex = dataHtml.IndexOf("</head>");
            //dataHtml = dataHtml.Insert(iInserIndex, strHead);
            //#endregion
            //获取模板物理路径 ~/HTMTemp
            //string path = HttpContext.Current.Server.MapPath(@"~/HTMTemp/");
            string htmlfilename = userid + "-" + DateTime.Now.ToString("yyyyMMdd") + ".html";  //年月日  时分秒毫秒

            StreamWriter writer = null;
            try
            {

                writer = new StreamWriter(strHtmlPhysicalPath + htmlfilename, false, System.Text.Encoding.Unicode);
                writer.Write(dataHtml);//写入
                writer.Flush();
            }
            catch (Exception ex)
            {
                //计入日志
                return null;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
            return strHtmlFilePath + htmlfilename;//返回相对根目录路径

        }
        /// <summary>
        /// 定时清空 HTMTemp 目录临时文件 
        /// </summary>
        public void ClearHTMTempDir()
        {
            //string srcPath = HttpContext.Current.Server.MapPath(@"~/HTMTemp");
            if (!Directory.Exists(strHtmlPhysicalPath))
            {
                return;//不存在此目录
            }
            try
            {
                DirectoryInfo dir = new DirectoryInfo(strHtmlPhysicalPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 定时清空 教师excel题目文件 目录临时文件 仅删除子目录  其他文件有用 不删
        /// 默认暂不删除
        /// </summary>
        public void ClearUpfilesDir()
        {
            string srcPath = HttpContext.Current.Server.MapPath(strTeacherUpfilesPath);
            if (!Directory.Exists(srcPath))
            {
                return;//不存在此目录
            }
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹  仅删除文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    //else
                    //{
                    //    File.Delete(i.FullName);      //删除指定文件
                    //}
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据用户id判断 文件是否存在 
        /// </summary>
        /// <param name="userid">传递过来的用户id    文件名称：用户id+日期</param>
        /// <returns></returns>
        public bool IsFileExit(string userid)
        {
            if (!Directory.Exists(strHtmlPhysicalPath))
            {
                return false;//不存在此目录
            }
            else
            {
                string htmlfilename = userid + "-" + DateTime.Now.ToString("yyyyMMdd") + ".html";  //年月日
                string fileFullPath = strHtmlPhysicalPath + htmlfilename;
                if (File.Exists(fileFullPath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public string GetFilePath(string userid)
        {
            if(IsFileExit(userid))
            {
                string htmlfilename = userid + "-" + DateTime.Now.ToString("yyyyMMdd") + ".html";  //年月日
                string strRootPath = strHtmlFilePath + htmlfilename;
                strRootPath = strRootPath.Substring(1, strRootPath.Length - 1);//去掉 "~"
                return strRootPath;
            }
            else
            {
                return null;
            }
        }
    }

}