using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;

namespace OnLineExam.BusinessLogicLayer
{
    /// <summary>
    /// Role ��ժҪ˵����
    /// </summary>
    public class Role
    {
        #region ˽�г�Ա

        private int _roleId;			//��ɫ��ְ��ID
        private string _roleName;		//��ɫ��ְ����
        private int _HasDuty_DepartmentManage;
        private int _HasDuty_UserManage;
        private int _HasDuty_RoleManage;
        private int _HasDuty_Role;
        private int _HasDuty_CourseManage;
        private int _HasDuty_PaperSetup;
        private int _HasDuty_PaperLists;
        private int _HasDuty_UserPaperList;
        private int _HasDuty_UserScore;   
        private int _HasDuty_SingleSelectManage;
        private int _HasDuty_MultiSelectManage;
        private int _HasDuty_FillBlankManage;
        private int _HasDuty_JudgeManage;
        private int _HasDuty_QuestionManage;
        private bool _exist;				//�Ƿ���ڱ�־

        #endregion ˽�г�Ա

        #region ����

        public int RoleId
        {
            get
            {
                return this._roleId;
            }
        }
        public string RoleName
        {
            set
            {
                this._roleName = value;
            }
            get
            {
                return this._roleName;
            }
        }
        public int HasDuty_DepartmentManage
        {
            set
            {
                this._HasDuty_DepartmentManage = value;
            }
            get
            {
                return this._HasDuty_DepartmentManage;
            }
        }
        public int HasDuty_UserManage
        {
            set
            {
                this._HasDuty_UserManage = value;
            }
            get
            {
                return this._HasDuty_UserManage;
            }
        }
        public int HasDuty_RoleManage
        {
            set
            {
                this._HasDuty_RoleManage = value;
            }
            get
            {
                return this._HasDuty_RoleManage;
            }
        }
        public int HasDuty_Role
        {
            set
            {
                this._HasDuty_Role = value;
            }
            get
            {
                return this._HasDuty_Role;
            }
        }
        public int HasDuty_UserScore
        {
            set
            {
                this._HasDuty_UserScore = value;
            }
            get
            {
                return this._HasDuty_UserScore;
            }
        }
        public int HasDuty_CourseManage
        {
            set
            {
                this._HasDuty_CourseManage = value;
            }
            get
            {
                return this._HasDuty_CourseManage;
            }
        }
        public int HasDuty_PaperSetup
        {
            set
            {
                this._HasDuty_PaperSetup = value;
            }
            get
            {
                return this._HasDuty_PaperSetup;
            }
        }
        public int HasDuty_PaperLists
        {
            set
            {
                this._HasDuty_PaperLists = value;
            }
            get
            {
                return this._HasDuty_PaperLists;
            }
        }
        public int HasDuty_UserPaperList
        {
            set
            {
                this._HasDuty_UserPaperList = value;
            }
            get
            {
                return this._HasDuty_UserPaperList;
            }
        }
        public int HasDuty_SingleSelectManage
        {
            set
            {
                this._HasDuty_SingleSelectManage = value;
            }
            get
            {
                return this._HasDuty_SingleSelectManage;
            }
        }
        public int HasDuty_MultiSelectManage
        {
            set
            {
                this._HasDuty_MultiSelectManage = value;
            }
            get
            {
                return this._HasDuty_MultiSelectManage;
            }
        }
        public int HasDuty_FillBlankManage
        {
            set
            {
                this._HasDuty_FillBlankManage = value;
            }
            get
            {
                return this._HasDuty_FillBlankManage;
            }
        }
        public int HasDuty_JudgeManage
        {
            set
            {
                this._HasDuty_JudgeManage = value;
            }
            get
            {
                return this._HasDuty_JudgeManage;
            }
        }
        public int HasDuty_QuestionManage
        {
            set
            {
                this._HasDuty_QuestionManage = value;
            }
            get
            {
                return this._HasDuty_QuestionManage;
            }
        }
       
        public bool Exist
        {
            get
            {
                return this._exist;
            }
        }

        #endregion ����

        #region ����

