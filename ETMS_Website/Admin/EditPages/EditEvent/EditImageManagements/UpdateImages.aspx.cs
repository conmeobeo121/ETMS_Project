﻿using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EditPages.EditEvent.EditImageManagements
{
    public partial class UpdateImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnUpdateImg_Click(object sender, EventArgs e)
        {
            if (cvFullCheckValidate.IsValid)
            {
                try
                {
                    int eventID = int.Parse(ddlEvents.SelectedValue);
                    int imgID = int.Parse(hdnIDImage.Value);
                    string savedFileName = lbCurrentFile.Text;
                    if (fuChooseImg.HasFile)
                    {
                        savedFileName = SaveFileToImagesFolder(fuChooseImg);
                    }
                    if (!savedFileName.StartsWith("~/Images/"))
                    {
                        savedFileName = "~/Images/" + savedFileName;
                    }
                    ImagesEventBLL bLL = new ImagesEventBLL();
                    bLL.UpdateImageEvent(savedFileName, eventID, imgID);
                    LoadData();
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Updated image successfully");
                }
                catch (Exception)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
        }

        public void LoadData()
        {
            LoadEvents(ddlEvents);
            int id = -1;
            string idStr = Request.QueryString["id"];
            if (!int.TryParse(idStr, out id))
            {
                string exMsg = $"Invalid id: {id}";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
                return;
            }
            LoadDataToControl(id);
        }

        private void LoadDataToControl(int id)
        {
            ImagesEventBLL bLL = new ImagesEventBLL();
            try
            {
                var data = bLL.GetImagesEventByID(id);
                if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                {
                    string exMsg = $"Invalid id: {id}";
                    HandleFunction.GoToErrorPage(Response, Context, exMsg);
                    return;
                }
                DataTable dt = data.Tables[0];
                hdnIDImage.Value = dt.Rows[0]["EventID"].ToString();
                lbCurrentFile.Text = dt.Rows[0]["ImageUrl"].ToString();
                ddlEvents.SelectedValue = dt.Rows[0]["EventID"].ToString();
            }
            catch (Exception)
            {
                string exMsg = $"An error occurred from server.";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
            }
        }

        private void LoadEvents(DropDownList ddlEvents)
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetAllEvents();
            ddlEvents.DataSource = data.Tables[0];
            ddlEvents.DataTextField = "EventName";
            ddlEvents.DataValueField = "EventID";
            ddlEvents.DataBind();
        }

        private string CheckFileImgExtension(string fileName)
        {
            string[] allowedExtensions = { ".bmp", ".gif", ".ico", ".cur", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp", ".png", ".svg", ".tif", ".tiff", ".webp" };
            string fileExtension = Path.GetExtension(fileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return "Invalid file type. Please upload a valid image file.";
            }
            return string.Empty;
        }

        private void CheckFileImg(FileUpload file)
        {
            if (!file.HasFile)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "No file selected. Please select a file to update.");
                throw new Exception();
            }
            string fileName = file.FileName;
            string checkResult = CheckFileImgExtension(fileName);
            if (!string.IsNullOrEmpty(checkResult))
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", checkResult);
                throw new Exception();
            }
        }

        private void CheckDDLEmpty(DropDownList ddlEvents)
        {
            if (ddlEvents.Items.Count == 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "You have not selected a event yet.");
                throw new Exception();
            }
        }

        private string SaveFileToImagesFolder(FileUpload file)
        {
            string folderPath = Server.MapPath("~/Images/");
            if (!Directory.Exists(folderPath) && !Directory.CreateDirectory(folderPath).Exists)
            {
                return string.Empty;
            }
            //string hashSessionID = BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(HttpContext.Current.Session.SessionID))).Replace("-", "");
            string fileExtension = Path.GetExtension(file.FileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            string newFileName = $"{fileNameWithoutExtension}_{DateTime.Now.Ticks}{fileExtension}";
            string saveLocation = Path.Combine(folderPath, newFileName);
            file.SaveAs(saveLocation);
            return newFileName;
        }

        protected void cvFullCheckValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!fuChooseImg.HasFile && !string.IsNullOrEmpty(lbCurrentFile.Text))
                {
                    CheckDDLEmpty(ddlEvents);
                }
                else
                {
                    CheckFileImg(fuChooseImg);
                    CheckDDLEmpty(ddlEvents);
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void txtFilterEvents_TextChanged(object sender, EventArgs e)
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.FilterByName(txtFilterEvents.Text);
            ddlEvents.DataSource = data.Tables[0];
            ddlEvents.DataTextField = "EventName";
            ddlEvents.DataValueField = "EventID";
            ddlEvents.DataBind();
        }
    }
}