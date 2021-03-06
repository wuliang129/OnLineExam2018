﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Data.SqlClient;
using OnLineExam.DataAccessHelper;

namespace OnLineExam.BusinessLogicLayer
{

    /// <summary>
    /// Students 的摘要说明
    /// 操作后台的  Students表
    /// </summary>
    public class Students:Users
    {
        public Students()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 方法

        //根据用户 UserID 初始化该用户
        //输入：
        //      XUserID - 用户编号；
        //输出：
        //      用户存在：返回True；
        //      用户不在：返回False；
        public override bool LoadData(string XUserID)
        {
            SqlParameter[] Params = new SqlParameter[1];
             DBHelper db = new DBHelper();

            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 50, XUserID); //用户编号    
         
            string strSQL = "SELECT StudentId as UserID,StudentName as UserName,StudentPWD as UserPwd,Students.DepartmentId as DepartmentId," +
	           "Department.DepartmentName as DepartmentName FROM Students,[dbo].[Department] WHERE Students.StudentId=@UserID AND Students.DepartmentId = [dbo].[Department].DepartmentId";

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
                this.DepartmentName = GetSafeData.ValidateDataRow_S(DR, "DepartmentName");     //所在部门名称 
                //this.RoleId = GetSafeData.ValidateDataRow_N(DR, "RoleId");                     //用户权限 
                //this.RoleName = GetSafeData.ValidateDataRow_S(DR, "RoleName");                     //用户权限 
                
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据UserID(用户账号)和UserPassword判断密码是否正确
        /// </summary>
        /// <param name="XUserID"> XUserID - 用户编号(账号)； </param>
        /// <param name="strUserPassword">密码(未加密) 可选参数</param>
        /// <returns>
        /// 用户存在：返回True；
        ///用户不在：返回False；
        /// </returns>
        public override bool CheckUserPassword(string strUserID, string strUserPassword = null)
        {
            SqlParameter[] Params = null;
            string strSQL = "";
            if (strUserPassword != null)
            {
                Params = new SqlParameter[2];
                Params[1] = new SqlParameter("@UserPWD", System.Data.SqlDbType.NVarChar, 100);
                string strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(strUserPassword, "MD5");
                Params[1].SqlValue = strMD5Password;
                strSQL = "SELECT COUNT([Sid]) FROM Students where [StudentId] = @UserID and [StudentPWD] = @UserPWD;";
            }
            else
            {
                Params = new SqlParameter[1];
                strSQL = "SELECT COUNT([Sid]) FROM Students where [StudentId] = @UserID;";
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
        /// 判断学生是否已经考试
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PaperID"></param>
        /// <returns>
        /// true 已经考试过了
        /// false 尚未考试
        /// </returns>
        public bool IsStudentTest(string UserID, int PaperID)
        {
            SqlParameter[] Params = new SqlParameter[2];

            DBHelper db = new DBHelper();
            Params[0] = db.MakeInParam("@UserID", SqlDbType.VarChar, 20, UserID);
            Params[1] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, PaperID);
            string strSQL = "SELECT COUNT(StudentID)  FROM StudentAnswer  WHERE StudentID=@UserID AND PaperID=@PaperID";
            int iResult = db.ExecuteSelect(strSQL, Params);
            if (iResult >= 1)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// 根据用户 账号 密码  修改用户的密码
        /// </summary>
        /// <param name="strUserID">用户编号</param>
        /// <param name="strUserOldPwd">旧密码</param>
        /// <param name="strUserNewPwd">新密码</param>
        /// <returns></returns>
        public override bool ModifyPassword(string strUserID, string strUserOldPwd, string strUserNewPwd)
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
            string strSQL = "UPDATE [Students] SET [StudentPWD] = @UserNewPWD WHERE [StudentId] = @UserID and [StudentPWD] = @UserOldPWD";

            int iResult = db.ExecuteSql(strSQL, Params);

            if (iResult > 0) return false;
            else return true;
        }

        /// <summary>
        /// 删除学生  根据 用户 账号
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
            string strSQL = "DELETE FROM [Students] WHERE [StudentId] = @UserID";

            int iResult = db.ExecuteSql(strSQL, Params);
            if (iResult > 0)
            { return false; }
            else
            { return true; }
        }

        //查询用户
        //查询所用用户
        //不需要参数

        /// <summary>
        /// /查询所有 学生 记录 学号，学院，姓名 班级尚未处理
        /// </summary>
        /// <returns>所有用户集合</returns>
        public virtual DataSet QueryUsers()
        {
            DBHelper db = new DBHelper();
            string strSQL = "SELECT Students.StudentId,Students.StudentName,[dbo].[Department].[DepartmentName]" +
 " FROM Students,[dbo].[Department],[dbo].[Role] WHERE Students.[DepartmentId]=[dbo].[Department].[DepartmentId];";

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
            string sql = "Select * From [Students],[Department] " + where;

            if (where == "")
                sql += " Where";
            else
                sql += " And";

            sql += " [Students].DepartmentId=[Department].DepartmentId;";

            DBHelper db = new DBHelper();
            return db.GetDataSet(sql).Tables[0];
        }       
        
       

        #endregion 方法

    }

}