        /// <summary>
        /// ���ݲ���roleId����ȡ��ɫ��ְ����ϸ��Ϣ
        /// </summary>
        /// <param name="topicID">��ɫ��ְ��ID</param>
        public void LoadData(int roleId)
        {
            DataBase db = new DataBase();		//ʵ����һ��Database��

            string sql = "";
            sql = "Select * from [Role] where RoleId = " + roleId;

            DataRow dr = db.GetDataRow(sql);	//����Database���GetDataRow������ѯ����

            //���ݲ�ѯ�õ������ݣ��Գ�Ա��ֵ
            if (dr != null)
            {
                this._roleId = GetSafeData.ValidateDataRow_N(dr, "RoleId");
                this._roleName = GetSafeData.ValidateDataRow_S(dr, "RoleName");

                this._exist = true;
            }
            else
            {
                this._exist = false;
            }
        }
        public bool CheckRole(string XRoleName)
        {
            SqlParameter[] Params = new SqlParameter[1];
            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@RoleName", SqlDbType.VarChar, 20, XRoleName);

            SqlDataReader DR = DB.RunProcGetReader("Proc_RoleDetail", Params);
            if (!DR.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool InsertByProc()
        {
            SqlParameter[] Params = new SqlParameter[15];

            DataBase DB = new DataBase();
            
            Params[0] = DB.MakeInParam("@RoleName", SqlDbType.VarChar, 20, RoleName);
            Params[1] = DB.MakeInParam("@HasDuty_DepartmentManage", SqlDbType.Int, 4, HasDuty_DepartmentManage);
            Params[2] = DB.MakeInParam("@HasDuty_UserManage", SqlDbType.Int, 4, HasDuty_UserManage);
            Params[3] = DB.MakeInParam("@HasDuty_RoleManage", SqlDbType.Int, 4,HasDuty_RoleManage);
            Params[4] = DB.MakeInParam("@HasDuty_Role", SqlDbType.Int, 4, HasDuty_Role);
            Params[5] = DB.MakeInParam("@HasDuty_UserScore", SqlDbType.Int, 4, HasDuty_UserScore);
            Params[6] = DB.MakeInParam("@HasDuty_CourseManage", SqlDbType.Int, 4, HasDuty_CourseManage);
            Params[7] = DB.MakeInParam("@HasDuty_PaperSetup", SqlDbType.Int, 4, HasDuty_PaperSetup);
            Params[8] = DB.MakeInParam("@HasDuty_PaperLists", SqlDbType.Int, 4, HasDuty_PaperLists);
            Params[9] = DB.MakeInParam("@HasDuty_SingleSelectManage", SqlDbType.Int, 4, HasDuty_SingleSelectManage);
            Params[10] = DB.MakeInParam("@HasDuty_MultiSelectManage", SqlDbType.Int, 4, HasDuty_MultiSelectManage);
            Params[11] = DB.MakeInParam("@HasDuty_FillBlankManage", SqlDbType.Int, 4, HasDuty_FillBlankManage);
            Params[12] = DB.MakeInParam("@HasDuty_JudgeManage", SqlDbType.Int, 4, HasDuty_JudgeManage);
            Params[13] = DB.MakeInParam("@HasDuty_QuestionManage", SqlDbType.Int, 4, HasDuty_QuestionManage);
            Params[14] = DB.MakeInParam("@HasDuty_UserPaperList", SqlDbType.Int, 4, HasDuty_UserPaperList);
      
            int Count = -1;
            Count = DB.RunProc("Proc_RoleAdd", Params);
            if (Count > 0)
                return true;
            else return false;
        }
        /// <summary>
        /// ���ݲ�ѯ������ϣ��,��ѯ����
        /// </summary>
        /// <param name="queryItems">��ѯ������ϣ��</param>
        /// <returns>��ѯ�������DataTable</returns>
        public static DataTable Query(Hashtable queryItems)
        {
            string where = SQLString.GetConditionClause(queryItems);
            string sql = "Select top 2 * From [Role] order by RoleId" + where;
            DataBase db = new DataBase();
            return db.GetDataTable(sql);
        }

        /// <summary>
        /// �޸Ľ�ɫȨ����Ϣ
        /// </summary>
        /// <param name="roleInfo">��ɫȨ����Ϣ��ϣ��</param>
        public static void Update(Hashtable roleInfo, string where)
        {
            DataBase db = new DataBase();			//ʵ����һ��Database��
            db.Update("[Role]", roleInfo, where);	//����Database���Update�����޸�����
        }
        public bool UpdateByProc(int XRoleId)
        {
            SqlParameter[] Params = new SqlParameter[2];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@RoleId", SqlDbType.Int, 4, XRoleId);                     
            Params[1] = DB.MakeInParam("@RoleName", SqlDbType.VarChar, 50, RoleName);               

            int Count = -1;
            Count = DB.RunProc("Proc_RoleModify", Params);
            if (Count > 0)
                return true;
            else return false;
        }
        public bool DeleteByProc(int XRoleId)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@RoleId", SqlDbType.Int, 4,XRoleId);               //��Ŀ���          

            int Count = -1;
            Count = DB.RunProc("Proc_RoleDelete", Params);
            if (Count > 0)
                return true;
            else return false;
        }
        //��ѯ���н�ɫ
        //����Ҫ����
        public DataSet QueryRole()
        {
            DataBase DB = new DataBase();
            return DB.GetDataSet("Proc_RoleList");
        }
        #endregion ����
    }
}
