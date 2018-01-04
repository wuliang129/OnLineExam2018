using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace OnLineExam.BusinessLogicLayer
{
    /// <summary>
    ///ImportEXCEL 的摘要说明
    ///
    /// 处理 excel 导入导出的业务类
    /// </summary>
    public class ImportEXCEL
    {
        public ImportEXCEL()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 把EXCEL文件上传到服务器并返回文件路径
        /// </summary>
        /// <param name="fileloads"></param>
        /// <returns></returns>
        public String UploadFile(FileUpload fileloads)
        {
            string newFile = null;//上传的文件物理路径

                //没有选择文件提示用户
                if (!fileloads.HasFile)
                {
                    //Response.Write("<script language='javascript'>alert('请选择上传的单选题EXCEL文件！');</script>");
                    throw new Exception("请选择上传的单选题EXCEL文件！");
                }
                /*
                         * 代码示例中，先是获取上传的文件名，此包含有路径； 
                            接下来还在知道上传的扩展名； 
                            第三行代码是使用Guid类的方法NewGuid()与扩展名组合一个新的文件名。
                            第四行代码创建上传文件的目标路径。
                            最后是保存。
                         */
                string uploadfile = fileloads.PostedFile.FileName;
                string fileExtension = uploadfile.Substring(uploadfile.LastIndexOf("."));
                #region 判断文件扩展名
                if ((fileExtension != ".xls" && fileExtension != ".xlsx"))
                {
                    //Response.Write("<script language='javascript'>alert('导入文件格式不对,请导入 正确excel格式文件!');</script>");
                    throw new Exception("导入文件格式不对,请导入 正确excel格式文件!");
                }
                #endregion

                string newFileName = Guid.NewGuid().ToString() + fileExtension;

                //ImportEXCEL importExcel = new ImportEXCEL();//
                newFile = HttpContext.Current.Server.MapPath("~/" + TemporaryDirectory) + newFileName;
                fileloads.SaveAs(newFile);//保存到本地服务器
                return newFile;
        }

        /// <summary>
        /// 把数据导入 DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataTable ImportToDataSet(string path)
        {
            //string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "Data Source=" + path + ";" + "Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";

            string strConn = GetExcelConnectionString(path);

            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                DataTable dt = new DataTable();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string strExcel = "select * from [Sheet1$]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, conn);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        /// <summary>
        /// 把数据 从 grideview 导入到  数据库中
        /// 给出提示信息 例如导入成功几条记录 有几条记录是重复的 重复的记录显示出来
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ImportToSQL(GridView gv_data,string btn_ID)
        {

            #region  原来代码
            //DBMan.DBHelper db = new DBHelper();
            ////string strSQL = "insert into GrowthPlan (Year,HangHao,TestType,SeriesName,Repeat,SeedSource,HangLength,HangSpace) values (@Year,@HangHao,@TestType,@SeriesName,@Repeat,@SeedSource,@HangLength,@HangSpace) ";
            //bool HasERROR = false;
            //int rusult = 0;
            //for (int j = 0; j < (dgv.Rows.Count - 1); j++)
            //{
            //    //if ("" == dgv.Rows[j].Cells[1].Value.ToString())//年份额为空 证明没有数据 
            //    //{
            //    //    continue;
            //    //}
            //    rusult = 0;
            //    //写内容 第一列 ID不用写入
            //    for (int k = 0; k < dgv.Columns.Count; k++)
            //    {
            //        cmdParms[k].Value = dgv.Rows[j].Cells[k].Value.ToString();
            //    }

            //    try
            //    {
            //        rusult = db.ExecuteSql(strSQL, cmdParms);
            //        if (rusult > 0)
            //        {
            //            //MessageBox.Show("插入数据成功！");
            //        }
            //        else
            //        {
            //            //MessageBox.Show("插入数据失败！请查看您的输入是否正确！");
            //            dgv.Rows[j].Cells[0].Value = "插入失败！";
            //            HasERROR = true;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("插入数据失败！" + ex.Message);
            //        HasERROR = true;
            //    }

            //}
            //if (HasERROR)
            //{
            //    MessageBox.Show("有部分数据导入失败！请修改后再次导入！");
            //    return false;
            //}
            //else
            //{
            //    //MessageBox.Show("导入成功！");
            //    return true;
            //}
            //return null;
            #endregion
            return null;
        }


        /// <summary>
        /// 不同版本的Excel文件使用哪一种连接字串
        /// </summary>
        /// <param name="filepath">已经是物理路径</param>
        /// <returns></returns>
        public static string GetExcelConnectionString(string filepath)
        {
            string connectionString = string.Empty;
            string fileExtension = filepath.Substring(filepath.LastIndexOf(".") + 1);
            switch (fileExtension)
            {
                case "xls":
                    //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(filepath) + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                    break;
                case "xlsx":
                    //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Server.MapPath(filepath) + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
                    break;
            }
            return connectionString;
        }

        /// <summary>
        /// 在根目录下创建 Upfiles 临时文件目录 子目录按照时间来生成目录
        /// </summary>
        public string TemporaryDirectory
        {
            get
            {
                string temporaryDirectory = "~/Upfiles/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";//创建路径用
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(temporaryDirectory)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(temporaryDirectory));
                }
                temporaryDirectory = "Upfiles/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";//路径映射用
                return temporaryDirectory;
            }
        }


        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dr"></param>
        //public void DataRowAdd(DataTable dt,DataRowCollection dr)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //            dtNew.Rows.Add(dt.Rows[i].ItemArray);  //添加数据行
        //    }
        //}
    }


}