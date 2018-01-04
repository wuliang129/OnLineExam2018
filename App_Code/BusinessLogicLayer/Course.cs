using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;


namespace OnLineExam.BusinessLogicLayer
{
    //考试科目类
    public class Course
    {
        #region 私有成员
        private int _ID;             //题目编号            
        private string _Name;        //题目        
        private string _TeacherId;  //课程教师工号
        private string _CourseNo;  //课程编号

        #endregion 私有成员

        #region 属性

        public int ID
        {
            set
            {
                //this._ID = value;
            }
            get
            {
                return this._ID;
            }
        }
        public string Name
        {
            set
            {
                this._Name = value;
            }
            get
            {
                return this._Name;
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

        public string CourseNo
        {
            set
            {
                this._CourseNo = value;
            }
            get
            {
                return this._CourseNo;
            }
        }
        
        #endregion 属性

        #region 方法

        /// <summary>
        /// 默认构造方法
        /// </summary>
         public Course()
        { }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="ID">课程id</param>
         public Course(int ID)
         {
             string strSQL = "SELECT [ID],[CourseNo],[Name],[TeacherId]  FROM [Course] where [ID]=@ID;";
             SqlParameter[] Params = new SqlParameter[1];
             DBHelper db = new DBHelper();
             Params[0] = db.MakeInParam("@ID", SqlDbType.Int, 4, ID);  //课程编号 
             DataSet ds = db.GetDataSet(strSQL, Params);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 this._ID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                 this._CourseNo = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                 this._Name = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                 this._TeacherId = ds.Tables[0].Rows[0].ItemArray[3].ToString();
             }
         }   


        /// <summary>
         /// 向Course表中添加考试科目信息
        /// </summary>
         /// <param name="CourseInsert">课程对象</param>
         /// <returns>
         /// 插入成功：返回True； 插入失败：返回False；
         /// </returns>
         public bool InsertByProc(Course CourseInsert)
        {
            SqlParameter[] Params = new SqlParameter[3];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@Name", SqlDbType.VarChar, 200, CourseInsert.Name);      //课程名称
            Params[1] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, CourseInsert.TeacherId);      //课程教师工号            
            Params[2] = db.MakeInParam("@CourseNo", SqlDbType.VarChar, 20, CourseInsert.CourseNo);      //课程编号   

            string strSQL = "INSERT INTO [Course] ([CourseNo],[Name],[TeacherId] VALUES(@CourseNo,@Name,@TeacherId)";

            int Count = db.ExecuteSql(strSQL, Params);
            if (Count >= 1)
                return true;
            else return false;
        }

        /// <summary>
         /// 更新科目的信息 名称，课程编号，授课教师工号
        /// </summary>
        /// <param name="CourseUpdate">课程对象</param>
        /// <returns>
         /// 成功：返回True； 插入失败：返回False；
        /// </returns>
        public bool UpdateByProc(Course CourseUpdate)
        {
            SqlParameter[] Params = new SqlParameter[4];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@ID", SqlDbType.Int, 4, CourseUpdate.ID);               //课程编号            
            Params[1] = db.MakeInParam("@Name", SqlDbType.VarChar, 200, CourseUpdate.Name);      //课程名称
            Params[2] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, CourseUpdate.TeacherId);      //课程教师工号   
            Params[3] = db.MakeInParam("@CourseNo", SqlDbType.VarChar, 20, CourseUpdate.CourseNo);      //课程编号   

            string strSQL = "UPDATE [Course] SET [Name] = @Name,TeacherId = @TeacherId,CourseNo = @CourseNo  WHERE ( [ID] = @ID)";
            int Count = db.ExecuteSql(strSQL, Params);

            if (Count >= 1)
                return true;
            else return false;
        }

        /// <summary>
        /// 根据科目编号 删除科目
        /// </summary>
        /// <param name="CID">CID - 科目编号；</param>
        /// <returns>删除成功：返回True；删除失败：返回False；</returns>
        public bool DeleteByCourseNo(string strCourseNo)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@CourseNo", SqlDbType.VarChar, 20, strCourseNo);  //科目编号          
           
            string strSQL = "DELETE [Course] WHERE ([CourseNo]= @CourseNo)";

            int Count = db.ExecuteSql(strSQL, Params);

            if (Count >= 1)
                return true;
            else return false;
        }

        /// <summary>
        /// 删除科目 条件：课程ID
        /// </summary>
        /// <param name="CID">CID - 课程ID</param>
        /// <returns>删除成功：返回True；删除失败：返回False；</returns>
        public bool DeleteByID(int CID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@ID", SqlDbType.Int,4, CID);               //科目编号          
            string strSQL = "DELETE [Course] WHERE ([ID]= @ID)";

            int Count = db.ExecuteSql(strSQL, Params);

            if (Count >= 1)
                return true;
            else return false;
        }
        

        /// <summary>
        /// 查询可用用考试科目
        /// </summary>
        /// <returns></returns>
        public DataSet QueryCourse()
        {
            DBHelper db = new DBHelper();

            string strSQL = "SELECT [Course].[ID] as ID,[CourseNo],[Name],[Course].[TeacherId] as TeacherId,[ClassTime],[Location],[TotleHours],[CourseDetails],[CourseType],[remark],TeacherName" +
                  " FROM [Course],Teacher_InfoTable where Teacher_InfoTable.TeacherId = [Course].TeacherId;";
            return db.GetDataSet(strSQL);
        }

        /// <summary>
        /// 查询可用用考试科目  当前教师ID 
        /// </summary>
        /// <param name="strTeacherID"></param>
        /// <returns></returns>
        public DataSet QueryCourse(string strTeacherID)
        {
            DBHelper db = new DBHelper();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = db.MakeInParam("@TeacherId", SqlDbType.VarChar, 50, strTeacherID); //用户编号    

            string strSQL = "SELECT [Course].[ID] as ID,[CourseNo],[Name],[Course].[TeacherId] as TeacherId,[ClassTime],[Location],[TotleHours],[CourseDetails],[CourseType],[remark],TeacherName" +
                   " FROM [Course],Teacher_InfoTable where Teacher_InfoTable.TeacherId = [Course].TeacherId and [Course].[TeacherId] = @TeacherId;";
           return db.GetDataSet(strSQL, Params);
        }
        
        #endregion 方法
    }
}