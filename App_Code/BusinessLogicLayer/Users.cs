using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Security;

using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;
using OnLineExam.CommonComponent;

namespace OnLineExam.BusinessLogicLayer
{
    /// <summary>
    /// 用户类  操作对应的 后台 Users 表记录
    /// 修改完毕 全部用sql语句实现
    /// wla 2017-12-29
    /// </summary>
    public class Users
    {
        #region 私有成员
        private string _userID;                                               //用户编号
        private string _userPwd;                                         //用户密码
        private string _userName;                                             //用户姓名        
        private int _department;	                    //用户部门id
        private string _DepartmentName;	                    //用户部门名称
        private int _roleid;		                     //用户角色
        private string _rolename;
        private ArrayList _duties = new ArrayList();	//用户所有的权限

        private string _struserType;    //用户类型

        #endregion 私有成员

        #region 属性

        public string UserID
        {
            set
            {
                this._userID = value;
            }
            get
            {
                return this._userID;
            }
        }
        public string UserPwd
        {
            set
            {
                this._userPwd = value;
            }
            get
            {
                return this._userPwd;
            }
        }
      
        public string UserName
        {
            set
            {
                this._userName = value;
            }
            get
            {
                return this._userName;
            }
        }
        public int DepartmentId
        {
            set
            {
                this._department = value;
            }
            get
            {
                return this._department;
            }
        }

        public string DepartmentName
        {
            set
            {
                this._DepartmentName = value;
            }
            get
            {
                return this._DepartmentName;
            }
        }
        public int RoleId
        {
            set
            {
                this._roleid = value;
            }
            get
            {
                return this._roleid;
            }
        }
        public string RoleName
        {
            set
            {
                this._rolename = value;
            }
            get
            {
                return this._rolename;
            }
        }
        public ArrayList Duties
        {
            set
            {
                this._duties = value;
            }
            get
            {
                return this._duties;
            }
        }

        public string strUserType
        {
            set
            {
                this._struserType = value;
            }
            get
            {
                return this._struserType;
            }
        }

        #endregion 属性

        #region 方法


        /// <summary>
        /// 根据用户 UserID 初始化该用户
        /// </summary>
        /// <param name="XUserID">用户编号(账号)</param>
        /// <returns>
        ///  /// 输出：用户存在：返回True； 用户不在：返回False；
        /// </returns>
        public virtual bool LoadData(string XUserID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            DBHelper db = new DBHelper();
            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 50, XUserID); //用户编号  
            string strSQL = "SELECT * FROM [dbo].[Users],[dbo].[Department],[dbo].[Role]" +
                 " WHERE UserID=@UserID AND [dbo].[Users].DepartmentId = [dbo].[Department].DepartmentId And " +
             " [dbo].[Users].RoleId = [dbo].[Role].RoleId";

            DataSet ds = db.GetDataSet(strSQL, Params);

            ds.CaseSensitive = false;
            DataRow DR;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DR = ds.Tables[0].Rows[0];
                this.UserID = GetSafeData.ValidateDataRow_S(DR, "UserID");                         //用户编号              
                this.UserName = GetSafeData.ValidateDataRow_S(DR, "UserName");                     //用户姓名 
                this.UserPwd = GetSafeData.ValidateDataRow_S(DR, "UserPwd");                   //用户密码
                this.DepartmentId = GetSafeData.ValidateDataRow_N(DR, "DepartmentId");           //所在部门  
                this.RoleId = GetSafeData.ValidateDataRow_N(DR, "RoleId");                     //用户权限 
                this.RoleName = GetSafeData.ValidateDataRow_S(DR, "RoleName");                     //用户权限 
                this.DepartmentName = GetSafeData.ValidateDataRow_S(DR, "DepartmentName");     //所在部门名称 

