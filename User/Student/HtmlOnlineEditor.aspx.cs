using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Winthusiasm.HtmlEditor;
using OnLineExam.BusinessLogicLayer;

public partial class HtmlOnlineEditor : System.Web.UI.Page 
{
    protected string initialHtml = @"<h1>Heading 1</h1>
<p>This text is <strong>bold</strong>, this <em>italic</em>, and this <u>underlined</u>.</p>
<p>This text is <span style='font-family: Arial'>Arial</span>, this <span style='font-family: Garamond'>Garamond</span>, and this <span style='font-family: Verdana'>Verdana</span>.</p>
<ul>
<li>Bullet 1 </li>
<li>Bullet 2</li>
</ul>";

    protected string infoText = @"<h2>HTML Editor for ASP.NET AJAX</h2>
<p>This <strong>Basic Edition</strong> is designed specifically for ASP.NET AJAX.</p>
<p>Features:</p>
<ol>
<li>Implements the <strong>IScriptControl </strong>interface. </li>
<li>Creates a related client-side JavaScript object derived from <em>Sys.UI.Control</em>. </li>
<li>Runs within an <strong>UpdatePanel</strong>. </li>
<li>Runs in multiple instances.</li>
</ol>
<p>This initial text is set in the code-behind C# located in Demo.aspx.cs.</p>
<p>In this sample web page, a single HtmlEditor control is placed within an UpdatePanel. The page also includes a second UpdatePanel that contains the preview text displayed within the <strong>Preview</strong> box at the bottom of the page.</p>
<p>To test the editor:</p>
<ul>
<li>Click either the Design or Html tab to display the editor in either mode. </li>
<li>Modify the text in either mode. </li>
<li>Save changes by clicking the Save button.</li>
<li>Click the Preview button to display the saved text within the boxed area below, either as formatted or unformatted html, depending on the radio button settings. </li>
<li>Click the Clear button to clear the editor text. </li>
<li>Click the Info button to replace the text you currently see in the editor with the info text defined in the code-behind C#.</li>
<li>Click the Trigger Update button to trigger an Update of the editor UpdatePanel.</li>
<li>Check/Uncheck the XHTML box to enable/disable the conversion to XHTML.</li>
<li>Check/Uncheck the Deprecated box to enable/disable the conversion of deprecated syntax.</li>
<li>Check/Uncheck the Paragraphs box to enable/disable the conversion of paragraphs in Internet Explorer.</li>
<li>Click one of the Toggle Mode radio buttons to change how the user switches between Design and Html mode.</li>
<li>Click one of the Color Scheme radio buttons to change the overall color scheme (The Custom setting is the same as the Default setting until the developer modifies specific color elements).</li>
<li>Check the Flat toolstrips box to display the Toolstrips without a background image.</li>
</ul>";

    protected bool InternetExplorer
    {
        get { return Request.Browser.Browser.Equals("IE"); }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        sm.RegisterAsyncPostBackControl(SaveButton);
        sm.RegisterAsyncPostBackControl(ClearButton);
        sm.RegisterAsyncPostBackControl(InfoButton);
        sm.RegisterAsyncPostBackControl(XHTMLBox);
        sm.RegisterAsyncPostBackControl(DeprecatedBox);
        sm.RegisterAsyncPostBackControl(ParagraphsBox);

        //if (Convert.ToString(Session["userID"]) == null)
        //{
        //    Response.Write("<script language=javascript>location='Login.aspx'</script>");
        //}

        if (!IsPostBack)
        {
            //string userid = Session["userID"].ToString();
            //this.lblUser.Text = userid;
            //Users user = new Users();
            //user.LoadData(userid);
        }
        if (!IsPostBack)
        {
            Editor.Text = initialHtml;

            XHTMLBox.Checked = Editor.OutputXHTML;
            DeprecatedBox.Checked = Editor.ConvertDeprecatedSyntax;
            DeprecatedBox.Enabled = XHTMLBox.Checked;
            ParagraphsBox.Checked = Editor.ConvertParagraphs;
            ParagraphsBox.Enabled = DeprecatedBox.Checked && DeprecatedBox.Enabled && InternetExplorer && Request.Browser.MajorVersion < 9;
            ToggleModeRadioButtonList.SelectedValue = Editor.ToggleMode.ToString();
            ColorSchemeRadioButtonList.SelectedValue = Editor.ColorScheme.ToString();
            NoToolstripBackgroundImageBox.Checked = Editor.NoToolstripBackgroundImage;
        }
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        if (!IsPostBack)
        {
            string toggleMode = this.Request.QueryString["ToggleMode"];
            if (toggleMode != null)
                Editor.ToggleMode = GetToggleMode(toggleMode);

            string colorScheme = this.Request.QueryString["ColorScheme"];
            if (colorScheme != null)
                Editor.ColorScheme = GetColorScheme(colorScheme);

            string noToolstripBackgroundImage = this.Request.QueryString["NoToolstripBackgroundImage"];
            if (noToolstripBackgroundImage != null)
                Editor.NoToolstripBackgroundImage = noToolstripBackgroundImage == "true";

            string xhtml = this.Request.QueryString["XHTML"];
            if (xhtml != null)
                Editor.OutputXHTML = xhtml == "true";

            string deprecated = this.Request.QueryString["Deprecated"];
            if (deprecated != null)
                Editor.ConvertDeprecatedSyntax = deprecated == "true";

            string paragraphs = this.Request.QueryString["Paragraphs"];
            if (paragraphs != null)
                Editor.ConvertParagraphs = paragraphs == "true";
        }
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);

