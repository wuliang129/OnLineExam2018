using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;


namespace OnLineExam.BusinessLogicLayer
{
    /// <summary>
    ///BaseProblem 所有题目的基础类
    /// </summary>
    public class BaseProblem
    {
        public BaseProblem()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 根据题目类型和课程编号查询所有题目
        /// </summary>
        /// <param name="Type">题目类型：单选题，多选题，判断题，问答题，填空题（默认）</param>
        /// <param name="CourseID">课程ID</param>
        /// <returns></returns>
        public DataSet GetAllProblemsByTypeandCourseID(string Type, int CourseID)
        {
            SqlParameter[] Params = new SqlParameter[2];
            DataBase DB = new DataBase();
            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);               //科目编号
            Params[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, Type);            //题目类型        
            DataSet ds = DB.GetDataSet("Proc_ProblemsDetail", Params);
            return ds;
        }
    }

}