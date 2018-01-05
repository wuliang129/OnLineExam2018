<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="BulkImportPaper.aspx.cs" Inherits="Web_teacher_BulkImportPaper" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>试卷制定—批量导入试题</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      

        <tr style="background: url(~/Images/lineS.jpg) repeat-x;">
            <td style="height:25;" colspan="3">
                &nbsp;&nbsp;&nbsp;欢迎您：<asp:Label ID="labUser" runat="server" Text="Label" Width="70px"></asp:Label>&nbsp;&nbsp;
                                <script type="text/javascript">                                    getDate();</script>

                <span id="ShowTime"></span></td>         
        </tr>


          <tr>
            <td  style="width: 130px" align="center" valign="top">
              
           </td>          
              <td  style="width: 4px;  background: url(~/Images/line.gif) repeat-y;"> 
            </td>   
            <td  valign="top" align="left"  width="960px">
            
           
                       <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
               <tr height="50px">
                    <td bgcolor="#eeeeee"  colspan="3"> <div class="title" align="left"><h4>试卷制定—批量导入试题</h4></div></td>
                    <td bgcolor="#eeeeee" width="120px">下载题目EXCEL模板</td>                    
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>考试科目：</b></td>
                    <td colspan="3" >&nbsp;<div align="left" >
                        <asp:dropdownlist id="ddlCourse" 
                            runat="server" Font-Size="10pt" Width="226px" Height="23px"></asp:dropdownlist></div>
                    </td>
                    
                </tr>                 
				<tr> 
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>单选题批量导入</b>：</td>
                      <td ><asp:FileUpload ID="FileUpload_Single" runat="server" /></td>
                     <td >
                         <asp:Button ID="btn_SingleImport" runat="server" Text="单选批量导入" 
                             onclick="btn_SingleImport_Click" /></td>
                     <td >
                         <a href="../../Upfiles/temp.html" target="_blank">查看导入失败记录</a>
                    </td>
                </tr>
                <tr> 
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>多选题批量导入</b>：</td>
                      <td ><asp:FileUpload ID="FileUpload_MultiSelect" runat="server" /></td>
                     <td >
                         <asp:Button ID="btn_MultiSelectImport" runat="server" Text="多选题批量导入" onclick="btn_MultiSelectImport_Click"
                              /></td>
                     <td >
                         <a href="../../Upfiles/temp.html" target="_blank">查看导入失败记录</a>
                    </td>
                </tr>                  
                <tr> 
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>判断题批量导入</b>：</td>
                      <td ><asp:FileUpload ID="FileUpload_JudgeProblem" runat="server" /></td>
                     <td >
                         <asp:Button ID="btn_JudgeProblem" runat="server" Text="判断题批量导入" onclick="btn_JudgeProblem_Click" 
                              /></td>
                     <td >
                         <a href="../../Upfiles/temp.html" target="_blank">查看导入失败记录</a>
                    </td>
                </tr>               
                 <tr> 
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>填空题批量导入</b>：</td>
                      <td ><asp:FileUpload ID="FileUpload_FillBlankProblem" runat="server" /></td>
                     <td >
                         <asp:Button ID="btn_FillBlankProblem" runat="server" Text="填空题批量导入" onclick="btn_FillBlankProblem_Click" 
                              /></td>
                     <td >
                         <a href="../../Upfiles/temp.html" target="_blank">查看导入失败记录</a>
                    </td>
                </tr>
                <tr> 
                    <td bgcolor="#eeeeee" style="text-align:right;"><b>问答题批量导入</b>：</td>
                      <td ><asp:FileUpload ID="FileUpload_QuestionProblem" runat="server" /></td>
                     <td >
                         <asp:Button ID="btn_QuestionProblem" runat="server" Text="问答题批量导入" onclick="btn_QuestionProblem_Click"
                              /></td>
                     <td >
                         <a href="../../Upfiles/temp.html" target="_blank">查看导入失败记录</a>
                    </td>
                </tr>                                
                <tr>                  
                   
                     <td colspan="4" height="13px" style="background-image: url('../../Images/speater_table.jpg'); background-repeat: repeat-x"></td>
                </tr>
				<tr>
				    <td colspan=4>                       
                            <table cellSpacing="0" style="FONT-SIZE: 12px; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse; " cellPadding="0" width=100%	bgColor="#ffffff" border="1" bordercolor=gray>
				                <tr>
				                    <td align="center">
                                        
                                        <asp:GridView ID="gv_ExcelData" runat="server" 
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="13px"
                                            PageSize="15" Width="900px" CellPadding="3" 
                                            onrowdatabound="gv_ExcelData_RowDataBound">
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
				                </tr>
				            </table>                      
				    </td>
				</tr>
				
            </table>         
              
            </td>
        </tr>
    </table>

</asp:Content>