                //获取权限集合
                string colName = "";
                for (int i = 0; i < DR.ItemArray.Length; i++)
                {
                    colName = DR.Table.Columns[i].ColumnName;
                    if (colName.StartsWith("HasDuty_") && GetSafeData.ValidateDataRow_N(DR, colName) == 1)
                    {
                        this.Duties.Add(DR.Table.Columns[i].ColumnName.Substring(8));	//去掉前缀“HasDuty_”
                    }
                }
                return true;
            }
            else
            {
                return false;
            }      
        }


        /// <summary>
        /// 根据UserID判断该用户是否存在
        /// </summary>
        /// <param name="XUserID">用户编号(账号)</param>
        /// <returns>
        /// 输出：用户存在：返回True； 用户不在：返回False；
        /// </returns>
        public virtual bool CheckUser(string XUserID)
        {
            return CheckUserPassword(XUserID);
        }


       /// <summary>
        /// 根据UserID UserPassword判断该用户是否存在 
       /// </summary>
        /// <param name="strUserID">用户编号</param>
       /// <param name="strUserPassword">可选参数 用户密码</param>
       /// <returns>
        /// 输出：用户存在：返回True； 用户不在：返回False；
       /// </returns>
        public virtual bool CheckUserPassword(string strUserID, string strUserPassword = null)
        {
            SqlParameter[] Params = null;
            string strSQL = "";
            if (strUserPassword != null)
            {
                Params = new SqlParameter[2];
                Params[1] = new SqlParameter("@UserPWD", System.Data.SqlDbType.NVarChar, 100);
                string strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(strUserPassword, "MD5");
                Params[1].SqlValue = strMD5Password;
                strSQL = "SELECT COUNT([ID]) FROM [Users] where [UserID] = @UserID and [UserPwd] = @UserPWD;";
            }
            else
            {
                Params = new SqlParameter[1];
                strSQL = "SELECT COUNT([ID]) FROM [Users] where [UserID] = @UserID;";
            }
            Params[0] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar, 50);
            Params[0].SqlValue = strUserID;

            DBHelper db = new DBHelper();
            int iResult = db.ExecuteSelect(strSQL, Params);
            if (iResult > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 根据用户 账号 密码  修改用户的密码
        /// </summary>
        /// <param name="strUserID">用户编号</param>
        /// <param name="strUserOldPwd">旧密码</param>
        /// <param name="strUserNewPwd">新密码</param>
        /// <returns></returns>
        public virtual bool ModifyPassword(string strUserID, string strUserOldPwd, string strUserNewPwd)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar, 50);
            Params[0].SqlValue = strUserID;

            Params[1] = new SqlParameter("@UserNewPWD", System.Data.SqlDbType.NVarChar, 100);
            string strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(strUserNewPwd, "MD5");
            Params[1].SqlValue = strMD5Password;

            Params[2] = new SqlParameter("@UserOldPWD", System.Data.SqlDbType.NVarChar, 100);
            strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(strUserOldPwd, "MD5");
            Params[2].SqlValue = strMD5Password;

            DBHelper db = new DBHelper();
            string strSQL = "UPDATE Users SET [UserPwd] = @UserNewPWD WHERE [UserID] = @UserID and [UserPwd] = @UserOldPWD";

            int iResult = db.ExecuteSql(strSQL, Params);
            
            if (iResult > 0)  return false;
            else return true;
        }



        /// <summary>
        /// 向Users表中添加用户信息  采用SQL命令来做 
        /// </summary>
        /// <returns>
        ///  输出：
        ///  插入成功：返回True； 
        ///  插入失败：返回False；
        /// </returns>
        public virtual bool InsertUser()
        {
            SqlParameter[] Params = new SqlParameter[5];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 50, UserID);               //用户编号
            Params[1] = db.MakeInParam("@UserName", SqlDbType.VarChar, 50, UserName);           //用户姓名
            Params[2] = db.MakeInParam("@UserPwd", SqlDbType.VarChar, 64, UserPwd);    //用户密码
            Params[3] = db.MakeInParam("@RoleId", SqlDbType.Int, 4, RoleId);    //角色
            Params[4] = db.MakeInParam("@DepartmentId", SqlDbType.Int, 4, DepartmentId);    //部门

            string strSQL = "INSERT INTO [OnLineExam].[dbo].[Users] ([UserID],[UserName],[UserPwd],[DepartmentId],[RoleId]) " +
                " VALUES ( @UserID, @UserName,@UserPwd,@DepartmentId,@RoleId)";
            int Count = db.ExecuteSql(strSQL, Params);
            if (Count > 0)
                return true;
            else return false;
        }
        


        /// <summary>
        /// 更新用户的信息 
        /// </summary>
        /// <param name="XUserID"></param>
        /// <returns></returns>
        public virtual bool UpdateUser(string XUserID)
        {
            SqlParameter[] Params = new SqlParameter[4];

            DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 50, XUserID);               //用户编号           
            Params[1] = db.MakeInParam("@UserName", SqlDbType.VarChar, 50, UserName);           //用户姓名           
            Params[2] = db.MakeInParam("@DepartmentId", SqlDbType.Int, 4, DepartmentId);        //部门           
            Params[3] = db.MakeInParam("@RoleId", SqlDbType.Int, 4, RoleId);           //角色
         
            string strSQL = "UPDATE [OnLineExam].[dbo].[Users] SET [UserName]= @UserName,[DepartmentId] = @DepartmentId,[RoleId] = @RoleId" +
                           " WHERE ([UserID] = @UserID)";
            int Count = db.ExecuteSql(strSQL,Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 删除用户  根据 用户 账号
        /// </summary>
        /// <param name="XUserID">XUserID - 用户编号（账号）；</param>
        /// <returns>
        /// 输出： 删除成功：返回True；删除失败：返回False；
        /// </returns>
        public virtual bool DeleteByUserID(string XUserID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DBHelper db = new DBHelper();
            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 50, XUserID);               //用户编号  
            string strSQL = "DELETE FROM [Users] WHERE UserID = @UserID";

            int iResult = db.ExecuteSql(strSQL, Params);
            if (iResult > 0)
            { return false; }
            else
            { return true; }
        }


        /// <summary>
        /// /查询所用用户 记录  需要什么信息随后根据需要调整 
        /// </summary>
        /// <returns>所有用户集合</returns>
        public virtual DataSet QueryUsers()
        {
            DBHelper db = new DBHelper();
            string strSQL = "SELECT [dbo].[Users].[UserID],[dbo].[Users].[UserName],[dbo].[Department].[DepartmentName]," +
                 "[dbo].[Role].[RoleName] FROM [dbo].[Users],[dbo].[Department],[dbo].[Role] WHERE [dbo].[Users].[DepartmentId]=[dbo].[Department].[DepartmentId]" +
                 " AND [dbo].[Users].[RoleId]=[dbo].[Role].[RoleId]";

            return db.GetDataSet(strSQL);
        }


        /// <summary>
        /// 采用 dbhelper 的方法构建用户数据表格
        /// 
        /// 条件queryItems是什么暂不清楚 回头调试修改
        /// </summary>
        /// <param name="queryItems">条件是什么暂不清楚 回头调试修改</param>
        /// <returns></returns>
        public virtual DataTable QueryUsers(Hashtable queryItems)
        {
            string where = SQLString.GetConditionClause(queryItems);
            string sql = "Select * From [Users],[Department],[Role] " + where;

            if (where == "")
                sql += " Where";
            else
                sql += " And";

            sql += " [Users].DepartmentId=[Department].DepartmentId"
                + " And [Users].RoleId=[Role].RoleId";

            DBHelper db = new DBHelper();
            return db.GetDataSet(sql).Tables[0];
        }

        #endregion 方法
    }
}