using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class layoutmanager : System.Web.UI.Page
{

    Button newLayoutBtn = new Button();
    SQLConnector sqlconnector = new SQLConnector();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        sqlconnector = (SQLConnector)Session["SQLConnector"];

        if (Session["IsLogedIn"] == null || !(bool)Session["IsLogedIn"])
        {
            Response.Redirect("./Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadControls();
        LoadControlsVisibility("pageload");
        //sqlconnector = (SQLConnector)Session["SQLConnector"];
        MessagePlaceholder.Controls.Clear();

        if (Session["message"] != null && Session["message"].Equals("layoutdeleted"))
        {
            Label msglbl = new Label();
            msglbl.Text = "Layout Deleted Successfully.";
            msglbl.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Controls.Add(msglbl);
            
        }
        else if (Session["message"] != null && Session["message"].Equals("layoutnameupdated"))
        {
            sqlconnector.loadLayouts();
            EditLayoutNameMessagelbl.Text = "Layout Name Saved";
            EditLayoutNameMessagelbl.ForeColor = ColorTranslator.FromHtml("green");
            string layoutid = (string)Session["LayoutID"];
            EditLayoutNameBtn.CommandArgument = layoutid;
            EditUploadFileBtn.CommandArgument = layoutid;
            EditChooseColorBtn.CommandArgument = layoutid;
            LoadControlsVisibility("editlayout");
            editLayout(sqlconnector.getUser().getLayout(Convert.ToInt32(layoutid)));
        }
        else if (Session["message"] != null && Session["message"].Equals("logoupdated"))
        {
            sqlconnector.loadLayouts();
            EditUploadFileMessagelbl.Text = "Logo Updated Successfully";
            EditUploadFileMessagelbl.ForeColor = ColorTranslator.FromHtml("green");
            string layoutid = (string)Session["LayoutID"];
            EditLayoutNameBtn.CommandArgument = layoutid;
            EditUploadFileBtn.CommandArgument = layoutid;
            EditChooseColorBtn.CommandArgument = layoutid;
            LoadControlsVisibility("editlayout");
            editLayout(sqlconnector.getUser().getLayout(Convert.ToInt32(layoutid)));
        }
        else if (Session["message"] != null && Session["message"].Equals("layoutbackgroundcolorupdated"))
        {
            sqlconnector.loadLayouts();
            EditChooseColorMessageLbl.Text = "Background Color Updated";
            EditChooseColorMessageLbl.ForeColor = ColorTranslator.FromHtml("green");
            string layoutid = (string)Session["LayoutID"];
            EditLayoutNameBtn.CommandArgument = layoutid;
            EditUploadFileBtn.CommandArgument = layoutid;
            EditChooseColorBtn.CommandArgument = layoutid;
            LoadControlsVisibility("editlayout");
            editLayout(sqlconnector.getUser().getLayout(Convert.ToInt32(layoutid)));
        }
        Session["message"] = null;

        displayLayouts();
        //sqlconnector.getUser();
    }

    private void LoadControls()
    {


    }

    private void LoadControlsVisibility(string n)
    {
        NewLayoutPlaceholder.Visible = false;
        EditLayoutPlaceholder.Visible = false;
        DefaultLayoutEditorPlaceholder.Visible = false;
        EditLayoutPlaceholder.Visible = false;
        if(n == null || n == "pageload")
        {
            DefaultLayoutEditorPlaceholder.Visible = true;
        }
        switch (n)
        {
            case "newLayout":
                NewLayoutPlaceholder.Visible = true;
                
                break;

            case "editlayout":
                EditLayoutPlaceholder.Visible = true;
                break;
        }
    }

    protected void NewLayoutbtn_OnClick(object sender, EventArgs e)
    {
        //newLayout();
        LoadControlsVisibility("newLayout");
    }

    protected void SaveNewLayoutbtn_OnClick(object sender, EventArgs e)
    {
        if (saveNewLayout())
        {
            Label l = new Label();
            l.Text = "Layout saved successfully. <br/>To view or edit layout, click the \"Edit\" buttn.";
            l.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Controls.Add(l);
            displayLayouts();
        }
        else
        {
            LoadControlsVisibility("newLayout");
        }
    }

    protected void EditLayoutNameBtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int layoutID = Convert.ToInt32(btn.CommandArgument.ToString());
        string newname = EditLayoutNametxt.Text;
        EditLayoutNameBtn.CommandArgument = layoutID.ToString();
        EditUploadFileBtn.CommandArgument = layoutID.ToString();
        EditChooseColorBtn.CommandArgument = layoutID.ToString();
        if ((newname != "" || newname != null) && sqlconnector.updateLayoutName(layoutID, newname))
        {
            Session["message"] = "layoutnameupdated";
            Session["LayoutID"] = layoutID.ToString();
            Response.Redirect(Request.Path);
        }
        else
        {
            EditLayoutNameMessagelbl.Text = "Name invalid or failed to save.";
            editLayout(sqlconnector.getUser().getLayout(layoutID));
        }


    }

    protected void EditUpdateLogoBtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int layoutID = Convert.ToInt32(btn.CommandArgument.ToString());
        Logo newlogo = new Logo();
        EditLayoutNameBtn.CommandArgument = layoutID.ToString();
        EditUploadFileBtn.CommandArgument = layoutID.ToString();
        EditChooseColorBtn.CommandArgument = layoutID.ToString();

        if (EditUploadFile.HasFile)
        {
            HttpPostedFile postedFile = EditUploadFile.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;
            if (fileExtension.ToLower().Equals(".jpg") || fileExtension.ToLower().Equals(".bmp") ||
                fileExtension.ToLower().Equals(".gif") || fileExtension.ToLower().Equals(".png"))
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] image = binaryReader.ReadBytes((int)stream.Length);
                int logoID = sqlconnector.getUser().getLayout(layoutID).getLogo().getLogoID();

                newlogo = new Logo(logoID, fileSize, fileName, image);

                if (sqlconnector.updateLogo(newlogo))
                {
                    Session["message"] = "logoupdated";
                    Session["LayoutID"] = layoutID.ToString();
                    Response.Redirect(Request.Path);
                }
                else
                {
                    EditUploadFileMessagelbl.Text = "Upload Failed";
                    EditUploadFileMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
                    
                }
            }
            else
            {
                EditUploadFileMessagelbl.Text = "Only image files .jpg, .bmp, .gif, .png are allowed";
                EditUploadFileMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
                

            }
        }
        else
        {
            EditUploadFileMessagelbl.Text = "There was no logo entered.";
            EditUploadFileMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
        }
        LoadControlsVisibility("editlayout");
        editLayout(sqlconnector.getUser().getLayout(layoutID));
    }

    protected void EditChooseColorBtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int layoutID = Convert.ToInt32(btn.CommandArgument.ToString());
        string newcolor = Request.Form["Editbackgroundcolor"];
        if (newcolor != "" && sqlconnector.updateLayoutBackgroundColor(layoutID, newcolor))
        {
            Session["message"] = "layoutbackgroundcolorupdated";
            Session["LayoutID"] = layoutID.ToString();
            Response.Redirect(Request.Path);
        }
        else
        {
            EditChooseColorMessageLbl.Text = "Color invalid or failed to save.";
            editLayout(sqlconnector.getUser().getLayout(layoutID));

        }
        LoadControlsVisibility("editlayout");
    }

    protected void EditLayoutbtn_OnClick(object sender, EventArgs e)
    {
        Button editbtn = (Button)sender;
        Layout layout = sqlconnector.getUser().getLayout(Convert.ToInt32(editbtn.CommandArgument));
        editLayout(layout);
    }

    protected void DeleteLayoutbtn_OnClick(object sender, EventArgs e)
    {
        Button deletebtn = (Button)sender;

        if (sqlconnector.deleteLayout(sqlconnector.getUser().getLayout(Convert.ToInt32(deletebtn.CommandArgument))))
        {
            Session["message"] = "layoutdeleted";
            Response.Redirect(Request.Path);
        }
        else
        {
            Label errorLbl = new Label();
            errorLbl.Text = "Failed to delete layout.";
            errorLbl.ForeColor = ColorTranslator.FromHtml("red");
            MessagePlaceholder.Controls.Add(errorLbl);
        }        
    }

    protected void Cancelbtn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Path);
    }

    private bool saveNewLayout()
    {
        int LayoutID = 0;
        int UserID = sqlconnector.getUser().getUserID();
        Logo logo = new Logo();
        string backgroundcolor = Request.Form["backgroundcolor"];
        string LayoutName = NewLayoutNametxt.Text;

        if (LayoutName != null && !LayoutName.Equals(""))
        {
            // Get and check posted file
            if (NewLogoFileUpload.HasFile)
            {
                HttpPostedFile postedFile = NewLogoFileUpload.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);
                int fileSize = postedFile.ContentLength;
                if (fileExtension.ToLower().Equals(".jpg") || fileExtension.ToLower().Equals(".bmp") ||
                    fileExtension.ToLower().Equals(".gif") || fileExtension.ToLower().Equals(".png"))
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] image = binaryReader.ReadBytes((int)stream.Length);

                    logo = new Logo(0, fileSize, fileName, image);
                }
                else
                {
                    Label errorLbl = new Label();
                    errorLbl.Text = "Only image files .jpg, .bmp, .gif, .png are allowed";
                    errorLbl.ForeColor = ColorTranslator.FromHtml("red");
                    MessagePlaceholder.Controls.Add(errorLbl);
                    return false;
                }
            }
            else
            {
                Label errorLbl = new Label();
                errorLbl.Text = "There was no logo entered.";
                MessagePlaceholder.Controls.Add(errorLbl);
                return false;
            }
        }
        else
        {
            Label errorLbl = new Label();
            errorLbl.Text = "No layout name entered.";
            errorLbl.ForeColor = ColorTranslator.FromHtml("red");
            MessagePlaceholder.Controls.Add(errorLbl);
            return false;
        }


        Layout layout = new Layout(LayoutID, UserID, backgroundcolor, LayoutName, logo);
        //return true;
        if (sqlconnector.creatLayout(layout))
        {
            return true;
        }
        else
        {
            Label errorLbl = new Label();
            errorLbl.Text = "There was a problem saving the layout.";
            
            errorLbl.ForeColor = ColorTranslator.FromHtml("red");
            MessagePlaceholder.Controls.Add(errorLbl);
            return false;
        }
    }

    private void newLayout()
    {
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='container'>" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='row'>" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='col-12'>" });
        // Create Tittle
        Label tittlelbl = new Label();
        tittlelbl.Text = "Tittle";
        LayoutEditorPlaceholder.Controls.Add(tittlelbl);
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<br />" });
        TextBox tittletxt = new TextBox();
        tittletxt.ID = "tittletxt";
        LayoutEditorPlaceholder.Controls.Add(tittletxt);
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<br />" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "</div>" });

        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='col-12' style='margin-top:10px;'>" });

        // Upload Logo
        Label uploadlogolbl = new Label();
        uploadlogolbl.Text = "Upload Logo";
        uploadlogolbl.Attributes.Add("style", "margin-top:10px;");
        LayoutEditorPlaceholder.Controls.Add(uploadlogolbl);
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<br />" });
        

        LayoutEditorPlaceholder.Controls.Add(NewLogoFileUpload);

        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<br />" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "</div>" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='col-12' style='margin-top:10px;'>" });

        // Change Background Color
        Label colorselectorlbl = new Label();
        colorselectorlbl.Text = "Click to select color";
        LayoutEditorPlaceholder.Controls.Add(colorselectorlbl);
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<input type='color' name='backgroundcolor' value='#ff0000' style='margin-left:10px;'>" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "</div>" });

        // Submit Button
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<div class='col-12' style='margin-top:10px;'>" });

        newLayoutBtn.CommandArgument = "0";
        LayoutEditorPlaceholder.Controls.Add(newLayoutBtn);
        //LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "<input type='button' name='sumbitbtn' value='Submit' onclick='hello()'>" });

        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "</div>" });
        LayoutEditorPlaceholder.Controls.Add(new Literal { Text = "</div>" });

        LayoutEditorPlaceholder.Controls.Remove(DefaultLayoutEditorPlaceholder);
        LoadControlsVisibility("newLayout");
    }

    private void displayLayouts()
    {
        LayoutsPlaceholder.Controls.Clear();
        LinkedList<Layout> layouts = sqlconnector.getUser().getLayouts();
        foreach(Layout l in layouts)
        {
            
            LayoutsPlaceholder.Controls.Add(new Literal
            {
                Text = "<div class=\"container-fluid\" style=\"border:solid 2px #0094ff;border-radius:5px;" 
                + "margin-top:10px;padding:10px 10px 10px 10px\">"
            });
            LayoutsPlaceholder.Controls.Add(new Literal
            {
                Text = "<div class=\"row\">"
            });
            LayoutsPlaceholder.Controls.Add(new Literal
            {
                Text = "<div class=\"col-12\">"
            });
            // Layout Name
            Label layoutlbl = new Label();
            layoutlbl.Text = l.getLayoutName();
            LayoutsPlaceholder.Controls.Add(layoutlbl);
            LayoutsPlaceholder.Controls.Add(new Literal { Text = "<br/>" });

            // Delete Button
            Button deletebtn = new Button();
            deletebtn.ID = "DeleteBtn" + l.getLayoutID();
            deletebtn.Click += new EventHandler(DeleteLayoutbtn_OnClick);
            deletebtn.CommandArgument = l.getLayoutID().ToString();
            deletebtn.Text = "Delete";
            deletebtn.CssClass = "btn btn-danger btn-sm btn-edit";
            LayoutsPlaceholder.Controls.Add(deletebtn);

            // Edit Button
            Button editbtn = new Button();
            editbtn.ID = "EditBtn" + l.getLayoutID();
            editbtn.Click += new EventHandler(EditLayoutbtn_OnClick);
            editbtn.CommandArgument = l.getLayoutID().ToString();
            editbtn.Text = "Edit";
            editbtn.CssClass = "btn btn-primary btn-sm";
            LayoutsPlaceholder.Controls.Add(editbtn);

            LayoutsPlaceholder.Controls.Add(new Literal
            {
                Text = "</div></div></div>"
            });
        }
    }

    private void editLayout(Layout layout)
    {
        if (layout != null)
        {
            EditLayoutNametxt.Text = layout.getLayoutName();
            EditLayoutNameBtn.CommandArgument = layout.getLayoutID().ToString();
            EditUploadFileBtn.CommandArgument = layout.getLayoutID().ToString();
            EditChooseColorBtn.CommandArgument = layout.getLayoutID().ToString();
            byte[] bytes = layout.getLogo().getLogo();
            string strBase64 = Convert.ToBase64String(bytes);
            SampleLogo.ImageUrl = "data:Image/png;base64," + strBase64;
            Panel1.BackColor = ColorTranslator.FromHtml(layout.getBackgroundColor());
            EditChooseColorPlaceholder.Controls.Add(new Literal { Text = "<input type='color' name='Editbackgroundcolor' value='" + layout.getBackgroundColor() + "' />" });

            LoadControlsVisibility("editlayout");
            //editLayout(layout);
        }
        else
        {
            Label errorLbl = new Label();
            errorLbl.Text = "Could not find the layout you selected.";
            errorLbl.ForeColor = ColorTranslator.FromHtml("red");
            MessagePlaceholder.Controls.Add(errorLbl);
        }
    }

}