        PreviewHeading.Style["background-color"] = ColorTranslator.ToHtml(Editor.SelectedTabBackColor);
        PreviewHeading.Style["color"] = ColorTranslator.ToHtml(Editor.SelectedTabTextColor);
    }

    protected HtmlEditor.ToggleModeType GetToggleMode(string toggleMode)
    {
        HtmlEditor.ToggleModeType toggleModeType;

        switch (toggleMode)
        {
            case "Tabs":
                toggleModeType = HtmlEditor.ToggleModeType.Tabs;
                break;
            case "ToggleButton":
                toggleModeType = HtmlEditor.ToggleModeType.ToggleButton;
                break;
            case "Buttons":
                toggleModeType = HtmlEditor.ToggleModeType.Buttons;
                break;
            case "None":
                toggleModeType = HtmlEditor.ToggleModeType.None;
                break;
            default :
                toggleModeType = HtmlEditor.ToggleModeType.Tabs;
                break;
        }

        return toggleModeType;
    }

    protected HtmlEditor.ColorSchemeType GetColorScheme(string colorScheme)
    {
        HtmlEditor.ColorSchemeType colorSchemeType;

        switch (colorScheme)
        {
            case "Custom":
                colorSchemeType = HtmlEditor.ColorSchemeType.Custom;
                break;
            case "VisualStudio":
                colorSchemeType = HtmlEditor.ColorSchemeType.VisualStudio;
                break;
            default:
                colorSchemeType = HtmlEditor.ColorSchemeType.Default;
                break;
        }

        return colorSchemeType;
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        Editor.Text = String.Empty;
    }

    protected void InfoButton_Click(object sender, EventArgs e)
    {
        Editor.Text = infoText;
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        Editor.Text = initialHtml;
        UpdatePanel1.Update();
    }

    protected void XHTMLBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox box = (CheckBox)sender;
        Editor.OutputXHTML = box.Checked;
        DeprecatedBox.Enabled = box.Checked;
        ParagraphsBox.Enabled = InternetExplorer && DeprecatedBox.Enabled && DeprecatedBox.Checked;
        UpdatePanel2.Update();

        Editor.Revert();
        UpdatePanel1.Update();
    }

    protected void DeprecatedBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox box = (CheckBox)sender;
        Editor.ConvertDeprecatedSyntax = box.Checked;
        ParagraphsBox.Enabled = InternetExplorer && DeprecatedBox.Enabled && DeprecatedBox.Checked;

        Editor.Revert();
        UpdatePanel1.Update();
    }

    protected void ParagraphsBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox box = (CheckBox)sender;
        Editor.ConvertParagraphs = box.Checked;

        Editor.Revert();
        UpdatePanel1.Update();
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataStore.StoreHtml(Editor.Text);
    }

    protected void PreviewButton_Click(object sender, EventArgs e)
    {
        switch (Selections.SelectedValue)
        {
            case "Formatted":
                TextPreview.InnerHtml = Editor.Text;
                break;
            case "Html":
                TextPreview.InnerText = Editor.Text;
                break;
            default:
                break;
        }
    }

    protected string GetRedirectUrl()
    {
        string url = "HtmlOnlineEditor.aspx?";
        url += "ToggleMode=" + ToggleModeRadioButtonList.SelectedValue;
        url += "&ColorScheme=" + ColorSchemeRadioButtonList.SelectedValue;
        url += "&NoToolstripBackgroundImage=" + (NoToolstripBackgroundImageBox.Checked ? "true" : "false");
        url += "&XHTML=" + (XHTMLBox.Checked ? "true" : "false");
        url += "&Deprecated=" + (DeprecatedBox.Checked ? "true" : "false");
        url += "&Paragraphs=" + (ParagraphsBox.Checked ? "true" : "false");

        return url;
    }
    
    protected void Redirect_EventHandler(object sender, EventArgs e)
    {
        this.Response.Redirect(GetRedirectUrl());
    }

    protected class DataStore
    {
        public static void StoreHtml(string html)
        {
        }
    }
}