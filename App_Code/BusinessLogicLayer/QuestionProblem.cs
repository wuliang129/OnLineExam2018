using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;

namespace OnLineExam.BusinessLogicLayer
{
    //问答题
    public class QuestionProblem : BaseProblem
	{
        #region 私有成员
        private int _ID;                                               //题目编号
        private int _CourseID;                                         //所属科目        
        private string _Title;                                         //题目       
        private string _Answer;                                       //答案
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
        public string Title
        {
            set
            {
                this._Title = value;
            }
            get
            {
                return this._Title;
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

        /// <summary>
        /// 根据题目ID 初始化题目
        /// </summary>
        /// <param name="TID">题目编号</param>
        /// <returns>题目存在：返回True；     题目不在：返回False；</returns>
        public bool LoadData(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                  //用户编号            

            DataSet ds = DB.GetDataSet("Proc_QuestionProblemDetail", Params);
            ds.CaseSensitive = false;
            DataRow DR;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DR = ds.Tables[0].Rows[0];
                this._CourseID = GetSafeData.ValidateDataRow_N(DR, "CourseID");                   //科目编号                
                this._Title = GetSafeData.ValidateDataRow_S(DR, "Title");                         //题目
                this._Answer = GetSafeData.ValidateDataRow_S(DR, "Answer");                     //答案                
                this._Explain = GetSafeData.ValidateDataRow_S(DR, "Explain");                     //解释说明

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 向表中添加题目信息(采用存储过程)
        /// </summary>
        /// <returns>插入成功：返回True； 插入失败：返回False；</returns>
        public bool InsertByProc()
        {
            SqlParameter[] Params = new SqlParameter[4];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                 //科目编号
            Params[1] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);                //题目            
            Params[2] = DB.MakeInParam("@Answer", SqlDbType.VarChar,1000, Answer);                      //答案A            
            Params[3] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //解释说明

            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemAdd", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 更新判断题的信息
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public bool UpdateByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[5];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                           //题目编号
            Params[1] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                //科目编号
            Params[2] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);               //题目            
            Params[3] = DB.MakeInParam("@Answer", SqlDbType.VarChar, 1000, Answer);                    //答案           
            Params[4] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //解释说明


            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemModify", Params);
            if (Count > 0)
                return true;
            else return false;
        }


        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="TID">题目编号</param>
        /// <returns>删除成功：返回True；  删除失败：返回False；</returns>
        public bool DeleteByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);               //题目编号          

            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemDelete", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 根据课程编号查询题目信息
        /// </summary>
        /// <param name="TCourseID">课程编号</param>
        /// <returns></returns>
        public DataSet QueryQuestionProblem(int TCourseID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, TCourseID);               //题目编号           
            return DB.GetDataSet("Proc_QuestionProblemList", Params);
        }


        /// <summary>
        /// 根据问答 title 名称查询记录是否存在
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public bool IsRecord_Exit_ByTitle(string Title)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);                //题目     

            if (DB.GetDataSet("Proc_QuestionProblemIsExitByTitle", Params).Tables[0].Rows.Count > 0)
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
