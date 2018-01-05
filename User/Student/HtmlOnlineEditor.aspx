<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/common_head.master" CodeFile="HtmlOnlineEditor.aspx.cs" Inherits="HtmlOnlineEditor" EnableEventValidation="false" %>

<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Src="~/Controls/StudentLeftTree.ascx" TagName="StudentLeftTree" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>HTML在线编辑</title>
    <style type="text/css">
        body { font-family: Verdana; font-size: 8pt; margin: 10px; }
        .button { font-family: Verdana; font-size: 8pt; width: 100px; height: 30px; }
        .previewButton { margin-left: 10px; margin-right: 10px; margin-top: 3px; width: 75px; height: 28px; }
        .radiobuttonList label { margin-right: 5px; }
        .preview { width: 834px; padding: 10px; }
        div#Content { width: 1024px; }
        table#DemoTable { width: 1024px; }
        td#EditorCell { width: 844px; vertical-align: top; }
        td#OptionsCell { width: 180px; vertical-align: top; }
        div#Options { width: 150px; margin-left: 5px; border:1px solid #7B68EE; }
        div#DemoControls { width: 844px; height: 25px; line-height: 25px; text-align: center; }
        div#Preview { width: 844px; border: solid 1px gray; margin-top: 25px; }
        div#PreviewControls { height: 35px; line-height: 35px; text-align: left; border-bottom: solid 1px gray; }
        div.demoHeading { height: 25px; line-height: 25px; color: black; font-weight: bold; border-bottom: solid 1px gray; text-align: center; }
        div.optionsHeading { font-size: 10pt; border: none; text-align: left; margin-left: 10px; }
        div.optionsLabel { margin: 10px; font-weight: bold; }
        div.optionControls { margin-left: 10px; }
        div#Footer { margin-top: 10px; color: #7f9db9; font-size: 7pt; }
        div#Footer { margin-top: 10px; color: black; font-size: 7pt; }
        a:link.poweredby, a:visited.poweredby, a:active.poweredby { color: black; text-decoration: none; }
        a:hover.poweredby { text-decoration: underline; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">HTML在线编辑</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        
                       <div id="Content">
        
            <table id="DemoTable" border="0" cellpadding="0" cellspacing="0">
            
                <tr>
                
                    <td id="EditorCell">

                        <div id="EditorPanel">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="UpdateButton" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ToggleModeRadioButtonList" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ColorSchemeRadioButtonList" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="NoToolstripBackgroundImageBox" EventName="CheckedChanged" />
                                </Triggers>
                                <ContentTemplate>
                                
                                    <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Width="844px" />
                                
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <div id="DemoControls">
                            
                                <asp:Button ID="SaveButton" runat="server" Text="保存" OnClick="SaveButton_Click" 
                                    CssClass="button" ToolTip="Save the current editor text" />
                                <asp:Button ID="ClearButton" runat="server" Text="清空" 
                                    OnClick="ClearButton_Click" CssClass="button" 
                                    ToolTip="Clear the text in the editor above" />
                                <asp:Button ID="InfoButton" runat="server" Text="信息" OnClick="InfoButton_Click" 
                                    CssClass="button" ToolTip="Set the text in the editor above to info text" />
                                <asp:Button ID="UpdateButton" runat="server" Text="触发更新" 
                                    OnClick="UpdateButton_Click" CssClass="button" 
                                    ToolTip="Trigger an Update of the UpdatePanel that contains the editor above" />
                                
                            </div>
                            
                            <div id="Preview">
                            
                                <div id="PreviewHeading" runat="server" class="demoHeading">预览</div>
                                
                                <div id="PreviewControls">

                                    <asp:Button ID="PreviewButton" runat="server" Text="网页预览" 
                                        OnClick="PreviewButton_Click" CssClass="previewButton" 
                                        ToolTip="Display the current saved editor text as either formatted or Html below" />
                                    <asp:RadioButtonList ID="Selections" CssClass="radiobuttonList button" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                        <asp:ListItem Text="Formatted" Value="Formatted" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Html" Value="Html"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                
                                <div id="PreviewText">
                                
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="PreviewButton" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div id="TextPreview" runat="server" class="preview"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                    
                            
                    
                        </div>

                    </td>
                
                    <td id="OptionsCell">

                        <div id="Options">

                            <div class="demoHeading" style="background-color:#B0C4DE">选项</div>

                            <div>
                            
                                <div class="optionsLabel">Conversions</div>
                                <div class="optionControls">

                                    <asp:CheckBox ID="XHTMLBox" runat="server" AutoPostBack="True" OnCheckedChanged="XHTMLBox_CheckedChanged" Text="XHTML" CssClass="button" ToolTip="Set to convert to XHTML" /><br />

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="DeprecatedBox" runat="server" AutoPostBack="True" OnCheckedChanged="DeprecatedBox_CheckedChanged" Text="Deprecated" CssClass="button" ToolTip="Set to convert deprecated syntax" /><br />
                                            <asp:CheckBox ID="ParagraphsBox" runat="server" AutoPostBack="True" OnCheckedChanged="ParagraphsBox_CheckedChanged" Text="Paragraphs" CssClass="button" ToolTip="Set to convert paragraphs (IE)" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                
                                </div>
                            
                                <div class="optionsLabel">Toggle Mode</div>
                                <div class="optionControls">
                                
                                    <asp:RadioButtonList ID="ToggleModeRadioButtonList" CssClass="button" runat="server" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="Redirect_EventHandler">
                                        <asp:ListItem Text="Tabs" Value="Tabs" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Toggle Button" Value="ToggleButton"></asp:ListItem>
                                        <asp:ListItem Text="Buttons" Value="Buttons"></asp:ListItem>
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                    </asp:RadioButtonList>
                                
                                </div>

                                <div class="optionsLabel">配色方案</div>
                                <div class="optionControls">
                                
                                    <asp:RadioButtonList ID="ColorSchemeRadioButtonList" CssClass="button" runat="server" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="Redirect_EventHandler">
                                        <asp:ListItem Text="自定义" Value="Custom" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Visual Studio" Value="VisualStudio"></asp:ListItem>
                                        <asp:ListItem Text="默认" Value="Default"></asp:ListItem>
                                    </asp:RadioButtonList>
                                
                                </div>

                                <div class="optionsLabel">其他</div>
                                <div class="optionControls">
                                
                                    <asp:CheckBox ID="NoToolstripBackgroundImageBox" runat="server" 
                                        AutoPostBack="True" OnCheckedChanged="Redirect_EventHandler" Text="平铺工具条" 
                                        CssClass="button" 
                                        ToolTip="Set to display toolstrips without background images" />
                                
                                </div>

                            </div>
                        </div>

                    </td>

                </tr>
            
            </table>

        </div>
	    </td>
        </tr>
    </table>


    

<script type="text/javascript">

function GetHtmlEditor()
{
    return $find('<%= Editor.ClientID %>');
}

</script>

</asp:Content>
