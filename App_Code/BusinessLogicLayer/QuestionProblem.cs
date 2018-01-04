using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;
using OnLineExam.DataAccessHelper;

namespace OnLineExam.BusinessLogicLayer
{
    //�ʴ���
    public class QuestionProblem : BaseProblem
	{
        #region ˽�г�Ա
        private int _ID;                                               //��Ŀ���
        private int _CourseID;                                         //������Ŀ        
        private string _Title;                                         //��Ŀ       
        private string _Answer;                                       //��
        private string _Explain;                                        //����˵��


        #endregion ˽�г�Ա

        #region ����

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

        #endregion ����

         #region ����

        /// <summary>
        /// ������ĿID ��ʼ����Ŀ
        /// </summary>
        /// <param name="TID">��Ŀ���</param>
        /// <returns>��Ŀ���ڣ�����True��     ��Ŀ���ڣ�����False��</returns>
        public bool LoadData(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                  //�û����            

            DataSet ds = DB.GetDataSet("Proc_QuestionProblemDetail", Params);
            ds.CaseSensitive = false;
            DataRow DR;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DR = ds.Tables[0].Rows[0];
                this._CourseID = GetSafeData.ValidateDataRow_N(DR, "CourseID");                   //��Ŀ���                
                this._Title = GetSafeData.ValidateDataRow_S(DR, "Title");                         //��Ŀ
                this._Answer = GetSafeData.ValidateDataRow_S(DR, "Answer");                     //��                
                this._Explain = GetSafeData.ValidateDataRow_S(DR, "Explain");                     //����˵��

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// ����������Ŀ��Ϣ(���ô洢����)
        /// </summary>
        /// <returns>����ɹ�������True�� ����ʧ�ܣ�����False��</returns>
        public bool InsertByProc()
        {
            SqlParameter[] Params = new SqlParameter[4];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                 //��Ŀ���
            Params[1] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);                //��Ŀ            
            Params[2] = DB.MakeInParam("@Answer", SqlDbType.VarChar,1000, Answer);                      //��A            
            Params[3] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //����˵��

            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemAdd", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// �����ж������Ϣ
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public bool UpdateByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[5];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);                           //��Ŀ���
            Params[1] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, CourseID);                //��Ŀ���
            Params[2] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);               //��Ŀ            
            Params[3] = DB.MakeInParam("@Answer", SqlDbType.VarChar, 1000, Answer);                    //��           
            Params[4] = DB.MakeInParam("@Explain", SqlDbType.VarChar, 500, Explain);              //����˵��


            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemModify", Params);
            if (Count > 0)
                return true;
            else return false;
        }


        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="TID">��Ŀ���</param>
        /// <returns>ɾ���ɹ�������True��  ɾ��ʧ�ܣ�����False��</returns>
        public bool DeleteByProc(int TID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@ID", SqlDbType.Int, 4, TID);               //��Ŀ���          

            int Count = -1;
            Count = DB.RunProc("Proc_QuestionProblemDelete", Params);
            if (Count > 0)
                return true;
            else return false;
        }

        /// <summary>
        /// ���ݿγ̱�Ų�ѯ��Ŀ��Ϣ
        /// </summary>
        /// <param name="TCourseID">�γ̱��</param>
        /// <returns></returns>
        public DataSet QueryQuestionProblem(int TCourseID)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@CourseID", SqlDbType.Int, 4, TCourseID);               //��Ŀ���           
            return DB.GetDataSet("Proc_QuestionProblemList", Params);
        }


        /// <summary>
        /// �����ʴ� title ���Ʋ�ѯ��¼�Ƿ����
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public bool IsRecord_Exit_ByTitle(string Title)
        {
            SqlParameter[] Params = new SqlParameter[1];

            DataBase DB = new DataBase();

            Params[0] = DB.MakeInParam("@Title", SqlDbType.VarChar, 1000, Title);                //��Ŀ     

            if (DB.GetDataSet("Proc_QuestionProblemIsExitByTitle", Params).Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      
        #endregion ����
    }
}
