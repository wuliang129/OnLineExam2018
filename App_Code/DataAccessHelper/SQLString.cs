using System;
using System.Collections;
//
namespace OnLineExam.DataAccessHelper
{
	//�Դ��ݹ������ַ������д������
	public class SQLString
	{
		//���о�̬��������SQL�ַ��������(')ת����('')
		public static String GetSafeSqlString(String XStr)
		{
			return XStr.Replace("'","''");
		}
       
		//���о�̬��������SQL�ַ��������(')ת����('')�������ַ��������߼���(')
		public static String GetQuotedString(String XStr)
		{
			return ("'" + GetSafeSqlString(XStr) + "'");
		}
        public static String GetConditionClause(Hashtable queryItems)
        {

            int Count = 0;
            String Where = "";

            //���ݹ�ϣ��ѭ�����������Ӿ�
            foreach (DictionaryEntry item in queryItems)
            {
                if (Count == 0)
                    Where = " Where ";
                else
                    Where += " And ";

                //���ݲ�ѯ�е��������ͣ������Ƿ�ӵ�����
                if (item.Value.GetType().ToString() == "System.String" || item.Value.GetType().ToString() == "System.DateTime")
                {
                    Where += item.Key.ToString()
                        + " Like "
                        + SQLString.GetQuotedString("%"
                        + item.Value.ToString()
                        + "%");
                }
                else
                {
                    Where += item.Key.ToString() + "= " + item.Value.ToString();
                }
                Count++;
            }
            return Where;
        }
        /// <summary>
        /// ����������ϣ��,����SQL����е������Ӿ�
        /// </summary>
        /// <param name="conditionHash">������ϣ��</param>
        /// <param name="type">�뻹�ǻ��ѯ��ȡֵ={"AND","OR"}</param>
        /// <returns>AND��ϵ�����Ӿ�</returns>
        public static String GetConditionClause(Hashtable queryItems, string type)
        {

            int Count = 0;
            String Where = "";

            //���ݹ�ϣ��ѭ�����������Ӿ�
            foreach (DictionaryEntry item in queryItems)
            {
                if (Count == 0)
                    Where = " Where ";
                else
                    Where += " " + type + " ";

                //���ݲ�ѯ�е��������ͣ������Ƿ�ӵ�����
                if (item.Value.GetType().ToString() == "System.String" || item.Value.GetType().ToString() == "System.DateTime")
                {
                    Where += item.Key.ToString()
                        + " Like "
                        + SQLString.GetQuotedString("%"
                        + item.Value.ToString()
                        + "%");
                }
                else
                {
                    Where += item.Key.ToString() + "= " + item.Value.ToString();
                }
                Count++;
            }
            return Where;
        }
	}
}
