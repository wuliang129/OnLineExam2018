using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;


namespace OnLineExam.BusinessLogicLayer
{
   
    /// <summary>
    ///  学生成绩操作类 2017-12-29 0:16 
    ///  wla
    ///  修改完毕 全部用sql 语句实现
    /// </summary>
    public class Scores
    {
        #region 私有成员
        private int _ID;
        private string _StudentId;                                       
        private int _paperID;                                       
        private int _score;
        private DateTime _examtime;//考试时间
        private DateTime _judgetime;  //评阅时间   
        private string _pingyu;  //评阅时间  
        private int _IsUserView=0;//用户是否可见试卷
       
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
        public string StudentId
        {
            set
            {
                this._StudentId = value;
            }
            get
            {
                return this._StudentId;
            }
        }
        public int PaperID
        {
            set
            {
                this._paperID = value;
            }
            get
            {
                return this._paperID;
            }
        }
        public int Score
        {
            set
            {
                this._score = value;
            }
            get
            {
                return this._score;
            }
        }
        public DateTime ExamTime
        {
            set
            {
                this._examtime = value;
            }
            get
            {
                return this._examtime;
            }
        }
        public DateTime JudgeTime
        {
            set
            {
                this._judgetime = value;
            }
            get
            {
                return this._judgetime;
            }
        }
        public string PingYu
        {
            set
            {
                this._pingyu = value;
            }
            get
            {
                return this._pingyu;
            }
        }

        public int IsUserView
        {
            set
            {
                this._IsUserView = value;
            }
            get
            {
                return this._IsUserView;
            }
        }

        #endregion 属性

        #region 方法

        /// <summary>
        /// 向Score表中添加成绩
        /// 输出： 插入成功：返回True； 
        /// 插入失败：返回False；
        /// </summary>
        /// <returns></returns>
        public bool InsertScore()
        {
            SqlParameter[] Params = new SqlParameter[5];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, StudentId);
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, PaperID);
            Params[2] = db.MakeInParam("@Score", SqlDbType.Int, 4, Score);
            Params[3] = db.MakeInParam("@ExamTime", SqlDbType.DateTime, 8, ExamTime);
            Params[4] = db.MakeInParam("@JudgeTime", SqlDbType.DateTime, 8, DateTime.Now);

            //Params[5] = db.MakeInParam("@PingYu", SqlDbType.VarChar, 300, PingYu);
            //Params[6] = db.MakeInParam("@IsUserView", SqlDbType.Int, 4, IsUserView);

            //string strSQL = "INSERT INTO Score ([StudentId],[PaperID],[Score],[ExamTime],[JudgeTime],[PingYu],[IsUserView]) " +
            //    " VALUES( @StudentId,@PaperID,@Score,@ExamTime,@JudgeTime,@PingYu,@IsUserView);";

            string strSQL = "INSERT INTO Score ([StudentId],[PaperID],[Score],[ExamTime],[JudgeTime]) " +
                " VALUES( @StudentId,@PaperID,@Score,@ExamTime,@JudgeTime);";
            int Count = db.ExecuteSql(strSQL,Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 向Score表中添加成绩
        /// 输出： 插入成功：返回True； 
        /// 插入失败：返回False；
        /// </summary>
        /// <returns></returns>
        public bool UpdateScore(Scores NewScore)
        {
            SqlParameter[] Params = new SqlParameter[7];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, StudentId);
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, PaperID);
            Params[2] = db.MakeInParam("@Score", SqlDbType.Int, 4, Score);
            Params[3] = db.MakeInParam("@ExamTime", SqlDbType.DateTime, 8, ExamTime);
            Params[4] = db.MakeInParam("@JudgeTime", SqlDbType.DateTime, 8, DateTime.Now);
            Params[5] = db.MakeInParam("@PingYu", SqlDbType.VarChar, 300, PingYu);
            Params[6] = db.MakeInParam("@IsUserView", SqlDbType.Int, 4, IsUserView);

            //string strSQL = "INSERT INTO Score ([StudentId],[PaperID],[Score],[ExamTime],[JudgeTime],[PingYu],[IsUserView]) " +
            //    " VALUES( @StudentId,@PaperID,@Score,@ExamTime,@JudgeTime,@PingYu,@IsUserView);";

