using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;

/*
 * 2017-5-3 修改表结构 增加 题目注释字段 Explain 随之所有内容都需要修改
 * 
 * 
 * */
namespace OnLineExam.BusinessLogicLayer
{
    //填空题类
    public class FillBlankProblem : BaseProblem
    {
        #region 私有成员
        private int _ID;                                               //题目编号
        private int _CourseID;                                         //所属科目        
        private string _FrontTitle;                                    //题目前部分    
        private string _BackTitle;                                     //题目后部分
        private string _Answer;                                        //答案
        private string _Explain;                                        //解释说明

        #endregion 私有成员

        #region 属性

        public int ID
        {
            set
            {
                this._ID = value;
            }
            get
            {
                return this._ID;
            }
        }
        public int CourseID
        {
            set
            {
                this._CourseID = value;
            }
            get
            {
                return this._CourseID;
            }
        }
        public string FrontTitle
        {
            set
            {
                this._FrontTitle = value;
            }
            get
            {
                return this._FrontTitle;
            }
        }
        public string BackTitle
        {
            set
            {
                this._BackTitle = value;
            }
            get
            {
                return this._BackTitle;
            }
        }
        public string Answer
        {
            set
            {
                this._Answer = value;
            }
            get
            {
                return this._Answer;
            }
        }
        public string Explain
        {
            set
            {
                this._Explain = value;
            }
            get
            {
                return this._Explain;
            }
        }

        #endregion 属性

        #region 方法

        //根据题目ID 初始化题目
        //输入：
        //      TID - 题目编号；
        //输出：
        //      题目存在：返回True；
        //      题目不在：返回False；
        public bool LoadData(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                  //用户编号            

            DataSet ds = DB.GetDataSet("Proc_FillBlankProblemDetail", Params);
            ds.CaseSensitive = false;
            DataRow DR;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DR = ds.Tables[0].Rows[0];
                this._CourseID = GetSafeData.ValidateDataRow_N(DR, "CourseID");                   //科目编号                
                this._FrontTitle = GetSafeData.ValidateDataRow_S(DR, "FrontTitle");               //题目前部分
                this._BackTitle = GetSafeData.ValidateDataRow_S(DR, "BackTitle");                 //题目后部分                
                this._Answer = GetSafeData.ValidateDataRow_S(DR, "Answer");                       //答案
                this._Explain = GetSafeData.ValidateDataRow_S(DR, "Explain");                     //解释说明
                return true;
            }
            else
            {
                return false;
            }
        }


        //向FillBlankProblem表中添加题目信息(采用存储过程)
        //输出：
        //      插入成功：返回True；
        //      插入失败：返回False；
        public bool InsertByProc()
        {
            SqlParameter[] Params = new SqlParameter[5];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                //科目编号
            Params[1] = DB.MakeInParam("@FrontTitle", SqlDbType.VarChar, 500, FrontTitle);      //题目前部分      
            Params[2] = DB.MakeInParam("@BackTitle", SqlDbType.VarChar, 500, BackTitle);        //题名后部分            
            Params[3] = DB.MakeInParam("@Answer", SqlDbType.VarChar, 200, Answer);              //答案
            Params[4] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //解释说明
            int Count = -1;
            Count = DB.RunProc("Proc_FillBlankProblemAdd", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 更新填空题的信息
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public bool UpdateByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[6];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                           //题目编号
            Params[1] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                //科目编号
            Params[2] = DB.MakeInParam("@FrontTitle", SqlDbType.VarChar, 500, FrontTitle);      //题目前部分      
            Params[3] = DB.MakeInParam("@BackTitle", SqlDbType.VarChar, 500, BackTitle);        //题名后部分            
            Params[4] = DB.MakeInParam("@Answer", SqlDbType.VarChar, 200, Answer);              //答案
            Params[5] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //解释说明

            int Count = -1;
            Count = DB.RunProc("Proc_FillBlankProblemModify", Params);
            if (Count > 0)
                return true;
            else return false;
        }




        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="TID">题目编号</param>
        /// <returns>删除成功：返回True； 删除失败：返回False；</returns>
        public bool DeleteByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);               //题目编号          

            int Count = -1;
            Count = DB.RunProc("Proc_FillBlankProblemDelete", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 查询单选题   
        /// </summary>
        /// <param name="TCourseID">课程编号</param>
        /// <returns></returns>
        public DataSet QueryFillBlankProblem(int TCourseID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, TCourseID);               //题目编号           
            return DB.GetDataSet("Proc_FillBlankProblemList", Params);
        }

        


        /// <summary>
        /// 根据多选题 title 名称查询记录是否存在
        /// </summary>
        /// <param name="FrontTitle">FrontTitle</param>
        /// <param name="BackTitle">BackTitle</param>
        /// <returns></returns>
        public bool IsRecord_Exit_ByTitle(string FrontTitle, string BackTitle)
        {
            SqlParameter[] Params = new SqlParameter[2];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@FrontTitle", SqlDbType.VarChar, 1000, FrontTitle);                //题目     
            Params[1] = DB.MakeInParam("@BackTitle", SqlDbType.VarChar, 1000, BackTitle);                //题目 
            if (DB.GetDataSet("Proc_FillBlankProblemIsExitByTitle", Params).Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      
        #endregion 方法
    }
}