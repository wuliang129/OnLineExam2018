using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;


namespace OnLineExam.BusinessLogicLayer
{
    /*
     * UpdatePaperState(string strTeacherId, int XPaperID, int PaperState) 待处理测试
     * 
     * */
    public class Paper
    {
        #region 私有成员
        private int _paperID;                                               //试卷编号
        private int _courseID;                                              //科目编号 
        private string _paperName;                                          //试卷名称  
        private int _paperState;                                           //试卷状态 0,1对应的后台 false 和 true 
        private string _type;
        private string _TeacherId;
        private string _state;

        private string _StudentId; //学生ID

        #endregion 私有成员

        #region 属性
      
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
        public int CourseID
        {
            set
            {
                this._courseID = value;
            }
            get
            {
                return this._courseID;
            }
        }
        public string PaperName
        {
            set
            {
                this._paperName = value;
            }
            get
            {
                return this._paperName;
            }
        }        
        public int PaperState
        {
            set
            {
                this._paperState = value;
            }
            get
            {
                return this._paperState;
            }
        }
        public string Type
        {
            set
            {
                this._type = value;
            }
            get
            {
                return this._type;
            }
        }
        public string TeacherId
        {
            set
            {
                this._TeacherId = value;
            }
            get
            {
                return this._TeacherId;
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

        public string state
        {
            set
            {
                this._state = value;
            }
            get
            {
                return this._state;
            }
        }
        
        #endregion 属性

        #region 方法

        public Paper()
        { }


        /// <summary>
        /// 根据试卷 id 构建试卷对象 用于修改
        /// </summary>
        /// <param name="PID"></param>
        public Paper(int PID)
        {
            DBHelper db = new DBHelper();

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, PID);               //科目编号
            
            string strSQL = "SELECT [CourseID],[PaperName],[PaperState],[TeacherId] FROM [Paper] where [PaperID]=@PaperID";

            DataSet ds = db.GetDataSet(strSQL, Params);
            if(ds.Tables[0].Rows.Count > 0)
            {
                this._paperID = PID;
                this._courseID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                this._paperName = ds.Tables[0].Rows[1].ItemArray[1].ToString();
                this._paperState = Convert.ToInt32(ds.Tables[0].Rows[1].ItemArray[2].ToString());
                this._TeacherId = ds.Tables[0].Rows[1].ItemArray[3].ToString();
            }
        }
        /// <summary>
        /// 向Paper表中添加试卷信息(
        /// </summary>
        /// <returns>
        /// 成功：返回True；插入失败：返回False；
        /// </returns>
        public bool InsertPaper()
        {
            SqlParameter[] Params = new SqlParameter[3];
            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);               //科目编号
            Params[1] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, PaperName);       //试卷名称 
            Params[2] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, TeacherId);        //出卷人 