            string strSQL = "UPDATE [Score] SET [Score] = @Score,[ExamTime] = @ExamTime,[JudgeTime] = @JudgeTime,[PingYu] = @PingYu" +
                            ",[IsUserView] = @IsUserView WHERE [StudentId] = @StudentId and [PaperID] = @PaperID;";
            int Count = db.ExecuteSql(strSQL, Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 根据学生ID 和试卷ID 查询是否有记录 验证成绩是否存在
        /// </summary>
        /// <param name="XUserID">学生ID </param>
        /// <param name="XPaperID">试卷ID</param>
        /// <returns>
        /// 有记录：返回true； 没有记录：返回False；
        /// </returns>
        public bool CheckScore(string XUserID,int XPaperID)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, XUserID);
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, XPaperID);

            string strSQL = "SELECT COUNT(StudentId) FROM  Score WHERE StudentId =@StudentId and PaperID= @PaperID";
            int iResult = db.ExecuteSelect(strSQL, Params);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        /// <summary>
        /// 删除成绩
        /// </summary>
        /// <param name="SID">成绩表 id</param>
        /// <returns>
        /// 删除成功：返回True； 删除失败：返回False；
        /// </returns>
        public bool DeleteByProc(int SID)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@ID", SqlDbType.Int, 4, SID); //成绩编号
           
            string strSQL = "DELETE Score WHERE ([ID] = @ID)";
            int Count = db.ExecuteSql(strSQL, Params);
                    
            if (Count > 0)
                return true;
            else return false;
        }
        
        /// <summary>
        /// 查询所用成绩 不需要参数
        /// </summary>
        /// <returns></returns>
        public DataSet QueryScore()
        {
            string strSQL = "SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID;";
            DBHelper db = new DBHelper();
            return db.GetDataSet(strSQL);
        }

        /// <summary>
        /// 教师查询所用成绩 不需要参数 显示该教师下的成绩
        /// </summary>
        /// <param name="strTeacherID">教师ID</param>
        /// <returns></returns>
        public DataSet QueryScore(string strTeacherID)
        {
            string strSQL = "SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID and Paper.TeacherId=@TeacherId;";
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherID);
            return db.GetDataSet(strSQL,Params);
        }


       
        /// <summary>
        /// 教师端 查询某个用户成绩  id  考试科目名称
        /// </summary>
        /// <param name="XUserID">学生 StudentId </param>
        /// <param name="PaperName">科目名称 可选参数</param>
        /// <returns></returns>
        public DataSet QueryUserScore(string XUserID, string PaperName =null)
        {
            SqlParameter[] Params = null;
            DBHelper db = new DBHelper();
            string strSQL = "";
            if (PaperName == null)
            {
                strSQL = "SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                        "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                        " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID and Students.StudentId=@StudentId;";
                Params = new SqlParameter[1];
                Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, XUserID);
            }
            else
            {
                strSQL = "SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                        "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                        " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID and Students.StudentId=@StudentId AND Paper.PaperName LIKE '%'+@PaperName+'%';";
                Params = new SqlParameter[2];
                Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, XUserID);
                Params[1] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, PaperName);
            }
            return db.GetDataSet(strSQL, Params);
        }


        /// <summary>
        /// 查询某个用户成绩  学生搜索 只可见开放的成绩
        /// </summary>
        /// <param name="XUserID">学生ID</param>
        /// <param name="PaperName">科目名称 可选参数</param>
        /// <returns></returns>
        public DataSet StudentQueryScore(string XUserID, string PaperName=null)
        {
            SqlParameter[] Params = null;
            DBHelper db = new DBHelper();
            string strSQL = "";
            if (PaperName == null )
            {
                strSQL  ="SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                        "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                        " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID and Students.StudentId=@StudentId and Score.IsUserView >0;";
                Params = new SqlParameter[1];
                Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, XUserID);
            }
            else
            {
                strSQL = "SELECT Students.StudentId,Students.StudentName,Score.ID,Score.Score,Score.[JudgeTime]," +
                        "Score.[ExamTime],[dbo].[Paper].[PaperName],[dbo].[Paper].PaperID" +
                        " FROM Students,Score,Paper WHERE Students.StudentId=Score.StudentId and Score.PaperID=Paper.PaperID and Students.StudentId=@StudentId AND Paper.PaperName LIKE '%'+@PaperName+'%' and Score.IsUserView >0;";
                Params = new SqlParameter[2];
                Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 20, XUserID);
                Params[1] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, PaperName);
            }
            return db.GetDataSet(strSQL, Params);
        }

        /// <summary>
        /// 根据学生ID 和试卷ID 查找用户该门试卷成绩
        /// </summary>
        /// <param name="strStudentId">学生ID</param>
        /// <param name="iPaperID">试卷ID</param>
        /// <returns></returns>
        public DataSet QueryUserScore(string strStudentId, int iPaperID)
        {
            SqlParameter[] Params = new SqlParameter[2];
            DBHelper db = new DBHelper();
            Params[0] = db.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int,4,iPaperID);

            string strSQL = "SELECT [ID],[StudentId],[PaperID],[Score],[ExamTime],[JudgeTime],[PingYu],[IsUserView]" +
                            " FROM Score WHERE StudentId =@StudentId and PaperID= @PaperID;";
            return db.GetDataSet(strSQL, Params);
        }

        #endregion 方法
    }
}