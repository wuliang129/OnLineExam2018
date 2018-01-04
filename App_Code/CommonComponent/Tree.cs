using System;
using System.Data;
using OnLineExam.DataAccessLayer;

namespace OnLineExam.CommonComponent
{
    /// <summary>
    /// һ��ͨ�õ���״�˵���
    /// </summary>
    public class Tree
    {
        private string _treeHtml;
        private DataTable _dataTable;

        /// <summary>
        /// ����DataTable��������һ����
        /// </summary>
        /// <param name="dataTable">���нڵ������</param>
        /// <returns>����HTML����</returns>
        public string CreateTree(DataTable dataTable)
        {
            this._dataTable = dataTable;
            this.CreateSubTree(0);
            return _treeHtml;
        }


        /// <summary>
        /// ��ȡ���ڵ���ΪparentId�����нڵ㣬����DataTable����
        /// </summary>
        /// <param name="parentId">���ڵ���</param>
        /// <returns>DataTable��ʽ�����к��ӽڵ�����</returns>
        private DataTable GetChilds(int parentId)
        {
            DataTable childNodes = new DataTable();
            childNodes = this._dataTable.Clone();

            foreach (DataRow dr in this._dataTable.Rows)
            {
                if (Convert.ToInt32(dr["ParentId"]) == parentId)
                {
                    childNodes.ImportRow(dr);
                }
            }

            return childNodes;
        }

        /// <summary>
        /// �жϱ��ΪnodeId�Ľڵ��Ƿ�ΪҶ�ӽڵ�
        /// </summary>
        /// <param name="nodeId">���жϵĽڵ���</param>
        /// <returns>��Ҷ�ӽڵ㣺����true�����򣺷���false</returns>
        private bool IsLeaf(int nodeId)
        {
            foreach (DataRow dr in this._dataTable.Rows)
                if (Convert.ToInt32(dr["ParentId"]) == nodeId)
                    return false;
            return true;
        }

        /// <summary>
        /// �õ����ΪnodeId�Ľڵ�ĸ��ڵ���
        /// </summary>
        /// <param name="nodeId">�ڵ���</param>
        /// <returns>���ڵ���</returns>
        private int GetParent(int nodeId)
        {
            foreach (DataRow dr in this._dataTable.Rows)
                if (Convert.ToInt32(dr["NodeId"]) == nodeId)
                    return Convert.ToInt32(dr["ParentId"]);
            return -1;

        }

        /// <summary>
        /// �õ����ΪnodeId�Ľڵ�ļ��𣬸��ڵ�Ϊ0
        /// </summary>
        /// <param name="nodeId">������ľ޶����</param>
        /// <returns>�ڵ�ļ��𣬸��ڵ�Ϊ0</returns>
        private int GetLevel(int nodeId)
        {
            int parentId = GetParent(nodeId);
            if (parentId == 0) return 1;
            else
                return GetLevel(parentId) + 1;	//�ݹ�
        }

        /// <summary>
        /// �ݹ����ɸ����ΪnodeId����
        /// </summary>
        /// <param name="nodeId">��Ҫ���������ĸ��ڵ�</param>
        private void CreateSubTree(int nodeId)
        {
            DataTable childNodes = this.GetChilds(nodeId);	//��ȡ���ڵ�����к���

            //ѭ�����ɸ��ڵ�����к��Ӷ�Ӧ��HTML
            int childId = 0;
            foreach (DataRow dr in childNodes.Rows)
            {
                childId = Convert.ToInt32(dr["nodeId"]);
                this._treeHtml += "<div id=div_" + childId.ToString() + ">";

                //���ݸú��ӵļ�������һЩ�ո������ֲ�νṹ
                for (int i = 0; i < GetLevel(childId); i++)
                    this._treeHtml += "&nbsp;&nbsp;";

                //����ú�����Ҷ�ӽڵ㣬��������HTML����
                if (this.IsLeaf(childId))
                {
                    this._treeHtml += "<img src='..\\..\\Images\\folder.gif'/><a href=" + dr["Url"] + ">" + dr["Text"] + "</a></div>";
                }
                //����ú���Ϊ�м�ڵ㣬�����ȹ�����HTML��Ȼ��ݹ����������к��ӵ�HTML
                else
                {
                    this._treeHtml += "<img src='..\\..\\Images\\folderopen.gif'/><a href=" + dr["Url"] + ">" + dr["Text"] + "</a></div>";
                    this.CreateSubTree(childId);//�ݹ�
                }
            }
        }

    }
}