            string strSQL = "insert into Paper(CourseID,PaperName,TeacherId) values(@CourseID,@PaperName,@TeacherId);";
            int Count = db.ExecuteSql(strSQL, Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 更新试卷信息
        /// </summary>
        /// <param name="PID"></param>
        /// <returns>
        /// 成功：返回True；插入失败：返回False；
        /// </returns>
        public bool UpdatePaperByID(int PID)
        {
            SqlParameter[] Params = new SqlParameter[5];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, PID);                      //试卷编号                       
            Params[1] = db.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);               //科目编号
            Params[2] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, PaperName);       //试卷名称 
            Params[3] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, TeacherId);        //出卷人 
            Params[4] = db.MakeInParam("@PaperState", SqlDbType.Bit, 1, PaperState);            //试卷状态
            string strSQL = "UPDATE [Paper] SET [CourseID] = @CourseID,[PaperName] = @PaperName ,[PaperState] = @PaperState," +
                        "[TeacherId] = @TeacherId WHERE PaperID = @PaperID";
            int Count = db.ExecuteSql(strSQL, Params);
            if (Count >= 1)
                return true;
            else return false;
        }
        
        /// <summary>
        /// 更新试卷是否评阅的状态  只有整个试卷都用完才能修改 m目前没有用
        /// 后期调试时再说 
        /// </summary>
        /// <param name="strTeacherId">教师ID 可选参数 </param>
        /// <param name="XPaperID">试卷id</param>
        /// <param name="PaperState">
        /// 试卷状态： 1 可用  0 不可用
        /// </param>
        /// <returns></returns>
        public bool UpdatePaperState(int XPaperID, int PaperState,string strTeacherId=null)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = null;
            string strSQL;

            if (strTeacherId != null)
            {
                Params = new SqlParameter[3];
                Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherId);
                Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, XPaperID);
                Params[2] = db.MakeInParam("@PaperState", SqlDbType.Int, 1, PaperState);
                strSQL = "UPDATE [Paper] SET [PaperState] = @PaperState WHERE [PaperID] = @PaperID and [TeacherId] =@TeacherId;";
            }
            else
            {
                Params = new SqlParameter[2];
                Params[0] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, XPaperID);
                Params[1] = db.MakeInParam("@PaperState", SqlDbType.Int, 1, PaperState);
                strSQL = "UPDATE [Paper] SET [PaperState] = @PaperState WHERE [PaperID] = @PaperID;";
            }

            int Count = db.ExecuteSql(strSQL,Params);
            if (Count > 0)
                return true;
            else return false;
        }

        

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="PID">TID - 试卷编号</param>
        /// <returns>
        /// 删除成功：返回True；删除失败：返回False；
        /// </returns>
        public bool DeleteByPID(int PID)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@ID", SqlDbType.Int, 4, PID);               //题目编号          
           
            int Count = db.RunProcedureExecuteSql("Proc_PaperDelete", Params);
            if (Count > 0)
                return true;
            else return false;
        }
        
        /// <summary>
        /// 删除某位考生的考试题目记录和 成绩表成绩记录
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="paperid"></param>
        /// <returns></returns>
        public bool DeleteByProc(string studentID,int paperID)
        {
            DBHelper db = new DBHelper();

            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = db.MakeInParam("@StudentID", SqlDbType.VarChar, 50, studentID);      //学生ＩＤ          
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷ＩＤ 

            string strSQL = "DELETE [Score] WHERE  [PaperID] = @PaperID and [StudentId] = @StudentID;";
            strSQL += "DELETE [StudentAnswer] WHERE  [PaperID] = @PaperID and [StudentID] = @StudentID;";
            int Count = db.ExecuteSql(strSQL, Params);
            if (Count >= 1)
                return true;
            else return false;
        }
     
        /// <summary>
        /// 查询所用试卷
        /// </summary>
        /// <returns></returns>
        public DataSet QueryAllPaper()
        {
            DBHelper db = new DBHelper();

            string strSQL = "SELECT *,CASE  [dbo].[Paper].PaperState WHEN 1 THEN '可用'  ELSE  '不可用'  END AS state" +
                            " FROM  [dbo].[Paper],[dbo].[Course] where [Paper].CourseID=[Course].ID";

            return db.GetDataSet(strSQL);
        }

       /// <summary>
        /// 根据工号 查询所用试卷
       /// </summary>
       /// <param name="strTeacherId">教师工号</param>
       /// <returns></returns>
        public DataSet QueryAllPaper(string strTeacherId)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherId);

            string strSQL = "SELECT *,CASE  [dbo].[Paper].PaperState WHEN 1 THEN '可用'  ELSE  '不可用'  END AS state" +
                            " FROM  [dbo].[Paper],[dbo].[Course] where [Paper].CourseID=[Course].ID and [Paper].TeacherId =@TeacherId;";

            return db.GetDataSet(strSQL,Params);
        }


        /// <summary>
        /// 查询所用可用试卷
        /// </summary>
        /// <returns></returns>
        public DataSet QueryPaper()
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@PaperState", SqlDbType.Bit, 1, 1);

            string strSQL = "SELECT [PaperID],[CourseID],[PaperName],[PaperState],[TeacherId],[CreateDate] FROM [Paper] where PaperState=@paperState;";

            return db.GetDataSet(strSQL, Params);
        }
        

        /// <summary>
        /// 查询学生考试的试卷记录
        /// </summary>
        /// <param name="strStudentID">
        /// 默认为null 查询所有学生考试记录
        /// 否则根据输入的 学生id 查询某个学生的试卷考试记录
        /// </param>
        /// <returns></returns>
        public DataSet QueryStudentPaperList(string strTeacherID,string strStudentID= null)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = null;

            if(strStudentID !=null)
            { 
                Params = new SqlParameter[2];
                Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherID);
                Params[1] = db.MakeInParam("@StudentID", SqlDbType.VarChar, 50, strStudentID);
            }
            else
            {
                Params = new SqlParameter[1];
                Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherID);
            }
            return db.RunProcedureGetDataSet("Proc_StudentPaperList", Params);
        }

        /// <summary>
        /// 查询某个学生考试的试卷
        /// </summary>
        /// <param name="type"></param>
        /// <param name="studentid">学生ID</param>
        /// <param name="paperID">试卷id 可选参数</param>
        /// <returns></returns>
        public DataSet QueryStudentPaper(string type,string studentid,int paperID)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[3];
            
            Params[0] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, type);            //题目类型   
            Params[1] = db.MakeInParam("@StudentID", SqlDbType.VarChar, 50, studentid);            //用户ID 
            Params[2] = db.MakeInParam("@paperID", SqlDbType.Int, 4, paperID);            //试卷编号

            return db.RunProcedureGetDataSet("Proc_StudentAnswer", Params);
        }       
        #endregion 方法
    }
}